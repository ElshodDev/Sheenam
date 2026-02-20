//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================
namespace Sheenam.Blazor.Brokers.Apis
{
    public partial class ApiBroker : IApiBroker
    {
        private readonly HttpClient httpClient;

        public ApiBroker(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async ValueTask<T> PostAsync<T>(string relativeUrl, T content)
        {
            var response = await this.httpClient.PostAsJsonAsync(relativeUrl, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        // 2. Har xil tipli (IApiBroker.PostAsync<T, TResult> uchun)
        public async ValueTask<TResult> PostAsync<T, TResult>(string relativeUrl, T content)
        {
            var response = await this.httpClient.PostAsJsonAsync(relativeUrl, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TResult>();
        }

        public async ValueTask<T> GetAsync<T>(string relativeUrl)
        {
            return await this.httpClient.GetFromJsonAsync<T>(relativeUrl);
        }

        public async ValueTask<T> PutAsync<T>(string relativeUrl, T content)
        {
            var response = await this.httpClient.PutAsJsonAsync(relativeUrl, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async ValueTask<T> DeleteAsync<T>(string relativeUrl)
        {
            var response = await this.httpClient.DeleteAsync(relativeUrl);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
    }
}