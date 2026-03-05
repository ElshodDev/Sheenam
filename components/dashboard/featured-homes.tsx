import Link from "next/link"
import { ArrowRight } from "lucide-react"
import { homes } from "@/lib/mock-data"
import { PropertyCard } from "@/components/property-card"

export function FeaturedHomes() {
  const featured = homes.filter((h) => h.isFeatured).slice(0, 3)

  return (
    <section>
      <div className="mb-4 flex items-center justify-between">
        <div>
          <h2 className="text-lg font-semibold text-foreground">
            Featured Properties
          </h2>
          <p className="text-sm text-muted-foreground">
            Hand-picked properties for you
          </p>
        </div>
        <Link
          href="/homes"
          className="flex items-center gap-1.5 text-sm font-medium text-primary hover:underline"
        >
          View all
          <ArrowRight className="h-4 w-4" />
        </Link>
      </div>
      <div className="grid grid-cols-1 gap-4 md:grid-cols-2 xl:grid-cols-3">
        {featured.map((home) => (
          <PropertyCard key={home.id} home={home} />
        ))}
      </div>
    </section>
  )
}
