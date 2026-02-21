import { AuthService } from '@app/service/auth/auth-service';
import { inject } from '@angular/core';
import { Router, type CanActivateFn } from '@angular/router';

export const loggedGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  if (!authService.isLoggedIn()) {
    return true;
  } else {

    router.navigate(['/']);
    return false;
  }
};
