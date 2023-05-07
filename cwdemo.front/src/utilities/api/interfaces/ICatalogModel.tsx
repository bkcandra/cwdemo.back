export interface ICreateCatalog {
  name: string;
  description: string;
  price: number;
  type: number;
  storeId: number;
}

export interface IUpdateCatalog extends ICreateCatalog {
  id: number;
  active: boolean;
}

export interface ICatalog extends IUpdateCatalog {
  storeName: string;
}


export interface IAction {
  slug: string
  apimethod:string
}