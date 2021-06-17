import { CommentLike } from "./commentLike";
import { UserSummary } from "./user";

export class Comment{
    id: number;

    articleId: number;

    titel: string;

    inhalt: string;

    datum: Date;

    autor: UserSummary;

    likes: CommentLike[];
}
