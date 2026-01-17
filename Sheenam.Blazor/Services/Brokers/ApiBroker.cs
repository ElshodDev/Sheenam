//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

namespace Sheenam.Blazor.Services.Brokers
{
    public partial class ApiBroker : IApiBroker
    {
        private readonly HttpClient httpClient;

        public ApiBroker(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        private async ValueTask<T> GetAsync<T>(string relativeUrl) =>
            await this.httpClient.GetFromJsonAsync<T>(relativeUrl);

        private async ValueTask<T> PostAsync<T>(string relativeUrl, T content)
        {
            HttpResponseMessage response =
                await this.httpClient.PostAsJsonAsync(relativeUrl, content);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<T>();
        }

        private async ValueTask<T> PutAsync<T>(string relativeUrl, T content)
        {
            HttpResponseMessage response =
                await this.httpClient.PutAsJsonAsync(relativeUrl, content);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<T>();
        }

        private async ValueTask<T> DeleteAsync<T>(string relativeUrl)
        {
            HttpResponseMessage response =
                await this.httpClient.DeleteAsync(relativeUrl);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<T>();
        }
    }
}
