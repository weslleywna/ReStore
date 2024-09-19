import basket from './routes/basket';
import catalog from './routes/catalog';
import testErrors from './routes/test-errors';

const agent = {
	catalog,
	testErrors,
	basket
};

export default agent;
