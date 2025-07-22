import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Camera } from '../models/camera.model';

@Injectable({
  providedIn: 'root'
})
export class CameraService {
  private API_BASE_URL = ' https://localhost:7056/api'; 

  constructor(private http: HttpClient) {}

  getCameras(): Observable<Camera[]> {
    return this.http.get<Camera[]>(`${this.API_BASE_URL}/Cameras`);
  }
}
