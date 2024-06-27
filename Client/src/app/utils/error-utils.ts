export const handleError = (error: unknown) => {
	console.error(error);
};

export const handleValidationError = (errors: string[]) => {
	console.error(errors);
	return Promise.reject(errors);
};
