import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { branchesUrl, messagesUrl, userForMessagesUrl } from 'src/environments/environment';
import { Store } from '../../redux/store';
import { NgRedux } from 'ng2-redux';
import { Action } from '../../redux/action';
import { ActionType } from '../../redux/action-type';
import { Observable } from 'rxjs';
import { Contact } from '../../models/Contact';
import { User } from '../../models/User';
import { LogService } from '../log.service';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  public constructor(private http: HttpClient, private redux:NgRedux<Store>, private logger:LogService) {}
  
  
  public GetAllMessages(): void {
    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
    let observable = this.http.get<Contact[]>(messagesUrl, { headers: he });
    observable.subscribe(messages=>{
      const action: Action={type:ActionType.GetAllMessages, payload:messages};
      this.redux.dispatch(action);
      this.logger.debug("GetAllMessages: ", messages);
    }, error => {
      const action: Action={type:ActionType.GetAllMessagesError, payload:error.message};
      this.redux.dispatch(action);
      this.logger.error("GetAllMessagesError: ", error.message);
    });
  }

  public GetMessagesByUserId(userId:string): void {
    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
    let observable = this.http.get<Contact[]>(messagesUrl+userId, { headers: he });
    observable.subscribe(messages=>{
      const action: Action={type:ActionType.GetAllMessages, payload:messages};
      this.redux.dispatch(action);
      this.logger.debug("GetAllMessagesErrorByUser: ", messages);
    }, error => {
      const action: Action={type:ActionType.GetAllMessagesError, payload:error.message};
      this.redux.dispatch(action);
      this.logger.error("GetAllMessagesErrorByUserError: ", error.message);
    });
  }
  
  public GetOneMessageById(messageId: number): void {
    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
    let observable = this.http.get<Contact>(messagesUrl + messageId, { headers: he });
    observable.subscribe(message=>{
      const action: Action={type:ActionType.GetMessage, payload:message};
      this.redux.dispatch(action);
      this.logger.debug("GetOneMessageById: ", message);
    }, error => {
      const action: Action={type:ActionType.GetMessageError, payload:error.message};
      this.redux.dispatch(action);
      this.logger.error("GetOneMessageByIdError: ", error.message);
    });
  }




  public AddMessage(contact: Contact): Observable<Contact> {
    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
    return this.http.post<Contact>(messagesUrl, contact, { headers: he });
  }
  
  public UpdateMessage(contact: Contact) :Observable<Contact> {
    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
    return this.http.put<Contact>(messagesUrl+contact.messageID, contact, { headers: he });
  }

  public DeleteMessage(messageId: number) :Observable<any> {
    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
    return this.http.delete<any>(messagesUrl+messageId, { headers: he });
  }

  public DeleteMessageByUser(userId: string): Observable<any> {
    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
    return this.http.delete<any>(branchesUrl+userId, { headers: he });
  }
  
  public GetUserForMessage(): Observable<User> {
    let he = new HttpHeaders({'Content-Type':  'application/json','Authorization': 'Bearer ' + sessionStorage.getItem('access_token') });
    return this.http.get<User>(userForMessagesUrl, { headers: he });
  }
}
