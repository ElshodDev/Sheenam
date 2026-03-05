"use client"

import { Star, User } from "lucide-react"
import { reviews } from "@/lib/mock-data"

export function ReviewsContent() {
  const averageRating =
    reviews.length > 0
      ? (
          reviews.reduce((sum, r) => sum + r.rating, 0) / reviews.length
        ).toFixed(1)
      : "0"

  return (
    <div className="flex flex-col gap-6">
      <div>
        <h1 className="text-2xl font-bold text-foreground">Reviews</h1>
        <p className="text-sm text-muted-foreground">
          All reviews from guests across the platform
        </p>
      </div>

      {/* Summary */}
      <div className="flex flex-wrap items-center gap-6 rounded-xl border border-border bg-card p-6 shadow-sm">
        <div className="flex items-center gap-3">
          <div className="flex h-14 w-14 items-center justify-center rounded-xl bg-warning/10">
            <Star className="h-7 w-7 fill-warning text-warning" />
          </div>
          <div>
            <p className="text-3xl font-bold text-card-foreground">
              {averageRating}
            </p>
            <p className="text-xs text-muted-foreground">Average rating</p>
          </div>
        </div>
        <div className="h-10 w-px bg-border" />
        <div>
          <p className="text-3xl font-bold text-card-foreground">
            {reviews.length}
          </p>
          <p className="text-xs text-muted-foreground">Total reviews</p>
        </div>
      </div>

      {/* Review Cards */}
      <div className="grid grid-cols-1 gap-4 md:grid-cols-2">
        {reviews.map((review) => (
          <div
            key={review.id}
            className="rounded-xl border border-border bg-card p-5 shadow-sm"
          >
            <div className="flex items-start justify-between">
              <div className="flex items-center gap-3">
                <div className="flex h-10 w-10 items-center justify-center rounded-full bg-primary/10 text-primary">
                  <User className="h-5 w-5" />
                </div>
                <div>
                  <p className="font-medium text-card-foreground">
                    {review.guest?.firstName} {review.guest?.lastName}
                  </p>
                  <p className="text-xs text-muted-foreground">
                    {review.home?.address}
                  </p>
                </div>
              </div>
              <div className="flex items-center gap-0.5">
                {Array.from({ length: 5 }).map((_, i) => (
                  <Star
                    key={i}
                    className={`h-4 w-4 ${
                      i < review.rating
                        ? "fill-warning text-warning"
                        : "text-muted-foreground/30"
                    }`}
                  />
                ))}
              </div>
            </div>
            <p className="mt-3 text-sm leading-relaxed text-muted-foreground">
              {review.comment}
            </p>
            <p className="mt-3 text-xs text-muted-foreground/70">
              {new Date(review.createdDate).toLocaleDateString()}
            </p>
          </div>
        ))}
      </div>
    </div>
  )
}
