import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  constructor() { }
  declare title: string;
  declare description: string;
  ngOnInit(): void {
    this.title = 'Home Page';
    this.description = 'Welcome to the home page!';
  }
}
