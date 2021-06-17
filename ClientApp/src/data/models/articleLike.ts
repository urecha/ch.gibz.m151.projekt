import { UserSummary } from "./user";

export class ArticleLike{
    id: number;

    istDislike: boolean;

    articleId: number;

    user: UserSummary;
}

export class ArticleLikePreview{
    id: number;

    istDislike: boolean;

    articleId: number;

    aricleTitel: string;

    user: UserSummary;
}