import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({ providedIn: 'root' })
export class FilesApi {
  constructor(private http: HttpClient) { }

  list() {
    return this.http.get<Attachment[]>('/attachment/list');
  }

  delete(id: string) {
    return this.http.delete('/attachment/delete/' + id);
  }

  upload(form: FormData) {
    return this.http.post('/attachment/upload', form);
  }
}

export interface Attachment {
  id: string;
  fileName: string;
  size: number;
}
