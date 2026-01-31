//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Sheenam.Api.Models.Foundations.Reviews;
using Sheenam.Api.Models.Foundations.Reviews.Exceptions;
using System;

namespace Sheenam.Api.Services.Foundations.Reviews
{
    public partial class ReviewService
    {
        private void ValidateReviewOnAdd(Review review)
        {
            ValidateReviewIsNotNull(review);

            Validate(
                (Rule: IsInvalid(review.Id), Parameter: nameof(Review.Id)),
                (Rule: IsInvalid(review.UserId), Parameter: nameof(Review.UserId)),
                (Rule: IsInvalid(review.Rating), Parameter: nameof(Review.Rating)),
                (Rule: IsInvalid(review.Comment), Parameter: nameof(Review.Comment)),
                (Rule: IsInvalid(review.CreatedDate), Parameter: nameof(Review.CreatedDate)),
                (Rule: IsInvalid(review.UpdatedDate), Parameter: nameof(Review.UpdatedDate)),

                (Rule: IsSame(
                    firstDate: review.CreatedDate,
                    secondDate: review.UpdatedDate,
                    secondDateName: nameof(Review.UpdatedDate)),
                Parameter: nameof(Review.CreatedDate)),

                (Rule: IsNotRecent(review.CreatedDate), Parameter: nameof(Review.CreatedDate))
            );
        }

        private void ValidateReviewOnModify(Review review)
        {
            ValidateReviewIsNotNull(review);

            Validate(
                (Rule: IsInvalid(review.Id), Parameter: nameof(Review.Id)),
                (Rule: IsInvalid(review.UserId), Parameter: nameof(Review.UserId)),
                (Rule: IsInvalid(review.Rating), Parameter: nameof(Review.Rating)),
                (Rule: IsInvalid(review.Comment), Parameter: nameof(Review.Comment)),
                (Rule: IsInvalid(review.CreatedDate), Parameter: nameof(Review.CreatedDate)),
                (Rule: IsInvalid(review.UpdatedDate), Parameter: nameof(Review.UpdatedDate)),

                (Rule: IsNotRecent(review.UpdatedDate), Parameter: nameof(Review.UpdatedDate))
            );
        }

        private void ValidateAgainstStorageReviewOnModify(Review inputReview, Review storageReview)
        {
            Validate(
                (Rule: IsNotSame(
                    firstDate: inputReview.CreatedDate,
                    secondDate: storageReview.CreatedDate,
                    secondDateName: nameof(Review.CreatedDate)),
                Parameter: nameof(Review.CreatedDate))
            );
        }

        private static void ValidateReviewId(Guid reviewId) =>
            Validate((Rule: IsInvalid(reviewId), Parameter: nameof(Review.Id)));

        private static void ValidateStorageReview(Review maybeReview, Guid reviewId)
        {
            if (maybeReview is null)
            {
                throw new NotFoundReviewException(reviewId);
            }
        }

        private static void ValidateReviewIsNotNull(Review review)
        {
            if (review is null)
            {
                throw new NullReviewException();
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static dynamic IsInvalid(int number) => new
        {
            Condition = number < 1 || number > 5,
            Message = "Rating must be between 1 and 5"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private dynamic IsNotSame(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate != secondDate,
                Message = $"Date is not same as {secondDateName}"
            };

        private dynamic IsSame(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate == secondDate,
                Message = $"Date is same as {secondDateName}"
            };

        private dynamic IsNotRecent(DateTimeOffset date) => new
        {
            Condition = IsDateNotRecent(date),
            Message = "Date is not recent"
        };

        private bool IsDateNotRecent(DateTimeOffset date)
        {
            DateTimeOffset currentDateTime = this.dateTimeBroker.GetCurrentDateTimeOffset();
            TimeSpan timeDifference = currentDateTime.Subtract(date);

            return timeDifference.TotalSeconds is > 60 or < 0;
        }

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidReviewException = new InvalidReviewException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidReviewException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidReviewException.ThrowIfContainsErrors();
        }
    }
}