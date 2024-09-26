import { Button, ButtonGroup, Typography } from '@mui/material';
import { useAppDispatch, useAppSelector } from '../../app/store/configure-store';
import { decrement, increment } from './counter-slice';

export default function ContactPage() {
	const dispatch = useAppDispatch();
	const {data, title} = useAppSelector(state => state.counter);

	return (
		<>
			<Typography>
				{title}
			</Typography>
			<Typography>
				{data}
			</Typography>
			<ButtonGroup>
				<Button onClick={() => dispatch(increment(1))}>
					Increment
				</Button>
				<Button onClick={() => dispatch(decrement(1))}>
					Decrement
				</Button>
			</ButtonGroup>
		</>
	) 
}
