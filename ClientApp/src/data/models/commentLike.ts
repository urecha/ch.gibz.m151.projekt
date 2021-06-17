import { UserSummary } from "./user";

export class CommentLike{
    id: number;

    commentId: number;

    articleId: number;

    istDislike: boolean;

    user: UserSummary;
}

export class CommentLikePreview{
    id: number;

    commentId: number;

    commentPreview: string;

    articleId: number;

    articleTitel: string;

    istDislike: boolean;
}