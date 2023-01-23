import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { PageEvent } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { ShipList } from '../ship.model';
import { ShipService } from '../ship.service';

@Component({
  selector: 'app-ship-list',
  templateUrl: './ship-list.component.html',
  styleUrls: ['./ship-list.component.scss']
})
export class ShipListComponent implements OnInit, OnDestroy {
  private _unsubscribeAll: Subject<any> = new Subject<any>();

  public currentPage = 0;
  public pageSize = 10;
  public total = 0;

  public rows: ShipList[] = [];
  public columns: string[] = ['edit', 'remove', 'name', 'code', 'length', 'width'];

  public searchForm: FormGroup;

  constructor(
    private _formBuilder: FormBuilder,
    private _shipService: ShipService,
    private _router: Router
  ) {
    this.searchForm = this._formBuilder.group({
      name: [''],
      code: ['']
    });
  }

  ngOnInit(): void {
    this.setPage();
  }

  setPage(pageNumber: number = 0): void {
    this.currentPage = pageNumber;

    const formValue = this.searchForm?.value;

    this._shipService.list(formValue.name, formValue.code, this.currentPage * this.pageSize, this.pageSize)
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe((result) => {
        this.rows = result?.data ?? [];
        this.total = result?.total ?? 0;
      });
  }

  add(): void {
    this._router.navigate(['ship/', '']);
  }

  edit(id: number): void {
    if (id) {
      this._router.navigate(['ship/', id]);
    }
  }

  remove(id: number): void {
    if (id) {
      this._shipService.remove(id)
        .pipe(takeUntil(this._unsubscribeAll))
        .subscribe(() => {
          this.setPage(this.currentPage);
        });
    }
  }

  onPaginateChange(event: PageEvent): void {
    this.pageSize = event.pageSize;
    this.setPage(event.pageIndex);
  }

  ngOnDestroy(): void {
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }
}
