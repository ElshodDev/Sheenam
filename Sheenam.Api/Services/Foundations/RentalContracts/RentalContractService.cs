using Microsoft.Data.SqlClient;
using Sheenam.Api.Brokers.DateTimes;
using Sheenam.Api.Brokers.Guids;
using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.RentalContracts;
using Sheenam.Api.Models.Foundations.RentalContracts.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.RentalContacts
{
    public partial class RentalContractService : IRentalContractService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly IGuidBroker guidBroker;
        public RentalContractService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker,
            IDateTimeBroker dateTimeBroker,
            IGuidBroker guidBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.guidBroker = guidBroker;
        }

        public ValueTask<RentalContract> AddRentalContactAsync(RentalContract rentalContract) =>
      TryCatch(async () =>
      {
          this.ValidateRentalContractOnAdd(rentalContract);

          rentalContract.Id = this.guidBroker.GetGuid();

          DateTimeOffset now =
              this.dateTimeBroker.GetCurrentDateTime();

          rentalContract.CreatedDate = now;
          rentalContract.SignedDate = now;
          rentalContract.UpdatedDate = now;
          rentalContract.Status = ContractStatus.Active;


          return await this.storageBroker.InsertRentalContractAsync(rentalContract);
      });

        public IQueryable<RentalContract> RetrieveAllRentalContracts()
        {
            return this.storageBroker.SelectAllRentalContracts();
        }

        public async ValueTask<RentalContract> RetrieveRentalContractByIdAsync(Guid rentalContractId)
        {
            RentalContract mayRentalContract = await
                 this.storageBroker.SelectRentalContractByIdAsync(rentalContractId);

            if (mayRentalContract is null)
            {
                throw new NotFoundRentalContractException(rentalContractId);
            }
            return mayRentalContract;
        }
        public async ValueTask<RentalContract> ModifyRentalContractAsync(RentalContract rentalContract)
        {
            ValidateRentalContractOnModify(rentalContract);

            RentalContract trackedRentalContract = await
                this.storageBroker.SelectRentalContractByIdAsync(rentalContract.Id);

            ValidateStorageRentalContract(trackedRentalContract, rentalContract.Id);

            trackedRentalContract.HomeRequestId = rentalContract.HomeRequestId;
            trackedRentalContract.GuestId = rentalContract.GuestId;
            trackedRentalContract.HostId = rentalContract.HostId;
            trackedRentalContract.HomeId = rentalContract.HomeId;
            trackedRentalContract.StartDate = rentalContract.StartDate;
            trackedRentalContract.EndDate = rentalContract.EndDate;
            trackedRentalContract.MonthlyRent = rentalContract.MonthlyRent;
            trackedRentalContract.SecurityDeposit = rentalContract.SecurityDeposit;
            trackedRentalContract.Terms = rentalContract.Terms;
            trackedRentalContract.Status = rentalContract.Status;
            trackedRentalContract.SignedDate = rentalContract.SignedDate;
            trackedRentalContract.CreatedDate = rentalContract.CreatedDate;
            trackedRentalContract.UpdatedDate = rentalContract.UpdatedDate;
            return await this.storageBroker.UpdateRentalContractAsync(trackedRentalContract);

        }

        private void ValidateRentalContractOnModify(RentalContract rentalContract)
        {
            if (rentalContract is null)
            {
                throw new NullRentalContractException();
            }
        }
        private void ValidateStorageRentalContract(
            RentalContract storageRentalContract,
            Guid rentalContractId)
        {
            if (storageRentalContract is null)
            {
                throw new NotFoundRentalContractException(rentalContractId);
            }
        }

        public async ValueTask<RentalContract> RemoveRentalContractByIdAsync(Guid rentalContractId)
        {
            try
            {
                RentalContract maybeRentalContract =
                    await this.storageBroker.SelectRentalContractByIdAsync(rentalContractId);
                if (maybeRentalContract is null)
                {
                    throw new NotFoundRentalContractException(rentalContractId);
                }
                RentalContract deletedRentalContract =
                    await this.storageBroker.DeleteRentalContractAsync(maybeRentalContract);

                return deletedRentalContract;
            }
            catch (NotFoundRentalContractException notFoundRentalContractException)
            {
                throw new RentalContractValidationException(notFoundRentalContractException);
            }
            catch (SqlException sqlException)
            {
                throw new RentalContractDependencyException(
                    new FailedRentalContractStorageException(sqlException));
            }
            catch (Exception exception)
            {
                throw new RentalContractServiceException(
                    new FailedRentalContractServiceException(exception));
            }
        }
    }
}
