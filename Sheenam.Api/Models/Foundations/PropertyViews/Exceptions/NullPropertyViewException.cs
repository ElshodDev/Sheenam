//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.PropertyViews.Exceptions
{
    public class NullPropertyViewException : Xeption
    {
        public NullPropertyViewException()
            : base(message: "PropertyView is null.") { }
    }
}
