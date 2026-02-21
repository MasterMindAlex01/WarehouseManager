import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SearchRequest } from '@app/interfaces/common/pagination-dto';
import { PaginatedResult } from '@app/interfaces/paginated-result';
import { CreateProductRequest, ProductDto, UpdateProductRequest } from '@app/interfaces/product';
import { environment } from '@environments/environment';
import { Observable } from 'rxjs/internal/Observable';

@Injectable()
export class ProductsService {
        private readonly apiUrl = environment.apiUrl;

    constructor(private http: HttpClient) { }

    getProducts(searchFilter: SearchRequest): Observable<PaginatedResult<ProductDto>> {
        return this.http.post<PaginatedResult<ProductDto>>(`${this.apiUrl}/products/search`, searchFilter);
    }

    getProductById(id: string): Observable<ProductDto> {
        return this.http.get<ProductDto>(`${this.apiUrl}/products/${id}`);
    }

    createProduct(request: CreateProductRequest): Observable<ProductDto> {
        return this.http.post<ProductDto>(`${this.apiUrl}/products`, request);
    }

    updateProduct(id: string,request: UpdateProductRequest): Observable<void> {
        return this.http.put<void>(`${this.apiUrl}/products/${id}`, request);
    }

    deleteProduct(id: string): Observable<string> {
        return this.http.delete<string>(`${this.apiUrl}/products/${id}`);
    }
}
