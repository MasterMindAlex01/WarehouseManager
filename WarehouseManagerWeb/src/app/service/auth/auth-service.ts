
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AuthResponse } from '@app/interfaces/auth';
import { Result } from '@app/interfaces/result';
import { environment } from '@environments/environment';
import { Observable, tap } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private readonly apiUrl = environment.apiUrl;
    private readonly ACCESS_TOKEN_KEY = 'access_token';
    private readonly REFRESH_TOKEN_KEY = 'refresh_token';
    private readonly TENANT_KEY = 'tenant';

    constructor(private http: HttpClient, private router: Router) { }

    login(credentials: { email: string; password: string; tenant: string }): Observable<Result<AuthResponse>> {

        const headers = new HttpHeaders({
            'Content-Type': 'application/json',
            'tenant': credentials.tenant
        });

        return this.http.post<Result<AuthResponse>>(`${this.apiUrl}/tokens`, {
            email: credentials.email,
            password: credentials.password
        }, {
            headers,
        }).pipe(
            tap((response: Result<AuthResponse>) => {
                this.saveTenant(credentials.tenant);
                this.saveTokens(response.data!);
            })
        );
    }

    refreshToken(): Observable<Result<AuthResponse>> {
        const headers = new HttpHeaders({
            'Content-Type': 'application/json',
            'tenant': this.getTenant()!
        });

        return this.http.post<Result<AuthResponse>>(`${this.apiUrl}/tokens/refresh`, {
            token: this.getAccessToken(),
            refreshToken: this.getRefreshToken()
        }, {
            headers
        }).pipe(
            tap((response: Result<AuthResponse>) => {
                this.saveTokens(response.data!);
            })
        );
    }

    logout(): void {
        this.clearTokens();
        this.router.navigate(['/auth/login']);
    }

    private clearTokens(): void {
        localStorage.removeItem(this.ACCESS_TOKEN_KEY);
        localStorage.removeItem(this.REFRESH_TOKEN_KEY);
        localStorage.removeItem(this.TENANT_KEY);
    }

    saveTokens(response: AuthResponse): void {
        localStorage.setItem(this.ACCESS_TOKEN_KEY, response.token);
        localStorage.setItem(this.REFRESH_TOKEN_KEY, response.refreshToken);
    }

    saveTenant(tenant: string): void {
        localStorage.setItem(this.TENANT_KEY, tenant);
    }

    getAccessToken(): string | null {
        return localStorage.getItem(this.ACCESS_TOKEN_KEY);
    }

    getRefreshToken(): string | null {
        return localStorage.getItem(this.REFRESH_TOKEN_KEY);
    }

    getTenant(): string | null {
        return localStorage.getItem(this.TENANT_KEY);
    }

    isLoggedIn(): boolean {
        return !!this.getAccessToken();
    }
}
