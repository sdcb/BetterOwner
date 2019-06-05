import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({providedIn: 'root'})
export class ExploreApiService {
  constructor(private http: HttpClient) { }
  treasures(query: TreasureQuery) {
    return this.http.get<TreasureExploreDto[]>('/api/explore/treasures', {
      params: <any>query
    });
  }
}

export interface TreasureQuery {
  pageToken?: string;
}

export interface TreasureExploreDto {
  id: string;
  title: string;
  price: number;
  publishUser: string;
  pictureUrl: string;
}
