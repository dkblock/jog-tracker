import React from 'react';
import { useDispatch, useSelector } from 'react-redux';

import { SELECTORS } from '../../store';
import { updateJog } from '../../store/actions';
import JogForm from './jog-form';

const UpdateJogModal = ({ onClose }) => {
  const dispatch = useDispatch();
  const { props } = useSelector(SELECTORS.MODAL.props);
  const { jog } = props;

  return (
    <JogForm
      distanceInM={jog.distanceInMeters}
      distanceInKm={jog.distanceInKilometers}
      jogTime={jog.time}
      jogDate={jog.date}
      title="Update jog"
      buttonText="Update"
      onSubmit={(updatedJog) => dispatch(updateJog({ jog: { ...updatedJog, id: jog.id } }))}
      onClose={onClose}
    />
  );
};

export default UpdateJogModal;
