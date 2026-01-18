//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.ML;
using Sheenam.Api.Models.AI.Recommendations;
using System.Collections.Generic;
using System.IO;

namespace Sheenam.Api.Brokers.MachineLearning
{
    public class MLBroker : IMLBroker
    {
        private readonly MLContext mlContext;

        public MLBroker()
        {
            this.mlContext = new MLContext(seed: 0);
        }

        public ITransformer TrainRecommendationModel(List<HomeFeatureData> trainingData)
        {
            IDataView dataView = this.mlContext.Data.LoadFromEnumerable(trainingData);

            var pipeline = this.mlContext.Transforms
                .Concatenate("Features",
                    nameof(HomeFeatureData.Price),
                    nameof(HomeFeatureData.Bedrooms),
                    nameof(HomeFeatureData.Bathrooms),
                    nameof(HomeFeatureData.Area),
                    nameof(HomeFeatureData.IsPetAllowed),
                    nameof(HomeFeatureData.IsShared),
                    nameof(HomeFeatureData.TypeScore))
                .Append(this.mlContext.Regression.Trainers.Sdca(
                    labelColumnName: nameof(HomeFeatureData.Label),
                    featureColumnName: "Features",
                    maximumNumberOfIterations: 100));

            var model = pipeline.Fit(dataView);
            return model;
        }

        public float PredictScore(ITransformer model, HomeFeatureData input)
        {
            var predictionEngine = this.mlContext.Model
                .CreatePredictionEngine<HomeFeatureData, HomeScorePrediction>(model);

            var prediction = predictionEngine.Predict(input);
            return prediction.PredictedScore;
        }

        public void SaveModel(ITransformer model, string modelPath)
        {
            using (var fileStream = new FileStream(modelPath, FileMode.Create, FileAccess.Write))
            {
                this.mlContext.Model.Save(model, null, fileStream);
            }
        }

        public ITransformer LoadModel(string modelPath)
        {
            if (!File.Exists(modelPath))
                return null;

            using (var fileStream = new FileStream(modelPath, FileMode.Open, FileAccess.Read))
            {
                return this.mlContext.Model.Load(fileStream, out _);
            }
        }
    }
}
