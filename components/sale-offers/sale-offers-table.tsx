"use client"

import { Clock, CheckCircle, XCircle } from "lucide-react"
import { saleOffers } from "@/lib/mock-data"
import { DataTable } from "@/components/data-table"
import type { SaleOffer } from "@/lib/types"

function StatusBadge({ status }: { status: string }) {
  const styles: Record<string, string> = {
    Pending: "bg-warning/10 text-warning",
    Approved: "bg-success/10 text-success",
    Rejected: "bg-destructive/10 text-destructive",
  }
  const icons: Record<string, React.ReactNode> = {
    Pending: <Clock className="h-3 w-3" />,
    Approved: <CheckCircle className="h-3 w-3" />,
    Rejected: <XCircle className="h-3 w-3" />,
  }

  return (
    <span
      className={`inline-flex items-center gap-1 rounded-full px-2.5 py-0.5 text-xs font-medium ${styles[status] || "bg-muted text-muted-foreground"}`}
    >
      {icons[status]}
      {status}
    </span>
  )
}

export function SaleOffersTable() {
  return (
    <DataTable<SaleOffer>
      title="Sale Offers"
      description="View and manage purchase offers for properties"
      data={saleOffers}
      searchPlaceholder="Search offers..."
      searchFn={(offer, q) =>
        `${offer.guest?.firstName} ${offer.guest?.lastName}`
          .toLowerCase()
          .includes(q) ||
        (offer.home?.address || "").toLowerCase().includes(q)
      }
      columns={[
        {
          header: "Buyer",
          accessor: (offer) => (
            <p className="font-medium text-card-foreground">
              {offer.guest?.firstName} {offer.guest?.lastName}
            </p>
          ),
        },
        {
          header: "Property",
          accessor: (offer) => (
            <span className="text-muted-foreground">
              {offer.home?.address}
            </span>
          ),
        },
        {
          header: "Offer Amount",
          accessor: (offer) => (
            <span className="font-semibold text-card-foreground">
              ${offer.offerAmount.toLocaleString()}
            </span>
          ),
        },
        {
          header: "Message",
          accessor: (offer) => (
            <span className="max-w-xs truncate text-muted-foreground">
              {offer.message}
            </span>
          ),
          className: "max-w-xs",
        },
        {
          header: "Status",
          accessor: (offer) => <StatusBadge status={offer.status} />,
        },
        {
          header: "Date",
          accessor: (offer) => (
            <span className="text-muted-foreground">
              {new Date(offer.createdDate).toLocaleDateString()}
            </span>
          ),
        },
      ]}
    />
  )
}
