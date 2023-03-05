import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaggedVideosComponent } from './tagged-videos.component';

describe('TaggedVideosComponent', () => {
  let component: TaggedVideosComponent;
  let fixture: ComponentFixture<TaggedVideosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaggedVideosComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TaggedVideosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
