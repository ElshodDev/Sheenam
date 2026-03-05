import { DashboardShell } from "@/components/dashboard-shell"
import { PaymentsTable } from "@/components/payments/payments-table"

export default function PaymentsPage() {
  return (
    <DashboardShell>
      <PaymentsTable />
    </DashboardShell>
  )
}
