import { Component, ElementRef, ViewChild, OnInit,Injector} from '@angular/core';
import { BaseComponent } from 'src/app/lib/base.component';
import { Observable} from 'rxjs';
import 'rxjs/add/observable/combineLatest';
import 'rxjs/add/operator/takeUntil';
import { DatePipe } from '@angular/common';
import 'keen-slider/keen-slider.min.css';
import KeenSlider from 'keen-slider';

@Component({
  selector: 'app-silde-header',
  templateUrl: './silde-header.component.html',
  styleUrls: ['./silde-header.component.css']
})
export class SildeHeaderComponent extends BaseComponent implements OnInit{


  slider: any = null;
  public tintucs:any;
  public totalRecords: any;
  public pageSize = 3;
  public page = 1;

  constructor(injector: Injector) { 
    super(injector);
  }

  ngOnInit(): void {
    this.search();
  }

  search() { 
    this.page = 1;
    this.pageSize = 3;
    this._api.post('api/tintuc/search',{page: this.page, pageSize: this.pageSize, maloai: 1}).takeUntil(this.unsubscribe).subscribe(res => {
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
  // @ViewChild("sliderRef1") sliderRef1: ElementRef<HTMLElement>;
  // ngAfterContentInit() {
  //   this.slider = new KeenSlider(this.sliderRef1.nativeElement, {
  //     loop: true,
  //     rtl: true,
  //     slidesPerView: 3,
  //     spacing: 10,
  //   });
  // }

  // ngOnDestroy() {
  //   if (this.slider) this.slider.destroy()
  // }

}
