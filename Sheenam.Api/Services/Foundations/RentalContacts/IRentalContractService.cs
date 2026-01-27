using Sheenam.Api.Models.Foundations.RentalContracts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.RentalContacts
{
    public interface IRentalContractService
    {
        ValueTask<RentalContract> AddRentalContactAsync(RentalContract rentalContract);
        IQueryable<RentalContract> RetrieveAllRentalContracts();
        ValueTask<RentalContract> RetrieveRentalContractByIdAsync(Guid rentalContractId);
        ValueTask<RentalContract> ModifyRentalContractAsync(RentalContract rentalContract);
        ValueTask<RentalContract> RemoveRentalContractByIdAsync(Guid rentalContractId);
    }
}
