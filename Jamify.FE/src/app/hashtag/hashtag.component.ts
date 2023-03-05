import { Component, Input, OnInit } from '@angular/core';
import { Hashtag } from 'src/Models/Hashtag';
import { MockDataService } from 'src/app/Services/MockDataService';

@Component({
  selector: 'app-hashtag',
  templateUrl: './hashtag.component.html',
  styleUrls: ['./hashtag.component.scss']
})
export class HashtagComponent implements OnInit {

  @Input() hashtag!: Hashtag;

  constructor() { }

  ngOnInit(): void {
  }

}
