import { Injectable } from '@angular/core';
import { AuthenticationService } from '../Services/authentication.service'
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable()
export class Interceptor implements HttpInterceptor {


constructor() { }
  intercept(req: HttpRequest<any>, next: HttpHandler):
    Observable<HttpEvent<any>> 
    {
        //I get the token directly from local storage is this okay? Or do I need to make a get token function in my authentication service?
        const reqWithToken = req.clone({setHeaders: {Authorization: `Bearer ${localStorage.getItem("token")}`}})
        return next.handle(reqWithToken);
    }
}