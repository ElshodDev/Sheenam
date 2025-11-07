//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.Hosts;
using Sheenam.Api.Models.Foundations.Hosts.Exceptions;
using System;
using System.Linq;
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

        public ValueTask<Host> AddHostAsync(Host host) =>
         TryCatch(async () =>
         {
             ValidateHostOnAdd(host);

             return await this.storageBroker.InsertHostAsync(host);
         });

        public IQueryable<Host> RetrieveAllHosts() =>
            this.storageBroker.SelectAllHosts();

        public async ValueTask<Host> RetrieveHostByIdAsync(Guid Id)
        {
            Host maybeHost = await this.storageBroker.SelectHostByIdAsync(Id);

            if (maybeHost is null)
            {
                throw new NotFoundHostException(Id);
            }

            return maybeHost;
        }
        public async ValueTask<Host> ModifyHostAsync(Host host)
        {
            return await TryCatch(async () =>
            {
                ValidateHostOnModify(host);
                Host trackedHost =
                    await this.storageBroker.SelectHostByIdAsync(host.Id);
                if (trackedHost is null)
                {
                    throw new NotFoundHostException(host.Id);
                }
                return await this.storageBroker.UpdateHostAsync(host);
            });
        }
        private void ValidateHostOnModify(Host host)
        {
            ValidateHostNotNull(host);

            Validate(
              (Rule: IsInvalid(host.Id), Parameter: nameof(Host.Id)),
              (Rule: IsInvalid(host.FirstName), Parameter: nameof(Host.FirstName)),
              (Rule: IsInvalid(host.LastName), Parameter: nameof(Host.LastName)),
              (Rule: IsInvalid(host.DateOfBirth), Parameter: nameof(Host.DateOfBirth)),
              (Rule: IsInvalid(host.Email), Parameter: nameof(Host.Email)),
              (Rule: IsInvalid(host.Gender), Parameter: nameof(Host.Gender)));
        }

        public ValueTask<Host> RemoveHostByIdAsync(Guid Id)
        {
            return TryCatch(async () =>
            {
                Host maybeHost = await this.storageBroker.SelectHostByIdAsync(Id);
                if (maybeHost is null)
                {
                    throw new NotFoundHostException(Id);
                }
                return await this.storageBroker.DeleteHostAsync(maybeHost);
            });
        }
    }

}
