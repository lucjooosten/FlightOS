import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'flights',
    loadChildren: () =>
      import('./flights/flights.module').then((m) => m.FlightsModule),
  },
  {
    path: 'bookings',
    loadChildren: () =>
      import('./bookings/bookings.module').then((m) => m.BookingsModule),
  },
  { path: '', redirectTo: 'flights', pathMatch: 'full' }, // Default to flights
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CustomerDashboardRoutingModule {}
