import { ErrorInterceptor } from './error.interceptor';
import { SessionService } from '../services/session.service';
import { HttpErrorResponse, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { of, throwError } from 'rxjs';

describe('ErrorInterceptor', () => {
  let interceptor: ErrorInterceptor;
  let sessionServiceSpy: jasmine.SpyObj<SessionService>;
  let next: HttpHandler;

  beforeEach(() => {
    sessionServiceSpy = jasmine.createSpyObj('SessionService', ['clearSession']);
    interceptor = new ErrorInterceptor(sessionServiceSpy);
    next = {
      handle: jasmine.createSpy('handle').and.returnValue(throwError(() => new HttpErrorResponse({ status: 401 })))
    };
  });

  afterEach(() => {
    // No TestBed usage, pero se puede limpiar spies si se agregan más adelante
  });

  it('should call clearSession on 401 error', (done) => {
    const req = new HttpRequest('GET', '/test');
    interceptor.intercept(req, next).subscribe({
      error: (error) => {
        expect(sessionServiceSpy.clearSession).toHaveBeenCalled();
        expect(error.status).toBe(401);
        done();
      }
    });
  });

  it('should pass through non-401 errors', (done) => {
    next.handle = jasmine.createSpy('handle').and.returnValue(throwError(() => new HttpErrorResponse({ status: 500 })));
    const req = new HttpRequest('GET', '/test');
    interceptor.intercept(req, next).subscribe({
      error: (error) => {
        expect(sessionServiceSpy.clearSession).not.toHaveBeenCalled();
        expect(error.status).toBe(500);
        done();
      }
    });
  });
});
