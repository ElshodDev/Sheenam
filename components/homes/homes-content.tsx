"use client"

import { useState, useMemo } from "react"
import { Search, Filter, Grid3X3, List } from "lucide-react"
import { homes } from "@/lib/mock-data"
import { PropertyCard } from "@/components/property-card"
import type { HouseType, ListingType } from "@/lib/types"

const houseTypes: HouseType[] = [
  "Flat",
  "Bungalow",
  "Duplex",
  "Villa",
  "Townhouse",
  "Studio",
  "Penthouse",
  "Cottage",
]

const listingTypes: { value: ListingType | "All"; label: string }[] = [
  { value: "All", label: "All Listings" },
  { value: "ForRent", label: "For Rent" },
  { value: "ForSale", label: "For Sale" },
  { value: "Both", label: "Rent / Sale" },
]

export function HomesContent() {
  const [search, setSearch] = useState("")
  const [listingFilter, setListingFilter] = useState<ListingType | "All">("All")
  const [typeFilter, setTypeFilter] = useState<HouseType | "All">("All")
  const [viewMode, setViewMode] = useState<"grid" | "list">("grid")

  const filtered = useMemo(() => {
    return homes.filter((home) => {
      const matchesSearch =
        home.address.toLowerCase().includes(search.toLowerCase()) ||
        home.additionalInfo.toLowerCase().includes(search.toLowerCase())
      const matchesListing =
        listingFilter === "All" || home.listingType === listingFilter
      const matchesType = typeFilter === "All" || home.type === typeFilter
      return matchesSearch && matchesListing && matchesType
    })
  }, [search, listingFilter, typeFilter])

  return (
    <div className="flex flex-col gap-6">
      <div>
        <h1 className="text-2xl font-bold text-foreground">Properties</h1>
        <p className="text-sm text-muted-foreground">
          Browse all available homes and apartments
        </p>
      </div>

      {/* Filters */}
      <div className="flex flex-col gap-3 rounded-xl border border-border bg-card p-4 shadow-sm sm:flex-row sm:items-center sm:justify-between">
        <div className="flex flex-1 flex-wrap items-center gap-3">
          <div className="relative flex-1 sm:max-w-xs">
            <Search className="absolute left-3 top-1/2 h-4 w-4 -translate-y-1/2 text-muted-foreground" />
            <input
              type="text"
              placeholder="Search properties..."
              value={search}
              onChange={(e) => setSearch(e.target.value)}
              className="h-9 w-full rounded-lg border border-input bg-background pl-9 pr-3 text-sm text-foreground placeholder:text-muted-foreground focus:outline-none focus:ring-2 focus:ring-ring"
            />
          </div>
          <select
            value={listingFilter}
            onChange={(e) =>
              setListingFilter(e.target.value as ListingType | "All")
            }
            className="h-9 rounded-lg border border-input bg-background px-3 text-sm text-foreground focus:outline-none focus:ring-2 focus:ring-ring"
            aria-label="Filter by listing type"
          >
            {listingTypes.map((lt) => (
              <option key={lt.value} value={lt.value}>
                {lt.label}
              </option>
            ))}
          </select>
          <select
            value={typeFilter}
            onChange={(e) =>
              setTypeFilter(e.target.value as HouseType | "All")
            }
            className="h-9 rounded-lg border border-input bg-background px-3 text-sm text-foreground focus:outline-none focus:ring-2 focus:ring-ring"
            aria-label="Filter by property type"
          >
            <option value="All">All Types</option>
            {houseTypes.map((ht) => (
              <option key={ht} value={ht}>
                {ht}
              </option>
            ))}
          </select>
        </div>
        <div className="flex items-center gap-1 rounded-lg border border-border p-0.5">
          <button
            onClick={() => setViewMode("grid")}
            className={`rounded-md p-1.5 ${viewMode === "grid" ? "bg-primary text-primary-foreground" : "text-muted-foreground hover:text-foreground"}`}
            aria-label="Grid view"
          >
            <Grid3X3 className="h-4 w-4" />
          </button>
          <button
            onClick={() => setViewMode("list")}
            className={`rounded-md p-1.5 ${viewMode === "list" ? "bg-primary text-primary-foreground" : "text-muted-foreground hover:text-foreground"}`}
            aria-label="List view"
          >
            <List className="h-4 w-4" />
          </button>
        </div>
      </div>

      {/* Results count */}
      <div className="flex items-center gap-2">
        <Filter className="h-4 w-4 text-muted-foreground" />
        <span className="text-sm text-muted-foreground">
          {filtered.length} {filtered.length === 1 ? "property" : "properties"}{" "}
          found
        </span>
      </div>

      {/* Property grid */}
      {filtered.length > 0 ? (
        <div
          className={
            viewMode === "grid"
              ? "grid grid-cols-1 gap-4 md:grid-cols-2 xl:grid-cols-3"
              : "flex flex-col gap-4"
          }
        >
          {filtered.map((home) => (
            <PropertyCard key={home.id} home={home} />
          ))}
        </div>
      ) : (
        <div className="flex flex-col items-center justify-center rounded-xl border border-border bg-card px-6 py-16">
          <Search className="h-12 w-12 text-muted-foreground/50" />
          <h3 className="mt-4 text-lg font-semibold text-foreground">
            No properties found
          </h3>
          <p className="mt-1 text-sm text-muted-foreground">
            Try adjusting your search or filter criteria.
          </p>
        </div>
      )}
    </div>
  )
}
