import React from 'react';
import ReactDOM from 'react-dom/client';
import './app/layout/styles.scss';
import { RouterProvider } from 'react-router-dom';
import { router } from './app/router/Router.tsx';
import { Provider } from 'react-redux';
import { store } from './app/store/configure-store.ts';

ReactDOM.createRoot(document.getElementById('root')!).render(
	<React.StrictMode>
			<Provider store={store}>
				<RouterProvider router={router} />
			</Provider>
	</React.StrictMode>
);
