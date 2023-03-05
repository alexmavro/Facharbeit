import { Hashtag } from "src/Models/Hashtag";
import { Post } from "src/Models/Post";

export interface IDataService{
    getPosts(accountId: string): Post[];
    getTrendingHashtags(): Hashtag[];
}