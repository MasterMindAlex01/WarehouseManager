import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { AbstractControl, FormControl, ReactiveFormsModule } from '@angular/forms';
import { FormErrorPipe } from '@app/pipes/form-error.pipe';
import { InputTextModule } from 'primeng/inputtext';
import { Password, PasswordModule } from "primeng/password";

@Component({
    selector: 'custom-form-input',
    standalone: true,
    imports: [
        InputTextModule,
        PasswordModule,
        ReactiveFormsModule,
        CommonModule,
        FormErrorPipe,
        Password
    ],
    templateUrl: './custom-form-input.html',
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CustomFormInput {

    @Input() control: AbstractControl = new FormControl();
    @Input() nameControl: string = '';
    @Input() label: string = '';
    @Input() assistiveText: string = '';
    @Input() type: string = 'text';
    @Input() placeholder: string = 'Ingresar';
    @Input() isReadOnly: boolean = false;
    @Input() styleClass: string = 'mb-4';

    isPasswordVisible = false;

    get formControl(): FormControl {
        return this.control as FormControl;
    }

    get isInvalid(): boolean {
        return this.control.invalid && (this.control.dirty || this.control.touched);
    }

    get isDisabled(): boolean {
        return this.formControl.disabled;
    }

    get actualType(): string {
        if (this.type === 'password') {
            return this.isPasswordVisible ? 'text' : 'password';
        }
        return this.type;
    }

    togglePasswordVisibility(): void {
        if (this.type === 'password') {
            this.isPasswordVisible = !this.isPasswordVisible;
        }
    }

    getClasses() {
        return {
            [this.styleClass]: !this.isInvalid,
            '': this.isInvalid
        };
    }

    getClassesInvalid() {
        return {
            [this.styleClass]: this.isInvalid,
            '': !this.isInvalid
        };
    }
}
