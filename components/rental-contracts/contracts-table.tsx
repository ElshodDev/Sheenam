"use client"

import { rentalContracts } from "@/lib/mock-data"
import { DataTable } from "@/components/data-table"
import type { RentalContract } from "@/lib/types"

function StatusBadge({ status }: { status: string }) {
  const styles: Record<string, string> = {
    Active: "bg-success/10 text-success",
    Expiring: "bg-warning/10 text-warning",
    Expired: "bg-destructive/10 text-destructive",
    Terminated: "bg-muted text-muted-foreground",
  }

  return (
    <span
      className={`inline-flex items-center gap-1 rounded-full px-2.5 py-0.5 text-xs font-medium ${styles[status] || "bg-muted text-muted-foreground"}`}
    >
      <span
        className={`h-1.5 w-1.5 rounded-full ${
          status === "Active"
            ? "bg-success"
            : status === "Expiring"
              ? "bg-warning"
              : "bg-destructive"
        }`}
      />
      {status}
    </span>
  )
}

export function ContractsTable() {
  return (
    <DataTable<RentalContract>
      title="Rental Contracts"
      description="Manage active and past rental agreements"
      data={rentalContracts}
      searchPlaceholder="Search contracts..."
      searchFn={(contract, q) =>
        `${contract.guest?.firstName} ${contract.guest?.lastName}`
          .toLowerCase()
          .includes(q) ||
        (contract.home?.address || "").toLowerCase().includes(q)
      }
      columns={[
        {
          header: "Tenant",
          accessor: (contract) => (
            <p className="font-medium text-card-foreground">
              {contract.guest?.firstName} {contract.guest?.lastName}
            </p>
          ),
        },
        {
          header: "Property",
          accessor: (contract) => (
            <span className="text-muted-foreground">
              {contract.home?.address}
            </span>
          ),
        },
        {
          header: "Period",
          accessor: (contract) => (
            <span className="text-muted-foreground">
              {new Date(contract.startDate).toLocaleDateString()} -{" "}
              {new Date(contract.endDate).toLocaleDateString()}
            </span>
          ),
        },
        {
          header: "Monthly Rent",
          accessor: (contract) => (
            <span className="font-semibold text-card-foreground">
              ${contract.monthlyRent.toLocaleString()}
            </span>
          ),
        },
        {
          header: "Deposit",
          accessor: (contract) => (
            <span className="text-muted-foreground">
              ${contract.securityDeposit.toLocaleString()}
            </span>
          ),
        },
        {
          header: "Status",
          accessor: (contract) => <StatusBadge status={contract.status} />,
        },
      ]}
    />
  )
}
