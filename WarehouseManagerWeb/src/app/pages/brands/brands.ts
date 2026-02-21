import { BrandDto, CreateBrandRequest, UpdateBrandRequest } from './../../interfaces/brand';
import { Component, OnInit, signal, ViewChild, inject } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Table, TableModule } from 'primeng/table';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { RippleModule } from 'primeng/ripple';
import { ToastModule } from 'primeng/toast';
import { ToolbarModule } from 'primeng/toolbar';
import { RatingModule } from 'primeng/rating';
import { InputTextModule } from 'primeng/inputtext';
import { TextareaModule } from 'primeng/textarea';
import { SelectModule } from 'primeng/select';
import { RadioButtonModule } from 'primeng/radiobutton';
import { InputNumberModule } from 'primeng/inputnumber';
import { DialogModule } from 'primeng/dialog';
import { TagModule } from 'primeng/tag';
import { InputIconModule } from 'primeng/inputicon';
import { IconFieldModule } from 'primeng/iconfield';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ProductDto, UpdateProductRequest } from '@app/interfaces/product';
import { CreateProductRequest } from '../../interfaces/product';
import { Paginator, PaginatorModule } from "primeng/paginator";
import { BrandsService } from '../../service/brands/brands.service';

interface Column {
    field: string;
    header: string;
    customExportHeader?: string;
}

interface ExportColumn {
    title: string;
    dataKey: string;
}

@Component({
    selector: 'app-products-crud',
    standalone: true,
    imports: [
        CommonModule,
        TableModule,
        FormsModule,
        ButtonModule,
        RippleModule,
        ToastModule,
        ToolbarModule,
        RatingModule,
        InputTextModule,
        TextareaModule,
        SelectModule,
        RadioButtonModule,
        InputNumberModule,
        DialogModule,
        TagModule,
        InputIconModule,
        IconFieldModule,
        ConfirmDialogModule,
        Paginator,
        PaginatorModule
    ],
    templateUrl: './brands.html',
    providers: [MessageService, BrandsService, ConfirmationService]
})
export class Brands implements OnInit {
    brandDialog: boolean = false;

    brands = signal<BrandDto[]>([]);
    brand!: BrandDto;
    submitted: boolean = false;

    @ViewChild('dt') dt!: Table;

    exportColumns!: ExportColumn[];

    cols!: Column[];

    isEditing: boolean = false
    first: number = 0;
    rows: number = 10;
    totalRecords: number = 0;

    brandsService = inject(BrandsService);
    messageService = inject(MessageService);
    confirmationService = inject(ConfirmationService);

    ngOnInit() {
        this.loadData();
    }

    loadData(page: number = 1, limit: number = 10) {

        this.brandsService.getBrands({
            pageNumber: page,
            pageSize: limit
        }).subscribe((data) => {
            this.totalRecords = data.totalCount;
            this.brands.set(data.data);
        });

        this.cols = [
            { field: 'name', header: 'Name' },
            { field: 'description', header: 'Description' },
        ];

        this.exportColumns = this.cols.map((col) => ({ title: col.header, dataKey: col.field }));
    }

    onGlobalFilter(table: Table, event: Event) {
        table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
    }

    openNew() {
        this.isEditing = false;
        this.brand = {};
        this.submitted = false;
        this.brandDialog = true;
    }

    editBrand(brand: BrandDto) {
        this.isEditing = true;
        this.brand = { ...brand };
        this.brandDialog = true;
    }

    hideDialog() {
        this.brandDialog = false;
        this.submitted = false;
    }

    saveBrand() {
        this.submitted = true;

        if (!this.brand.name?.trim()) {
            this.messageService.add({
                severity: 'error',
                summary: 'Error',
                detail: 'Debe ingresar un nombre para la marca',
                life: 3000
            });
            return;
        }

        if (this.brand.name?.trim()) {
            if (this.brand.id) {
                const updateBrandRequest: UpdateBrandRequest = {
                    id: this.brand.id!,
                    name: this.brand.name!,
                    description: this.brand.description
                };
                this.brandsService.updateBrand(this.brand.id.toString(), updateBrandRequest)
                    .subscribe({
                        next: () => {
                            this.messageService.add({
                                severity: 'success',
                                summary: 'Successful',
                                detail: 'Marca actualizada',
                                life: 3000
                            });
                            this.loadData();
                        }
                    });
            } else {
                const createBrandRequest: CreateBrandRequest = {
                    name: this.brand.name!,
                    description: this.brand.description,
                };
                this.brandsService.createBrand(createBrandRequest).subscribe({
                    next: (response) => {
                        console.log(response);
                        this.messageService.add({
                            severity: 'success',
                            summary: 'Successful',
                            detail: 'Marca creada',
                            life: 3000
                        });
                        this.loadData();
                    }
                });

            }

            this.brandDialog = false;
            this.brand = {};
        }
    }

    deleteBrand(brand: BrandDto) {
        this.confirmationService.confirm({
            message: '¿Estás seguro de que quieres eliminar ' + brand.name + '?',
            header: 'Confirmar',
            icon: 'pi pi-exclamation-triangle',
            accept: () => {

                this.brandsService.deleteBrand(brand.id!).subscribe({
                    next: () => {
                        this.loadData();
                        this.messageService.add({
                            severity: 'success',
                            summary: 'Successful',
                            detail: 'Marca eliminada',
                            life: 3000
                        });
                    },
                    error: () => {
                        this.messageService.add({
                            severity: 'error',
                            summary: 'Error',
                            detail: 'Error al eliminar la marca',
                            life: 3000
                        });
                    },
                });
            }
        });
    }

    onPageChange(event: any) {
        this.first = event.first;
        this.rows = event.rows;
        const page = this.first / this.rows + 1;

        this.loadData(page, this.rows);
    }
}
