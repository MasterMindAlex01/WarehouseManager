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
import { ProductsService } from '../../service/products/products.service';
import { ProductDto, UpdateProductRequest } from '@app/interfaces/product';
import { CreateProductRequest } from '../../interfaces/product';
import { Paginator, PaginatorModule } from "primeng/paginator";
import { BrandDto } from '@app/interfaces/brand';
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
    templateUrl: './products.html',
    providers: [MessageService, ProductsService,BrandsService, ConfirmationService]
})
export class Products implements OnInit {
    productDialog: boolean = false;

    products = signal<ProductDto[]>([]);
    brands: BrandDto[] = [];
    selectBrand: BrandDto | null = null;

    product!: ProductDto;

    selectedProducts!: ProductDto[] | null;
    submitted: boolean = false;

    @ViewChild('dt') dt!: Table;

    exportColumns!: ExportColumn[];

    cols!: Column[];

    isEditing:boolean = false
    first: number = 0;
    rows: number = 10;
    totalRecords: number = 0;

    productsService = inject(ProductsService);
    brandsService = inject(BrandsService);
    messageService = inject(MessageService);
    confirmationService = inject(ConfirmationService);

    ngOnInit() {
        this.loadData();
    }

    loadData(page: number = 1, limit: number = 10) {

        this.brandsService.getBrands({
            pageNumber: 1,
            pageSize: 100
        }).subscribe((data) => {
            this.brands = data.data;
        });

        this.productsService.getProducts({
            pageNumber: page,
            pageSize: limit
        }).subscribe((data) => {
            this.totalRecords = data.totalCount;
            this.products.set(data.data);
        });

        this.cols = [
            { field: 'image', header: 'Image' },
            { field: 'name', header: 'Name' },
            { field: 'description', header: 'Description' },
            { field: 'rate', header: 'Rate' },
            { field: 'brandName', header: 'BrandName' },
        ];

        this.exportColumns = this.cols.map((col) => ({ title: col.header, dataKey: col.field }));
    }

    onGlobalFilter(table: Table, event: Event) {
        table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
    }

    openNew() {
        this.isEditing = false;
        this.product = {};
        this.selectBrand = null;
        this.submitted = false;
        this.productDialog = true;
    }

    editProduct(product: ProductDto) {
        this.isEditing = true;
        this.product = { ...product };
        this.selectBrand = this.brands.find(brand => brand.id === product.brandId) || null;
        this.productDialog = true;
    }

    hideDialog() {
        this.productDialog = false;
        this.submitted = false;
    }

    saveProduct() {
        this.submitted = true;

        if (!this.selectBrand) {
            this.messageService.add({
                severity: 'error',
                summary: 'Error',
                detail: 'Debe seleccionar una marca',
                life: 3000
            });
            return;
        }

        if (this.product.name?.trim()) {
            this.product.brandId = this.selectBrand.id;
            if (this.product.id) {
                const updateProductRequest: UpdateProductRequest = {
                    id: this.product.id!,
                    name: this.product.name!,
                    description: this.product.description,
                    rate: this.product.rate!,
                    brandId: this.product.brandId!,
                    deleteCurrentImage: false
                };
                this.productsService.updateProduct(this.product.id.toString(), updateProductRequest)
                .subscribe({
                    next: () => {
                        this.messageService.add({
                            severity: 'success',
                            summary: 'Successful',
                            detail: 'Producto actualizado',
                            life: 3000
                        });
                        this.loadData();
                    }
                });
            } else {
                const createProductRequest: CreateProductRequest = {
                    name: this.product.name!,
                    description: this.product.description,
                    rate: this.product.rate!,
                    brandId: this.product.brandId!,
                };
                this.productsService.createProduct(createProductRequest).subscribe({
                    next: (response) => {
                        console.log(response);
                        this.messageService.add({
                            severity: 'success',
                            summary: 'Successful',
                            detail: 'Producto creado',
                            life: 3000
                        });
                        this.loadData();
                    }
                });

            }

            this.productDialog = false;
            this.product = {};
        }
    }

    deleteProduct(product: ProductDto) {
        this.confirmationService.confirm({
            message: '¿Estás seguro de que quieres eliminar ' + product.name + '?',
            header: 'Confirmar',
            icon: 'pi pi-exclamation-triangle',
            accept: () => {
                this.products.set(this.products().filter((val) => val.id !== product.id));
                this.product = {};

                this.productsService.deleteProduct(product.id!.toString()).subscribe({
                    next: () => {
                        this.messageService.add({
                            severity: 'success',
                            summary: 'Successful',
                            detail: 'Producto eliminado',
                            life: 3000
                        });
                    },
                    error: () => {                        this.messageService.add({
                            severity: 'error',
                            summary: 'Error',
                            detail: 'Error al eliminar el producto',
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
