//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using System;

namespace Sheenam.Api.Brokers.Guids
{
    public class GuidBroker : IGuidBroker
    {
        public Guid GetGuid() =>
       Guid.NewGuid();
    }
}
