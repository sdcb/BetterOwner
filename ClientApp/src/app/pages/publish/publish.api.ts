import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({providedIn: 'root'})
export class PublishApiService {
  constructor(private httpClient: HttpClient) { }

  publish(title: string, price: number, description: string, files: File[]) {
    const form = new FormData();
    form.append('title', title);
    form.append('price', price.toString());
    form.append('description', description);
    for (const file of files) { form.append('files', file, file.name); }
    return this.httpClient.post('/api/publish/create', form);
  }
}
