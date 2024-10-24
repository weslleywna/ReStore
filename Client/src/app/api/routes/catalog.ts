import { handleError } from '../../utils/error-utils';
import requests from '../requests';

const endpointPrefix = 'products';

const catalog = {
	list: (params: URLSearchParams) => requests.get(`${endpointPrefix}`, params).catch(handleError),
	details: (id: string | undefined) =>
		requests.get(`${endpointPrefix}/${id}`),
	filters: () => requests.get(`${endpointPrefix}/filters`)
};

export default catalog;
