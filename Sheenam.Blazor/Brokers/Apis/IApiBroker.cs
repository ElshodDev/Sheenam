//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================
namespace Sheenam.Blazor.Brokers.Apis
{
    public partial interface IApiBroker
    {
        ValueTask<T> PostAsync<T>(string relativeUrl, T content);
        ValueTask<TResult> PostAsync<T, TResult>(string relativeUrl, T content);

        ValueTask<T> GetAsync<T>(string relativeUrl);
        ValueTask<T> PutAsync<T>(string relativeUrl, T content);
        ValueTask<T> DeleteAsync<T>(string relativeUrl);
    }
}