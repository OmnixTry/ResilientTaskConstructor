import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'projects/auth/src/lib/services/authentication.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(private registrationService: AuthenticationService) { }

  ngOnInit(): void {
  }

  dummyStudent(){
    this.registrationService.dummyStusent().subscribe(res => console.log(res));
  }

  dummyTeacher(){
    this.registrationService.dummyTeacher().subscribe(res => console.log(res));
  }

  // teacher@mail.com
  // Teacher0@mail.com

  // teacher1@mail.com
  // Teacher0@mail.com

  // student@student.com
  // Student0@student.com
}
