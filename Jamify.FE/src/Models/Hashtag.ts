export class Hashtag{
    public HashTagId: string = "";
    public Title: string = ""    

    constructor(hashTagId: string, title: string) {
        this.HashTagId = hashTagId;
        this.Title = title;
    }
}