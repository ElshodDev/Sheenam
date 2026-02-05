//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.AIs.Exceptions;

namespace Sheenam.Api.Services.Foundations.AIs
{
    public partial class AiService
    {
        private static void ValidateText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new NullAiTextException();
            }
        }
    }
}