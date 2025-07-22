import { Component, EventEmitter, Output } from '@angular/core';
import { CameraService } from '../../services/camera.service';
import { FormsModule } from '@angular/forms';
import { debounceTime, distinctUntilChanged, Subject, Subscription, switchMap } from 'rxjs';

@Component({
  selector: 'search',
  imports: [FormsModule],
  templateUrl: './search.html',
  styleUrl: './search.scss'
})
export class SearchComponent {
  @Output() resultsFound = new EventEmitter<any[]>();
  searchText = '';
  private searchSubject = new Subject<string>();
  private subscription: Subscription;

  constructor(private cameraService: CameraService) {
    this.subscription = this.searchSubject
      .pipe(
        debounceTime(300),
        distinctUntilChanged(),
        switchMap(text => this.cameraService.searchCameras(text))
      )
      .subscribe({
        next: (data) => this.resultsFound.emit(data),
        error: (err) => {
          console.error('Search error:', err);
          this.resultsFound.emit([]);
        }
      });
  }

  onSearch(): void {
     this.searchSubject.next(this.searchText.trim());
  }
}
