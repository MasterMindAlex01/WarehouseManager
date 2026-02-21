import { Routes } from '@angular/router';
import { Users } from './users/users';
import { Products } from './products/products';
import { Brands } from './brands/brands';

export default [
    { path: 'users', component: Users },
    { path: 'brands', component: Brands },
    { path: 'products', component: Products },
    { path: '**', redirectTo: '/notfound' }
] as Routes;
