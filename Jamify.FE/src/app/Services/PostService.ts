import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { lastValueFrom, Observable } from "rxjs";
import { catchError, retry } from 'rxjs/operators';
import { environment } from "src/environments/environment";
import { Post } from "src/Models/Post";
import { UserReaction } from "src/Models/UserReaction";

@Injectable()
export class PostService{
    private apiUrl: string = "https://localhost:7211/api/";

    private httpOptions = {
        headers: new HttpHeaders({
          'Content-Type':  'application/json'
        })
      };
    
    constructor(private http: HttpClient) { }

    getPostbyId(id: string): Observable<Post> {
        return this.http.get<Post>(this.apiUrl + `post/${ id }`);
        
    }

    getLikeCountByPostId(id: string): Observable<number> {
        return this.http.get<number>(this.apiUrl + `post/likes/${ id }`);
        
    }

    getDislikeCountByPostId(id: string): Observable<number> {
        return this.http.get<number>(this.apiUrl + `post/dislikes/${ id }`);
        
    }


    getPostsByFollowedUser(id: string): Observable<Post[]>{
        return this.http.get<Post[]>(this.apiUrl + `post/followedByUser/${id}`);
    } 

    getReactionByUserAndPostId(postId: string, userId: string): Observable<UserReaction>{
        return this.http.get<UserReaction>(this.apiUrl + `post/react/${postId}/${userId}`);
        
    } 

    createReaction(reaction: UserReaction): Observable<UserReaction> {
        let url = this.apiUrl + `post/react/${reaction.postId}`;
        var jsonReaction = JSON.stringify(reaction)
        return this.http.post<UserReaction>(url, 
        //     {
        //     Id: reaction.id,
        //     UserId: reaction.userId,
        //     PostId: reaction.postId,
        //     Reaction: reaction.reaction
        // },
        jsonReaction,
        this.httpOptions);
    }
    
    deleteReaction(reactionId: string | null): Observable<any> {
        return this.http.delete(this.apiUrl + `post/react/${reactionId}`);
    }
    
    uploadFileAndData(file: File, title: string, userId: string){
        const formData = new FormData();
        formData.append('title', title);
        formData.append('media',file);
        formData.append('userId', userId);
        formData.append('mediaType', file.type);
        return this.http.post<any>(this.apiUrl + "post/publish", formData);
      }
      
}