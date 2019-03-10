import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css']
})
export class FileUploadComponent implements OnInit {
  @ViewChild('file')
  file: ElementRef<HTMLInputElement>;

  files = Array<FileItem>();

  constructor() { }

  ngOnInit() {
  }

  hasData() { return this.files.length > 0; }

  remove(fileItem: File, event: MouseEvent) {
    this.files = this.files.filter(x => x.file !== fileItem);
    event.stopPropagation();
  }

  async fileSelected(files: FileList) {
    for (let i = 0; i < files.length; ++i) {
      this.files.push(await FileItem.create(files[i]));
    }
  }
}

export class FileItem {
  file: File;
  dataUrl: string;

  static async create(file: File) {
    return {
      file: file,
      dataUrl: await readFileAsync(file)
    };
  }
}

function readFileAsync(file: File) {
  return new Promise<string>(resolve => {
    const reader = new FileReader();
    reader.readAsDataURL(file);

    reader.onload = ev => {
      resolve(<string>reader.result);
    };
  });
}
