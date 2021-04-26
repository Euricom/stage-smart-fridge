import { Directive, Input } from "@angular/core";
import { AbstractControl, NG_VALIDATORS, ValidationErrors, Validator } from "@angular/forms";


@Directive({
    selector: '[appConfirmEqualValidator]',
    providers: [{
        provide: NG_VALIDATORS,
        useExisting: ConfirmEqualValidator,
        multi: true
    }]
})

export class ConfirmEqualValidator implements Validator
{
    
    //{[key:string]: any}
    //ValidationErrors
    @Input() appConfirmEqualValidator: string = "";
    validate(control: AbstractControl): {[key:string]: any} | null {
        const controlToCompare = control.parent?.get(this.appConfirmEqualValidator);
        // console.log(this.appConfirmEqualValidator);
        // console.log( control.parent?.get(this.appConfirmEqualValidator));
        // console.log("pas", control.parent?.get("password")?.value);
        // console.log("conrtol", control.value);
        // console.log("contocomp", controlToCompare?.value);
        if(controlToCompare?.value != control.value )
        {
            return {'notEqual': true}
        }
        return null;
    }
    

}