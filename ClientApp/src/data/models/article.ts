import { ArticleLike } from "./articleLike";
import { UserSummary } from "./user";

export class Article {
  id: number;

  autor: UserSummary;

  titel: string;
  
  datum: Date;

  inhalt: string;

  beitragLikes: ArticleLike[];

  kommentare: Comment[]; 

  bilder: string[];

  get likes(): number{
    return this.beitragLikes.filter(bl => !bl.istDislike).length;
  }

  get dislikes(): number{
    return this.beitragLikes.filter(bl => bl.istDislike).length;
  }
}

export class ArticleSummary{
  id: number;

  autor: UserSummary;

  titel: string;

  inhaltPreview: string;

  datum: Date;

  beitragLikes: ArticleLike[]; //like-objects please.
}
