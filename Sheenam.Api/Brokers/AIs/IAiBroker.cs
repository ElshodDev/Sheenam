//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.AIs
{
    public interface IAiBroker
    {
        ValueTask<float> PredictSentimentAsync(string text);
    }
}
