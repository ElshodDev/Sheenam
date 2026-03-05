"use client"

import { useState, useMemo } from "react"
import { Search, ChevronLeft, ChevronRight } from "lucide-react"

interface Column<T> {
  header: string
  accessor: (row: T) => React.ReactNode
  className?: string
}

interface DataTableProps<T> {
  title: string
  description: string
  data: T[]
  columns: Column<T>[]
  searchPlaceholder?: string
  searchFn?: (item: T, query: string) => boolean
  pageSize?: number
}

export function DataTable<T>({
  title,
  description,
  data,
  columns,
  searchPlaceholder = "Search...",
  searchFn,
  pageSize = 10,
}: DataTableProps<T>) {
  const [search, setSearch] = useState("")
  const [page, setPage] = useState(0)

  const filtered = useMemo(() => {
    if (!search || !searchFn) return data
    return data.filter((item) => searchFn(item, search.toLowerCase()))
  }, [data, search, searchFn])

  const totalPages = Math.ceil(filtered.length / pageSize)
  const paged = filtered.slice(page * pageSize, (page + 1) * pageSize)

  return (
    <div className="flex flex-col gap-6">
      <div>
        <h1 className="text-2xl font-bold text-foreground">{title}</h1>
        <p className="text-sm text-muted-foreground">{description}</p>
      </div>

      <div className="rounded-xl border border-border bg-card shadow-sm">
        {/* Search bar */}
        {searchFn && (
          <div className="border-b border-border p-4">
            <div className="relative max-w-sm">
              <Search className="absolute left-3 top-1/2 h-4 w-4 -translate-y-1/2 text-muted-foreground" />
              <input
                type="text"
                placeholder={searchPlaceholder}
                value={search}
                onChange={(e) => {
                  setSearch(e.target.value)
                  setPage(0)
                }}
                className="h-9 w-full rounded-lg border border-input bg-background pl-9 pr-3 text-sm text-foreground placeholder:text-muted-foreground focus:outline-none focus:ring-2 focus:ring-ring"
              />
            </div>
          </div>
        )}

        {/* Table */}
        <div className="overflow-x-auto">
          <table className="w-full">
            <thead>
              <tr className="border-b border-border">
                {columns.map((col, i) => (
                  <th
                    key={i}
                    className={`px-5 py-3 text-left text-xs font-semibold uppercase tracking-wider text-muted-foreground ${col.className || ""}`}
                  >
                    {col.header}
                  </th>
                ))}
              </tr>
            </thead>
            <tbody className="divide-y divide-border">
              {paged.map((row, i) => (
                <tr
                  key={i}
                  className="transition-colors hover:bg-muted/50"
                >
                  {columns.map((col, j) => (
                    <td
                      key={j}
                      className={`px-5 py-3 text-sm ${col.className || ""}`}
                    >
                      {col.accessor(row)}
                    </td>
                  ))}
                </tr>
              ))}
              {paged.length === 0 && (
                <tr>
                  <td
                    colSpan={columns.length}
                    className="px-5 py-12 text-center text-sm text-muted-foreground"
                  >
                    No records found.
                  </td>
                </tr>
              )}
            </tbody>
          </table>
        </div>

        {/* Pagination */}
        {totalPages > 1 && (
          <div className="flex items-center justify-between border-t border-border px-5 py-3">
            <span className="text-xs text-muted-foreground">
              Showing {page * pageSize + 1} to{" "}
              {Math.min((page + 1) * pageSize, filtered.length)} of{" "}
              {filtered.length}
            </span>
            <div className="flex items-center gap-1">
              <button
                onClick={() => setPage(page - 1)}
                disabled={page === 0}
                className="rounded-md p-1.5 text-muted-foreground hover:bg-muted disabled:opacity-50"
                aria-label="Previous page"
              >
                <ChevronLeft className="h-4 w-4" />
              </button>
              <span className="px-2 text-xs font-medium text-foreground">
                {page + 1} / {totalPages}
              </span>
              <button
                onClick={() => setPage(page + 1)}
                disabled={page >= totalPages - 1}
                className="rounded-md p-1.5 text-muted-foreground hover:bg-muted disabled:opacity-50"
                aria-label="Next page"
              >
                <ChevronRight className="h-4 w-4" />
              </button>
            </div>
          </div>
        )}
      </div>
    </div>
  )
}
