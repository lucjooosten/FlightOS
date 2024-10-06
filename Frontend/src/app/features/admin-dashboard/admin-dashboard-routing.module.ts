import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'airlines',
    loadChildren: () =>
      import('./airlines/airlines.module').then((m) => m.AirlinesModule),
  },
  {
    path: 'airports',
    loadChildren: () =>
      import('./airports/airports.module').then((m) => m.AirportsModule),
  },
  {
    path: 'flights',
    loadChildren: () =>
      import('./flights/flights.module').then((m) => m.FlightsModule),
  },
  {
    path: 'reservations',
    loadChildren: () =>
      import('./reservations/reservations.module').then(
        (m) => m.ReservationsModule
      ),
  },
  { 
    path: 'users', 
    loadChildren: () => import('./users/users.module').then(m => m.UsersModule) 
  },
  { path: '', redirectTo: 'flights', pathMatch: 'full' }, // Default to flights
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AdminDashboardRoutingModule {}
