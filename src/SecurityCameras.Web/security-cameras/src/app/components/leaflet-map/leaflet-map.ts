import { Component, OnInit, AfterViewInit, Input, SimpleChanges } from '@angular/core';
import * as L from 'leaflet';
import { Camera } from '../../models/camera.model';

@Component({
  selector: 'leaflet-map',
  imports: [],
  templateUrl: './leaflet-map.html',
  styleUrl: './leaflet-map.scss'
})
export class LeafletMapComponent implements OnInit, AfterViewInit {
  @Input() cameras: Camera[] = [];

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['cameras']) {
      this.updateMap();
    }
  }

  private map!: L.Map
  markers: L.Marker[] = [
    L.marker([52.0914, 5.1115]) //Utrecht
  ];

  constructor() { }

  ngOnInit() {
    this.setIcon();
  }

  ngAfterViewInit() {
    this.initMap();
    this.centerMap();
  }

  private setIcon(){
    delete (L.Icon.Default.prototype as any)._getIconUrl;

    L.Icon.Default.mergeOptions({
      iconRetinaUrl: 'assets/leaflet/marker-icon-2x.png',
      iconUrl: 'assets/leaflet/marker-icon.png',
      shadowUrl: 'assets/leaflet/marker-shadow.png',
    });
  }

  private initMap() {
    const baseMapURl = 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png'
    this.map = L.map('map');
    L.tileLayer(baseMapURl).addTo(this.map);
  }

  private centerMap() {
    // Create a boundary based on the markers
    const bounds = L.latLngBounds(this.markers.map(marker => marker.getLatLng()));
    this.map.fitBounds(bounds);
  }

  private updateMap(){
    if (!this.map) return;

    const L = (window as any).L;
    
    // Clear existing markers
    this.map.eachLayer((layer: any) => {
      if (layer instanceof L.Marker) {
        this.map.removeLayer(layer);
      }
    });

    // Add new markers
    this.cameras.forEach(camera => {
      const marker = L.marker([camera.lat, camera.lon])
        .addTo(this.map)
        .bindPopup(`
          <strong>Camera ${camera.number}</strong><br>
          ${camera.name}<br>
          <small>${camera.lat}, ${camera.lon}</small>
        `);
    });

    // Fit map to show all markers
    if (this.cameras.length > 0) {
      const group = new L.featureGroup(this.cameras.map(camera => 
        L.marker([camera.lat, camera.lon])
      ));
      this.map.fitBounds(group.getBounds());
    }
  }
}
