import React from 'react';
import { useDispatch } from 'react-redux';

import { createJog } from '../../store/actions';
import JogForm from './jog-form';

const CreateJogModal = ({ onClose }) => {
  const dispatch = useDispatch();
  return (
    <JogForm
      distanceInM={0}
      distanceInKm={0}
      jogDate={new Date()}
      title="New jog"
      onSubmit={(jog) => dispatch(createJog({ jog }))}
      onClose={onClose}
    />
  );
};

export default CreateJogModal;
