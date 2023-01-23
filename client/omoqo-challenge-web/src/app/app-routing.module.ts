import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DefaultLayoutComponent } from './layout/default-layout/default-layout.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'ship' },

  {
    path: '',
    component: DefaultLayoutComponent,
    data: {
      layout: 'empty'
    },
    children: [
      { path: 'ship', loadChildren: () => import('./modules/ship/ship-list/ship-list.module').then(m => m.ShipListModule) },
      { path: 'ship/:id', loadChildren: () => import('./modules/ship/ship-entry/ship-entry.module').then(m => m.ShipEntryModule) }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
