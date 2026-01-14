using Sheenam.Api.Models.Foundations.HomeRequests;
using Sheenam.Api.Models.Foundations.HomeRequests.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.HomeRequests
{
    public partial class HomeRequestService
    {
        public async ValueTask<HomeRequest> ApproveHomeRequestAsync(Guid homeRequestId)
        {
            HomeRequest maybeHomeRequest =
                await this.storageBroker.SelectHomeRequestByIdAsync(homeRequestId);

            ValidateHomeRequestExists(maybeHomeRequest, homeRequestId);
            ValidateHomeRequestCanBeApproved(maybeHomeRequest);

            maybeHomeRequest.Status = HomeRequestStatus.Approved;
            maybeHomeRequest.UpdatedDate = DateTimeOffset.UtcNow;

            return await this.storageBroker.UpdateHomeRequestAsync(maybeHomeRequest);
        }
        public async ValueTask<HomeRequest> RejectHomeRequestAsync(
            Guid homeRequestId, string rejectionReason = null)
        {
            HomeRequest maybeHomeRequest =
                await this.storageBroker.SelectHomeRequestByIdAsync(homeRequestId);

            ValidateHomeRequestExists(maybeHomeRequest, homeRequestId);
            ValidateHomeRequestCanBeRejected(maybeHomeRequest);

            maybeHomeRequest.Status = HomeRequestStatus.Rejected;
            maybeHomeRequest.RejectionReason = rejectionReason;
            maybeHomeRequest.UpdatedDate = DateTimeOffset.UtcNow;

            return await this.storageBroker.UpdateHomeRequestAsync(maybeHomeRequest);
        }
        public async ValueTask<HomeRequest> CancelHomeRequestAsync(Guid homeRequestId)
        {
            HomeRequest maybeHomeRequest =
                await this.storageBroker.SelectHomeRequestByIdAsync(homeRequestId);

            ValidateHomeRequestExists(maybeHomeRequest, homeRequestId);
            ValidateHomeRequestCanBeCancelled(maybeHomeRequest);

            maybeHomeRequest.Status = HomeRequestStatus.Cancelled;
            maybeHomeRequest.UpdatedDate = DateTimeOffset.UtcNow;

            return await this.storageBroker.UpdateHomeRequestAsync(maybeHomeRequest);
        }
        public IQueryable<HomeRequest> RetrieveHomeRequestsByStatusAsync(HomeRequestStatus status)
        {
            return this.storageBroker.SelectAllHomeRequests()
                  .Where(request => request.Status == status);
        }
        private void ValidateHomeRequestExists(HomeRequest homeRequest, Guid homeRequestId)
        {
            if (homeRequest is null)
            {
                throw new NotFoundHomeRequestException(homeRequestId);
            }
        }

        private void ValidateHomeRequestCanBeApproved(HomeRequest homeRequest)
        {
            if (homeRequest.Status != HomeRequestStatus.Pending)
            {
                throw new InvalidHomeRequestStatusException(
                    $"HomeRequest with id {homeRequest.Id} cannot be approved. " +
                    $"Current status: {homeRequest.Status}. Only Pending requests can be approved.");
            }
        }

        private void ValidateHomeRequestCanBeRejected(HomeRequest homeRequest)
        {
            if (homeRequest.Status != HomeRequestStatus.Pending)
            {
                throw new InvalidHomeRequestStatusException(
                    $"HomeRequest with id {homeRequest.Id} cannot be rejected. " +
                    $"Current status: {homeRequest.Status}. Only Pending requests can be rejected.");
            }
        }

        private void ValidateHomeRequestCanBeCancelled(HomeRequest homeRequest)
        {
            if (homeRequest.Status == HomeRequestStatus.Approved ||
                homeRequest.Status == HomeRequestStatus.Rejected)
            {
                throw new InvalidHomeRequestStatusException(
                    $"HomeRequest with id {homeRequest.Id} cannot be cancelled. " +
                    $"Current status: {homeRequest.Status}. " +
                    $"Approved or Rejected requests cannot be cancelled.");
            }
        }
    }
}
