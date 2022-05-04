import { BaseComponent } from 'src/app/lib/base.component';
import { Component, OnInit,Injector, ElementRef, ViewChild } from '@angular/core';
import { Observable} from 'rxjs';
import 'rxjs/add/observable/combineLatest';
import 'rxjs/add/operator/takeUntil';
import 'keen-slider/keen-slider.min.css';
import KeenSlider from 'keen-slider';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: [
    "../../../../node_modules/keen-slider/keen-slider.min.css",
    './home.component.css']
})
export class HomeComponent extends BaseComponent implements OnInit {
  thongbaos:any;
  dstintuc:any;
  chiases: any;
  constructor(injector: Injector) { 
    super(injector);}

  ngOnInit(): void {
    this._api.post('api/tintuc/search',{page: 1, pageSize: 4, maloai: 2, trangthai: "duyệt"}).takeUntil(this.unsubscribe).subscribe(res => {
      this.thongbaos = res.data;
    });
    this._api.post('api/tintuc/search',{page: 1, pageSize: 7, trangthai: "duyệt"}).takeUntil(this.unsubscribe).subscribe(res => {
      this.dstintuc = res.data;
    });
    this._api.post('api/baiviet/search',{page: 1, pageSize: 2, trangthai: "duyệt"}).takeUntil(this.unsubscribe).subscribe(res => {
      this.chiases = res.data
    });
  }

  catText(text: string, limit: number): string {
    if(text.length > limit) {
      return text.substr(0, limit) + "...";
    }
    return text;
  }

  @ViewChild("sliderRef") sliderRef: ElementRef<HTMLElement>

  slider: any = null

  ngAfterViewInit() {
    this.slider = new KeenSlider(this.sliderRef.nativeElement, { loop: true })
  }

  ngOnDestroy() {
    if (this.slider) this.slider.destroy()
  }

}
