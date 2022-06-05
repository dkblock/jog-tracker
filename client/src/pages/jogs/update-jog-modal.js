import React from 'react';
import { useDispatch } from 'react-redux';

import { updateJog } from '../../store/actions';
import JogForm from './jog-form';

const UpdateJogModal = ({ jog, onClose }) => {
  const dispatch = useDispatch();
  return (
    <JogForm
      distanceInM={jog.distanceInMeters}
      distanceInKm={jog.distanceInKilometers}
      jogTime={jog.time}
      jogDate={jog.date}
      title="Edit jog"
      buttonText="Edit"
      onSubmit={(updatedJog) => dispatch(updateJog({ jog: { ...updatedJog, id: jog.id } }))}
      onClose={onClose}
    />
  );
};

export default UpdateJogModal;
