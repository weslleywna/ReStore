/* eslint-disable @typescript-eslint/no-explicit-any */
import {
  createAsyncThunk,
  createEntityAdapter,
  createSlice
} from '@reduxjs/toolkit';
import agent from '../../app/api/agent';
import { catalogStatusEnum } from '../../app/enums/catalog-status.enum';
import { Product, ProductParams } from '../../app/models/product';
import { RootState } from '../../app/store/configure-store';
import { getQueryStringParams } from '../../app/utils/param-util';
import { ProductOrderEnum } from '../../app/enums/product-order.enum';
import { MetaData } from '../../app/models/pagination';

interface CatalogState {
  productsLoaded: boolean;
  filtersLoaded: boolean;
  status: string;
  brands: string[];
  types: string[];
  productParams: ProductParams;
  metaData: MetaData | null;
}

const productAdapter = createEntityAdapter<Product>();

export const fetchProductsAsync = createAsyncThunk<
  Product[],
  void,
  { state: RootState }
>('catalog/fetchProductsAsync', async (_, thunkAPI) => {
  const params = getQueryStringParams(
    thunkAPI.getState().catalog.productParams
  );
  try {
    const response = await agent.catalog.list(params);
    thunkAPI.dispatch(setMetaData(response.metaData));
    return response.items;
  } catch (error: any) {
    return thunkAPI.rejectWithValue({ error: error.data });
  }
});

export const fetchProductAsync = createAsyncThunk<Product, string>(
  'catalog/fetchProductAsync',
  async (productId, thunkAPI) => {
    try {
      return await agent.catalog.details(productId);
    } catch (error: any) {
      return thunkAPI.rejectWithValue({ error: error.data });
    }
  }
);

export const fetchFiltersAsync = createAsyncThunk(
  'catalog/fetchFiltersAsync',
  async (_, thunkAPI) => {
    try {
      return await agent.catalog.filters();
    } catch (error: any) {
      return thunkAPI.rejectWithValue({ error: error.data });
    }
  }
);

function initParams() {
  return {
    pageNumber: 1,
    pageSize: 9,
    orderBy: ProductOrderEnum.CREATED_AT_DESC,
    brands: [],
    types: []
  };
}

export const catalogSlice = createSlice({
  name: 'catalog',
  initialState: productAdapter.getInitialState<CatalogState>({
    productsLoaded: false,
    filtersLoaded: false,
    status: catalogStatusEnum.IDLE,
    brands: [],
    types: [],
    productParams: initParams(),
    metaData: null
  }),
  reducers: {
    setProductParams: (state, action) => {
      state.productsLoaded = false;
      state.productParams = {
        ...state.productParams,
        ...action.payload,
        pageNumber: 1
      };
    },
    setPageNumber: (state, action) => {
      state.productsLoaded = false;
      state.productParams = {
        ...state.productParams,
        ...action.payload
      };
    },
    setMetaData: (state, action) => {
      state.metaData = action.payload;
    },
    resetProductParams: (state) => {
      state.productParams = initParams();
    }
  },
  extraReducers: (builder) => {
    builder.addCase(fetchProductsAsync.pending, (state) => {
      state.status = catalogStatusEnum.PENDING_FETCH_PRODUCTS;
    });
    builder.addCase(fetchProductsAsync.fulfilled, (state, action) => {
      productAdapter.setAll(state, action.payload);
      state.status = catalogStatusEnum.IDLE;
      state.productsLoaded = true;
    });
    builder.addCase(fetchProductsAsync.rejected, (state, action) => {
      console.log(action);
      state.status = catalogStatusEnum.IDLE;
    });

    builder.addCase(fetchProductAsync.pending, (state) => {
      state.status = catalogStatusEnum.PENDING_FETCH_PRODUCT;
    });
    builder.addCase(fetchProductAsync.fulfilled, (state, action) => {
      productAdapter.upsertOne(state, action.payload);
      state.status = catalogStatusEnum.IDLE;
    });
    builder.addCase(fetchProductAsync.rejected, (state, action) => {
      console.log(action);
      state.status = catalogStatusEnum.IDLE;
    });

    builder.addCase(fetchFiltersAsync.pending, (state) => {
      state.status = catalogStatusEnum.PENDING_FETCH_FILTERS;
    });
    builder.addCase(fetchFiltersAsync.fulfilled, (state, action) => {
      state.brands = action.payload.brands;
      state.types = action.payload.types;
      state.status = catalogStatusEnum.IDLE;
      state.filtersLoaded = true;
    });
    builder.addCase(fetchFiltersAsync.rejected, (state, action) => {
      console.log(action);
      state.status = catalogStatusEnum.IDLE;
    });
  }
});

export const productSelectors = productAdapter.getSelectors(
  (state: RootState) => state.catalog
);

export const {
  setProductParams,
  resetProductParams,
  setMetaData,
  setPageNumber
} = catalogSlice.actions;