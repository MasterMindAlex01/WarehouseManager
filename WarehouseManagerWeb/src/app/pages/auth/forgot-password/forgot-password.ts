import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, inject, signal } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { CustomFormInput } from '@app/components/input/custom-form-input';
import { AppFloatingConfigurator } from '@app/layout/component/app.floatingconfigurator';
import { ResetPasswordService } from '@app/service/auth/reset-password-service';
import { emailValidator } from '@app/validators/email-validator';
import { MessageService } from 'primeng/api';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { RippleModule } from 'primeng/ripple';
import { ToastModule } from 'primeng/toast';

@Component({
    selector: 'app-forgot-password',
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
    templateUrl: './forgot-password.html',
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ForgotPassword {
    private readonly resetPasswordService = inject(ResetPasswordService);
    private readonly toastService = inject(MessageService)
    private readonly router = inject(Router);
    forgotPasswordForm: FormGroup;

    public isLoading = signal(false);

    constructor(private fb: FormBuilder) {
        this.forgotPasswordForm = this.fb.group({
            tenant: ['', [Validators.required]],
            email: ['', [Validators.required, emailValidator()]],
        });
    }

    get tenantControl() {
        return this.forgotPasswordForm.get('tenant');
    }

    get emailControl() {
        return this.forgotPasswordForm.get('email');
    }


    onSubmit() {
        if (this.forgotPasswordForm.valid) {
            this.isLoading.set(true);
            console.log('Formulario enviado:', this.forgotPasswordForm.value);
            const payload = {
                tenant: this.forgotPasswordForm.value.tenant,
                email: this.forgotPasswordForm.value.email,
            };

            this.resetPasswordService.forgotPassword(payload).subscribe({
                next: () => {
                    this.toastService.add({
                        severity: 'success',
                        summary: 'success',
                        detail: 'Hemos enviado un enlace a tu correo para restablecer tu contraseña.'
                    });
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
            this.forgotPasswordForm.markAllAsTouched();
        }
    }
}
