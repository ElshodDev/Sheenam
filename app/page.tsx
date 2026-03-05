import { DashboardShell } from "@/components/dashboard-shell"
import { HeroSection } from "@/components/dashboard/hero-section"
import { StatCards } from "@/components/dashboard/stat-cards"
import { FeaturedHomes } from "@/components/dashboard/featured-homes"
import {
  RecentRequests,
  RecentTransactions,
} from "@/components/dashboard/recent-activity"

export default function DashboardPage() {
  return (
    <DashboardShell>
      <div className="flex flex-col gap-6">
        <HeroSection />
        <StatCards />
        <FeaturedHomes />
        <div className="grid grid-cols-1 gap-4 lg:grid-cols-2">
          <RecentRequests />
          <RecentTransactions />
        </div>
      </div>
    </DashboardShell>
  )
}
