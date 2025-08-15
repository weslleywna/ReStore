import { ProductOrderEnum } from "../enums/product-order.enum";

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

export interface ProductParams{
    orderBy: ProductOrderEnum;
    searchTerm?: string;
    types: string[];
    brands: string[];
	pageNumber: number;
    pageSize: number;
}
