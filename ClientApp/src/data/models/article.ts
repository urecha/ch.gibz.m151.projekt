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

}

export class ArticleSummary{
  id: number;

  autor: UserSummary;

  titel: string;

  inhaltPreview: string;

  datum: Date;

  beitragLikes: ArticleLike[]; //like-objects please.
}
