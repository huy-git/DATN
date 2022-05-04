import { BaseComponent } from 'src/app/lib/base.component';
import { Component, OnInit, Injector } from '@angular/core';

@Component({
  selector: 'app-cttintuc',
  templateUrl: './cttintuc.component.html',
  styleUrls: ['./cttintuc.component.css']
})
export class CttintucComponent extends BaseComponent implements OnInit {
  item:any;
  constructor(injector: Injector) { 
    super(injector);
  }

  ngOnInit(): void {
    this.item = {};
    this._route.params.subscribe(params => {
      let id = params['id'];
      this._api.get('api/tintuc/get-by-id/'+id).takeUntil(this.unsubscribe).subscribe(res => {
        this.item = res;
        setTimeout(() => {
          this.loadScripts();
        });
       
      }); 
    });
  }
  catText(text: string, limit: number): string {
    if(text.length > limit) {
      return text.substr(0, limit) + "...";
    }
    return text;
  }

}
