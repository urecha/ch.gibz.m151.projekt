import { User } from "./user";

export class Article {
  id: string;

  autor: User;

  titel: string;

  inhalt: string;

  beitragLikes: number;

  beitragDislikes: number;
}
