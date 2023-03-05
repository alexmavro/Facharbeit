import { Component, OnInit } from '@angular/core';
import { Hashtag } from 'src/Models/Hashtag';
import { MockDataService } from 'src/app/Services/MockDataService';

@Component({
  selector: 'app-hashtag-sidebar',
  templateUrl: './hashtag-sidebar.component.html',
  styleUrls: ['./hashtag-sidebar.component.scss']
})
export class HashtagSidebarComponent implements OnInit {

  public TrendingHashtags: Hashtag[] = [];

  constructor(private _dataservice: MockDataService) { }
  
  ngOnInit(): void {
    this.TrendingHashtags = this._dataservice.getFiveTrendingHashtags();
  }

}
