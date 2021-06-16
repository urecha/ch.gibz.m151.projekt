export class Article {
  autor: string;

  titel: string;

  inhalt: string;

  beitragLikes: number;
}

export class ArticleSummary{
  id: string;

  autor: string;

  titel: string;

  inhaltPreview: string;

  bilder: Buffer[];

  datum: Date;

  beitragLikes: number; //like objects please.
}
