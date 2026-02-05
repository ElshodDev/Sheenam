//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.AIs
{
    public interface IAiService
    {
        ValueTask<bool> AnalyzeSentimentAsync(string text);
    }
}
