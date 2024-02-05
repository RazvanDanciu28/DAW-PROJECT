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

  export interface UserDetails {
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
    password: string;
  }

  export interface User {
    email: string;
    password: string;
  }