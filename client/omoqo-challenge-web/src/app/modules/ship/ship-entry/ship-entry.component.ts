import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { finalize, Subject, takeUntil } from 'rxjs';
import { Ship } from './../ship.model';
import { ShipService } from './../ship.service';

@Component({
  selector: 'app-ship-entry',
  templateUrl: './ship-entry.component.html',
  styleUrls: ['./ship-entry.component.scss']
})
export class ShipEntryComponent implements OnInit {
  private _unsubscribeAll: Subject<any> = new Subject<any>();
  private _id?: number;
  private _ship?: Ship;

  public form?: FormGroup;

  constructor(
    private _route: ActivatedRoute,
    private _shipService: ShipService,
    private _formBuilder: FormBuilder,
    private _router: Router
  ) { }

  ngOnInit(): void {
    this._route.params
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe((params: Params) => {
        this._id = params['id'];

        if (this._id) {
          this._shipService.get(this._id).subscribe((result) => {
            if (!result) {
              this.redirectToList();
            }

            this._ship = result;
            this.initForm();
          });
        }
        else {
          this.initForm();
        }
      });
  }

  initForm(): void {
    this.form = this._formBuilder.group({
      name: ['', Validators.required],
      code: ['', Validators.required],
      length: ['', Validators.required],
      width: ['', Validators.required]
    });

    if (this._id) {
      this.form.patchValue(this._ship!);
    }
  }

  save(): void {
    if (!this.form) {
      return;
    }

    if (!this.form.valid) {
      this.form.markAllAsTouched();
      return;
    }

    const ship: Ship = this.form.value;

    this.form.disable();

    if (this._id) {
      ship.id = this._id;

      this._shipService.update(ship)
        .pipe(takeUntil(this._unsubscribeAll), finalize(() => this.form?.enable()))
        .subscribe(() => this.redirectToList());
    }
    else {
      this._shipService.add(ship)
        .pipe(takeUntil(this._unsubscribeAll), finalize(() => this.form?.enable()))
        .subscribe(() => this.redirectToList());
    }
  }

  redirectToList(): void {
    this._router.navigate(['ship']);
  }

  ngOnDestroy(): void {
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }
}
