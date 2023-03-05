import { Injectable } from "@angular/core";
import { Hashtag } from "src/Models/Hashtag";
import { Post } from "src/Models/Post";
import { IDataService } from "./IDataService";

@Injectable()
export class MockDataService implements IDataService{
    getPosts(accountId: string): Post[] {
        throw new Error("Method not implemented.");
    }
    // getPosts(accountId: string): Post[] {
    //     return [
    //         new Post("decription yeah", "I am stupid", "2355BG-436A-23456",
    //         1000, "https://google.com"),
    //         new Post("decription yeah", "I am stupid", "2355BG-436A-23456",
    //         1000, "https://google.com"),
    //         new Post("decription yeah", "I am stupid", "2355BG-436A-23456",
    //         1000, "https://google.com"),
    //         new Post("decription yeah", "I am stupid", "2355BG-436A-23456",
    //         1000, "https://google.com"),
    //         new Post("decription yeah", "I am stupid", "2355BG-436A-23456",
    //         1000, "https://google.com"),
    //         new Post("decription yeah", "I am stupid", "2355BG-436A-23456",
    //         1000, "https://google.com"),
    //         new Post("decription yeah", "I am stupid", "2355BG-436A-23456",
    //         1000, "https://google.com"),
    //         new Post("decription yeah", "I am stupid", "2355BG-436A-23456",
    //         1000, "https://google.com")

    //     ]
    // }

    getTrendingHashtags(): Hashtag[] {
        return [
            new Hashtag("bdflgkj34", "#Kosakenzipfel"),
            new Hashtag("gerge", "#BitteLassMichInFrieden"),
            new Hashtag("bdflkggr3j34", "#GibMirLikes"),
            new Hashtag("bdflgkrfj34", "#IchBinEineBiene"),
            new Hashtag("bdfltefkj34", "#HEHEHEHA"),
            new Hashtag("bdffgglkj34", "#Hashtag"),
            new Hashtag("bdfl3g4kj34", "#Tomatensalat"),
            new Hashtag("bdf5lkj34", "#HEHEHEHA"),
            
        ]
    }

    getFiveTrendingHashtags(): Hashtag[] {
        return [
            new Hashtag("bdflgkj34", "Kosakenzipfel"),
            new Hashtag("gerge", "#BitteLassMichInFrieden"),
            new Hashtag("bdflkggr3j34", "#GibMirLikes"),
            new Hashtag("bdflgkrfj34", "#IchBinEineBiene"),
            new Hashtag("bdfltefkj34", "#HEHEHEHA"),
            
        ]
    }
    
}