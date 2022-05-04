import { BaseComponent } from 'src/app/lib/base.component';
import { Component, OnInit,Injector } from '@angular/core';
import { Observable} from 'rxjs';
import { ViewChild, ElementRef } from '@angular/core';
import jsPDF from 'jspdf';
import html2canvas from 'html2canvas';
import 'rxjs/add/observable/combineLatest';
import 'rxjs/add/operator/takeUntil';

@Component({
  selector: 'app-ct-tkb',
  templateUrl: './ct-tkb.component.html',
  styleUrls: ['./ct-tkb.component.css']
})
export class CtTkbComponent extends BaseComponent implements OnInit {
  @ViewChild('couponPage') couponPage: ElementRef; 
  public list_tkb:any;
  public tiets=['1', '2', '3', '4', '5'];
  public lop: any;
  constructor(injector: Injector) { 
    super(injector);
  }
  ngOnInit(): void {
    this.list_tkb = {};
    this._route.params.subscribe(params => {
      let id = params['id'];
      this.lop=id;
      this._api.get('api/tkb/get-by-lop/'+id).takeUntil(this.unsubscribe).subscribe(res => {
        this.list_tkb = res;
        setTimeout(() => {
          this.loadScripts();
        });
      }); 
    });
  }
  SavePDF(): void {
    let data = document.getElementById('couponPage');
    html2canvas(data).then(canvas => {
      let imgWidth = 208;
      let imgHeight = canvas.height * imgWidth / canvas.width;
      const contentDataURL = canvas.toDataURL('image/png')
      let pdf = new jsPDF('p', 'pt', 'a4');
      var position = 1;
      pdf.addImage(contentDataURL, 'PNG', 0, position, imgWidth, imgHeight)
      pdf.save('Diem.pdf');
    });
  }
  catText(text: string, limit: number): string {
    if(text.length > limit) {
      return text.substr(0, limit) + "...";
    }
    return text;
  }
}