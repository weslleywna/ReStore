/* eslint-disable @typescript-eslint/no-explicit-any */
import {
  FormControl,
  RadioGroup,
  FormControlLabel,
  Radio
} from '@mui/material';
import { ProductOrderEnum } from '../enums/product-order.enum';

interface Props {
  options: any[];
  onChange: (event: any) => void;
  selectedValue: ProductOrderEnum;
}

export default function RadioButtonGroup({
  options,
  onChange,
  selectedValue
}: Props) {
  return (
    <FormControl component="fieldset">
      <RadioGroup onChange={onChange} value={selectedValue}>
        {options.map(({ value, label }) => (
          <FormControlLabel
            value={value}
            control={<Radio />}
            label={label}
            key={value}
          />
        ))}
      </RadioGroup>
    </FormControl>
  );
}