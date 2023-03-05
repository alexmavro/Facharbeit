export class User{
    public id: string = "";
    public email: string = "";
    public username: string = "";
    public firstName: number = 0;
    public lastName: number = 0;
    public createdAt?: Date;
    public birthday?: Date;
    public posts: string[] = []
    public followers: string[] = [];
    public followings: string[] = [];
    public password: string ="";

}