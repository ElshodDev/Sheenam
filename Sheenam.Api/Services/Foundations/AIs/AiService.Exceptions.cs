//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.AIs.Exceptions;
using System;
using System.Threading.Tasks;
using Xeptions;

namespace Sheenam.Api.Services.Foundations.AIs
{
    public partial class AiService
    {
        private delegate ValueTask<bool> ReturningBooleanFunction();

        private async ValueTask<bool> TryCatch(ReturningBooleanFunction returningBooleanFunction)
        {
            try
            {
                return await returningBooleanFunction();
            }
            catch (NullAiTextException nullAiTextException) // Matn bo'sh bo'lsa
            {
                throw CreateAndLogValidationException(nullAiTextException);
            }
            catch (Exception exception) // Kutilmagan xatolar (Baza yoki AI broker xatosi)
            {
                var failedAiServiceException = new FailedAiServiceException(exception);
                throw CreateAndLogServiceException(failedAiServiceException);
            }
        }

        private AiValidationException CreateAndLogValidationException(Xeption exception)
        {
            var aiValidationException = new AiValidationException(exception);
            this.loggingBroker.LogError(aiValidationException);

            return aiValidationException;
        }

        private AiServiceException CreateAndLogServiceException(Xeption exception)
        {
            var aiServiceException = new AiServiceException(exception);
            this.loggingBroker.LogError(aiServiceException);

            return aiServiceException;
        }
    }
}