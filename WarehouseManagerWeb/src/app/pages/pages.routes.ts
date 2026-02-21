import { Routes } from '@angular/router';
import { Users } from './users/users';
import { Products } from './products/products';
import { Brands } from './brands/brands';
import { Profile } from './profile/profile';

export default [
    { path: 'users', component: Users },
    { path: 'brands', component: Brands },
    { path: 'products', component: Products },
    { path: 'profile', component: Profile },
    { path: '**', redirectTo: '/notfound' }
] as Routes;
