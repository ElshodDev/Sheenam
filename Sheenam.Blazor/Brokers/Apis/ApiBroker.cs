//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using RESTFulSense.WebAssembly.Clients;

namespace Sheenam.Blazor.Brokers.Apis
{
    public partial class ApiBroker : RESTFulApiFactoryClient, IApiBroker
    {
        public ApiBroker(HttpClient httpClient)
            : base(httpClient)
        {
        }
    }
}