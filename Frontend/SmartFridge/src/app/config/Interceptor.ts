import { Injectable } from '@angular/core';
import { AuthenticationService } from '../services/authentication/authentication.service'
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable()
export class Interceptor implements HttpInterceptor {


constructor() { }
  intercept(req: HttpRequest<any>, next: HttpHandler):
    Observable<HttpEvent<any>> 
    {
        const reqWithToken = req.clone({setHeaders: {Authorization: `Bearer ${localStorage.getItem("token")}`}})
        return next.handle(reqWithToken);
    }
}