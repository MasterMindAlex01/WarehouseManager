import { Routes } from '@angular/router';
import { Users } from './users/users';
import { Products } from './products/products';

export default [
    { path: 'users', component: Users },
    { path: 'products', component: Products },
    { path: '**', redirectTo: '/notfound' }
] as Routes;
