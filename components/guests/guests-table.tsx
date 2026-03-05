"use client"

import { User, Mail, Phone, MapPin } from "lucide-react"
import { guests } from "@/lib/mock-data"
import { DataTable } from "@/components/data-table"
import type { Guest } from "@/lib/types"

export function GuestsTable() {
  return (
    <DataTable<Guest>
      title="Guests"
      description="All registered guests on the platform"
      data={guests}
      searchPlaceholder="Search guests..."
      searchFn={(guest, q) =>
        `${guest.firstName} ${guest.lastName}`.toLowerCase().includes(q) ||
        guest.email.toLowerCase().includes(q)
      }
      columns={[
        {
          header: "Guest",
          accessor: (guest) => (
            <div className="flex items-center gap-3">
              <div className="flex h-9 w-9 items-center justify-center rounded-full bg-chart-2/10 text-chart-2">
                <User className="h-4 w-4" />
              </div>
              <div>
                <p className="font-medium text-card-foreground">
                  {guest.firstName} {guest.lastName}
                </p>
                <p className="text-xs text-muted-foreground">{guest.gender}</p>
              </div>
            </div>
          ),
        },
        {
          header: "Email",
          accessor: (guest) => (
            <div className="flex items-center gap-2 text-muted-foreground">
              <Mail className="h-3.5 w-3.5" />
              <span>{guest.email}</span>
            </div>
          ),
        },
        {
          header: "Phone",
          accessor: (guest) => (
            <div className="flex items-center gap-2 text-muted-foreground">
              <Phone className="h-3.5 w-3.5" />
              <span>{guest.phoneNumber}</span>
            </div>
          ),
        },
        {
          header: "Address",
          accessor: (guest) => (
            <div className="flex items-center gap-2 text-muted-foreground">
              <MapPin className="h-3.5 w-3.5" />
              <span>{guest.address}</span>
            </div>
          ),
        },
      ]}
    />
  )
}
