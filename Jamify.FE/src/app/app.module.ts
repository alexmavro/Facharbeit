import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { MainViewComponent } from './main-view/main-view.component';
import { MainContentComponent } from './main-content/main-content.component';
import { PostComponent } from './post/post.component';
import { MockDataService } from 'src/app/Services/MockDataService';
import { HashtagSidebarComponent } from './hashtag-sidebar/hashtag-sidebar.component';
import { HashtagComponent } from './hashtag/hashtag.component';
import { RightSidebarComponent } from './right-sidebar/right-sidebar.component';
import { TaggedVideosComponent } from './tagged-videos/tagged-videos.component';
import { PostService } from './Services/PostService';
import { UserService } from './Services/UserService';
import { PublishPostComponent } from './publish-post/publish-post.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    MainViewComponent,
    MainContentComponent,
    PostComponent,
    HashtagSidebarComponent,
    HashtagComponent,
    RightSidebarComponent,
    TaggedVideosComponent,
    PublishPostComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [PostService, UserService, MockDataService],
  bootstrap: [AppComponent]
})
export class AppModule { }
