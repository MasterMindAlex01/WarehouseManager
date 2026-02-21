import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from '@app/service/auth/auth-service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private session: AuthService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.status === 401) {
          this.session.logout();
        }
        // Puedes agregar lógica para mostrar mensajes de error globales aquí
        return throwError(() => error);
      })
    );
  }
}
