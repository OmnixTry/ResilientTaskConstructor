import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from 'projects/auth/src/lib/services/authentication.service';

@Component({
  selector: 'app-top-panel',
  templateUrl: './top-panel.component.html',
  styleUrls: ['./top-panel.component.scss']
})
export class TopPanelComponent implements OnInit {

  constructor(public authService: AuthenticationService, private router: Router) { }

  ngOnInit(): void {
  }

  onLogOut(){
    this.authService.logOut();
    this.router.navigate(['/login']);
  }

}
