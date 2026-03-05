import Image from "next/image"
import Link from "next/link"
import { ArrowRight, Search } from "lucide-react"

export function HeroSection() {
  return (
    <section className="relative overflow-hidden rounded-2xl">
      <div className="absolute inset-0">
        <Image
          src="/images/hero-bg.jpg"
          alt=""
          fill
          className="object-cover"
          priority
        />
        <div className="absolute inset-0 bg-gradient-to-r from-foreground/80 to-foreground/40" />
      </div>
      <div className="relative flex flex-col gap-4 px-6 py-10 md:px-10 md:py-14 lg:max-w-2xl">
        <h1 className="text-balance text-2xl font-bold text-background md:text-3xl lg:text-4xl">
          Find Your Perfect Home with Sheenam
        </h1>
        <p className="text-pretty text-sm text-background/80 md:text-base">
          Discover a wide range of properties for rent and sale. From cozy
          studios to luxury villas, we have something for everyone.
        </p>
        <div className="flex flex-wrap gap-3">
          <Link
            href="/homes"
            className="inline-flex items-center gap-2 rounded-lg bg-primary px-5 py-2.5 text-sm font-semibold text-primary-foreground transition-colors hover:bg-primary/90"
          >
            <Search className="h-4 w-4" />
            Browse Properties
          </Link>
          <Link
            href="/requests"
            className="inline-flex items-center gap-2 rounded-lg bg-background/20 px-5 py-2.5 text-sm font-semibold text-background backdrop-blur-sm transition-colors hover:bg-background/30"
          >
            Submit a Request
            <ArrowRight className="h-4 w-4" />
          </Link>
        </div>
      </div>
    </section>
  )
}
