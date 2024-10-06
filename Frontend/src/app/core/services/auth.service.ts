import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private apiUrl = 'http://localhost:6600/api/v1';
    private currentUserSubject: BehaviorSubject<any>;
    public currentUser: Observable<any>;

    constructor(private http: HttpClient) {
        const storedUser = localStorage.getItem('currentUser');
        this.currentUserSubject = new BehaviorSubject<any>(storedUser ? JSON.parse(storedUser) : null);
        this.currentUser = this.currentUserSubject.asObservable();
    }

    public get currentUserValue(): any {
        return this.currentUserSubject.value;
    }

    login(username: string, password: string): Observable<any> {
        return this.http.post<any>(`${this.apiUrl}/auth/login`, { username, password })
            .pipe(map(user => {
                if (user && user.token) {
                    localStorage.setItem('currentUser', JSON.stringify(user));
                    this.currentUserSubject.next(user);
                }
                return user;
            }));
    }

    register(username: string, password: string): Observable<any> {
        return this.http.post<any>(`${this.apiUrl}/auth/register`, { username, password });
    }

    logout(): void {
        localStorage.removeItem('currentUser');
        this.currentUserSubject.next(null);
    }

    getUser(): any {
        return this.currentUserValue;
    }

    changePassword(oldPassword: string, newPassword: string): Observable<any> {
        return this.http.post<any>(`${this.apiUrl}/auth/change-password`, { oldPassword, newPassword });
    }

    isAuthenticated(): boolean {
        return !!this.currentUserValue;
    }

    getRole(): string {
        return this.currentUserValue ? this.currentUserValue.role : null;
    }
}