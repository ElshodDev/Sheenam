import { DashboardShell } from "@/components/dashboard-shell"
import { PropertyDetails } from "@/components/homes/property-details"
import { homes, reviews as allReviews } from "@/lib/mock-data"
import { notFound } from "next/navigation"

export default async function PropertyPage({
  params,
}: {
  params: Promise<{ id: string }>
}) {
  const { id } = await params
  const home = homes.find((h) => h.id === id)

  if (!home) {
    notFound()
  }

  const propertyReviews = allReviews.filter((r) => r.homeId === id)

  return (
    <DashboardShell>
      <PropertyDetails home={home} reviews={propertyReviews} />
    </DashboardShell>
  )
}
