import { Box, Button, Grid, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from '@mui/material';
import { Add, Delete, Remove } from '@mui/icons-material';
import { LoadingButton } from '@mui/lab';
import { currencyFormat } from "../../app/utils/currency-util";
import BasketSummary from './BasketSummary';
import { Link } from 'react-router-dom';
import { useAppDispatch, useAppSelector } from '../../app/store/configure-store';
import { addBasketItemAsync, removeBasketItemAsync } from './basket-slice';

export default function BasketPage() {
    const {basket, status} = useAppSelector(state => state.basket);

	const dispatch = useAppDispatch();

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
									<LoadingButton loading={status.includes('pendingRemoveItem' + item.productId + 'rem')} onClick={() => dispatch(removeBasketItemAsync({productId: item.productId, quantity: 1, name: 'rem'}))} color='error'>
										<Remove>
											
										</Remove>
									</LoadingButton>
									<LoadingButton loading={status.includes('pendingAddItem' + item.productId)} onClick={() => dispatch(addBasketItemAsync({productId: item.productId}))} color='secondary'>
										<Add>
											
										</Add>
									</LoadingButton>
								</TableCell>
								<TableCell align="right">{currencyFormat(item.price * item.quantity)}</TableCell>
								<TableCell align="right">
									<LoadingButton loading={status.includes('pendingRemoveItem' + item.productId + 'del')} onClick={() => dispatch(removeBasketItemAsync({productId: item.productId, quantity: item.quantity, name: 'del'}))} color='error'>
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
