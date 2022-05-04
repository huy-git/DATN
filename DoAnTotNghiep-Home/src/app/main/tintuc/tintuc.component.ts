import { BaseComponent } from 'src/app/lib/base.component';
import { Component, OnInit ,Injector } from '@angular/core';
import { Observable} from 'rxjs';
import 'rxjs/add/observable/combineLatest';
import 'rxjs/add/operator/takeUntil';
import { DatePipe } from '@angular/common';
import { Router } from '@angular/router';
import { NewfetComponent } from '../layout/newfet/newfet.component';

@Component({
  selector: 'app-tintuc',
  templateUrl: './tintuc.component.html',
  styleUrls: ['./tintuc.component.css']
})
export class TintucComponent extends BaseComponent implements OnInit {
  public tintucs:any;
  public totalRecords: any;
  public pageSize = 4;
  public page = 1;
  public maloai :any;

  constructor(injector: Injector,private router: Router) { 
    super(injector);
  }

  ngOnInit(): void {
    this._route.params.subscribe(params => {
      this.maloai = params['id'];
    });
    this.router.routeReuseStrategy.shouldReuseRoute=()=> {
      this.search();
      return false;
    }
    this.search();
  }
  loadPage(page) { 
    this._api.post('api/tintuc/search',{page: page, pageSize: this.pageSize, maloai: this.maloai, trangthai: "duyệt"}).takeUntil(this.unsubscribe).subscribe(res => {
      this.tintucs = res.data;
      this.totalRecords =  res.totalItems;
      this.pageSize = res.pageSize;
      });
  } 
  search() { 
    this.page = 1;
    this.pageSize = 4;
    this._api.post('api/tintuc/search',{page: this.page, pageSize: this.pageSize, maloai: this.maloai, trangthai: "duyệt"}).takeUntil(this.unsubscribe).subscribe(res => {
      this.tintucs = res.data;
      this.totalRecords =  res.totalItems;
      this.pageSize = res.pageSize;
      });
  }
  catText(text: string, limit: number): string {
    if(text.length > limit) {
      return text.substr(0, limit) + "...";
    }
    return text;
  }
  
}
