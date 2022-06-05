import React, { useEffect, useState } from 'react';
import { useSelector } from 'react-redux';
import { TimeSpan } from 'timespan';

import { SELECTORS } from '../../store';
import { DatePicker, Modal, Switch, TextField } from '../../components';

const JogForm = ({ distanceInM, distanceInKm, jogDate, jogTime, title, buttonText, onSubmit, onClose }) => {
  const { isSaving } = useSelector(SELECTORS.JOGS.getFetching);
  const serverErrors = useSelector(SELECTORS.JOGS.getErrors);

  const [distanceInMeters, setDistanceInMeters] = useState(distanceInM);
  const [distanceInKilometers, setDistanceInKilometers] = useState(distanceInKm);
  const [date, setDate] = useState(jogDate);
  const [isDistanceInKilometers, setIsDistanceInKilometers] = useState(false);
  const [elapsedTime, setElapsedTime] = useState(jogTime ?? { hours: 0, minutes: 0, seconds: 0 });
  const [errors, setErrors] = useState({});

  useEffect(() => {
    setErrors({ ...errors, ...serverErrors });
  }, [serverErrors]);

  const handleDistanceChange = (value) => {
    if (isDistanceInKilometers) {
      setDistanceInKilometers(value);
      setDistanceInMeters(value * 1000);
    } else {
      setDistanceInMeters(value);
      setDistanceInKilometers(value / 1000);
    }
  };

  const handleTimeChange = (value, part) => {
    setElapsedTime({ ...elapsedTime, [part]: value });
  };

  const handleDistanceFocus = () => setErrors((prev) => ({ ...prev, distanceInMeters: null }));
  const handleTimeFocus = () => setErrors((prev) => ({ ...prev, time: null }));

  const handleTimeFocusOut = () => {
    const time = new TimeSpan(0, elapsedTime.seconds, elapsedTime.minutes, elapsedTime.hours);
    setElapsedTime({ hours: time.hours, minutes: time.minutes, seconds: time.seconds });
  };

  const handleSubmit = () => {
    const jog = { distanceInMeters, distanceInKilometers, elapsedTime, date };
    onSubmit(jog);
  };

  return (
    <Modal.Create
      title={title}
      isCreating={isSaving}
      createButtonText={buttonText}
      onCreate={handleSubmit}
      onClose={onClose}
    >
      <form className="input-form">
        <div className="jogs-form__row">
          <TextField
            className="jogs-form__distance"
            label={`Distance (${isDistanceInKilometers ? 'km' : 'm'})`}
            type="number"
            value={isDistanceInKilometers ? distanceInKilometers : distanceInMeters}
            error={Boolean(errors.distanceInMeters)}
            helperText={errors.distanceInMeters}
            onChange={handleDistanceChange}
            onFocus={handleDistanceFocus}
          />
          <Switch
            label="In km"
            checked={isDistanceInKilometers}
            onChange={() => setIsDistanceInKilometers((prev) => !prev)}
          />
        </div>
        <div className="jogs-form__timer">
          <TextField
            className="jogs-form__timer-control"
            label="Hours"
            type="number"
            value={elapsedTime.hours}
            error={Boolean(errors.time)}
            helperText={errors.time}
            onChange={(value) => handleTimeChange(value, 'hours')}
            onFocus={handleTimeFocus}
            onFocusOut={handleTimeFocusOut}
          />
          <TextField
            className="jogs-form__timer-control"
            label="Minutes"
            type="number"
            value={elapsedTime.minutes}
            error={Boolean(errors.time)}
            onChange={(value) => handleTimeChange(value, 'minutes')}
            onFocus={handleTimeFocus}
            onFocusOut={handleTimeFocusOut}
          />
          <TextField
            className="jogs-form__timer-control"
            label="Seconds"
            type="number"
            value={elapsedTime.seconds}
            error={Boolean(errors.time)}
            onChange={(value) => handleTimeChange(value, 'seconds')}
            onFocus={handleTimeFocus}
            onFocusOut={handleTimeFocusOut}
          />
        </div>
        <div className="jogs-form__row">
          <DatePicker label="Date" value={date} max={new Date()} onChange={(value) => setDate(value)} />
        </div>
      </form>
    </Modal.Create>
  );
};

export default JogForm;