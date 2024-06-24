import requests from '../requests';

const endpointPrefix = 'products';

const catalog = {
	list: () => requests.get(`${endpointPrefix}`),
	details: (id: string | undefined) =>
		requests.get(`${endpointPrefix}/${id}`),
};

export default catalog;
