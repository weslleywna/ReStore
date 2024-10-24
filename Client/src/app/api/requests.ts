import axios, { AxiosError, AxiosResponse } from 'axios';
import { toast } from 'react-toastify';
import { router } from '../router/Router';

axios.defaults.baseURL = 'http://localhost:5000/api/';
axios.defaults.withCredentials = true;

const responseBody = (response: AxiosResponse) => response.data;

axios.interceptors.response.use(
	(response) => {
		return response;
	},
	(error: AxiosError) => {
		const { data, status } = error.response as AxiosResponse;

		switch (status) {
			case 400:
				if (data.errors) throw Object.values(data.errors);
				toast.error(data.title);
				break;
			case 401:
				toast.error(data.title);
				break;
			case 404:
				router.navigate('/not-found');
				break;
			case 500:
				router.navigate('/server-error', { state: { error: data } });
				break;
			default:
				break;
		}

		return Promise.reject(error.response);
	}
);

const requests = {
	get: (url: string, params?: URLSearchParams) => axios.get(url, {params}).then(responseBody),
	post: (url: string, body: object) =>
		axios.post(url, body).then(responseBody),
	put: (url: string, body: object) => axios.put(url, body).then(responseBody),
	delete: (url: string) => axios.delete(url).then(responseBody),
};

export default requests;
