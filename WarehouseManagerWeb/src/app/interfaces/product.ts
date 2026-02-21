import { FileUploadRequest } from "./file-upload-request";

export interface ProductDto {
    id?:          string;
    name?:        string;
    description?: string;
    rate?:        number;
    imagePath?:   string;
    brandId?:     string;
    brandName?:   string;
}

export interface CreateProductRequest {
    name:        string;
    description?: string;
    rate:        number;
    brandId:     string;
    image?:       FileUploadRequest;
}

export interface UpdateProductRequest {
    id:                 string;
    name:               string;
    description?:        string;
    rate:               number;
    brandId:            string;
    deleteCurrentImage: boolean;
    image?:              FileUploadRequest;
}

