export interface Product {
    id: string;
    brand: string;
    name: string;
    type: string;
    price: number;
    quantityInStock: number;
    description?: string;
    pictureUrl?: string;
}