import { DashboardShell } from "@/components/dashboard-shell"
import { SaleOffersTable } from "@/components/sale-offers/sale-offers-table"

export default function SaleOffersPage() {
  return (
    <DashboardShell>
      <SaleOffersTable />
    </DashboardShell>
  )
}
