import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, inject, signal } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CustomFormInput } from '@app/components/input/custom-form-input';
import { AppFloatingConfigurator } from '@app/layout/component/app.floatingconfigurator';
import { AuthService } from '@app/service/auth/auth-service';
import { emailValidator } from '@app/validators/email-validator';
import { MessageService } from 'primeng/api';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { RippleModule } from 'primeng/ripple';
import { ToastModule } from 'primeng/toast';

@Component({
    selector: 'app-login',
    standalone: true,
    imports: [
        ButtonModule,
        CheckboxModule,
        InputTextModule,
        PasswordModule,
        FormsModule,
        RouterModule,
        RippleModule,
        AppFloatingConfigurator,
        CustomFormInput,
        ReactiveFormsModule,
        CommonModule,
        ToastModule
    ],
    providers: [MessageService],
    templateUrl: './login.html',
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class Login {
    private readonly authService = inject(AuthService);
    private readonly toastService = inject(MessageService)
    private readonly router = inject(Router);
    loginForm: FormGroup;

    public isLoading = signal(false);

    constructor(private fb: FormBuilder, private route: ActivatedRoute) {
        this.loginForm = this.fb.group({
            email: ['', [Validators.required, emailValidator()]],
            password: ['', [Validators.required]],
            tenant: ['', [Validators.required]]
        });
    }

    get emailControl() {
        return this.loginForm.get('email');
    }

    get passwordControl() {
        return this.loginForm.get('password');
    }

    get tenantControl() {
        return this.loginForm.get('tenant');
    }

    ngOnInit(): void {
        const unauthorized = this.route.snapshot.queryParamMap.get('unauthorized');

        if (unauthorized === 'true') {
            this.toastService.add({
                severity: 'warn',
                summary: 'error',
                detail: 'Tu sesión ha finalizado, por favor inicia sesión nuevamente.'
            });
            this.router.navigate([], {
                relativeTo: this.route,
                queryParams: { unauthorized: null },
                replaceUrl: true
            });
        }
    }

    onSubmit() {
        if (this.loginForm.valid) {
            this.isLoading.set(true);
            console.log('Formulario enviado:', this.loginForm.value);
            const payload = {
                email: this.loginForm.value.email,
                password: this.loginForm.value.password,
                tenant: this.loginForm.value.tenant,
            };

            this.authService.login(payload).subscribe({
                next: (response) => {
                    console.log(response);
                    this.router.navigate(['/']);
                },
                error: ({ error }) => {
                    this.isLoading.set(false);
                    console.log(error);
                    this.toastService.add({
                        severity: 'warn',
                        summary: 'error',
                        detail: 'Tu email o contraseña no son correctos.'
                    });
                }
            });
        } else {
            this.loginForm.markAllAsTouched();
        }
    }
}
