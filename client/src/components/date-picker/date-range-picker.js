import React, { useEffect, useState } from 'react';

import DatePicker from './date-picker';

const DateRangePicker = ({ startValue, endValue, startLabel, endLabel, min, max, onChange }) => {
  const [datePickerStartValue, setDatePickerStartValue] = useState(startValue);
  const [datePickerEndValue, setDatePickerEndValue] = useState(endValue);

  useEffect(() => {
    setDatePickerStartValue(startValue);
  }, [startValue]);

  useEffect(() => {
    setDatePickerEndValue(endValue);
  }, [endValue]);

  const handleChangeStartValue = (newValue) => {
    setDatePickerStartValue(newValue);
    onChange({ startValue: newValue, endValue: datePickerEndValue });
  };

  const handleChangeEndValue = (newValue) => {
    setDatePickerEndValue(newValue);
    onChange({ startValue: datePickerStartValue, endValue: newValue });
  };

  return (
    <div className="date-picker-range">
      <DatePicker
        value={datePickerStartValue}
        label={startLabel}
        min={min}
        max={datePickerEndValue}
        onChange={handleChangeStartValue}
      />
      <DatePicker
        value={datePickerEndValue}
        label={endLabel}
        min={datePickerStartValue}
        max={max}
        onChange={handleChangeEndValue}
      />
    </div>
  );
};

export default DateRangePicker;
