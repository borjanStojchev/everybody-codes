import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CameraTableComponent } from './components/camera-table/camera-table';
import { CommonModule } from '@angular/common';
import { CameraService } from './services/camera.service';
import { Camera } from './models/camera.model';
import { LeafletMapComponent } from "./components/leaflet-map/leaflet-map";

@Component({
  selector: 'app-root',
  imports: [CommonModule, CameraTableComponent, LeafletMapComponent],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('security-cameras');
  allCameras: Camera[] = [];
  cameras3: Camera[] = [];
  cameras5: Camera[] = [];
  cameras3and5: Camera[] = [];
  otherCameras: Camera[] = []; 
  
  constructor(private cameraService: CameraService) {}

  ngOnInit(): void {
    this.cameraService.getCameras().subscribe({
      next: (data) => {
        this.allCameras = data;
        data.forEach(camera => {
          if (camera.number !== null) {
            const div3 = camera.number % 3 === 0;
            const div5 = camera.number % 5 === 0;

            if (div3 && div5) {
              this.cameras3.push(camera);
            } else if (div3) {
              this.cameras5.push(camera);
            } else if (div5) {
              this.cameras3and5.push(camera); 
            } else {
              this.otherCameras.push(camera); 
            }
        }})
      },
      error:(err) => {
        console.error('Fething cameras error: ', err);
      }
    });
  }
}

