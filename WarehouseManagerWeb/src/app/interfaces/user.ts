import { FileUploadRequest } from "./file-upload-request"

export interface UserDataDto {
    id?: string,
    userName?: string,
    firstName?: string,
    lastName?: string,
    email?: string,
    isActive?: boolean,
    emailConfirmed?: boolean,
    phoneNumber?: string,
    imageUrl?: string
}

export interface CreateUserRequest {
    firstName: string,
    lastName: string,
    email: string,
    userName: string,
    password: string,
    confirmPassword: string,
    phoneNumber: string
}

export interface UpdateUserRequest {
    id: string,
    firstName: string,
    lastName: string,
    phoneNumber: string,
    email: string,
    image?: FileUploadRequest,
    deleteCurrentImage: boolean
}
