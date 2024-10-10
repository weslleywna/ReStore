export const handleError = (error: unknown) => {
	console.error(error);
	throw error;
};

export const handleValidationError = (errors: string[]) => {
	console.error(errors);
	return Promise.reject(errors);
};
