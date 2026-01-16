//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System.Net.Http.Headers;

namespace Sheenam.Blazor.Services
{
    public class AuthenticationHandler : DelegatingHandler
    {
        private readonly TokenStorageService tokenStorage;

        public AuthenticationHandler(TokenStorageService tokenStorage)
        {
            this.tokenStorage = tokenStorage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var token = await tokenStorage.GetTokenAsync();

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                System.Console.WriteLine($"🔑 Token added to request: {token.Substring(0, 20)}...");
            }
            else
            {
                System.Console.WriteLine("⚠️ No token found in storage");
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
