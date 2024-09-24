import {
	Divider,
	Grid,
	Table,
	TableBody,
	TableCell,
	TableContainer,
	TableRow,
	Typography,
} from '@mui/material';
import { useEffect, useState } from 'react';
import { Navigate, useParams } from 'react-router-dom';
import { Product } from '../../app/models/product';
import agent from '../../app/api/agent';
import LoadingComponent from '../../app/layout/LoadingComponent';

export default function ProductDetails() {
	const {basket, setBasket, removeItem} = useStoreContext();
	const { id } = useParams<{ id: string }>();
	const [product, setProduct] = useState<Product | null>(null);
	const [loading, setLoading] = useState(true);
	const [quantity, setQuantity] = useState(1);
	const [submitting, setSubmitting] = useState(false);
	const item = basket?.items.find(i => i.productId === product?.Id);

	useEffect(() => {
		if (item) setQuantity(item.quantity);
		id &&
			agent.catalog
				.details(id)
				.then((response) => setProduct(response))
				.finally(() => setLoading(false));
	}, [id, item]);

	function handleInputChange(event: ChangeEvent<HTMLInputElement>) {
		if (parseInt(event.currentTarget.value) > 0) {
			setQuantity(parseInt(event.currentTarget.value));
		}
	}

	function handleUpdateCart() {
		setSubmitting(true);
		if (!item || quantity > item.quantity) {
			const updatedQuantity = item ? quantity - item.quantity : quantity;
			agent.basket.addItem(product!.id, updatedQuantity).then(basket => setBasket(basket)).finally(() => setSubmitting(false));
		} 
		else {
			const updatedQuantity = item.quantity - quantity;
			agent.basket.removeItem(product!.id, updatedQuantity).then(() => removeItem(product!.id, updatedQuantity)).finally(() => setSubmitting(false));
		}
	}

	if (loading)
		return (
			<LoadingComponent message="Loading product..."></LoadingComponent>
		);

	if (!product) return <Navigate replace to="/not-found" />;

	return (
		<Grid container spacing={6}>
			<Grid item xs={6}>
				<img
					src={product.pictureUrl}
					alt={product.name}
					style={{ width: '100%' }}
				></img>
			</Grid>
			<Grid item xs={6}>
				<Typography variant="h3">{product.name}</Typography>
				<Divider sx={{ mb: 2 }}></Divider>
				<Typography variant="h4" color="secondary">
					{currencyFormat(product.price)}
				</Typography>
				<TableContainer>
					<Table>
						<TableBody>
							<TableRow>
								<TableCell>Name</TableCell>
								<TableCell>{product.name}</TableCell>
							</TableRow>
							<TableRow>
								<TableCell>Description</TableCell>
								<TableCell>{product.description}</TableCell>
							</TableRow>
							<TableRow>
								<TableCell>Type</TableCell>
								<TableCell>{product.type}</TableCell>
							</TableRow>
							<TableRow>
								<TableCell>Brand</TableCell>
								<TableCell>{product.brand}</TableCell>
							</TableRow>
							<TableRow>
								<TableCell>Quantity in stock</TableCell>
								<TableCell>{product.quantityInStock}</TableCell>
							</TableRow>
						</TableBody>
					</Table>
				</TableContainer>
				<Grid container spacing={2}>
					<Grid item xs={6}>
						<TextField onChange={handleInputChange} variant='outlined' type='number' label='Quantity in Cart' fullWidth value={quantity}>

						</TextField>
					</Grid>
					<Grid item xs={6}>
						<LoadingButton disabled={item?.quantity === quantity} loading={submitting} onClick={handleUpdateCart} sx={{height: '55px'}} color='primary' size='large' variant='contained' fullWidth>
							{item ? 'Update Quantity' : 'Add to Cart'}
						</LoadingButton>
					</Grid>
				</Grid>
			</Grid>
		</Grid>
	);
}
