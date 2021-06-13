import { User } from "./user";

export class Comment {
  id: string;

  articleId: string;

  autor: User;

  kommentarLikes: number;

  kommentarDislikes: number;
}
