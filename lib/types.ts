export type HouseType =
  | "Flat"
  | "Bungalow"
  | "Duplex"
  | "Villa"
  | "Townhouse"
  | "Studio"
  | "Penthouse"
  | "Cottage"

export type ListingType = "ForRent" | "ForSale" | "Both"

export type HomeRequestStatus = "Pending" | "Approved" | "Rejected"

export type PaymentStatus = "Pending" | "Completed" | "Failed" | "Refunded"

export type PaymentMethod = "Cash" | "BankTransfer" | "Card" | "Crypto"

export interface Home {
  id: string
  hostId: string
  address: string
  additionalInfo: string
  isVacant: boolean
  isPetAllowed: boolean
  isShared: boolean
  numberOfBedrooms: number
  numberOfBathrooms: number
  area: number
  type: HouseType
  listingType: ListingType
  monthlyRent?: number
  salePrice?: number
  securityDeposit?: number
  imageUrls: string
  isFeatured: boolean
  host?: Host
  createdDate: string
  updatedDate: string
}

export interface Host {
  id: string
  firstName: string
  lastName: string
  dateOfBirth: string
  email: string
  phoneNumber: string
  gender: string
  homes?: Home[]
}

export interface Guest {
  id: string
  firstName: string
  lastName: string
  dateOfBirth: string
  email: string
  phoneNumber: string
  address: string
  gender: string
}

export interface HomeRequest {
  id: string
  guestId: string
  homeId: string
  message: string
  startDate: string
  endDate: string
  createdDate: string
  updatedDate: string
  status: HomeRequestStatus
  rejectionReason?: string
  guest?: Guest
  home?: Home
}

export interface Payment {
  id: string
  userId: string
  rentalContractId?: string
  saleTransactionId?: string
  amount: number
  method: PaymentMethod
  status: PaymentStatus
  paymentDate: string
  transactionReference: string
  createdDate: string
  updatedDate: string
}

export interface Review {
  id: string
  homeId: string
  guestId: string
  rating: number
  comment: string
  createdDate: string
  guest?: Guest
  home?: Home
}

export interface Notification {
  id: string
  userId: string
  title: string
  message: string
  type: string
  isRead: boolean
  createdDate: string
}

export interface SaleOffer {
  id: string
  homeId: string
  guestId: string
  offerAmount: number
  message: string
  status: string
  createdDate: string
  guest?: Guest
  home?: Home
}

export interface RentalContract {
  id: string
  homeId: string
  guestId: string
  startDate: string
  endDate: string
  monthlyRent: number
  securityDeposit: number
  status: string
  createdDate: string
  guest?: Guest
  home?: Home
}
