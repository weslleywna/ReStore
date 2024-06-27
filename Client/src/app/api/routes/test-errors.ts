import { handleError, handleValidationError } from '../../utils/error-utils';
import requests from '../requests';

const endpointPrefix = 'buggy';

const testErrors = {
	get400Error: () =>
		requests.get(`${endpointPrefix}/bad-request`).catch(handleError),
	get401Error: () =>
		requests.get(`${endpointPrefix}/unauthorized`).catch(handleError),
	get404Error: () =>
		requests.get(`${endpointPrefix}/not-found`).catch(handleError),
	get500Error: () =>
		requests.get(`${endpointPrefix}/server-error`).catch(handleError),
	getValidationError: () =>
		requests
			.get(`${endpointPrefix}/validation-error`)
			.catch(handleValidationError),
};

export default testErrors;
