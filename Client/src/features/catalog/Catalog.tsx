import {
	Box,
	Checkbox,
	FormControl,
	FormControlLabel,
	FormGroup,
	Grid,
	Pagination,
	Paper,
	Radio,
	RadioGroup,
	TextField,
	Typography,
} from '@mui/material';
import LoadingComponent from '../../app/layout/LoadingComponent';
import {
	useAppDispatch,
	useAppSelector,
} from '../../app/store/configure-store';
import {
	productSelectors,
	fetchProductsAsync,
	fetchFiltersAsync,
} from './catalog-slice';
import ProductList from './ProductList';
import { useEffect } from 'react';

const sortOptions = [
	{ value: 'nameAsc', label: 'Alphabetical - A to Z' },
	{ value: 'nameDesc', label: 'Alphabetical - Z to A' },
	{ value: 'priceAsc', label: 'Price - Low to High' },
	{ value: 'priceDesc', label: 'Price - High to Low' },
	{ value: 'UpdatedAtAsc', label: 'Oldest First' },
	{ value: 'UpdatedAtDesc', label: 'Newest First' },
];

export default function Catalog() {
	const products = useAppSelector(productSelectors.selectAll);
	const { productsLoaded, status, filtersLoaded, brands, types } = useAppSelector(
		(state) => state.catalog
	);
	const dispatch = useAppDispatch();

	useEffect(() => {
		if (!productsLoaded) dispatch(fetchProductsAsync());
	}, [productsLoaded, dispatch]);

	useEffect(() => {
		if (!filtersLoaded) dispatch(fetchFiltersAsync());
	}, [filtersLoaded, dispatch]);

	if (status.includes('pending'))
		return (
			<LoadingComponent message="Loading products..."></LoadingComponent>
		);

	return (
		<>
			<Grid container spacing={4}>
				<Grid item xs={3}>
					<Paper sx={{ mb: 2 }}>
						<TextField
							label="Search Products"
							variant="outlined"
							fullWidth
						></TextField>
					</Paper>
					<Paper sx={{ mb: 2, p: 2 }}>
						<FormControl>
							<RadioGroup
								aria-labelledby="demo-radio-buttons-group-label"
								defaultValue="female"
								name="radio-buttons-group"
							>
								{sortOptions.map(({ value, label }) => (
									<FormControlLabel
										value={value}
										control={<Radio />}
										label={label}
										key={value}
									/>
								))}
							</RadioGroup>
						</FormControl>
					</Paper>

					<Paper sx={{ mb: 2, p: 2 }}>
						<FormGroup>
							{brands.map((brand) => (
								<FormControlLabel
								control={<Checkbox />}
								label={brand}
								key={brand}
							/>
							))}
						</FormGroup>
					</Paper>

					<Paper sx={{ mb: 2, p: 2 }}>
						<FormGroup>
							{types.map((type) => (
								<FormControlLabel
								control={<Checkbox />}
								label={type}
								key={type}
							/>
							))}
						</FormGroup>
					</Paper>

				</Grid>
				<Grid item xs={9}>
					<ProductList products={products}></ProductList>
				</Grid>
				<Grid item xs={3}>
					
				</Grid>
				<Grid item xs={9} sx={{mb: 2}}>
					<Box display='flex' justifyContent='space-between' alignItems='center'>
						<Typography>
							Displaying 1-6 of 20 items
						</Typography>
						<Pagination color='secondary' size='large' count={10} page={2}>

						</Pagination>
					</Box>
				</Grid>
			</Grid>
		</>
	);
}
