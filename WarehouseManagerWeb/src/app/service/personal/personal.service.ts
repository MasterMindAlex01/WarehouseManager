import { HttpClient } from "@angular/common/http";
import { UpdateUserRequest, UserDataDto } from "@app/interfaces/user";
import { Observable } from "rxjs";
import { Result } from "@app/interfaces/result";
import { environment } from "@environments/environment";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class PersonalService {
    private readonly apiUrl = environment.apiUrl;

    constructor(private http: HttpClient) { }

    getProfile(): Observable<Result<UserDataDto>> {
        return this.http.get<Result<UserDataDto>>(`${this.apiUrl}/personal/profile`);
    }

    updateProfile(request: UpdateUserRequest): Observable<Result<null>> {
        return this.http.put<Result<null>>(`${this.apiUrl}/personal/profile`, request);
    }
}
