//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Hosts;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.Hosts
{
    public interface IHostService
    {
        ValueTask<Host> AddHostAsync(Host someHost);
    }
}
