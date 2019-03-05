import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Attachment, FilesApi } from './files.api';

@Component({
  selector: 'app-files',
  templateUrl: './files.component.html',
  styleUrls: ['./files.component.css']
})
export class FilesComponent implements OnInit {
  attachments: Attachment[];
  @ViewChild("file") file: ElementRef<HTMLInputElement>;

  images = Array<string | ArrayBuffer>(0);

  constructor(private api: FilesApi) {
    this.loadData();
  }

  ngOnInit() {
  }

  loadData() {
    return this.api.list().subscribe(x => this.attachments = x);
  }

  delete(item: Attachment) {
    if (confirm("Are you sure?")) {
      this.api.delete(item.id).subscribe(() => this.loadData());
    }
  }

  async fileChange(files: FileList) {
    let result = Array<string|ArrayBuffer>(0);
    for (let i = 0; i < files.length; ++i) {
      result.push(await readFileAsync(files[i]));
    }
    this.images = result;
  }

  addFiles() {
    this.file.nativeElement.click();
  }

  upload(files: FileList) {
    const form = new FormData();
    for (let i = 0; i < files.length; ++i) {
      form.append(files[i].name, files[i]);
    }
    this.api.upload(form).subscribe(() => {
      this.loadData();
      this.images = [];
    });
  }
}

export function readFileAsync(file: File) {
  return new Promise<string | ArrayBuffer>(resolve => {
    const reader = new FileReader();
    reader.readAsDataURL(file);

    reader.onload = ev => {
      resolve(reader.result);
    };
  });
}
