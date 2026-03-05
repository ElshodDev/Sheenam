import { DashboardShell } from "@/components/dashboard-shell"
import { HostsTable } from "@/components/hosts/hosts-table"

export default function HostsPage() {
  return (
    <DashboardShell>
      <HostsTable />
    </DashboardShell>
  )
}
