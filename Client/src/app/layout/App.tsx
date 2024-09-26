import { useEffect, useState } from 'react';
import Header from './Header';
import {
	Container,
	CssBaseline,
	ThemeProvider,
	createTheme,
} from '@mui/material';
import { Outlet } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';
import { getCookie } from '../utils/cookie-utils';
import agent from '../api/agent';
import LoadingComponent from './LoadingComponent';
import { useAppDispatch } from '../store/configure-store';
import { setBasket } from '../../features/basket/basket-slice';

function App() {
	const dispatch = useAppDispatch();
	const [loading, setLoading] = useState(true);

	useEffect(() => {
		const buyerId = getCookie('buyerId');
		if (buyerId) {
			agent.basket
				.get()
				.then((basket) => dispatch(setBasket(basket)))
				.finally(() => setLoading(false));
		} else {
			setLoading(false);
		}
	}, [dispatch]);

	const [darkMode, setDarkMode] = useState(false);

	const paletteType = darkMode ? 'dark' : 'light';

	const theme = createTheme({
		palette: {
			mode: paletteType,
			background: {
				default: paletteType === 'light' ? '#eaeaea' : '#121212',
			},
		},
	});

	function handleThemeChange() {
		setDarkMode(!darkMode);
	}

	if (loading) return <LoadingComponent message='Initializing app...'></LoadingComponent>

	return (
		<>
			<ThemeProvider theme={theme}>
				<ToastContainer
					position="bottom-right"
					hideProgressBar
					theme="colored"
				/>
				<CssBaseline />
				<Header
					darkMode={darkMode}
					handleThemeChange={handleThemeChange}
				/>
				<Container>
					<Outlet />
				</Container>
			</ThemeProvider>
		</>
	);
}

export default App;
