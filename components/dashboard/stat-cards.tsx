import {
  Home,
  Users,
  UserCheck,
  TrendingUp,
  DollarSign,
  Clock,
} from "lucide-react"
import { dashboardStats } from "@/lib/mock-data"

const stats = [
  {
    label: "Total Homes",
    value: dashboardStats.totalHomes,
    icon: Home,
    trend: "+12%",
    trendUp: true,
    color: "bg-primary/10 text-primary",
  },
  {
    label: "Total Hosts",
    value: dashboardStats.totalHosts,
    icon: Users,
    trend: "+5%",
    trendUp: true,
    color: "bg-chart-2/10 text-chart-2",
  },
  {
    label: "Active Guests",
    value: dashboardStats.activeGuests,
    icon: UserCheck,
    trend: "+18%",
    trendUp: true,
    color: "bg-chart-3/10 text-chart-3",
  },
  {
    label: "Total Revenue",
    value: `$${dashboardStats.totalRevenue.toLocaleString()}`,
    icon: DollarSign,
    trend: "+24%",
    trendUp: true,
    color: "bg-chart-5/10 text-chart-5",
  },
  {
    label: "Transactions",
    value: dashboardStats.totalTransactions,
    icon: TrendingUp,
    trend: "+8%",
    trendUp: true,
    color: "bg-chart-4/10 text-chart-4",
  },
  {
    label: "Pending Requests",
    value: dashboardStats.pendingRequests,
    icon: Clock,
    trend: "2 new",
    trendUp: false,
    color: "bg-warning/10 text-warning",
  },
]

export function StatCards() {
  return (
    <div className="grid grid-cols-1 gap-4 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-6">
      {stats.map((stat) => (
        <div
          key={stat.label}
          className="rounded-xl border border-border bg-card p-4 shadow-sm transition-shadow hover:shadow-md"
        >
          <div className="flex items-center justify-between">
            <div
              className={`flex h-10 w-10 items-center justify-center rounded-lg ${stat.color}`}
            >
              <stat.icon className="h-5 w-5" />
            </div>
            <span
              className={`text-xs font-medium ${
                stat.trendUp ? "text-chart-2" : "text-warning"
              }`}
            >
              {stat.trend}
            </span>
          </div>
          <div className="mt-3">
            <p className="text-2xl font-bold text-card-foreground">
              {stat.value}
            </p>
            <p className="text-xs text-muted-foreground">{stat.label}</p>
          </div>
        </div>
      ))}
    </div>
  )
}
