export class Post {
    public id: string = "";
    public title: string = "";
    public userId: string = "";
    public likeCount: number = 0;
    public dislikeCount: number = 0;
    public media: File | null = null;
    public createdAt?: Date;
    public mediaType?: string;

    constructor() { }
    // constructor(postId: string ,title: string, userId: string, likes: number, dislikes: number, media: File, createdAt: Date, mediaType: string) {
    //     this.postId = postId;
    //     this.title = title;
    //     this.userId = userId;
    //     this.likes = likes;
    //     this.media= media;
    //     this.dislikes = dislikes;
    //     this.createdAt = createdAt;
    //     this.mediaType = mediaType;
    // }
}