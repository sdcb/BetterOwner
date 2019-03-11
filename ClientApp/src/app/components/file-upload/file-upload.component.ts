import { Component, OnInit, ElementRef, ViewChild, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css']
})
export class FileUploadComponent implements OnInit {
  @ViewChild('file')
  file: ElementRef<HTMLInputElement>;

  limit = new FileLimitations();

  @Input()
  set typeLimit(value: FileType) { this.limit.type = value; }

  @Input()
  set sizeLimit(value: number) { this.limit.size = value; }

  @Input()
  set countLimit(value: number) { this.limit.count = value; }

  @Output()
  fileChanged = new EventEmitter<File[]>();

  rawFiles = Array<FileItem>();

  constructor() { }

  ngOnInit() {
    console.log(this.limit);
  }

  getValidFiles() {
    return this.rawFiles
      .filter((x, index) => this.limit.isValid(x, index))
      .map(x => x.file);
  }

  validate(item: FileItem, index: number) { return this.limit.validate(item, index); }

  hasData() { return this.rawFiles.length > 0; }

  remove(fileItem: FileItem, event: MouseEvent) {
    this.rawFiles = this.rawFiles.filter(x => x !== fileItem);
    event.stopPropagation();
    this.fileChanged.emit(this.getValidFiles());
  }

  fileSelected(files: FileList) {
    for (let i = 0; i < files.length; ++i) {
      this.rawFiles.push(new FileItem(files[i]));
    }
    this.fileChanged.emit(this.getValidFiles());
  }
}

export type FileType = 'picture';

export class FileLimitations {
  type: FileType;
  size = 4 * 1024 * 1024;
  count = 5;

  validate(item: FileItem, index: number) {
    if (index >= this.count) { return `超过${this.count}个文件`; }
    if (item.file.size > this.size) { return `文件超限制大小${this.size}`; }
    if (this.type === 'picture' && !item.isPicture()) { return `"${item.file.type}"不是图片`; }
    return null;
  }

  isValid(item: FileItem, index: number) {
    return this.validate(item, index) === null;
  }
}

export class FileItem {
  static fileTypeMapping = {
    picture: [
      'image/gif',
      'image/jpeg',
      'image/png',
      'image/tiff']
  };

  dataUrl: string;

  constructor(public file: File) {
    if (this.isPicture()) { readFileAsync(file).then(x => this.dataUrl = x); }
  }

  isPicture() { return FileItem.fileTypeMapping.picture.includes(this.file.type); }
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
