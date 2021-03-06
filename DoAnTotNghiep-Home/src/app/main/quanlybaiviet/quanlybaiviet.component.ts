import { BaseComponent } from 'src/app/lib/base.component';
import { Component, OnInit, Injector, ViewChild  } from '@angular/core';
import { FileUpload } from 'primeng/fileupload';
import { BehaviorSubject, Observable} from 'rxjs';
import { FormBuilder, Validators} from '@angular/forms';
import 'rxjs/add/observable/combineLatest';
import 'rxjs/add/operator/takeUntil';
import { DatePipe } from '@angular/common';
import Swal from 'sweetalert2/dist/sweetalert2.js';  
declare var $:any;

@Component({
  selector: 'app-quanlybaiviet',
  templateUrl: './quanlybaiviet.component.html',
  styleUrls: ['./quanlybaiviet.component.css']
})
export class QuanlybaivietComponent extends BaseComponent implements OnInit {
  public baiviets: any;
  public baiviet: any;
  public totalRecords:any;
  public pageSize = 3;
  public page = 1;
  public uploadedFiles: any[] = [];
  public formsearch: any;
  public formBaiViet: any;
  public doneSetupFormBaiViet: any;  
  public showUpdateModalBaiViet:any;
  public user: any
  submitted = false;
  @ViewChild(FileUpload, { static: false }) file_image: FileUpload;
  constructor(private fb: FormBuilder, injector: Injector,private datePipe: DatePipe) {
    super(injector);
  }
  ngOnInit(): void {
    let tmp=new BehaviorSubject<any>(JSON.parse(localStorage.getItem('user')));
    this.user=tmp.value;
    this.formsearch = this.fb.group({
      'tieude': [''] 
    });
   this.search();
 
  }

  loadPage(page) { 
    this._api.post('api/baiviet/search',{page: page, pageSize: this.pageSize,taikhoan: this.user.maTK}).takeUntil(this.unsubscribe).subscribe(res => {
      this.baiviets = res.data;
      this.totalRecords =  res.totalItems;
      this.pageSize = res.pageSize;
      });
  } 

  search() { 
    this.page = 1;
    this.pageSize = 5;
    this._api.post('api/baiviet/search',{page: this.page, pageSize: this.pageSize, tieude: this.formsearch.get('tieude').value, taikhoan: this.user.maTK}).takeUntil(this.unsubscribe).subscribe(res => {
      this.baiviets = res.data;
      console.log(this.baiviets);
      this.totalRecords =  res.totalItems;
      this.pageSize = res.pageSize;
      });
  }

  

  get f() { return this.formBaiViet.controls; }

  onSubmit(value) {
    this.submitted = true;
      console.log(value);
      var date = new Date();
      let ngay =this.datePipe.transform(date,"yyyy-MM-dd");
      let anhtmp = value.hinhanh.split('\\');
      this.getEncodeFromImage(this.file_image).subscribe((data: any): void => {
        let data_image = data == '' ? null : data;
        let tmp = {
          TieuDe:value.tieude,
          HinhAnh:anhtmp[anhtmp.length -1],
          ThoiGian:ngay,
          TrangThai:"ch???",
          NoiDung:value.noidung,
          MaTK: this.user.maTK        
          };
        this._api.post('api/baiviet/create-baiviet',tmp).takeUntil(this.unsubscribe).subscribe(res => {
          Swal.fire(
            'Th??nh c??ng!',
            '???? th??m th??nh c??ng!',
            'success'
          );
          this.search();
          this.closeModal();
          });
        });
   
  } 

  onDelete(row) { 
    Swal.fire({
      title: 'B???n c?? ch???c mu???n xo???',
      text: 'B???n s??? kh??ng th??? kh??i ph???c b???n ghi n??y!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'C??',
      cancelButtonText: 'Kh??ng'
    }).then((result) => {
      if (result.value) {
        this._api.post('api/baiviet/delete-baiviet',{maBaiViet:row.maBaiViet}).takeUntil(this.unsubscribe).subscribe(res => {
          this.search(); 
          });
        Swal.fire(
          '???? xo??!',
          'B???n ghi kh??ng th??? kh??i ph???c',
          'success'
        )
      }
    })
    // this._api.post('api/baiviet/delete-baiviet',{maBaiViet:row.maBaiViet}).takeUntil(this.unsubscribe).subscribe(res => {
    //   alert('X??a th??nh c??ng');
    //   this.search(); 
    //   });
  }

  Reset() {  
    this.baiviet = null;
    this.formBaiViet = this.fb.group({
        'tieude': ['', Validators.required],
        'thoigian': ['', Validators.required],
        'trangthai': ['', Validators.required],
        'noidung': ['', Validators.required],
        'hinhanh': [''],
    }); 
  }

  createModal() {
    this.doneSetupFormBaiViet = false;
    this.showUpdateModalBaiViet = true;
    this.baiviet = null;
    setTimeout(() => {
      $("#createBaiVietModal").modal("show");
      this.formBaiViet = this.fb.group({
        'tieude': ['', Validators.required],
        'trangthai': ['', Validators.required],
        'noidung': ['', Validators.required],
        'hinhanh': [''],
      });
      this.doneSetupFormBaiViet = true;
    });
  }

  closeModal() {
    $('#createBaiVietModal').closest('.modal').modal('hide');
  }
  catText(text: string, limit: number): string {
    if(text.length > limit) {
      return text.substr(0, limit) + "...";
    }
    return text;
  }
}