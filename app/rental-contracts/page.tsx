import { DashboardShell } from "@/components/dashboard-shell"
import { ContractsTable } from "@/components/rental-contracts/contracts-table"

export default function RentalContractsPage() {
  return (
    <DashboardShell>
      <ContractsTable />
    </DashboardShell>
  )
}
