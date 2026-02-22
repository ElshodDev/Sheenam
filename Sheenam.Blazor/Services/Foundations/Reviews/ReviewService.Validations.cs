//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Models.Foundations.Reviews;
using Sheenam.Blazor.Models.Foundations.Reviews.Exceptions;

namespace Sheenam.Blazor.Services.Foundations.Reviews
{
    public partial class ReviewService
    {
        private static void ValidateReviewOnAdd(Review review) =>
            ValidateReviewFields(review);

        private static void ValidateReviewOnModify(Review review) =>
            ValidateReviewFields(review);

        private static void ValidateReviewFields(Review review)
        {
            ValidateReviewNotNull(review);

            Validate(
                (Rule: IsInvalid(review.Id), Parameter: nameof(Review.Id)),
                (Rule: IsInvalid(review.UserId), Parameter: nameof(Review.UserId)),
                (Rule: IsInvalid(review.Rating), Parameter: nameof(Review.Rating)),
                (Rule: IsInvalid(review.Comment), Parameter: nameof(Review.Comment)));
        }

        private static void ValidateReviewId(Guid reviewId) =>
            Validate((Rule: IsInvalid(reviewId), Parameter: nameof(Review.Id)));

        private static void ValidateReviewNotNull(Review review)
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

        private static dynamic IsInvalid(int rating) => new
        {
            Condition = rating < 0 || rating > 5,
            Message = "Rating must be between 0 and 5"
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
