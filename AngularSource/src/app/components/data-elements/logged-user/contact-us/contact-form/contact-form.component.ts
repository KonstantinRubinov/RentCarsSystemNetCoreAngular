import { Component, OnInit } from '@angular/core';
import { Contact } from 'src/app/models/Contact';
import { MessageService } from 'src/app/services/api-connections/message.service';
import { User } from 'src/app/models/User';
import { LogService } from 'src/app/services/log.service';

@Component({
  selector: 'app-contact-form',
  templateUrl: './contact-form.component.html',
  styleUrls: ['./contact-form.component.css']
})
export class ContactFormComponent implements OnInit {
  public contact:Contact=new Contact();
  public user:User=new User();

  constructor(private messageService: MessageService, private logger:LogService) { }

  ngOnInit() {
    const observable = this.messageService.GetUserForMessage();
    observable.subscribe(user => {
      this.logger.debug("UserForContact: ", user);
      this.user=user;
      this.contact.userFirstName=user.userFirstName;
      this.contact.userLastName=user.userLastName;
      this.contact.userEmail=user.userEmail;
    }, error => {
      this.logger.error("UserForContactError: ", error.message);
    });
  }

  public ContactUs(){
    const observable = this.messageService.AddMessage(this.contact);
    observable.subscribe(message => {
      this.logger.debug("ContactUs: ", message);
      this.contact=message;
      this.contact.userFirstName="";
      this.contact.userLastName="";
      this.contact.userEmail="";
      this.contact.userMessage="";
    }, error => {
      this.logger.error("ContactUsError: ", error.message);
    });
  }
}