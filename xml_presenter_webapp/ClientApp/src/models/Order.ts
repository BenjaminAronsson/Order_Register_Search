
export class Order {
    date: string;
    OrderNumber: string;
    OrderLineNumber: string;
    products: Product[];
    OrderDate: string;
    CustomerName: string;
    CustomerNumber: string;
  }

  export class Product {
    ProductNumber: string;
    Quantity: string;
    Name: string;
    Description: string;
    Price: string;
    ProductGroup: string;
  }