import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Following } from "src/Models/Following";
import { MinimalUser } from "src/Models/MinimalUser";
import { Post } from "src/Models/Post";
import { User } from "src/Models/User";

@Injectable()
export class UserService{
    private apiUrl: string = "https://localhost:7211/api/";
    
    constructor(private http: HttpClient) {
        
    }

    getUserbyId(id: string): Observable<User> {
        return this.http.get<User>(this.apiUrl + `user/${ id }`);
    }

    getMinimalUserbyId(id: string): Observable<MinimalUser> {
        return this.http.get<MinimalUser>(this.apiUrl + `user/${ id }`);
    }

    getFollowers(id: string): Observable<Following[]> {
        return this.http.get<Following[]>(this.apiUrl + `user/Followers/${ id }`);
    }

    getFollowings(id: string): Observable<Following[]> {
        return this.http.get<Following[]>(this.apiUrl + `user/Followings/${ id }`);
    }

    FollowUser(following: Following): Observable<Following> {
        return this.http.post<Following>(this.apiUrl + `user/Follow/`, {
            Id: following.id,
            UserId: following.userId,
            FollowerId: following.followerId
        });
    }

    getUser(): string{
        return "84AE68CC-A22C-4622-BF84-DB43A530DC7A";
    }
    
}