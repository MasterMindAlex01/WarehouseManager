import { Routes } from '@angular/router';
import { AppLayout } from './app/layout/component/app.layout';
import { Dashboard } from './app/pages/dashboard/dashboard';
import { Notfound } from './app/pages/notfound/notfound';
import { authGuard } from './app/guards/auth-guard';
import { loggedGuard } from './app/guards/logged-guard';

export const appRoutes: Routes = [
    {
        path: 'auth', loadChildren: () => import('./app/pages/auth/auth.routes'),
        canActivateChild: [loggedGuard]
    },
    {
        path: '',
        component: AppLayout,
        children: [
            { path: '', component: Dashboard },
            { path: 'pages', loadChildren: () => import('./app/pages/pages.routes') }
        ],
        canActivateChild: [authGuard]
    },
    { path: 'notfound', component: Notfound },
    {
        path: '**',
        redirectTo: 'auth/login',
        pathMatch: 'prefix'
    },
];
