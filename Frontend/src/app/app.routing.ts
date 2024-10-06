import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './core/guards/auth.guard';

import { HomeComponent } from './shared/pages/home/home.component';
import { SupportComponent } from './shared/pages/support/support.component';
import { NotFoundComponent } from './shared/pages/not-found/not-found.component';

export const routes: Routes = [
  { 
    path: 'auth', 
    loadChildren: () => import('./features/auth/auth.module').then(m => m.AuthModule) 
  },
  { 
    path: 'customer-dashboard', 
    canActivate: [AuthGuard], // Protect the customer dashboard route
    loadChildren: () => import('./features/customer-dashboard/customer-dashboard.module').then(m => m.CustomerDashboardModule) 
  },
  { 
    path: 'admin-dashboard',
    canActivate: [AuthGuard], // Protect the admin dashboard route
    loadChildren: () => import('./features/admin-dashboard/admin-dashboard.module').then(m => m.AdminDashboardModule) 
  },
  {
    path: 'home',
    component: HomeComponent
  },
  {
    path: 'support',
    component: SupportComponent
  },
  {
    path: '',
    redirectTo: '/home',
    pathMatch: 'full'
  },
  {
    path: '**',
    component: NotFoundComponent  // Wildcard route for a 404 page
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }