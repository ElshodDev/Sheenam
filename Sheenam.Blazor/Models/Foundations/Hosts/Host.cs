//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Models.Foundations.Guests;
using Sheenam.Blazor.Models.Foundations.Homes;

namespace Sheenam.Blazor.Models.Foundations.Hosts
{
    public class Host
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public GenderType Gender { get; set; }

        public IEnumerable<Home> Homes { get; set; }
    }
}
