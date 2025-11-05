//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.Hosts;
using Sheenam.Api.Models.Foundations.Hosts.Exceptions;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.Hosts
{
    public partial class HostService : IHostService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public HostService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker=loggingBroker;
        }

        public async ValueTask<Host> AddHostAsync(Host host) =>
            await TryCatch(async () =>
               await this.storageBroker.InsertHostAsync(host));

        private delegate ValueTask<Host> ReturningHostFunction();
        private async ValueTask<Host> TryCatch(ReturningHostFunction returningHostFunction)
        {
            try
            {
                return await returningHostFunction();
            }
            catch (NullHostException nullHostException)
            {
                var hostValidationException =
                    new HostValidationException(nullHostException);

                this.loggingBroker.LogError(hostValidationException);
                throw hostValidationException;
            }
        }
    }
}
