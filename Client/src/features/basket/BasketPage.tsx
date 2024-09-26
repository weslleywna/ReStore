import { Box, Button, Grid, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from '@mui/material';
import { Add, Delete, Remove } from '@mui/icons-material';
import { useState } from 'react';
import agent from '../../app/api/agent';
import { LoadingButton } from '@mui/lab';
import { currencyFormat } from "../../app/utils/currency-util";
import BasketSummary from './BasketSummary';
import { Link } from 'react-router-dom';
import { useAppDispatch, useAppSelector } from '../../app/store/configure-store';
import { removeItem, setBasket } from './basket-slice';

export default function BasketPage() {
    const {basket} = useAppSelector(state => state.basket);

	const dispatch = useAppDispatch();

	const [status, setStatus] = useState({
		loading: false,
		name: ''
	});

	function handleAddItem(productId: string, name: string) {
		setStatus({loading: true, name});
		agent.basket.addItem(productId).then(basket => dispatch(setBasket(basket))).finally(() => setStatus({loading: true, name: ''}));
	}

	function handleRemoveItem(productId: string, quantity: number, name: string) {
		setStatus({loading: true, name});
		agent.basket.removeItem(productId, quantity).then(() => dispatch(removeItem({productId, quantity}))).finally(() => setStatus({loading: true, name: ''}));
	}
	
	if (!basket)
		return <Typography variant="h3">Your basket is empty</Typography>;

	return (
		<>
			<TableContainer component={Paper}>
				<Table sx={{ minWidth: 650 }} aria-label="simple table">
					<TableHead>
						<TableRow>
							<TableCell>Product</TableCell>
							<TableCell align="right">Price</TableCell>
							<TableCell align="right">Quantity</TableCell>
							<TableCell align="right">Subtotal</TableCell>
                            <TableCell align="right"></TableCell>
						</TableRow>
					</TableHead>
					<TableBody>
						{basket.items.map((item) => (
							<TableRow
								key={item.productId}
								sx={{
									'&:last-child td, &:last-child th': {
										border: 0,
									},
								}}
							>
								<TableCell component="th" scope="row">
									<Box display='flex' alignItems='center'> 
										<img src={item.pictureUrl} alt={item.name} style={{height: 50, marginRight: 20}} />	
									</Box>
								</TableCell>
								<TableCell align="right">
									{currencyFormat(item.price)}
								</TableCell>
								<TableCell align="right">{item.quantity}
									<LoadingButton loading={status.loading && status.name === 'rem' + item.productId} onClick={() => handleRemoveItem(item.productId, 1, 'rem' + item.productId)} color='error'>
										<Remove>
											
										</Remove>
									</LoadingButton>
									<LoadingButton loading={status.loading && status.name === 'add' + item.productId} onClick={() => handleAddItem(item.productId, 'add' + item.productId)} color='secondary'>
										<Add>
											
										</Add>
									</LoadingButton>
								</TableCell>
								<TableCell align="right">{currencyFormat(item.price * item.quantity)}</TableCell>
								<TableCell align="right">
									<LoadingButton loading={status.loading && status.name === 'del' + item.productId} onClick={() => handleRemoveItem(item.productId, item.quantity, 'del' + item.productId)} color='error'>
                                        <Delete>

                                        </Delete>
                                    </LoadingButton>
								</TableCell>
							</TableRow>
						))}
					</TableBody>
				</Table>
			</TableContainer>
			<Grid container>
				<Grid item xs={6}></Grid>
				<Grid item xs={6}>
					<BasketSummary></BasketSummary>
					<Button component={Link} to='/checkout' variant='contained' size='large' fullWidth>

					</Button>
				</Grid>
			</Grid>
		</>
	);
}
