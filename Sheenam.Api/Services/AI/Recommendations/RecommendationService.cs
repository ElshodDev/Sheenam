//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.ML;
using Sheenam.Api.Brokers.MachineLearning;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.AI.Recommendations;
using Sheenam.Api.Models.Foundations.Homes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.AI.Recommendations
{
    public class RecommendationService : IRecommendationService
    {
        private readonly IStorageBroker storageBroker;
        private readonly IMLBroker mlBroker;
        private readonly string modelPath = "MLModels/recommendation_model.zip";
        private ITransformer model;

        public RecommendationService(
            IStorageBroker storageBroker,
            IMLBroker mlBroker)
        {
            this.storageBroker = storageBroker;
            this.mlBroker = mlBroker;
            LoadOrCreateModel();
        }

        public async Task<List<HomeRecommendation>> GetRecommendedHomesAsync(
            GuestPreferences preferences,
            int topN = 10)
        {
            var allHomes = this.storageBroker
                .SelectAllHomes()
                .Where(h => h.IsVacant)
                .ToList();

            var filteredHomes = FilterByPreferences(allHomes, preferences);
            var scoredHomes = await Task.Run(() => ScoreHomes(filteredHomes, preferences));

            return scoredHomes
                .OrderByDescending(h => h.MatchScore)
                .Take(topN)
                .Select((home, index) =>
                {
                    home.Rank = index + 1;
                    return home;
                })
                .ToList();
        }

        public async Task TrainModelAsync()
        {
            var trainingData = await GenerateTrainingDataAsync();
            this.model = await Task.Run(() =>
                this.mlBroker.TrainRecommendationModel(trainingData));

            Directory.CreateDirectory(Path.GetDirectoryName(modelPath));
            this.mlBroker.SaveModel(this.model, modelPath);
        }

        private List<Home> FilterByPreferences(
            List<Home> homes,
            GuestPreferences preferences)
        {
            return homes.Where(home =>
                home.Price <= preferences.MaxPrice &&
                home.NumberOfBedrooms >= preferences.MinBedrooms &&
                (!preferences.NeedsPetAllowed || home.IsPetAllowed) &&
                (!preferences.PreferPrivate || !home.IsShared))
                .ToList();
        }

        private List<HomeRecommendation> ScoreHomes(
            List<Home> homes,
            GuestPreferences preferences)
        {
            return homes.Select(home =>
            {
                var features = ConvertToFeatures(home, preferences);
                var score = this.model != null
                    ? this.mlBroker.PredictScore(this.model, features)
                    : CalculateRuleBasedScore(home, preferences);

                return new HomeRecommendation
                {
                    HomeId = home.Id,
                    Address = home.Address,
                    Price = home.Price,
                    NumberOfBedrooms = home.NumberOfBedrooms,
                    NumberOfBathrooms = home.NumberOfBathrooms,
                    Area = home.Area,
                    IsPetAllowed = home.IsPetAllowed,
                    IsShared = home.IsShared,
                    Type = home.Type.ToString(),
                    MatchScore = Math.Max(0, Math.Min(100, score * 100)),
                    MatchReason = GenerateMatchReason(home, preferences, score)
                };
            }).ToList();
        }

        private HomeFeatureData ConvertToFeatures(
            Home home,
            GuestPreferences preferences)
        {
            return new HomeFeatureData
            {
                Price = (float)(home.Price / preferences.MaxPrice),
                Bedrooms = home.NumberOfBedrooms,
                Bathrooms = home.NumberOfBathrooms,
                Area = (float)home.Area,
                IsPetAllowed = home.IsPetAllowed ? 1f : 0f,
                IsShared = home.IsShared ? 1f : 0f,
                TypeScore = CalculateTypeScore(home.Type.ToString(), preferences.PreferredType)
            };
        }

        private float CalculateRuleBasedScore(
            Home home,
            GuestPreferences preferences)
        {
            float score = 0.5f;
            var priceRatio = (float)(home.Price / preferences.MaxPrice);
            score += (1f - priceRatio) * 0.3f;

            if (home.NumberOfBedrooms >= preferences.MinBedrooms)
                score += 0.2f;

            if (!preferences.NeedsPetAllowed || home.IsPetAllowed)
                score += 0.15f;

            if (!preferences.PreferPrivate || !home.IsShared)
                score += 0.15f;

            score += CalculateTypeScore(home.Type.ToString(), preferences.PreferredType) * 0.2f;

            return score;
        }

        private float CalculateTypeScore(string homeType, string preferredType)
        {
            if (string.IsNullOrEmpty(preferredType))
                return 0.5f;

            return homeType.Equals(preferredType, StringComparison.OrdinalIgnoreCase)
                ? 1f : 0.2f;
        }

        private string GenerateMatchReason(
            Home home,
            GuestPreferences preferences,
            float score)
        {
            var reasons = new List<string>();

            if (home.Price < preferences.MaxPrice * 0.8m)
                reasons.Add("Great price");

            if (home.NumberOfBedrooms > preferences.MinBedrooms)
                reasons.Add("Extra bedrooms");

            if (preferences.NeedsPetAllowed && home.IsPetAllowed)
                reasons.Add("Pet-friendly");

            if (preferences.PreferPrivate && !home.IsShared)
                reasons.Add("Private space");

            if (score > 0.8f)
                reasons.Add("Highly recommended");

            return reasons.Count > 0
                ? string.Join(", ", reasons)
                : "Good match";
        }

        private async Task<List<HomeFeatureData>> GenerateTrainingDataAsync()
        {
            var homes = this.storageBroker.SelectAllHomes().ToList();
            var random = new Random();

            return await Task.Run(() => homes.Select(home => new HomeFeatureData
            {
                Price = (float)home.Price,
                Bedrooms = home.NumberOfBedrooms,
                Bathrooms = home.NumberOfBathrooms,
                Area = (float)home.Area,
                IsPetAllowed = home.IsPetAllowed ? 1f : 0f,
                IsShared = home.IsShared ? 1f : 0f,
                TypeScore = random.Next(0, 2),
                Label = (float)(0.5 + random.NextDouble() * 0.5)
            }).ToList());
        }

        private void LoadOrCreateModel()
        {
            if (File.Exists(modelPath))
            {
                this.model = this.mlBroker.LoadModel(modelPath);
            }
        }
    }
}
