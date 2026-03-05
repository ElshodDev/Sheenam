import { DashboardShell } from "@/components/dashboard-shell"
import { GuestsTable } from "@/components/guests/guests-table"

export default function GuestsPage() {
  return (
    <DashboardShell>
      <GuestsTable />
    </DashboardShell>
  )
}
