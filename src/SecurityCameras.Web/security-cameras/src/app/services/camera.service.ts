import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Camera } from '../models/camera.model';

@Injectable({
  providedIn: 'root'
})
export class CameraService {
  private API_BASE_URL = ' https://localhost:7056/api'; 

  constructor(private http: HttpClient) {}

  getCameras(): Observable<Camera[]> {
    return this.http.get<Camera[]>(`${this.API_BASE_URL}/cameras`);
  }

  searchCameras(searchText: string): Observable<Camera[]>{
    if(searchText){
      const params = new HttpParams().set('name', searchText);
      return this.http.get<Camera[]>(`${this.API_BASE_URL}/cameras/search`, {params});
    }
    else{
      return this.http.get<Camera[]>(`${this.API_BASE_URL}/cameras/search`);
    }
    
  }
}
