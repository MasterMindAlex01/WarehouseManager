export interface BrandDto {
    id?:          string;
    name?:        string;
    description?: string;
}

export interface CreateBrandRequest {
    name: string;
    description?: string;
}

export interface UpdateBrandRequest {
    id:          string;
    name:        string;
    description?: string;
}
