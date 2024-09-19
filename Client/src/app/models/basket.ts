import { BasketItem } from "./basket-item";

export interface Basket {
	id: string;
	buyerId: string;
	items: BasketItem[];
}

