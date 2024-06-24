import axios, { AxiosError, AxiosResponse } from 'axios';

axios.defaults.baseURL = 'http://localhost:5000/api/';

const responseBody = (response: AxiosResponse) => response.data;

axios.interceptors.response.use(
	(response) => {
		return response;
	},
	(error: AxiosError) => {
		console.log('caught by interceptor');
		return Promise.reject(error.response);
	}
);

const requests = {
	get: (url: string) => axios.get(url).then(responseBody),
	post: (url: string, body: object) =>
		axios.post(url, body).then(responseBody),
	put: (url: string, body: object) => axios.put(url, body).then(responseBody),
	delete: (url: string) => axios.delete(url).then(responseBody),
};

export default requests;
