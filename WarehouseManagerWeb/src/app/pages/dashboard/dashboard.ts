import { Component, inject, signal } from '@angular/core';
import { StatsWidget } from './components/statswidget';
import { ProductsService } from '@app/service/products/products.service';
import { BrandsService } from '@app/service/brands/brands.service';
import { UsersService } from '@app/service/users/users.service';

@Component({
    selector: 'app-dashboard',
    imports: [StatsWidget],
    providers: [ProductsService, BrandsService, UsersService],
    template: `
        <div class="grid grid-cols-12 gap-8">
            <app-stats-widget class="contents"
            [totalProductRecords]="totalProductRecords()"
            [totalBrandRecords]="totalBrandRecords()"
            [totalUserRecords]="totalUserRecords()" />
        </div>
    `
})
export class Dashboard {

    productsService = inject(ProductsService);
    brandsService = inject(BrandsService);
    usersService = inject(UsersService);

    totalProductRecords = signal<number>(0);
    totalBrandRecords = signal<number>(0);
    totalUserRecords = signal<number>(0);

    ngOnInit() {
        this.loadData();
    }

    loadData(page: number = 1, limit: number = 100) {

        this.brandsService.getBrands({
            pageNumber: page,
            pageSize: limit
        }).subscribe((data) => {
            this.totalBrandRecords.set(data.totalCount);
        });

        this.productsService.getProducts({
            pageNumber: page,
            pageSize: limit
        }).subscribe((data) => {
            this.totalProductRecords.set(data.totalCount);
        });

        this.usersService.getUserList().subscribe({
            next: (result) => {
                this.totalUserRecords.set(result.data?.length || 0);
            },
            error: (err) => {
                console.error(err)
            },
        });
    }
}
