import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'lib-forbidden-page',
  templateUrl: './forbidden-page.component.html',
  styleUrls: ['./forbidden-page.component.css']
})
export class ForbiddenPageComponent implements OnInit {

  private _returnUrl: string;
  constructor( private _router: Router, private _route: ActivatedRoute) { }

  ngOnInit(): void {
    this._returnUrl = this._route.snapshot.queryParams['returnUrl'] || '/';
  }

  public navigateToLogin = () => {
    this._router.navigate(['/login'], { queryParams: { returnUrl: this._returnUrl }});
  }

}
