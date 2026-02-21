import { ChangeDetectorRef, Component, inject, OnInit } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { UserDataDto } from "@app/interfaces/user";
import { PersonalService } from "@app/service/personal/personal.service";
import { ButtonModule } from "primeng/button";
import { FluidModule } from "primeng/fluid";
import { InputTextModule } from "primeng/inputtext";
import { SelectModule } from "primeng/select";
import { TextareaModule } from "primeng/textarea";
import { FileUploadModule } from 'primeng/fileupload';
import { FileUploadRequest } from "@app/interfaces/file-upload-request";
import { MessageService } from "primeng/api";
import { ToastModule } from "primeng/toast";
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { environment } from "@environments/environment";

@Component({
    selector: 'app-profile',
    standalone: true,
    imports: [
        InputTextModule,
        FluidModule,
        ButtonModule,
        SelectModule,
        FormsModule,
        TextareaModule,
        FileUploadModule,
        ToastModule,
        ProgressSpinnerModule
    ],
    providers: [PersonalService, MessageService],
    templateUrl: './profile.html'
})
export class Profile implements OnInit {
    urlBase = environment.urlBase;

    fileRequest: FileUploadRequest | null = null;
    user: UserDataDto | null = null;

    personalService = inject(PersonalService);
    messageService = inject(MessageService);
    private cdr: ChangeDetectorRef = inject(ChangeDetectorRef);

    ngOnInit() {
        this.loadData();
    }

    loadData() {
        this.personalService.getProfile().subscribe({
            next: (result) => {
                this.user = { ...result.data! };
                this.cdr.detectChanges();
            },
            error: (err) => {
                console.error(err);
                this.cdr.detectChanges();
            },
        });
    }

    onSelect(event: any) {
        const file: File = event.files[0];
        this.fileRequest = {
            name: `profile_${this.user!.id}_${Date.now()}`,
            extension: '.' + file.name.split('.').pop()!,
            data: '',
        } as FileUploadRequest;

        this.getBase64(file).then((base64) => {
            this.fileRequest!.data = base64;
            console.log('Uploading file:', this.fileRequest);
        });

        console.log('Uploading file:', this.fileRequest);
    }

    getBase64(file: File): Promise<string> {
        return new Promise((resolve, reject) => {
            const reader = new FileReader();

            reader.onload = () => {
                // reader.result incluye: "data:tipo;base64,AAAA..."
                const base64 = reader.result as string;
                resolve(base64);
            };

            reader.onerror = error => reject(error);

            reader.readAsDataURL(file);
        });
    }

    saveProfile() {

        if (!this.fileRequest || !this.fileRequest.data) {
            console.log('No file selected, skipping image upload');
            this.messageService.add({ severity: 'warn', summary: 'No image selected', detail: 'Please select an image to upload.' });
            return;
        }

        this.personalService.updateProfile({
            id: this.user!.id!,
            firstName: this.user!.firstName!,
            lastName: this.user!.lastName!,
            email: this.user!.email!,
            phoneNumber: this.user!.phoneNumber!,
            image: this.fileRequest ? {
                name: this.fileRequest.name,
                extension: this.fileRequest.extension,
                data: this.fileRequest.data,
            } : undefined,
            deleteCurrentImage: this.user!.imageUrl ? true : false,
        }).subscribe({
            next: (result) => {
                this.fileRequest = null;
                this.loadData();
                this.messageService.add({ severity: 'success', summary: 'Profile Updated', detail: 'Your profile has been updated successfully.' });
            },
            error: (err) => {
                this.fileRequest = null;
                this.messageService.add({ severity: 'error', summary: 'Update Failed', detail: 'There was an error updating your profile. Please try again.' });
            },
        });
    }
}
