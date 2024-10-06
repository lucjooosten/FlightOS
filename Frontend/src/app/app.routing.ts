import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from '../modules/auth/login/login.component';
import { RegisterComponent } from '../modules/auth/register/register.component';
import { ChangePasswordComponent } from '../modules/auth/change-password/change-password.component';
import { UpdateProfileComponent } from '../modules/user-management/update-profile/update-profile.component';
import { NotFoundComponent } from '../common/pages/not-found/not-found.component';
import { HomeComponent } from '../common/pages/home/home.component';

export const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'not-found', component: NotFoundComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'change-password', component: ChangePasswordComponent },
  { path: 'update-profile', component: UpdateProfileComponent },
  { path: '', redirectTo: '/login', pathMatch: 'full' }, // Redirect to login by default
  { path: '**', redirectTo: '/not-found' } // Redirect for any unknown paths
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
