import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { Basket } from "../../app/models/basket";
import agent from "../../app/api/agent";

interface BasketState {
    basket: Basket | null;
    status: string;
}

const initialState: BasketState = {
    basket: null,
    status: 'idle'
}

export const addBasketItemAsync = createAsyncThunk<Basket, {productId: string, quantity?: number}>(
    'basket/addBasketItemAsync',
    async ({productId, quantity = 1}, thunkApi) => {
        try {
            return await agent.basket.addItem(productId, quantity);
        } catch (error: any) {
            return thunkApi.rejectWithValue({error: error.data});
        }
    }
)

export const removeBasketItemAsync = createAsyncThunk<void, {productId: string, quantity: number, name?: string}>(
    'basket/removeBasketItemAsync',
    async ({productId, quantity}, thunkApi) => {
        try {
            return await agent.basket.removeItem(productId, quantity);
        } catch (error: any) {
            return thunkApi.rejectWithValue({error: error.data});
        }
    }
)

export const basketSlice = createSlice({
    name: 'basket',
    initialState,
    reducers: {
        setBasket: (state, action) => {
            state.basket = action.payload
        },
        removeItem: (state, action) => {
            const {productId, quantity} = action.payload;
            const itemIndex = state.basket?.items.findIndex(i => i.productId === productId);

            if (itemIndex === -1 || itemIndex === undefined) return;

            state.basket!.items[itemIndex].quantity -= quantity;

            if (state.basket?.items[itemIndex].quantity === 0) 
                state.basket.items.splice(itemIndex, 1);
        }
    },
    extraReducers: (builder => {
        builder.addCase(addBasketItemAsync.pending, (state, action) => {
            state.status = 'pendingAddItem' + action.meta.arg.productId;
        }),
        builder.addCase(addBasketItemAsync.fulfilled, (state, action) => {
            state.basket = action.payload;
            state.status = 'idle';
        }),
        builder.addCase(addBasketItemAsync.rejected, (state) => {
            state.status = 'idle';
        }),
        builder.addCase(removeBasketItemAsync.pending, (state, action) => {
            state.status = 'pendingRemoveItem' + action.meta.arg.productId + action.meta.arg.name;
        }),
        builder.addCase(removeBasketItemAsync.fulfilled, (state, action) => {
            const {productId, quantity} = action.meta.arg;
            const itemIndex = state.basket?.items.findIndex(i => i.productId === productId);

            if (itemIndex === -1 || itemIndex === undefined) return;

            state.basket!.items[itemIndex].quantity -= quantity;

            if (state.basket?.items[itemIndex].quantity === 0) 
                state.basket.items.splice(itemIndex, 1);

            state.status = 'idle';
        }),
        builder.addCase(removeBasketItemAsync.rejected, (state) => {
            state.status = 'idle';
        })
    })
});

export const {setBasket} = basketSlice.actions;