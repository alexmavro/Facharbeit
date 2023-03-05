import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { MainViewComponent } from './main-view/main-view.component';
import { PublishPostComponent } from './publish-post/publish-post.component';

const routes: Routes = [
  { path: '', component: MainViewComponent, pathMatch: 'full' },
  { path: 'publish-post', component: PublishPostComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
