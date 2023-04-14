import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, RequiredValidator, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterStateSnapshot } from '@angular/router';
import { ValidationMessages } from 'projects/shared/src/lib/enum/validation-messages.enum';
import { take } from 'rxjs';
import { AuthenticationService } from '../../services/authentication.service';
import { RoleRedirectionService } from '../../services/role-redirection.service';
//import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'lib-log-in-form',
  templateUrl: './log-in-form.component.html',
  styleUrls: ['./log-in-form.component.scss']
})
export class LogInFormComponent implements OnInit {

  loginForm: FormGroup;
  validationMessages = ValidationMessages;
  returnUrl = '/';
  constructor(private formBuilder: FormBuilder,
    private registrationService: AuthenticationService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private redirectionService: RoleRedirectionService) { }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      email: ['', Validators.required],
      password: ['', Validators.required]
    });

    this.activatedRoute.queryParamMap.pipe(take(1)).subscribe(res => {
      this.returnUrl = res.get("returnUrl") ?? '/';
    });
  }

  onSubmit() {
    this.registrationService
      .logIn(this.loginForm.value)
      .subscribe(res => {
        //this.router.navigate([this.returnUrl]);
        this.redirectionService.redirect();
      });
  }
}
