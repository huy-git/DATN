import { BaseComponent } from 'src/app/lib/base.component';
import { Component, OnInit,Injector } from '@angular/core';
import { Observable} from 'rxjs';
import 'rxjs/add/observable/combineLatest';
import 'rxjs/add/operator/takeUntil';

@Component({
  selector: 'app-vanban',
  templateUrl: './vanban.component.html',
  styleUrls: ['./vanban.component.css']
})
export class VanBanComponent extends BaseComponent implements OnInit {
  list_lop:any;
  constructor(injector: Injector) { 
    super(injector);
  }

  ngOnInit(): void {
    Observable.combineLatest(
      this._api.get('api/tintuc/get-loai/2'),
    ).takeUntil(this.unsubscribe).subscribe(res => {
      this.list_lop = res[0];
      setTimeout(() => {
        this.loadScripts();
      });
    }, err => { });
    console.log(this.list_lop);
  }

  catText(text: string, limit: number): string {
    if(text.length > limit) {
      return text.substr(0, limit) + "...";
    }
    return text;
  }
}