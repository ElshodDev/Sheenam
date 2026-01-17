//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Models.Foundations.Guests;

namespace Sheenam.Blazor.Models.ViewModels
{
    public class GuestViewModel
    {
        public Guest Guest { get; set; }
        public bool IsLoading { get; set; }
        public bool IsSubmitting { get; set; }
        public string ErrorMessage { get; set; }
        public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage);
        
        public string FullName => 
            $"{Guest?.FirstName} {Guest?.LastName}".Trim();
        
        public int Age
        {
            get
            {
                if (Guest?.DateOfBirth == null)
                    return 0;

                var today = DateTimeOffset.Now;
                var age = today.Year - Guest.DateOfBirth.Year;
                
                if (Guest.DateOfBirth.Date > today.AddYears(-age).Date)
                    age--;
                
                return age;
            }
        }

        public string GenderDisplay => Guest?.Gender.ToString() ?? "Unknown";
    }
}
