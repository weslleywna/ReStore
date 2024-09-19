import { handleError } from '../../utils/error-utils';
import requests from '../requests';

const endpointPrefix = 'baskets';

const basket = {
    get: () => requests.get(`${endpointPrefix}`).catch(handleError),
    addItem: (productId: string, quantity = 1) => requests.post(`${endpointPrefix}?productId=${productId}&quantity=${quantity}`, {}).catch(handleError),
    removeItem: (productId: string, quantity = 1) => requests.delete(`${endpointPrefix}?productId=${productId}&quantity=${quantity}`).catch(handleError)
};

export default basket;
