import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Store } from '../../redux/store';
import { NgRedux } from 'ng2-redux';
import { Observable } from 'rxjs';
import { User } from '../../models/User';
import { usersUrl } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  
  public constructor(private http: HttpClient, private redux:NgRedux<Store>) {}
  
  public addUser(user: User): Observable<User> {
    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
    return this.http.post<User>(usersUrl, user, { headers: he });
  }
  
  public updateUser(user: User): Observable<User> {
    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
    return this.http.put<User>(usersUrl+user.userID, user, { headers: he });
  }

  public deleteUser(id: string): Observable<any> {
    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
    return this.http.delete<any>(usersUrl+id, { headers: he });
  }

  public getAllUsers():  Observable<User[]> {
    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
    return this.http.get<User[]>(usersUrl, { headers: he });
  }
}
