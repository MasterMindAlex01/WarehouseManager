import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BrandDto, CreateBrandRequest, UpdateBrandRequest } from "@app/interfaces/brand";
import { SearchRequest } from "@app/interfaces/common/pagination-dto";
import { PaginatedResult } from "@app/interfaces/paginated-result";
import { environment } from "@environments/environment";
import { Observable } from "rxjs";

@Injectable()
export class BrandsService {
        private readonly apiUrl = environment.apiUrl;

    constructor(private http: HttpClient) { }

    getBrands(searchFilter: SearchRequest): Observable<PaginatedResult<BrandDto>> {
        return this.http.post<PaginatedResult<BrandDto>>(`${this.apiUrl}/brands/search`, searchFilter);
    }

    getBrandById(id: string): Observable<BrandDto> {
        return this.http.get<BrandDto>(`${this.apiUrl}/brands/${id}`);
    }

    createBrand(request: CreateBrandRequest): Observable<string> {
        return this.http.post<string>(`${this.apiUrl}/brands`, request);
    }

    updateBrand(id: string,request: UpdateBrandRequest): Observable<string> {
        return this.http.put<string>(`${this.apiUrl}/brands/${id}`, request);
    }
}
