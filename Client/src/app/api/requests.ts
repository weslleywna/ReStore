import axios, { AxiosError, AxiosResponse } from 'axios';
import { toast } from 'react-toastify';
import { router } from '../router/Router';
import { PaginatedResponse } from '../models/pagination';

const sleep = () => new Promise((resolve) => setTimeout(resolve, 250));

axios.defaults.baseURL = 'http://localhost:5000/api/';
axios.defaults.withCredentials = true;

const responseBody = (response: AxiosResponse) => response.data;

axios.interceptors.response.use(
  async (response) => {
    await sleep();
    const pagination = response.headers['pagination'];
    if (pagination) {
      response.data = new PaginatedResponse(
        response.data,
        JSON.parse(pagination)
      );
    }
    return response;
  },
  (error: AxiosError) => {
    const { data, status } = error.response as AxiosResponse;
    const showToast = (message: string) => toast.error(message);

    switch (status) {
      case 400:
        if (data.errors) {
          throw Object.values(data.errors).flat();
        }
        showToast(data.title);
        break;
      case 401:
        showToast(data.title);
        break;
      case 404:
        router.navigate('/not-found');
        break;
      case 500:
        router.navigate('/server-error', { state: { error: data } });
        showToast(data.title);
        break;
      default:
        break;
    }
    return Promise.reject(error.response);
  }
);

const requests = {
  get: (url: string, params?: URLSearchParams) =>
    axios.get(url, { params }).then(responseBody),
  post: (url: string, body: object) => axios.post(url, body).then(responseBody),
  put: (url: string, body: object) => axios.put(url, body).then(responseBody),
  delete: (url: string) => axios.delete(url).then(responseBody)
};

export default requests;