import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthModule } from '../features/auth/auth.module';
import { CustomerDashboardModule } from '../features/customer-dashboard/customer-dashboard.module';
import { AdminDashboardModule } from '../features/admin-dashboard/admin-dashboard.module';

@NgModule({
    declarations: [],
    imports: [
        CommonModule,
        AuthModule,
        CustomerDashboardModule,
        AdminDashboardModule
    ]
})
export class FeaturesModule { }