import requests from '../requests';

const endpointPrefix = 'buggy';

const testErrors = {
	get400Error: () =>
		requests
			.get(`${endpointPrefix}/bad-request`)
			.catch((error) => console.log(error)),
	get401Error: () =>
		requests
			.get(`${endpointPrefix}/unauthorized`)
			.catch((error) => console.log(error)),
	get404Error: () =>
		requests
			.get(`${endpointPrefix}/not-found`)
			.catch((error) => console.log(error)),
	get500Error: () =>
		requests
			.get(`${endpointPrefix}/server-error`)
			.catch((error) => console.log(error)),
	getValidationError: () =>
		requests
			.get(`${endpointPrefix}/validation-error`)
			.catch((error) => console.log(error)),
};

export default testErrors;
