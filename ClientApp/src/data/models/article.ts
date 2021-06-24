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
}

export class ArticleSummary{
  id: number;
  
  autor: UserSummary;
  
  titel: string;
  
  inhaltPreview: string;
  
  datum: Date;
  
  beitragLikes: ArticleLike[];

}
