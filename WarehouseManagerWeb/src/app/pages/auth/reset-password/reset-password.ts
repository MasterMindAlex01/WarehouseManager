import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CustomFormInput } from '@app/components/input/custom-form-input';
import { AppFloatingConfigurator } from '@app/layout/component/app.floatingconfigurator';
import { ResetPasswordService } from '@app/service/auth/reset-password-service';
import { emailValidator } from '@app/validators/email-validator';
import { passwordStrengthCompleteValidator } from '@app/validators/password-validator';
import { MessageService } from 'primeng/api';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { RippleModule } from 'primeng/ripple';
import { ToastModule } from 'primeng/toast';

@Component({
    selector: 'app-reset-password',
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
    templateUrl: './reset-password.html',
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ResetPassword {
    private readonly resetPasswordService = inject(ResetPasswordService);
    private readonly router = inject(Router);
    private readonly activatedRoute = inject(ActivatedRoute);
    private readonly toastService = inject(MessageService)
    token: string = '';
    tenant: string = '';
    resetPasswordForm: FormGroup;

    public isLoading = false;

    constructor(private fb: FormBuilder) {
        this.token = this.activatedRoute.snapshot.queryParams['Token'];
        this.tenant = this.activatedRoute.snapshot.queryParams['tenant'];
        this.resetPasswordForm = this.fb.group({
            email: ['', [Validators.required, emailValidator()]],
            newPassword: [
                '',
                [Validators.required, passwordStrengthCompleteValidator()],
            ],
            confirmNewPassword: ['', [Validators.required]],
        });
        this.resetPasswordForm.get('newPassword')?.valueChanges.subscribe(() => {
            this.validatePasswordMatch();
        });
        this.resetPasswordForm.get('confirmNewPassword')?.valueChanges.subscribe(() => {
            this.validatePasswordMatch();
        });
    }

    get emailControl() {
        return this.resetPasswordForm.get('email');
    }

    get newPasswordControl() {
        return this.resetPasswordForm.get('newPassword');
    }

    get confirmNewPasswordControl() {
        return this.resetPasswordForm.get('confirmNewPassword');
    }

    private validatePasswordMatch(): void {
        const newPassword = this.resetPasswordForm.get('newPassword')?.value;
        const confirmPassword = this.resetPasswordForm.get('confirmNewPassword')?.value;
        const confirmPasswordControl = this.resetPasswordForm.get('confirmNewPassword');
        if (
            confirmPasswordControl &&
            confirmPassword &&
            newPassword !== confirmPassword
        ) {
            const currentErrors = confirmPasswordControl.errors || {};
            confirmPasswordControl.setErrors({
                ...currentErrors,
                passwordMismatch: true,
            });
        } else if (confirmPasswordControl) {
            const currentErrors = confirmPasswordControl.errors;
            if (currentErrors && currentErrors['passwordMismatch']) {
                delete currentErrors['passwordMismatch'];
                if (Object.keys(currentErrors).length === 0) {
                    confirmPasswordControl.setErrors(null);
                } else {
                    confirmPasswordControl.setErrors(currentErrors);
                }
            }
        }
    }

    onSubmit() {
        if (this.resetPasswordForm.valid && this.token && this.tenant) {
            const { newPassword, email } = this.resetPasswordForm.value;

            this.isLoading = true;
            this.resetPasswordService.resetPassword({
                tenant: this.tenant,
                email,
                password: newPassword,
                token: this.token
            }).subscribe({
                next: (response) => {
                    console.log(response);
                    this.isLoading = false;
                    this.router.navigate(['/auth/login']);

                    this.toastService.add({
                        severity: 'success',
                        summary: 'Success Message',
                        detail: 'Tu cuenta ha sido activada correctamente. Ya puedes acceder con tu correo y contraseña'
                    });
                },
                error: (err) => {
                    this.isLoading = false;
                    console.error(err);
                    this.toastService.add({
                        severity: 'warn',
                        summary: 'error',
                        detail: 'No fue posible crear nueva contraseña. Intenta de nuevo mas tarde.'
                    });
                }
            });
        } else {
            this.resetPasswordForm.markAllAsTouched();
        }
    }
}
