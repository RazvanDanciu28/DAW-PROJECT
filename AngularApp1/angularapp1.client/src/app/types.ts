export default class Constants {
    static readonly API = "http://localhost:5256/app";
}

export interface Product {
    productId: string;
    productName: string;
    gender: string;
    color: string;
    description: string;
    price: number;
    size: string;
  }