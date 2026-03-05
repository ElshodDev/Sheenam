"use client"

import { User, Mail, Phone } from "lucide-react"
import { hosts } from "@/lib/mock-data"
import { DataTable } from "@/components/data-table"
import type { Host } from "@/lib/types"

export function HostsTable() {
  return (
    <DataTable<Host>
      title="Hosts"
      description="Manage property hosts on the platform"
      data={hosts}
      searchPlaceholder="Search hosts..."
      searchFn={(host, q) =>
        `${host.firstName} ${host.lastName}`.toLowerCase().includes(q) ||
        host.email.toLowerCase().includes(q)
      }
      columns={[
        {
          header: "Host",
          accessor: (host) => (
            <div className="flex items-center gap-3">
              <div className="flex h-9 w-9 items-center justify-center rounded-full bg-primary/10 text-primary">
                <User className="h-4 w-4" />
              </div>
              <div>
                <p className="font-medium text-card-foreground">
                  {host.firstName} {host.lastName}
                </p>
                <p className="text-xs text-muted-foreground">{host.gender}</p>
              </div>
            </div>
          ),
        },
        {
          header: "Email",
          accessor: (host) => (
            <div className="flex items-center gap-2 text-muted-foreground">
              <Mail className="h-3.5 w-3.5" />
              <span>{host.email}</span>
            </div>
          ),
        },
        {
          header: "Phone",
          accessor: (host) => (
            <div className="flex items-center gap-2 text-muted-foreground">
              <Phone className="h-3.5 w-3.5" />
              <span>{host.phoneNumber}</span>
            </div>
          ),
        },
        {
          header: "Date of Birth",
          accessor: (host) => (
            <span className="text-muted-foreground">
              {new Date(host.dateOfBirth).toLocaleDateString()}
            </span>
          ),
        },
      ]}
    />
  )
}
