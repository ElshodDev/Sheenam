import { DashboardShell } from "@/components/dashboard-shell"
import { RequestsTable } from "@/components/requests/requests-table"

export default function RequestsPage() {
  return (
    <DashboardShell>
      <RequestsTable />
    </DashboardShell>
  )
}
