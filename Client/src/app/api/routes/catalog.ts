import requests from '../requests';

const endpointPrefix = 'products';

const catalog = {
	list: () =>
		requests.get(`${endpointPrefix}`).catch((error) => console.log(error)),
	details: (id: string | undefined) =>
		requests
			.get(`${endpointPrefix}/${id}`)
			.catch((error) => console.log(error)),
};

export default catalog;
