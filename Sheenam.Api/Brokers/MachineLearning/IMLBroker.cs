//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.ML;
using Sheenam.Api.Models.AI.Recommendations;
using System.Collections.Generic;

namespace Sheenam.Api.Brokers.MachineLearning
{
    public interface IMLBroker
    {
        ITransformer TrainRecommendationModel(List<HomeFeatureData> trainingData);
        float PredictScore(ITransformer model, HomeFeatureData input);
        void SaveModel(ITransformer model, string modelPath);
        ITransformer LoadModel(string modelPath);
    }
}
