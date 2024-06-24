import requests from '../requests';

const endpointPrefix = 'buggy';

const testErrors = {
	get400Error: () => requests.get(`${endpointPrefix}/bad-request`),
	get401Error: () => requests.get(`${endpointPrefix}/unauthorized`),
	get404Error: () => requests.get(`${endpointPrefix}/not-found`),
	get500Error: () => requests.get(`${endpointPrefix}/server-error`),
	getValidationError: () =>
		requests.get(`${endpointPrefix}/validation-error`),
};

export default testErrors;
