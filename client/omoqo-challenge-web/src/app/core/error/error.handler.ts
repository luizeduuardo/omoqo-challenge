import { HttpErrorResponse } from '@angular/common/http';
import { ErrorHandler, Injectable, Injector, NgZone } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable()

export class ErrorsHandler implements ErrorHandler {
  constructor(
    private _injector: Injector
  ) { }

  handleError(error: Error | HttpErrorResponse): void {
    const zone = this._injector.get(NgZone);

    let errorMessage = '';

    if (error instanceof HttpErrorResponse) {
      if (error.status === 400) {
        if (error.error?.errors) {
          error.error.errors.forEach((element: any) => {
            if (errorMessage) {
              errorMessage += '; ';
            }

            errorMessage += element;
          });
        } else {
          errorMessage = error.message;
        }
      }
    } else {
      errorMessage = error.message ? error.message : error.toString();
    }

    if (errorMessage) {
      const snackBar = this._injector.get(MatSnackBar);

      zone.run(async () => {
        snackBar.open(errorMessage, 'Ok', {
          horizontalPosition: 'right',
          verticalPosition: 'top'
        });
      });
    }
  }
}
