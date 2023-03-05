import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HashtagSidebarComponent } from './hashtag-sidebar.component';

describe('HashtagSidebarComponent', () => {
  let component: HashtagSidebarComponent;
  let fixture: ComponentFixture<HashtagSidebarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HashtagSidebarComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HashtagSidebarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
