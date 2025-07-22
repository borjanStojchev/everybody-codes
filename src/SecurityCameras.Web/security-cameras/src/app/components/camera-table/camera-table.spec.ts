import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CameraTable } from './camera-table';

describe('CameraTable', () => {
  let component: CameraTable;
  let fixture: ComponentFixture<CameraTable>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CameraTable]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CameraTable);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
