import { ArrowRight, Clock, CheckCircle, XCircle } from "lucide-react"
import Link from "next/link"
import { homeRequests, payments } from "@/lib/mock-data"

function StatusBadge({ status }: { status: string }) {
  const styles: Record<string, string> = {
    Pending: "bg-warning/10 text-warning",
    Approved: "bg-success/10 text-success",
    Rejected: "bg-destructive/10 text-destructive",
    Completed: "bg-success/10 text-success",
    Failed: "bg-destructive/10 text-destructive",
  }
  const icons: Record<string, React.ReactNode> = {
    Pending: <Clock className="h-3 w-3" />,
    Approved: <CheckCircle className="h-3 w-3" />,
    Rejected: <XCircle className="h-3 w-3" />,
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

export function RecentRequests() {
  return (
    <div className="rounded-xl border border-border bg-card shadow-sm">
      <div className="flex items-center justify-between border-b border-border px-5 py-4">
        <div>
          <h3 className="font-semibold text-card-foreground">
            Recent Requests
          </h3>
          <p className="text-xs text-muted-foreground">
            Latest home requests from guests
          </p>
        </div>
        <Link
          href="/requests"
          className="flex items-center gap-1 text-xs font-medium text-primary hover:underline"
        >
          View all
          <ArrowRight className="h-3 w-3" />
        </Link>
      </div>
      <div className="divide-y divide-border">
        {homeRequests.slice(0, 4).map((req) => (
          <div
            key={req.id}
            className="flex items-center justify-between px-5 py-3"
          >
            <div className="min-w-0 flex-1">
              <p className="truncate text-sm font-medium text-card-foreground">
                {req.guest?.firstName} {req.guest?.lastName}
              </p>
              <p className="truncate text-xs text-muted-foreground">
                {req.home?.address}
              </p>
            </div>
            <StatusBadge status={req.status} />
          </div>
        ))}
      </div>
    </div>
  )
}

export function RecentTransactions() {
  return (
    <div className="rounded-xl border border-border bg-card shadow-sm">
      <div className="flex items-center justify-between border-b border-border px-5 py-4">
        <div>
          <h3 className="font-semibold text-card-foreground">
            Recent Transactions
          </h3>
          <p className="text-xs text-muted-foreground">
            Latest payment activity
          </p>
        </div>
        <Link
          href="/payments"
          className="flex items-center gap-1 text-xs font-medium text-primary hover:underline"
        >
          View all
          <ArrowRight className="h-3 w-3" />
        </Link>
      </div>
      <div className="divide-y divide-border">
        {payments.slice(0, 4).map((payment) => (
          <div
            key={payment.id}
            className="flex items-center justify-between px-5 py-3"
          >
            <div className="min-w-0 flex-1">
              <p className="text-sm font-medium text-card-foreground">
                {payment.transactionReference}
              </p>
              <p className="text-xs text-muted-foreground">
                {payment.method} &middot;{" "}
                {new Date(payment.paymentDate).toLocaleDateString()}
              </p>
            </div>
            <div className="flex items-center gap-3">
              <span className="text-sm font-semibold text-card-foreground">
                ${payment.amount.toLocaleString()}
              </span>
              <StatusBadge status={payment.status} />
            </div>
          </div>
        ))}
      </div>
    </div>
  )
}
