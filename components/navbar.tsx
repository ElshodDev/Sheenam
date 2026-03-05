"use client"

import { useState } from "react"
import { Bell, Search, User, Menu } from "lucide-react"
import { cn } from "@/lib/utils"
import { notifications } from "@/lib/mock-data"

interface NavbarProps {
  sidebarCollapsed: boolean
  onMobileMenuToggle: () => void
}

export function Navbar({ sidebarCollapsed, onMobileMenuToggle }: NavbarProps) {
  const [searchQuery, setSearchQuery] = useState("")
  const unreadCount = notifications.filter((n) => !n.isRead).length

  return (
    <header
      className={cn(
        "fixed right-0 top-0 z-30 flex h-16 items-center justify-between border-b border-border bg-card px-4 transition-all duration-300 md:px-6",
        sidebarCollapsed
          ? "left-0 md:left-[68px]"
          : "left-0 md:left-[260px]"
      )}
    >
      <div className="flex items-center gap-3">
        <button
          onClick={onMobileMenuToggle}
          className="rounded-lg p-2 text-muted-foreground hover:bg-accent md:hidden"
          aria-label="Toggle menu"
        >
          <Menu className="h-5 w-5" />
        </button>
        <div className="relative hidden sm:block">
          <Search className="absolute left-3 top-1/2 h-4 w-4 -translate-y-1/2 text-muted-foreground" />
          <input
            type="text"
            placeholder="Search properties, hosts, guests..."
            value={searchQuery}
            onChange={(e) => setSearchQuery(e.target.value)}
            className="h-10 w-64 rounded-lg border border-input bg-background pl-10 pr-4 text-sm text-foreground placeholder:text-muted-foreground focus:outline-none focus:ring-2 focus:ring-ring lg:w-80"
          />
        </div>
      </div>

      <div className="flex items-center gap-2">
        <button
          className="relative rounded-lg p-2.5 text-muted-foreground hover:bg-accent transition-colors"
          aria-label={`Notifications (${unreadCount} unread)`}
        >
          <Bell className="h-5 w-5" />
          {unreadCount > 0 && (
            <span className="absolute right-1.5 top-1.5 flex h-4 w-4 items-center justify-center rounded-full bg-destructive text-[10px] font-bold text-destructive-foreground">
              {unreadCount}
            </span>
          )}
        </button>
        <div className="flex items-center gap-3 rounded-lg border border-border px-3 py-1.5">
          <div className="flex h-8 w-8 items-center justify-center rounded-full bg-primary">
            <User className="h-4 w-4 text-primary-foreground" />
          </div>
          <div className="hidden sm:block">
            <p className="text-sm font-medium text-foreground">Admin User</p>
            <p className="text-xs text-muted-foreground">admin@sheenam.com</p>
          </div>
        </div>
      </div>
    </header>
  )
}
