import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-test-page-six',
  templateUrl: './test-page-six.component.html',
  styleUrls: ['./test-page-six.component.css']
})
export class TestPageSixComponent implements OnInit {

  form!: FormGroup;
  hide = true;
  
  constructor(private fb: FormBuilder) { }

  ngOnInit(): void {
    this.form = this.fb.group({
      Email: ["", Validators.required],
      Password: ["", Validators.required]
    })
  }

  onSubmit() {



  }


}
