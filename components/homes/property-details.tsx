"use client"

import Image from "next/image"
import Link from "next/link"
import {
  ArrowLeft,
  Bed,
  Bath,
  Maximize,
  MapPin,
  User,
  Mail,
  Phone,
  PawPrint,
  Users,
  Star,
  Calendar,
  Send,
  CheckCircle,
  XCircle,
} from "lucide-react"
import type { Home, Review } from "@/lib/types"

interface PropertyDetailsProps {
  home: Home
  reviews: Review[]
}

export function PropertyDetails({ home, reviews }: PropertyDetailsProps) {
  const listingBadge =
    home.listingType === "ForRent"
      ? "For Rent"
      : home.listingType === "ForSale"
        ? "For Sale"
        : "Rent / Sale"

  const price =
    home.listingType === "ForRent"
      ? `$${home.monthlyRent?.toLocaleString()}/mo`
      : home.listingType === "ForSale"
        ? `$${home.salePrice?.toLocaleString()}`
        : `$${home.monthlyRent?.toLocaleString()}/mo or $${home.salePrice?.toLocaleString()}`

  const averageRating =
    reviews.length > 0
      ? (reviews.reduce((sum, r) => sum + r.rating, 0) / reviews.length).toFixed(
          1
        )
      : null

  return (
    <div className="flex flex-col gap-6">
      {/* Breadcrumb */}
      <div className="flex items-center gap-2">
        <Link
          href="/homes"
          className="flex items-center gap-1.5 text-sm text-muted-foreground hover:text-foreground transition-colors"
        >
          <ArrowLeft className="h-4 w-4" />
          Back to Properties
        </Link>
      </div>

      {/* Image Gallery */}
      <div className="relative aspect-[21/9] overflow-hidden rounded-2xl">
        <Image
          src={home.imageUrls}
          alt={home.address}
          fill
          className="object-cover"
          priority
          sizes="100vw"
        />
        <div className="absolute left-4 top-4 flex gap-2">
          <span
            className={`rounded-lg px-3 py-1.5 text-sm font-semibold ${
              home.listingType === "ForRent"
                ? "bg-primary text-primary-foreground"
                : home.listingType === "ForSale"
                  ? "bg-chart-2 text-foreground"
                  : "bg-chart-3 text-foreground"
            }`}
          >
            {listingBadge}
          </span>
          {home.isFeatured && (
            <span className="rounded-lg bg-warning px-3 py-1.5 text-sm font-semibold text-warning-foreground">
              Featured
            </span>
          )}
        </div>
      </div>

      {/* Main Content Grid */}
      <div className="grid grid-cols-1 gap-6 lg:grid-cols-3">
        {/* Left Column - Property Info */}
        <div className="flex flex-col gap-6 lg:col-span-2">
          {/* Property Header */}
          <div className="rounded-xl border border-border bg-card p-6 shadow-sm">
            <div className="flex flex-wrap items-start justify-between gap-4">
              <div>
                <h1 className="text-2xl font-bold text-card-foreground">
                  {price}
                </h1>
                <div className="mt-1 flex items-center gap-1.5 text-muted-foreground">
                  <MapPin className="h-4 w-4 shrink-0" />
                  <span>{home.address}</span>
                </div>
                {averageRating && (
                  <div className="mt-2 flex items-center gap-1.5">
                    <Star className="h-4 w-4 fill-warning text-warning" />
                    <span className="text-sm font-medium text-card-foreground">
                      {averageRating}
                    </span>
                    <span className="text-sm text-muted-foreground">
                      ({reviews.length}{" "}
                      {reviews.length === 1 ? "review" : "reviews"})
                    </span>
                  </div>
                )}
              </div>
              <span className="rounded-lg bg-muted px-3 py-1.5 text-sm font-medium text-muted-foreground">
                {home.type}
              </span>
            </div>

            <div className="mt-6 grid grid-cols-2 gap-4 border-t border-border pt-6 sm:grid-cols-4">
              <div className="flex items-center gap-3">
                <div className="flex h-10 w-10 items-center justify-center rounded-lg bg-primary/10 text-primary">
                  <Bed className="h-5 w-5" />
                </div>
                <div>
                  <p className="text-lg font-bold text-card-foreground">
                    {home.numberOfBedrooms}
                  </p>
                  <p className="text-xs text-muted-foreground">Bedrooms</p>
                </div>
              </div>
              <div className="flex items-center gap-3">
                <div className="flex h-10 w-10 items-center justify-center rounded-lg bg-chart-2/10 text-chart-2">
                  <Bath className="h-5 w-5" />
                </div>
                <div>
                  <p className="text-lg font-bold text-card-foreground">
                    {home.numberOfBathrooms}
                  </p>
                  <p className="text-xs text-muted-foreground">Bathrooms</p>
                </div>
              </div>
              <div className="flex items-center gap-3">
                <div className="flex h-10 w-10 items-center justify-center rounded-lg bg-chart-3/10 text-chart-3">
                  <Maximize className="h-5 w-5" />
                </div>
                <div>
                  <p className="text-lg font-bold text-card-foreground">
                    {home.area}
                  </p>
                  <p className="text-xs text-muted-foreground">
                    {"Sq meters"}
                  </p>
                </div>
              </div>
              <div className="flex items-center gap-3">
                <div className="flex h-10 w-10 items-center justify-center rounded-lg bg-chart-4/10 text-chart-4">
                  <Calendar className="h-5 w-5" />
                </div>
                <div>
                  <p className="text-xs font-bold text-card-foreground">
                    {new Date(home.createdDate).toLocaleDateString()}
                  </p>
                  <p className="text-xs text-muted-foreground">Listed</p>
                </div>
              </div>
            </div>
          </div>

          {/* Description */}
          <div className="rounded-xl border border-border bg-card p-6 shadow-sm">
            <h2 className="text-lg font-semibold text-card-foreground">
              About This Property
            </h2>
            <p className="mt-3 leading-relaxed text-muted-foreground">
              {home.additionalInfo}
            </p>
            <div className="mt-4 flex flex-wrap gap-3">
              <div className="flex items-center gap-2 rounded-lg bg-muted px-3 py-1.5 text-sm">
                {home.isPetAllowed ? (
                  <CheckCircle className="h-4 w-4 text-success" />
                ) : (
                  <XCircle className="h-4 w-4 text-destructive" />
                )}
                <PawPrint className="h-4 w-4 text-muted-foreground" />
                <span className="text-muted-foreground">
                  {home.isPetAllowed ? "Pets Allowed" : "No Pets"}
                </span>
              </div>
              <div className="flex items-center gap-2 rounded-lg bg-muted px-3 py-1.5 text-sm">
                {home.isShared ? (
                  <Users className="h-4 w-4 text-warning" />
                ) : (
                  <CheckCircle className="h-4 w-4 text-success" />
                )}
                <span className="text-muted-foreground">
                  {home.isShared ? "Shared Property" : "Private Property"}
                </span>
              </div>
              <div className="flex items-center gap-2 rounded-lg bg-muted px-3 py-1.5 text-sm">
                <span
                  className={`h-2 w-2 rounded-full ${home.isVacant ? "bg-success" : "bg-destructive"}`}
                />
                <span className="text-muted-foreground">
                  {home.isVacant ? "Available" : "Occupied"}
                </span>
              </div>
            </div>
          </div>

          {/* Reviews */}
          <div className="rounded-xl border border-border bg-card p-6 shadow-sm">
            <h2 className="text-lg font-semibold text-card-foreground">
              Reviews ({reviews.length})
            </h2>
            {reviews.length > 0 ? (
              <div className="mt-4 flex flex-col gap-4">
                {reviews.map((review) => (
                  <div
                    key={review.id}
                    className="rounded-lg border border-border p-4"
                  >
                    <div className="flex items-start justify-between">
                      <div className="flex items-center gap-3">
                        <div className="flex h-9 w-9 items-center justify-center rounded-full bg-primary/10 text-primary">
                          <User className="h-4 w-4" />
                        </div>
                        <div>
                          <p className="text-sm font-medium text-card-foreground">
                            {review.guest?.firstName} {review.guest?.lastName}
                          </p>
                          <p className="text-xs text-muted-foreground">
                            {new Date(review.createdDate).toLocaleDateString()}
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
                  </div>
                ))}
              </div>
            ) : (
              <p className="mt-4 text-sm text-muted-foreground">
                No reviews yet for this property.
              </p>
            )}
          </div>
        </div>

        {/* Right Column - Host Info & Actions */}
        <div className="flex flex-col gap-6">
          {/* Host Info */}
          {home.host && (
            <div className="rounded-xl border border-border bg-card p-6 shadow-sm">
              <h3 className="text-sm font-semibold text-card-foreground">
                Hosted by
              </h3>
              <div className="mt-4 flex items-center gap-3">
                <div className="flex h-12 w-12 items-center justify-center rounded-full bg-primary">
                  <User className="h-6 w-6 text-primary-foreground" />
                </div>
                <div>
                  <p className="font-medium text-card-foreground">
                    {home.host.firstName} {home.host.lastName}
                  </p>
                  <p className="text-xs text-muted-foreground">
                    Property Host
                  </p>
                </div>
              </div>
              <div className="mt-4 flex flex-col gap-2.5">
                <div className="flex items-center gap-2 text-sm text-muted-foreground">
                  <Mail className="h-4 w-4" />
                  <span>{home.host.email}</span>
                </div>
                <div className="flex items-center gap-2 text-sm text-muted-foreground">
                  <Phone className="h-4 w-4" />
                  <span>{home.host.phoneNumber}</span>
                </div>
              </div>
            </div>
          )}

          {/* Pricing Summary */}
          <div className="rounded-xl border border-border bg-card p-6 shadow-sm">
            <h3 className="text-sm font-semibold text-card-foreground">
              Pricing
            </h3>
            <div className="mt-4 flex flex-col gap-3">
              {home.monthlyRent && (
                <div className="flex items-center justify-between">
                  <span className="text-sm text-muted-foreground">
                    Monthly Rent
                  </span>
                  <span className="font-semibold text-card-foreground">
                    ${home.monthlyRent.toLocaleString()}
                  </span>
                </div>
              )}
              {home.salePrice && (
                <div className="flex items-center justify-between">
                  <span className="text-sm text-muted-foreground">
                    Sale Price
                  </span>
                  <span className="font-semibold text-card-foreground">
                    ${home.salePrice.toLocaleString()}
                  </span>
                </div>
              )}
              {home.securityDeposit && (
                <div className="flex items-center justify-between border-t border-border pt-3">
                  <span className="text-sm text-muted-foreground">
                    Security Deposit
                  </span>
                  <span className="font-semibold text-card-foreground">
                    ${home.securityDeposit.toLocaleString()}
                  </span>
                </div>
              )}
            </div>
          </div>

          {/* CTA Buttons */}
          <div className="flex flex-col gap-3">
            <button className="flex items-center justify-center gap-2 rounded-lg bg-primary px-5 py-3 text-sm font-semibold text-primary-foreground transition-colors hover:bg-primary/90">
              <Send className="h-4 w-4" />
              {home.listingType === "ForSale"
                ? "Make an Offer"
                : "Request to Rent"}
            </button>
            <button className="flex items-center justify-center gap-2 rounded-lg border border-border bg-card px-5 py-3 text-sm font-semibold text-card-foreground transition-colors hover:bg-accent">
              <Phone className="h-4 w-4" />
              Contact Host
            </button>
          </div>
        </div>
      </div>
    </div>
  )
}
