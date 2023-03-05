import { Component, OnInit } from '@angular/core';
import { Post } from 'src/Models/Post';
import { MockDataService } from 'src/app/Services/MockDataService';
import { PostService } from '../Services/PostService';
import { UserService } from '../Services/UserService';
import { Following } from 'src/Models/Following';
import { User } from 'src/Models/User';
import { async, combineLatest, forkJoin, map, mergeMap, of, switchMap } from 'rxjs';

@Component({
  selector: 'app-main-content',
  templateUrl: './main-content.component.html',
  styleUrls: ['./main-content.component.scss']
})
export class MainContentComponent implements OnInit {

  public posts: Post[] = [];
  followings: Following[] = []
  userId: string = this._userService.getUser();

  constructor(private _postService: PostService, private _userService: UserService) { }

  ngOnInit() {  
    this._postService.getPostsByFollowedUser(this.userId).subscribe({
      next: data => {
        this.posts = data;
      },
      error: err => {
        console.log(err);
      }
    });
  }
}
