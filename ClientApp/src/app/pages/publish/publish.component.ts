import { PublishApiService } from './publish.api';
import { Component, OnInit } from '@angular/core';
import { SimpleSnackBar, MatSnackBar } from '@angular/material';

@Component({
  selector: 'app-publish',
  templateUrl: './publish.component.html',
  styleUrls: ['./publish.component.css']
})
export class PublishComponent implements OnInit {
  title: string;
  price: number;
  description: string;
  files: File[];

  constructor(
    private api: PublishApiService,
    private snackBar: MatSnackBar) { }

  ngOnInit() {
  }

  storeFiles(files: File[]) {
    this.files = files;
  }

  submit() {
    const error = this.validate();
    if (error !== null) {
      this.snackBar.open(error, '错误');
      return;
    }

    this.api.create(this.title, this.price, this.description, this.files);
  }

  validate() {
    if (!this.title) { return '标题不能为空'; }
    if (this.price <= 0) { return '价格必须>0'; }
    if (!this.description) { return '描述不能为空'; }
    if (this.files.length === 0) { return '请上传图片'; }
    return null;
  }
}
