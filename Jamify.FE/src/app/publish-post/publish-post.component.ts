import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PostService } from '../Services/PostService';
import { UserService } from '../Services/UserService';

@Component({
  selector: 'app-publish-post',
  templateUrl: './publish-post.component.html',
  styleUrls: ['./publish-post.component.scss']
})
export class PublishPostComponent implements OnInit {

  public file: File | null = null;
  public title: string | null = null;
  public userId = this._userService.getUser();


  constructor(private _postService: PostService, private _userService: UserService, private router: Router) { }

  ngOnInit(): void {
  }

  onFileSelected(event: Event) {
    const element = event.currentTarget as HTMLInputElement;
    this.file = element.files![0];
  }

  publishPost() {
    if (this.file && this.title) {
      this._postService.uploadFileAndData(this.file!, this.title!, this.userId).subscribe();
      this.router.navigate(['/']);
      return;
    }
    window.alert("Please specify title and file.");


  }

}
