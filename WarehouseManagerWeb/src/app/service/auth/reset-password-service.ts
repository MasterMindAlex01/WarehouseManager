import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Result } from '@app/interfaces/result';
import { environment } from '@environments/environment';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ResetPasswordService {
    private readonly apiUrl = environment.apiUrl;
    private readonly http = inject(HttpClient)
    private headers = new HttpHeaders({
        'Content-Type': 'application/json'
    });

    forgotPassword(credentials: { tenant: string; email: string; }): Observable<Result<null>>{
        const headers = new HttpHeaders({
            'Content-Type': 'application/json',
            'tenant': credentials.tenant
        });

        return this.http.post<Result<null>>(`${this.apiUrl}/users/forgot-password`, {
            email: credentials.email
        }, {
            headers,
        }).pipe(
            tap((response: Result<null>) => {
                console.log(response);
            })
        );
    }

    resetPassword(credentials: { tenant: string; email: string; password: string; token: string }): Observable<Result<null>>{
        const headers = new HttpHeaders({
            'Content-Type': 'application/json',
            'tenant': credentials.tenant
        });

        return this.http.post<Result<null>>(`${this.apiUrl}/users/reset-password`, {
            email: credentials.email,
            password: credentials.password,
            token: credentials.token
        }, {
            headers,
        }).pipe(
            tap((response: Result<null>) => {
                console.log(response);
            })
        );
    }
}
