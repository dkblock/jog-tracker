import React, { useEffect, useState } from 'react';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import FormControl from '@mui/material/FormControl';
import FormHelperText from '@mui/material/FormHelperText';
import InputLabel from '@mui/material/InputLabel';
import Select from '@mui/material/Select';
import MenuItem from '@mui/material/MenuItem';

import palette from '../constants/palette';

const theme = createTheme({ palette });

const MenuProps = {
  PaperProps: {
    style: { maxHeight: '50vh' },
  },
};

const Dropdown = ({ className, items, label, disabled = false, error, helperText, value, onChange }) => {
  const [dropdownValue, setDropdownValue] = useState(value ?? items[0]?.value);

  useEffect(() => {
    setDropdownValue(value);
  }, [value]);

  const handleChange = (event) => {
    const newValue = event.target.value;

    setDropdownValue(newValue);
    onChange(newValue);
  };

  return (
    <ThemeProvider theme={theme}>
      <FormControl className={className} variant="outlined" error={error}>
        <InputLabel>{label}</InputLabel>
        <Select
          label={label}
          value={dropdownValue}
          variant="outlined"
          size="small"
          disabled={disabled}
          onChange={handleChange}
          MenuProps={MenuProps}
        >
          {items.map((item) => (
            <MenuItem key={`dropdown-item-${item.value}`} value={item.value}>
              {item.label}
            </MenuItem>
          ))}
        </Select>
        {error && <FormHelperText>{helperText}</FormHelperText>}
      </FormControl>
    </ThemeProvider>
  );
};

export default Dropdown;
