"use client"

import { Bell, FileText, DollarSign, Star, Home, Circle } from "lucide-react"
import { notifications } from "@/lib/mock-data"

const typeIcons: Record<string, React.ReactNode> = {
  Request: <FileText className="h-5 w-5" />,
  Payment: <DollarSign className="h-5 w-5" />,
  Review: <Star className="h-5 w-5" />,
  Listing: <Home className="h-5 w-5" />,
}

const typeColors: Record<string, string> = {
  Request: "bg-primary/10 text-primary",
  Payment: "bg-success/10 text-success",
  Review: "bg-warning/10 text-warning",
  Listing: "bg-chart-4/10 text-chart-4",
}

export function NotificationsContent() {
  const unreadCount = notifications.filter((n) => !n.isRead).length

  return (
    <div className="flex flex-col gap-6">
      <div className="flex items-center justify-between">
        <div>
          <h1 className="text-2xl font-bold text-foreground">Notifications</h1>
          <p className="text-sm text-muted-foreground">
            {unreadCount} unread notification{unreadCount !== 1 ? "s" : ""}
          </p>
        </div>
        <button className="rounded-lg border border-border bg-card px-4 py-2 text-sm font-medium text-card-foreground hover:bg-accent transition-colors">
          Mark all as read
        </button>
      </div>

      <div className="flex flex-col gap-2">
        {notifications.map((notification) => (
          <div
            key={notification.id}
            className={`flex items-start gap-4 rounded-xl border border-border p-4 shadow-sm transition-colors ${
              notification.isRead ? "bg-card" : "bg-primary/[0.02] border-primary/20"
            }`}
          >
            <div
              className={`flex h-10 w-10 shrink-0 items-center justify-center rounded-lg ${typeColors[notification.type] || "bg-muted text-muted-foreground"}`}
            >
              {typeIcons[notification.type] || (
                <Bell className="h-5 w-5" />
              )}
            </div>
            <div className="min-w-0 flex-1">
              <div className="flex items-start justify-between gap-2">
                <p className="font-medium text-card-foreground">
                  {notification.title}
                </p>
                {!notification.isRead && (
                  <Circle className="mt-1 h-2 w-2 shrink-0 fill-primary text-primary" />
                )}
              </div>
              <p className="mt-0.5 text-sm text-muted-foreground">
                {notification.message}
              </p>
              <p className="mt-2 text-xs text-muted-foreground/70">
                {new Date(notification.createdDate).toLocaleString()}
              </p>
            </div>
          </div>
        ))}
      </div>
    </div>
  )
}
