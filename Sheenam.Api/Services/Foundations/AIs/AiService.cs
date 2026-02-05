//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Brokers.AIs;
using Sheenam.Api.Brokers.Loggings;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.AIs
{
    public partial class AiService : IAiService
    {
        private readonly IAiBroker aiBroker;
        private readonly ILoggingBroker loggingBroker;

        public AiService(IAiBroker aiBroker, ILoggingBroker loggingBroker)
        {
            this.aiBroker = aiBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<bool> AnalyzeSentimentAsync(string text) =>
        TryCatch(async () =>
        {
            // 1. Validatsiya (matn bo'sh emasligini tekshirish)
            ValidateText(text);

            // 2. Broker orqali AI natijasini olish
            float score = await this.aiBroker.PredictSentimentAsync(text);

            // 3. Natijani mantiqiy (boolean) ko'rinishga o'tkazish
            // Masalan: 0.5 dan yuqori bo'lsa ijobiy (true)
            return score >= 0.5f;
        });
    }
}
