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
                (Rule: IsInvalid(review.UpdatedDate), Parameter: nameof(Review.UpdatedDate))
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
                (Rule: IsSame(
                    firstDate: review.UpdatedDate,
                    secondDate: review.CreatedDate,
                    secondDateName: nameof(Review.CreatedDate)),
                Parameter: nameof(Review.UpdatedDate))
            );
        }

        public void ValidateReviewId(Guid reviewId) =>
            Validate((Rule: IsInvalid(reviewId), Parameter: nameof(Review.Id)));

        private static void ValidateStorageReview(Review maybeReview, Guid reviewId)
        {
            if (maybeReview is null)
            {
                throw new NotFoundReviewException(reviewId);
            }
        }

        private void ValidateReviewIsNotNull(Review review)
        {
            if (review is null)
            {
                throw new NullReviewException();
            }
        }

        private static void ValidateAgainstStorageReviewOnModify(Review inputReview, Review storageReview)
        {
            Validate(
                (Rule: IsNotSame(
                    firstDate: inputReview.CreatedDate,
                    secondDate: storageReview.CreatedDate,
                    secondDateName: nameof(Review.CreatedDate)),
                Parameter: nameof(Review.CreatedDate))
            );
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static dynamic IsInvalid(int rating) => new
        {
            Condition = rating < 0 || rating > 5,
            Message = "Rating must be between 0 and 5"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private static dynamic IsNotSame(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate != secondDate,
                Message = $"Date is not the same as {secondDateName}"
            };

        private static dynamic IsSame(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate == secondDate,
                Message = $"Date is the same as {secondDateName}"
            };

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