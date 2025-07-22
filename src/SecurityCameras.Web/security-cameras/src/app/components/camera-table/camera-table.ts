import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Camera } from '../../models/camera.model';

@Component({
  selector: 'camera-table',
  imports: [CommonModule],
  templateUrl: './camera-table.html',
  styleUrl: './camera-table.scss'
})
export class CameraTableComponent {
  @Input() title: string = '';
  @Input() cameras: Camera[] = [];
}
