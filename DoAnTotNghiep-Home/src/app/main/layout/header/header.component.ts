import { BaseComponent } from 'src/app/lib/base.component';
import { Component, OnInit, Injector } from '@angular/core';
import { BehaviorSubject, Observable} from 'rxjs';
import Swal from 'sweetalert2/dist/sweetalert2.js';  
import {
  FormGroup,
  FormControl,
  Validators,
  FormBuilder,
} from '@angular/forms';
import 'rxjs/add/observable/combineLatest';
import 'rxjs/add/operator/takeUntil';
declare var $:any;

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent extends BaseComponent implements OnInit {
  public user:any;
  error: any;
  public formdata:FormGroup;
  public doneSetupForm: any;  
  submitted = false;
  isChecked = false;
  constructor(injector: Injector,private formBuilder: FormBuilder,) { 
    super(injector);
  }
  ngOnInit(): void {
    let tmp=new BehaviorSubject<any>(JSON.parse(localStorage.getItem('user')));
    this.user=tmp.value;
  }

  get f() { return this.formdata.controls; }
  createModal(){
    setTimeout(() => {
      $('#createUserModal').modal('toggle');
      this.formdata = this.formBuilder.group({
        username: ['', Validators.required],
        password: ['', Validators.required],
        remember: [''],
      });
      this.doneSetupForm = true;
      this.submitted = false;
    });
  }
  onSubmit(value){
    this.submitted = true;
    if (this.formdata.invalid) {
      return;
    } 
    let tmp={
      Username: value.username,
      Password: value.password
    };
    this._api.post('api/taikhoan/authenticate',tmp).takeUntil(this.unsubscribe)
    .subscribe(user => {
      
      localStorage.setItem('user', JSON.stringify(user));
      $("#createUserModal").modal("hide");
      this.user=user;
      this.isChecked = false;
      Swal.fire(
        'Thành công!',
        'Đăng nhập thành công!',
        'success'
      );
    },(error) => {
      this.isChecked = true;
    }
    );
  }
  logout(){
    localStorage.removeItem('user');
    this.user=null;
  }

}
