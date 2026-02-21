import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Result } from '@app/interfaces/result';
import { CreateUserRequest, UpdateUserRequest, UserDataDto } from '@app/interfaces/user';
import { environment } from '@environments/environment';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class UsersService {
    private readonly apiUrl = environment.apiUrl;

    constructor(private http: HttpClient) { }

    getUserList(): Observable<Result<UserDataDto[]>> {
        return this.http.get<Result<UserDataDto[]>>(`${this.apiUrl}/users`);
    }

    getUser(id: string): Observable<Result<UserDataDto>> {
        return this.http.get<Result<UserDataDto>>(`${this.apiUrl}/users/${id}`);
    }

    createUser(request: CreateUserRequest): Observable<Result<string>> {
        return this.http.post<Result<string>>(`${this.apiUrl}/users`, request);
    }

    updateUser(request: UpdateUserRequest): Observable<Result<null>> {
        return this.http.put<Result<null>>(`${this.apiUrl}/users/${request.id}`, request);
    }
}
