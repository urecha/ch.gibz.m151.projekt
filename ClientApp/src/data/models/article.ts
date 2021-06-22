import { ArticleLike } from "./articleLike";
import { Comment } from "./comment";
import { UserSummary } from "./user";

export class Article {
  id: number;

  autor: UserSummary;

  titel: string;
  
  datum: Date;

  inhalt: string;

  beitragLikes: ArticleLike[];

  kommentare: Comment[]; 

  get likes(): number{
    if(!this.beitragLikes || !this.beitragLikes.length) return 0;
    return this.beitragLikes.filter(bl => !bl.istDislike).length;
  }

  get dislikes(): number{
    if(!this.beitragLikes || !this.beitragLikes.length) return 0;
    return this.beitragLikes.filter(bl => bl.istDislike).length;
  }
}

export class ArticleSummary{
  id: number;
  
  autor: UserSummary;
  
  titel: string;
  
  inhaltPreview: string;
  
  datum: Date;
  
  beitragLikes: ArticleLike[];
  
  get likes(): number{
    return this.beitragLikes.filter(bl => !bl.istDislike).length;
  }
  
  get dislikes(): number{
    return this.beitragLikes.filter(bl => bl.istDislike).length;
  }
}
