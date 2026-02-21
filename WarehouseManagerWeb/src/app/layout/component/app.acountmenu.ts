import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { AppMenuitem } from './app.menuitem';
import { AuthService } from '@app/service/auth/auth-service';

@Component({
    selector: 'app-acount-menu',
    standalone: true,
    imports: [CommonModule, AppMenuitem, RouterModule],
    template: `
        <div class="flex flex-col gap-4">
            <div>
                <ul class="layout-menu">
                    <ng-container *ngFor="let item of model; let i = index">
                        <li app-menuitem *ngIf="!item.separator"
                        [item]="item" [index]="i" [root]="true"></li>
                        <li *ngIf="item.separator" class="menu-separator"></li>
                    </ng-container>
                </ul>
            </div>
        </div>
    `,
    host: {
        class: 'hidden absolute top-13 right-0 w-72 p-4 bg-surface-0 dark:bg-surface-900 border border-surface rounded-border origin-top shadow-[0px_3px_5px_rgba(0,0,0,0.02),0px_0px_2px_rgba(0,0,0,0.05),0px_1px_4px_rgba(0,0,0,0.08)]'
    },
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AppAcountMenu {
    readonly authService = inject(AuthService);
    model: MenuItem[] = [];

    ngOnInit() {
        this.model = [
            {
                label: 'Acount',
                icon: 'pi pi-fw pi-user',
                items: [
                    {
                        label: 'Profile',
                        icon: 'pi pi-fw pi-user',
                        routerLink: ['/']
                    },
                    {
                        label: 'Quit',
                        icon: 'pi pi-fw pi-sign-out',
                        command: () => {
                            this.authService.logout();
                        },
                    }
                ]
            }
        ];
    }
}
