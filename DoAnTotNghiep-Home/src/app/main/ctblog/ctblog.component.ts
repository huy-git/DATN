import { BaseComponent } from 'src/app/lib/base.component';
import { Component, OnInit, Injector } from '@angular/core';
import {
  FormGroup,
  FormControl,
  Validators,
  FormBuilder,
} from '@angular/forms';
import { BehaviorSubject, Observable} from 'rxjs';
import 'rxjs/add/observable/combineLatest';
import 'rxjs/add/operator/takeUntil';
declare var $:any;

@Component({
  selector: 'app-ctblog',
  templateUrl: './ctblog.component.html',
  styleUrls: ['./ctblog.component.css']
})
export class CtblogComponent extends BaseComponent implements OnInit {
  item:any;
  error:any;
  public user:any;
  public totalRecords: any;
  public pageSize = 4;
  public page = 1;
  public formLogin:FormGroup;
  public formbinhluan:any;
  public binhluans: any;
  public binhluans1: any;
  public list_blog:any;
  public doneSetupForm: any;  
  submitted = false;
  constructor(injector: Injector,private formBuilder: FormBuilder,) { 
    super(injector);
  }

  ngOnInit(): void {
    this.formbinhluan = this.formBuilder.group({
      binhluan: ['', Validators.required],
    });
    Observable.combineLatest(
      this._api.get('api/baiviet/get-all'),).takeUntil(this.unsubscribe).subscribe(res => {
      this.list_blog = res[0];
      this.list_blog.length=5;
      setTimeout(() => {
        this.loadScripts();
      });
    }, err => { });
    
    let tmp=new BehaviorSubject<any>(JSON.parse(localStorage.getItem('user')));
    this.user=tmp.value;
    this.item = {};
    this._route.params.subscribe(params => {
      let id = params['id'];
      this._api.get('api/baiviet/get-by-id/'+id).takeUntil(this.unsubscribe).subscribe(res => {
        this.item = res;
        this.search();
        this._api.get('api/binhluan/get-bai-viet/'+this.item.maBaiViet).takeUntil(this.unsubscribe).subscribe(res => {
          this.binhluans=res;
        });
        setTimeout(() => {
          this.loadScripts();
        });
      }); 
    });
  }

  loadPage(page) { 
    this._api.post('api/binhluan/search',{page: page, pageSize: this.pageSize, mabv:this.item.maBaiViet}).takeUntil(this.unsubscribe).subscribe(res => {
      this.binhluans1 = res.data;
      this.totalRecords =  res.totalItems;
      this.pageSize = res.pageSize;
      });
  } 
  search() { 
    this.page = 1;
    this.pageSize = 3;
    this._api.post('api/binhluan/search',{page: this.page, pageSize: this.pageSize, mabv:this.item.maBaiViet}).takeUntil(this.unsubscribe).subscribe(res => {
      this.binhluans1 = res.data;
      this.totalRecords =  res.totalItems;
      this.pageSize = res.pageSize;
      });
  }
  get f() { return this.formLogin.controls; }
  createModal(){
    setTimeout(() => {
      $('#createLoginModal').modal('toggle');
      this.formLogin = this.formBuilder.group({
        username: ['', Validators.required],
        password: ['', Validators.required],
        remember: [''],
      });
      this.doneSetupForm = true;
    });
  }
  onSubmit(value){
    this.submitted = true;
    if (this.formLogin.invalid) {
      return;
    } 
    let tmp={
      Username: value.username,
      Password: value.password
    };
    this._api.post('api/taikhoan/authenticate',tmp).pipe()
    .subscribe(user => {
      localStorage.setItem('user', JSON.stringify(user));
      $("#createLoginModal").modal("hide");
      this.user=user;
      alert("Đăng nhập thành công!");
    },
    (error) => {
      this.error = error;
    }
    );
  }
  logout(){
    localStorage.removeItem('user');
    this.user=null;
  }
  ThemBinhLuan(){
    if(this.user){
      let tmp={
        MaTK: this.user.maTK,
        Username:this.user.username,
        HoTen:this.user.hoTen,
        NoiDung:this.formbinhluan.get('binhluan').value,
        TrangThai:'chờ',
        MaBaiViet:this.item.maBaiViet
      };
      this._api.post('api/binhluan/create-binhluan',tmp).subscribe(res => {
        alert("Bình luận thành công!");
        this.formbinhluan = this.formBuilder.group({
          binhluan: ['', Validators.required],
        });
        this._api.get('api/binhluan/get-bai-viet/'+this.item.maBaiViet).takeUntil(this.unsubscribe).subscribe(res => {
          this.binhluans=res;
        });
      });
    }else{
      this.createModal();
    }
  }
  catText(text: string, limit: number): string {
    if(text.length > limit) {
      return text.substr(0, limit) + "...";
    }
    return text;
  }
}
