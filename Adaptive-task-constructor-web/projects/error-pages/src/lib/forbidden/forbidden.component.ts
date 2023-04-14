import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { take } from 'rxjs';

@Component({
  selector: 'lib-forbidden',
  templateUrl: './forbidden.component.html',
  styleUrls: ['./forbidden.component.css']
})
export class ForbiddenComponent implements OnInit {
  returnUrl: string;
  
  constructor(private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.returnUrl =  this.route
      .snapshot
      .queryParams['returnUrl'];
  }

  goBack(){
    const extras =  {queryParams: { returnUrl: this.returnUrl } };
    this.router.navigate(['/login'], extras);
  }

  home(){
    this.router.navigate(['/home']);
  }
}
