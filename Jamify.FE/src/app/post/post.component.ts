import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { Post } from 'src/Models/Post';
import { User } from 'src/Models/User';
import { UserReaction } from 'src/Models/UserReaction';
import { PostService } from '../Services/PostService';
import { UserService } from '../Services/UserService';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.scss']
})
export class PostComponent implements OnInit {

  mediaUrl: string = "";


  @Input() postRef: Post | null = new Post();

  currentUserId: string = "84AE68CC-A22C-4622-BF84-DB43A530DC7A";
  postedReaction: UserReaction | null = null;

  likeIcon = document.getElementById("like");
  dislikeIcon = document.getElementById("dislike");

  constructor(private _postService: PostService, private _userService: UserService) { }

  ngOnInit(): void {
    this.load();

  }

  load() {
    this.mediaUrl = `https://localhost:7211/api/post/mediadata/${this.postRef?.id}`;
    this._postService.getReactionByUserAndPostId(this.postRef?.id!, this.currentUserId).subscribe({
      next: data => {
        this.postedReaction = data;
      },
      error: err => {
        this.postedReaction = null;
        console.log(err);
      }
    });
    this._postService.getLikeCountByPostId(this.postRef?.id!).subscribe({
      next: data => {
        this.postRef!.likeCount = data;
      }
    });
    this._postService.getDislikeCountByPostId(this.postRef?.id!).subscribe({
      next: data => {
        this.postRef!.dislikeCount = data;
      }
    });

  }

  private refreshLikeCounts() {

  }

  reactPost(reaction: number) {
    if (this.postedReaction) {
      if (this.postedReaction.reaction == reaction) {
        this._postService.deleteReaction(this.postedReaction.id).subscribe(() => {
          this.load();
        });
      }
      else {
        this.postedReaction.reaction = reaction;
        this._postService.createReaction(this.postedReaction).subscribe(() => {
          this.load();
        });
      }
    }
    else {
      this.postedReaction = {
        id: "00000000-0000-0000-0000-000000000000",
        userId: this.currentUserId,
        postId: this.postRef?.id!,
        reaction: reaction
      }
      this._postService.createReaction(this.postedReaction).subscribe({
        next: data => {
          this.postedReaction = data;
          this.load();
        },
        error: err => {
          console.log(err);
          this.load();
        }
      });
    }

  }

}
