"use client"

import { Clock, CheckCircle, XCircle } from "lucide-react"
import { homeRequests } from "@/lib/mock-data"
import { DataTable } from "@/components/data-table"
import type { HomeRequest } from "@/lib/types"

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

export function RequestsTable() {
  return (
    <DataTable<HomeRequest>
      title="Home Requests"
      description="Manage rental and viewing requests from guests"
      data={homeRequests}
      searchPlaceholder="Search requests..."
      searchFn={(req, q) =>
        `${req.guest?.firstName} ${req.guest?.lastName}`.toLowerCase().includes(q) ||
        (req.home?.address || "").toLowerCase().includes(q)
      }
      columns={[
        {
          header: "Guest",
          accessor: (req) => (
            <div>
              <p className="font-medium text-card-foreground">
                {req.guest?.firstName} {req.guest?.lastName}
              </p>
            </div>
          ),
        },
        {
          header: "Property",
          accessor: (req) => (
            <span className="text-muted-foreground">{req.home?.address}</span>
          ),
        },
        {
          header: "Period",
          accessor: (req) => (
            <span className="text-muted-foreground">
              {new Date(req.startDate).toLocaleDateString()} -{" "}
              {new Date(req.endDate).toLocaleDateString()}
            </span>
          ),
        },
        {
          header: "Message",
          accessor: (req) => (
            <span className="max-w-xs truncate text-muted-foreground">
              {req.message}
            </span>
          ),
          className: "max-w-xs",
        },
        {
          header: "Status",
          accessor: (req) => <StatusBadge status={req.status} />,
        },
        {
          header: "Date",
          accessor: (req) => (
            <span className="text-muted-foreground">
              {new Date(req.createdDate).toLocaleDateString()}
            </span>
          ),
        },
      ]}
    />
  )
}
