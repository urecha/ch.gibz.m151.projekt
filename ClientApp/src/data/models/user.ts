import { IUser } from "src/api-authorization/authorize.service";
import { ArticleLike } from "./articleLike";
import { CommentLike } from "./commentLike";

export class User implements IUser{
    id: string;

    name: string;

    beitragLikes: ArticleLike[];

    commentLikes: CommentLike[];
}

export class UserSummary implements IUser{
    id: string;

    name: string;
}