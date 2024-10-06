import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { FooterComponent } from '../common/components/footer/footer.component';
import { NavbarComponent } from '../common/components/navbar/navbar.component';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  //styleUrls: ['./app.component.css'],
  imports: [
    RouterOutlet,
    NavbarComponent, 
    FooterComponent,
  ],
  template: `
    <app-navbar></app-navbar>
    <router-outlet></router-outlet>
    <app-footer></app-footer>
  `,
})
export class AppComponent {
  title = 'FlightOS';
}
