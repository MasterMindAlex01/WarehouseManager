import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Result } from "@app/interfaces/result";
import { CreateOrUpdateRoleRequest, RoleDto } from "@app/interfaces/role";
import { environment } from "@environments/environment";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class RolesService {
    private readonly apiUrl = environment.apiUrl;

    constructor(private http: HttpClient) { }

    getRoleList(): Observable<Result<RoleDto[]>> {
        return this.http.get<Result<RoleDto[]>>(`${this.apiUrl}/roles`);
    }

    getRole(id: string): Observable<Result<RoleDto>> {
        return this.http.get<Result<RoleDto>>(`${this.apiUrl}/roles/${id}`);
    }

    createRole(request: CreateOrUpdateRoleRequest): Observable<Result<string>> {
        return this.http.post<Result<string>>(`${this.apiUrl}/roles`, request);
    }

    deleteRole(id: string): Observable<Result<RoleDto>> {
        return this.http.delete<Result<RoleDto>>(`${this.apiUrl}/roles/${id}`);
    }
}
