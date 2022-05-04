import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {FileUploadModule} from 'primeng/fileupload';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {InputTextModule} from 'primeng/inputtext';
import {TableModule} from 'primeng/table';
import {PanelModule} from 'primeng/panel';
import {CalendarModule} from 'primeng/calendar';
const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./main/main.module').then((m) => m.MainModule),
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes),
    FileUploadModule,
    NgbModule,
    PanelModule,
    TableModule,
    InputTextModule,
    CalendarModule,],
  exports: [RouterModule,
    ReactiveFormsModule,
    FormsModule,
    FileUploadModule,
    NgbModule,
    PanelModule,
    TableModule,
    InputTextModule,
    CalendarModule,]
})
export class AppRoutingModule { }
