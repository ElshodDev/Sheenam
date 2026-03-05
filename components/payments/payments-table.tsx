"use client"

import { Clock, CheckCircle, XCircle } from "lucide-react"
import { payments } from "@/lib/mock-data"
import { DataTable } from "@/components/data-table"
import type { Payment } from "@/lib/types"

function StatusBadge({ status }: { status: string }) {
  const styles: Record<string, string> = {
    Pending: "bg-warning/10 text-warning",
    Completed: "bg-success/10 text-success",
    Failed: "bg-destructive/10 text-destructive",
    Refunded: "bg-muted text-muted-foreground",
  }
  const icons: Record<string, React.ReactNode> = {
    Pending: <Clock className="h-3 w-3" />,
    Completed: <CheckCircle className="h-3 w-3" />,
    Failed: <XCircle className="h-3 w-3" />,
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

function MethodBadge({ method }: { method: string }) {
  return (
    <span className="rounded-md bg-muted px-2 py-0.5 text-xs font-medium text-muted-foreground">
      {method}
    </span>
  )
}

export function PaymentsTable() {
  return (
    <DataTable<Payment>
      title="Payments"
      description="Track all payment transactions"
      data={payments}
      searchPlaceholder="Search payments..."
      searchFn={(payment, q) =>
        payment.transactionReference.toLowerCase().includes(q)
      }
      columns={[
        {
          header: "Reference",
          accessor: (payment) => (
            <span className="font-mono text-sm font-medium text-card-foreground">
              {payment.transactionReference}
            </span>
          ),
        },
        {
          header: "Amount",
          accessor: (payment) => (
            <span className="font-semibold text-card-foreground">
              ${payment.amount.toLocaleString()}
            </span>
          ),
        },
        {
          header: "Method",
          accessor: (payment) => <MethodBadge method={payment.method} />,
        },
        {
          header: "Status",
          accessor: (payment) => <StatusBadge status={payment.status} />,
        },
        {
          header: "Date",
          accessor: (payment) => (
            <span className="text-muted-foreground">
              {new Date(payment.paymentDate).toLocaleDateString()}
            </span>
          ),
        },
      ]}
    />
  )
}
