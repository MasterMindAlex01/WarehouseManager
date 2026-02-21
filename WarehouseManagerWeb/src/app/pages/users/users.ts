import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, OnInit, signal, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CustomFormInput } from '@app/components/input/custom-form-input';
import { CreateUserRequest, UpdateUserRequest, UserDataDto } from '@app/interfaces/user';
import { UserDataClient } from '@app/service/users/user-data-client';
import { emailValidator } from '@app/validators/email-validator';
import { environment } from '@environments/environment';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DialogModule } from 'primeng/dialog';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { RadioButtonModule } from 'primeng/radiobutton';
import { RatingModule } from 'primeng/rating';
import { RippleModule } from 'primeng/ripple';
import { SelectModule } from 'primeng/select';
import { Table, TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';
import { TextareaModule } from 'primeng/textarea';
import { ToastModule } from 'primeng/toast';
import { ToolbarModule } from 'primeng/toolbar';

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
    selector: 'app-users',
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
        CheckboxModule,
        TextareaModule,
        SelectModule,
        RadioButtonModule,
        InputNumberModule,
        DialogModule,
        TagModule,
        InputIconModule,
        IconFieldModule,
        ConfirmDialogModule,
        CustomFormInput,
        ReactiveFormsModule,
    ],
    templateUrl: './users.html',
    providers: [MessageService, ConfirmationService],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class Users implements OnInit {
    private readonly defaultPassword = environment.defaultPassword;

    userDialog: boolean = false;

    users = signal<UserDataDto[]>([]);

    user!: UserDataDto;

    selectedUsers!: UserDataDto[] | null;

    submitted: boolean = false;

    @ViewChild('dt') dt!: Table;

    exportColumns!: ExportColumn[];

    cols!: Column[];

    userForm: FormGroup;

    constructor(
        private fb: FormBuilder,
        private userDataClient: UserDataClient,
        private messageService: MessageService,
        private confirmationService: ConfirmationService
    ) {
        this.userForm = this.fb.group({
            firstName: ['', [Validators.required]],
            lastName: ['', [Validators.required]],
            userName: ['', [Validators.required]],
            email: ['', [Validators.required, emailValidator()]],
            phoneNumber: ['']
        });
    }

    get firstNameControl() {
        return this.userForm.get('firstName');
    }

    get lastNameControl() {
        return this.userForm.get('lastName');
    }

    get userNameControl() {
        return this.userForm.get('userName');
    }

    get emailControl() {
        return this.userForm.get('email');
    }

    get phoneNumberControl() {
        return this.userForm.get('phoneNumber');
    }

    ngOnInit() {
        this.loadData();
    }

    loadData() {
        this.userDataClient.getUserList().subscribe({
            next: (result) => {
                this.users.set(result.data!)
            },
            error: (err) => {
                console.error(err)
            },
        });

        this.cols = [
            { field: 'id', header: 'id', customExportHeader: 'user Code' },
            { field: 'imageUrl', header: 'imageUrl' },
            { field: 'userName', header: 'userName' },
            { field: 'firstName', header: 'firstName' },
            { field: 'lastName', header: 'lastName' },
            { field: 'email', header: 'email' },
            { field: 'isActive', header: 'isActive' },
            { field: 'emailConfirmed', header: 'emailConfirmed' },
            { field: 'phoneNumber', header: 'phoneNumber' }
        ];

        this.exportColumns = this.cols.map((col) => ({ title: col.header, dataKey: col.field }));
    }

    onGlobalFilter(table: Table, event: Event) {
        table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
    }

    openNew() {
        this.user = {};
        this.userForm.patchValue({
            firstName: '',
            lastName: '',
            userName: '',
            email: '',
            phoneNumber: ''
        });
        this.submitted = false;
        this.userDialog = true;
    }

    editUser(user: UserDataDto) {
        this.user = { ...user };
        this.userForm.patchValue({
            firstName: user.firstName,
            lastName: user.lastName,
            userName: user.userName,
            email: user.email,
            phoneNumber: user.phoneNumber
        });
        this.userDialog = true;
    }

    hideDialog() {
        this.userDialog = false;
        this.submitted = false;
    }

    saveUser() {
        if (this.userForm.invalid) {
            this.userForm.markAllAsTouched();
            return;
        }

        this.submitted = true;
        if (this.user.id) {

            const request: UpdateUserRequest = {
                id: this.user.id,
                ...this.userForm.value,
                deleteCurrentImage: true
            };

            this.userDataClient.updateUser(request).subscribe({
                next: (result) => {
                    console.log(result);
                    this.messageService.add({
                        severity: 'success',
                        summary: 'Successful',
                        detail: 'Product Updated',
                        life: 3000
                    });
                    this.loadData();
                }
            });

        } else {

            const request: CreateUserRequest = {
                ...this.userForm.value,
                password: this.defaultPassword,
                confirmPassword: this.defaultPassword
            };

            this.userDataClient.createUser(request).subscribe({
                next: (result) => {
                    console.log(result);
                    this.messageService.add({
                        severity: 'success',
                        summary: 'Successful',
                        detail: 'User Created',
                        life: 3000
                    });
                    this.loadData();
                }
            });
        }

        this.userDialog = false;
        this.user = {};
    }
}
