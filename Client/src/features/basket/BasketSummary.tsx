import { Paper, Table, TableBody, TableCell, TableContainer, TableRow } from "@mui/material";
import { currencyFormat } from "../../app/utils/currency-util";

export default function BasketSummary() {
    const {basket, setBasket, removeItem} = useStoreContext();
    const subtotal = basket?.items.reduce((sum, item) => sum + item.price * item.quantity, 0) || 0;
    const deliveryFee = subtotal > 10000 ? 0 : 500;

    if (!basket)
		return <Typography variant="h3">Your basket is empty</Typography>;

    return (
        <>
            <TableContainer component={Paper} variant={'outlined'}>
                <Table>
                    <TableBody>
                        <TableRow>
                            <TableCell colSpan={2}>Subtotal</TableCell>
                            <TableCell align="right">{currencyFormat(subtotal)}</TableCell>
                        </TableRow>
                        <TableRow>
                            <TableCell colSpan={2}>Delivery fee</TableCell>
                            <TableCell align="right">{currencyFormat(deliveryFee)}</TableCell>
                        </TableRow>
                        <TableRow>
                            <TableCell colSpan={2}>Total</TableCell>
                            <TableCell align="right">{currencyFormat(subtotal + deliveryFee)}</TableCell>
                        </TableRow>
                        <TableRow>
                            <TableCell>
                                <span style={{fontStyle: 'italic'}}>Orders over $100 qualify for free delivery</span>
                            </TableCell>
                        </TableRow>
                    </TableBody>
                </Table>
            </TableContainer>
        </>
    )
}