import LoadingComponent from '../../app/layout/LoadingComponent';
import { useAppDispatch, useAppSelector } from '../../app/store/configure-store';
import ProductList from './ProductList';
import { useEffect } from 'react';

export default function Catalog() {
	const products = useAppSelector(productSelectors.selectAll);
	const {productsLoaded, status} = useAppSelector(state => state.catalog);
	const dispatch = useAppDispatch();

	useEffect(() => {
		if (!productsLoaded) dispatch(fetchProductsAsync());
	}, [productsLoaded, dispatch]);

	if (status.includes('pending'))
		return (
			<LoadingComponent message="Loading products..."></LoadingComponent>
		);

	return (
		<>
			<ProductList products={products}></ProductList>
		</>
	);
}
