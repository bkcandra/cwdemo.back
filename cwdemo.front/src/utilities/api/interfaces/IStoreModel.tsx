import { ParenthesizedExpression } from "typescript";

export interface ICreateStore {
  name: string;
  location: string;
  active: boolean;
}

export interface IStore extends ICreateStore {
  id: number;
}