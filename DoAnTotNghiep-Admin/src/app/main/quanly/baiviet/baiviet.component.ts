import { MustMatch } from '../../../helpers/must-match.validator';
import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { FileUpload } from 'primeng/fileupload';
import { FormBuilder, Validators} from '@angular/forms';
import { BaseComponent } from '../../../lib/base.component';
import { Observable} from 'rxjs';
import 'rxjs/add/observable/combineLatest';
import 'rxjs/add/operator/takeUntil';
import { AuthenticationService } from 'src/app/lib/authentication.service';
import { DatePipe } from '@angular/common';
import Swal from 'sweetalert2/dist/sweetalert2.js'; 
declare var $: any;

@Component({
  selector: 'app-baiviet',
  templateUrl: './baiviet.component.html',
  styleUrls: ['./baiviet.component.css']
})
export class BaivietComponent extends BaseComponent implements OnInit {
  public baiviets: any;
  public baiviet: any;
  public totalRecords:any;
  public pageSize = 3;
  public page = 1;
  public uploadedFiles: any[] = [];
  public formsearch: any;
  public formdata: any;
  public doneSetupForm: any;  
  public showUpdateModal:any;
  public isCreate:any;
  public loaitin:any;
  public user: any;
  submitted = false;
  @ViewChild(FileUpload, { static: false }) file_image: FileUpload;
  constructor(private fb: FormBuilder, injector: Injector,  private authenticationService: AuthenticationService, private datePipe: DatePipe) {
    super(injector);
  }

  ngOnInit(): void {
    this.formsearch = this.fb.group({
      'tieude': [''] 
    });
    this.user =  this.authenticationService.userValue;
   this.search();
 
  }

  loadPage(page) { 
    this._api.post('/api/baiviet/search',{page: page, pageSize: this.pageSize}).takeUntil(this.unsubscribe).subscribe(res => {
      this.baiviets = res.data;
      this.totalRecords =  res.totalItems;
      this.pageSize = res.pageSize;
      });
  } 

  search() { 
    this.page = 1;
    this.pageSize = 5;
    this._api.post('/api/baiviet/search',{page: this.page, pageSize: this.pageSize, tieude: this.formsearch.get('tieude').value}).takeUntil(this.unsubscribe).subscribe(res => {
      this.baiviets = res.data;
      console.log(this.baiviets);
      this.totalRecords =  res.totalItems;
      this.pageSize = res.pageSize;
      });
  }

  get f() { return this.formdata.controls; }

  onSubmit(value) {
    this.submitted = true;
    let anhtmp = value.hinhanh.split('\\');
    if(this.isCreate) { 
      var date = new Date();
      let ngay =this.datePipe.transform(date,"yyyy-MM-dd");
        let tmp = {
          TieuDe:value.tieude,
          HinhAnh:anhtmp[anhtmp.length -1],
          ThoiGian:ngay,
          TrangThai:value.trangthai,
          NoiDung:value.noidung,
          MaTK: this.authenticationService.userValue.maTK        
          };
        this._api.post('/api/baiviet/create-baiviet',tmp).takeUntil(this.unsubscribe).subscribe(res => {
          Swal.fire(
            'Th??nh c??ng!',
            'Th??m th??nh c??ng',
            'success'
          );
          this.search();
          this.closeModal();
          });
    } else { 
        let tmp = {
          maBaiViet:this.baiviet.maBaiViet,
          tieuDe:value.tieude,
          hinhAnh:anhtmp[anhtmp.length -1],
          thoiGian:this.baiviet.thoiGian,
          trangThai:value.trangthai,
          noiDung:value.noidung,
          maTK:this.baiviet.maTK       
          };
          console.log(tmp);
        this._api.post('/api/baiviet/update-baiviet',tmp).takeUntil(this.unsubscribe).subscribe(res => {
          Swal.fire(
            'Th??nh c??ng!',
            'C???p nh???t th??nh c??ng',
            'success'
          );
          this.search();
          this.closeModal();
          });
    }
   
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
          this._api.post('/api/baiviet/delete-baiviet',{maBaiViet:row.maBaiViet}).takeUntil(this.unsubscribe).subscribe(res => {
            this.search(); 
            Swal.fire(
              '???? xo??!',
              'B???n ghi kh??ng th??? kh??i ph???c',
              'success'
            );
            });
        }
      })
  }

  Reset() {  
    this.baiviet = null;
    this.formdata = this.fb.group({
        'tieude': ['', Validators.required],
        'hinhanh': ['',Validators.required],
        'thoigian': ['', Validators.required],
        'trangthai': ['', Validators.required],
        'noidung': ['', Validators.required],
    }); 
    this.formdata.get('trangthai').setValue('duy???t');
  }

  createModal() {
    this.doneSetupForm = false;
    this.showUpdateModal = true;
    this.isCreate = true;
    this.baiviet = null;
    setTimeout(() => {
      $("#createUserModal").modal("show");
      this.formdata = this.fb.group({
        'tieude': ['', Validators.required],
        'hinhanh': ['',Validators.required],
        'trangthai': ['', Validators.required],
        'noidung': ['', Validators.required],
      });
      this.formdata.get('trangthai').setValue('duy???t');
      this.doneSetupForm = true;
    });
  }

  public openUpdateModal(row) {
    this.doneSetupForm = false;
    this.showUpdateModal = true; 
    this.isCreate = false;
    setTimeout(() => {
      $('#createUserModal').modal('toggle');
      this._api.get('/api/baiviet/get-by-id/'+ row.maBaiViet).takeUntil(this.unsubscribe).subscribe((res:any) => {
        this.baiviet = res; 
        this.formdata = this.fb.group({
          'tieude': [this.baiviet.tieuDe, Validators.required],
          'hinhanh': ['',Validators.required],
          'trangthai': [this.baiviet.trangThai, Validators.required],
          'noidung': [this.baiviet.noiDung, Validators.required],
        });  
        this.formdata.get('trangthai').setValue(this.baiviet.trangThai);
          this.doneSetupForm = true;
        }); 
    }, 700);
  }

  DuyetBaiViet(item){
    let textConfig = 'B??i vi???t s??? ???????c hi???n th???';
    if(item.trangThai=="duy???t")
    {
      textConfig = 'B??i vi???t s??? ???????c ???n ??i';
    }
   Swal.fire({
    title: 'B???n mu???n thay ?????i?',
    text: textConfig,
    icon: 'warning',
    showCancelButton: true,
    confirmButtonText: 'C??',
    cancelButtonText: 'Kh??ng'
  }).then((result) => {
    if (result.value) {
      let tmp={
        maBaiViet:item.maBaiViet,
        tieuDe:item.tieuDe,
        hinhAnh:item.hinhAnh,
        thoiGian:item.thoiGian,
        trangThai:item.trangThai,
        noiDung:item.noiDung,
        maTK:item.maTK    
      };
      if(item.trangThai=="duy???t")
        {
          tmp.trangThai = 'ch???';
        }else {
          tmp.trangThai = 'duy???t';
        }
      this._api.post('/api/baiviet/update-baiviet',tmp).takeUntil(this.unsubscribe).subscribe(res => {
        Swal.fire(
          'Th??nh c??ng!',
          'C???p nh???t th??nh c??ng',
          'success'
        );
        this.search();
        });
    }
  })
  }

  closeModal() {
    $('#createUserModal').closest('.modal').modal('hide');
  }
  catText(text: string, limit: number): string {
    if(text.length > limit) {
      return text.substr(0, limit) + "...";
    }
    return text;
  }
}
