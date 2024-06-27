import { handleError } from '../../utils/error-utils';
import requests from '../requests';

const endpointPrefix = 'products';

const catalog = {
	list: () => requests.get(`${endpointPrefix}`).catch(handleError),
	details: (id: string | undefined) =>
		requests.get(`${endpointPrefix}/${id}`).catch(handleError),
};

export default catalog;
