import Image from "next/image"
import Link from "next/link"
import { Bed, Bath, Maximize, MapPin } from "lucide-react"
import type { Home } from "@/lib/types"

interface PropertyCardProps {
  home: Home
}

export function PropertyCard({ home }: PropertyCardProps) {
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
        : `$${home.monthlyRent?.toLocaleString()}/mo`

  return (
    <Link href={`/homes/${home.id}`} className="group block">
      <div className="overflow-hidden rounded-xl border border-border bg-card shadow-sm transition-all hover:shadow-lg">
        <div className="relative aspect-[16/10] overflow-hidden">
          <Image
            src={home.imageUrls}
            alt={home.address}
            fill
            className="object-cover transition-transform duration-300 group-hover:scale-105"
            sizes="(max-width: 640px) 100vw, (max-width: 1024px) 50vw, 33vw"
          />
          <div className="absolute left-3 top-3 flex gap-2">
            <span
              className={`rounded-md px-2.5 py-1 text-xs font-semibold ${
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
              <span className="rounded-md bg-warning px-2.5 py-1 text-xs font-semibold text-warning-foreground">
                Featured
              </span>
            )}
          </div>
          {!home.isVacant && (
            <div className="absolute inset-0 flex items-center justify-center bg-foreground/40">
              <span className="rounded-md bg-card px-3 py-1.5 text-sm font-semibold text-card-foreground">
                Not Available
              </span>
            </div>
          )}
        </div>
        <div className="p-4">
          <div className="flex items-start justify-between">
            <div>
              <p className="text-lg font-bold text-card-foreground">{price}</p>
              <p className="text-xs font-medium text-muted-foreground">
                {home.type}
              </p>
            </div>
          </div>
          <div className="mt-2 flex items-center gap-1 text-sm text-muted-foreground">
            <MapPin className="h-3.5 w-3.5 shrink-0" />
            <span className="truncate">{home.address}</span>
          </div>
          <div className="mt-3 flex items-center gap-4 border-t border-border pt-3 text-sm text-muted-foreground">
            <div className="flex items-center gap-1.5">
              <Bed className="h-4 w-4" />
              <span>
                {home.numberOfBedrooms}{" "}
                <span className="hidden sm:inline">Beds</span>
              </span>
            </div>
            <div className="flex items-center gap-1.5">
              <Bath className="h-4 w-4" />
              <span>
                {home.numberOfBathrooms}{" "}
                <span className="hidden sm:inline">Baths</span>
              </span>
            </div>
            <div className="flex items-center gap-1.5">
              <Maximize className="h-4 w-4" />
              <span>
                {home.area} m<sup>2</sup>
              </span>
            </div>
          </div>
        </div>
      </div>
    </Link>
  )
}
