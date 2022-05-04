import { MustMatch } from '../../../helpers/must-match.validator';
import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { FileUpload } from 'primeng/fileupload';
import { FormBuilder, Validators} from '@angular/forms';
import { BaseComponent } from '../../../lib/base.component';
import 'rxjs/add/operator/takeUntil';
import Swal from 'sweetalert2/dist/sweetalert2.js'; 
declare var $: any;

@Component({
  selector: 'app-danhmuc',
  templateUrl: './danhmuc.component.html',
  styleUrls: ['./danhmuc.component.css']
})
export class DanhmucComponent extends BaseComponent implements OnInit {
  public danhmucs: any;
  public danhmuc: any;
  public totalRecords:any;
  public pageSize = 3;
  public page = 1;
  public uploadedFiles: any[] = [];
  public formsearch: any;
  public formdata: any;
  public doneSetupForm: any;  
  public showUpdateModal:any;
  public isCreate:any;
  submitted = false;
  constructor(private fb: FormBuilder, injector: Injector) {
    super(injector);
  }

  ngOnInit(): void {
    this.formsearch = this.fb.group({
      'loaitin': [''] 
    });
   this.search();
  }

  loadPage(page) { 
    this._api.post('/api/danhmuctin/search',{page: page, pageSize: this.pageSize}).takeUntil(this.unsubscribe).subscribe(res => {
      this.danhmucs = res.data;
      this.totalRecords =  res.totalItems;
      this.pageSize = res.pageSize;
      });
      console.log(this.danhmucs);
  } 

  search() { 
    this.page = 1;
    this.pageSize = 5;
    this._api.post('/api/danhmuctin/search',{page: this.page, pageSize: this.pageSize, loaitin: this.formsearch.get('loaitin').value}).takeUntil(this.unsubscribe).subscribe(res => {
      this.danhmucs = res.data;
      console.log(this.danhmucs);
      this.totalRecords =  res.totalItems;
      this.pageSize = res.pageSize;
      });
  }

  get f() { return this.formdata.controls; }

  onSubmit(value) {
    this.submitted = true;
    if (this.formdata.invalid) {
      return;
    } 
    if(this.isCreate) { 
        let tmp = {
          maLoai:value.maloai,
          loaiTin:value.loaitin,     
          };
        this._api.post('/api/danhmuctin/create-danhmuc',tmp).takeUntil(this.unsubscribe).subscribe(res => {
          Swal.fire(
            'Thành công!',
            'Thêm thành công',
            'success'
          );
          this.search();
          this.closeModal();
          });
    } else { 
        let tmp = {
          maLoai:this.danhmuc.maLoai,
          loaiTin:value.loaitin,      
          };
        this._api.post('/api/danhmuctin/update-danhmuc',tmp).takeUntil(this.unsubscribe).subscribe(res => {
          Swal.fire(
            'Thành công!',
            'Cập nhật thành công',
            'success'
          );
          this.search();
          this.closeModal();
          });
    }
   
  } 

  onDelete(row) { 
      Swal.fire({
        title: 'Bạn có chắc muốn xoá?',
        text: 'Bạn sẽ không thể khôi phục bản ghi này!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Có',
        cancelButtonText: 'Không'
      }).then((result) => {
        if (result.value) {
          this._api.post('/api/danhmuctin/delete-danhmuc',{MaLoai:row.maLoai}).takeUntil(this.unsubscribe).subscribe(res => {
            this.search(); 
            Swal.fire(
              'Đã xoá!',
              'Bản ghi không thể khôi phục',
              'success'
            );
            });
        }
      })
  }

  Reset() {  
    this.danhmuc = null;
    this.formdata = this.fb.group({
      'maloai': ['', Validators.required],
      'loaitin': ['', Validators.required],

    }); 
  }

  createModal() {
    this.doneSetupForm = false;
    this.showUpdateModal = true;
    this.isCreate = true;
    this.danhmuc = null;
    setTimeout(() => {
      $("#createUserModal").modal("show");
      this.formdata = this.fb.group({
        'maloai': ['', Validators.required],
        'loaitin': ['', Validators.required],
      });
      this.doneSetupForm = true;
    });
  }

  public openUpdateModal(row) {
    this.doneSetupForm = false;
    this.showUpdateModal = true; 
    this.isCreate = false;
    setTimeout(() => {
      $('#createUserModal').modal('toggle');
      this._api.get('/api/danhmuctin/get-by-id/'+ row.maLoai).takeUntil(this.unsubscribe).subscribe((res:any) => {
        this.danhmuc = res; 
        console.log(this.danhmuc);
          this.formdata = this.fb.group({
            'maloai': [this.danhmuc.maLoai, Validators.required],
            'loaitin': [this.danhmuc.loaiTin, Validators.required],

          }); 
          this.doneSetupForm = true;
        }); 
    }, 700);
  }

  closeModal() {
    $('#createUserModal').closest('.modal').modal('hide');
  }
}

