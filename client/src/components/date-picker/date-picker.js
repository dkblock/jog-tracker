import React, { useEffect, useState } from 'react';
import ruLocale from 'date-fns/locale/ru';
import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFns';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { DatePicker as MuiDatePicker } from '@mui/x-date-pickers/DatePicker';
import { TextField } from '@mui/material';

import './date-picker.scss';

const DatePicker = ({ value, label, min, max, onChange }) => {
  const [datePickerValue, setDatePickerValue] = useState(value);

  useEffect(() => {
    setDatePickerValue(value);
  }, [value]);

  const handleChange = (newValue) => {
    setDatePickerValue(newValue);
    onChange(newValue);
  };

  return (
    <LocalizationProvider dateAdapter={AdapterDateFns} locale={ruLocale}>
      <MuiDatePicker
        label={label}
        value={datePickerValue}
        minDate={min ?? new Date(1970, 0, 1)}
        maxDate={max ?? new Date()}
        onChange={handleChange}
        renderInput={(params) => <TextField size="small" {...params} />}
      />
    </LocalizationProvider>
  );
};

export default DatePicker;
