import {
	Alert,
	AlertTitle,
	Button,
	ButtonGroup,
	Container,
	List,
	ListItem,
	ListItemText,
	Typography,
} from '@mui/material';
import agent from '../../app/api/agent';
import { useState } from 'react';

export default function AboutPage() {
	const [validationErrors, setValidationErrors] = useState<string[]>([]);

	function getValidationErrors() {
		agent.testErrors
			.getValidationError()
			.catch((error) => setValidationErrors(error));
	}

	return (
		<Container>
			<Typography gutterBottom variant="h2">
				Errors for testing purposes
			</Typography>
			<ButtonGroup fullWidth>
				<Button
					variant="contained"
					onClick={() => agent.testErrors.get400Error()}
				>
					Test 400 Error
				</Button>
				<Button
					variant="contained"
					onClick={() => agent.testErrors.get401Error()}
				>
					Test 401 Error
				</Button>
				<Button
					variant="contained"
					onClick={() => agent.testErrors.get404Error()}
				>
					Test 404 Error
				</Button>
				<Button
					variant="contained"
					onClick={() => agent.testErrors.get500Error()}
				>
					Test 500 Error
				</Button>
				<Button variant="contained" onClick={getValidationErrors}>
					Test Validation Error
				</Button>
			</ButtonGroup>
			{!!validationErrors?.length && (
				<Alert severity="error">
					<AlertTitle>Validation Errors</AlertTitle>
					<List>
						{validationErrors.map((error) => (
							<ListItem key={error}>
								<ListItemText>{error}</ListItemText>
							</ListItem>
						))}
					</List>
				</Alert>
			)}
		</Container>
	);
}
