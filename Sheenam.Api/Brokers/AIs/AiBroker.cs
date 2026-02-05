//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Microsoft.ML;
using Sheenam.Api.Models.Foundations.AIs;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.AIs
{
    public class AiBroker : IAiBroker
    {
        private readonly MLContext mlContext;
        private ITransformer trainedModel;

        public AiBroker()
        {
            this.mlContext = new MLContext();
            this.trainedModel = TrainModel(); // Dastur boshlanganda modelni o'qitadi
        }

        public async ValueTask<float> PredictSentimentAsync(string text)
        {
            return await Task.Run(() =>
            {
                var predictionEngine = this.mlContext.Model
                    .CreatePredictionEngine<SentimentInput, SentimentPrediction>(this.trainedModel);

                var result = predictionEngine.Predict(new SentimentInput { Text = text });

                return result.Score; // 0 dan 1 gacha bo'lgan ehtimollik qaytaradi
            });
        }

        private ITransformer TrainModel()
        {
            SentimentInput[] data = new[]
 {
    // Ijobiy sharhlar (Positive)
    new SentimentInput { Text = "Ajoyib uy, juda yoqdi!", Sentiment = true },
    new SentimentInput { Text = "Hammasi super, tavsiya qilaman.", Sentiment = true },
    new SentimentInput { Text = "Host juda xushmuomala ekan.", Sentiment = true },
    new SentimentInput { Text = "Juda shinam va toza joy.", Sentiment = true },
    new SentimentInput { Text = "Joylashuvi juda qulay, markazga yaqin.", Sentiment = true },
    new SentimentInput { Text = "Narxiga arziydigan sifat.", Sentiment = true },
    new SentimentInput { Text = "Menga juda ma'qul keldi, yana kelaman.", Sentiment = true },
    new SentimentInput { Text = "Uy jihozlari yangi va sifatli.", Sentiment = true },
    new SentimentInput { Text = "Hammasi a'lo darajada tashkil etilgan.", Sentiment = true },
    new SentimentInput { Text = "Internet tezligi juda yaxshi, ishlashga qulay.", Sentiment = true },
    new SentimentInput { Text = "Great place, very clean and cozy.", Sentiment = true },
    new SentimentInput { Text = "Everything was perfect, highly recommended!", Sentiment = true },
    new SentimentInput { Text = "Excellent service and friendly host.", Sentiment = true },
    new SentimentInput { Text = "Very satisfied with my stay.", Sentiment = true },
    new SentimentInput { Text = "Beautiful apartment in a great location.", Sentiment = true },

    // Salbiy sharhlar (Negative)
    new SentimentInput { Text = "Yomon, xizmat ko'rsatish nol.", Sentiment = false },
    new SentimentInput { Text = "Uy iflos va sovuq, yoqmadi.", Sentiment = false },
    new SentimentInput { Text = "Rasmdagidek emas, aldanib qoldik.", Sentiment = false },
    new SentimentInput { Text = "Shovqin juda ko'p, uxlash imkonsiz.", Sentiment = false },
    new SentimentInput { Text = "Mebel juda eski va hidi bor.", Sentiment = false },
    new SentimentInput { Text = "Host qo'pol muomalada bo'ldi.", Sentiment = false },
    new SentimentInput { Text = "Narxi sifatiga mos emas, juda qimmat.", Sentiment = false },
    new SentimentInput { Text = "Hammomda muammo bor, suv oqyapti.", Sentiment = false },
    new SentimentInput { Text = "Internet deyarli ishlamadi.", Sentiment = false },
    new SentimentInput { Text = "Xonalar juda kichik va qorong'u.", Sentiment = false },
    new SentimentInput { Text = "Terrible experience, would not recommend.", Sentiment = false },
    new SentimentInput { Text = "Dirty place and very rude host.", Sentiment = false },
    new SentimentInput { Text = "The smell was awful, not as described.", Sentiment = false },
    new SentimentInput { Text = "Worst stay ever, stay away!", Sentiment = false },
    new SentimentInput { Text = "Very noisy and uncomfortable bed.", Sentiment = false }
};

            IDataView trainingData = this.mlContext.Data.LoadFromEnumerable(data);

            var pipeline = this.mlContext.Transforms.Text.FeaturizeText(
         outputColumnName: "Features",
         inputColumnName: nameof(SentimentInput.Text))
         .Append(this.mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(
         labelColumnName: nameof(SentimentInput.Sentiment),
         featureColumnName: "Features"));

            return pipeline.Fit(trainingData);
        }
    }
}