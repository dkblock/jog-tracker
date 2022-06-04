import React from 'react';
import { IconButton as MaterialIconButton } from '@mui/material';

import palette from '../constants/palette';
import icons from './icons';
import iconTypes from './icon-types';
import './icon.scss';

const Icon = ({ type, color = '', size = 'medium' }) => {
  const SpecificIcon = icons[type];
  return <SpecificIcon style={{ color: color ? palette[color].main : '' }} fontSize={size} />;
};

const IconButton = ({ type, selected, disabled, onClick }) => {
  const SpecificIcon = icons[type];
  const color = selected ? palette.primary.main : '';

  return (
    <MaterialIconButton className="icon" disabled={disabled} onClick={onClick}>
      <SpecificIcon style={{ color: color }} />
    </MaterialIconButton>
  );
};

Icon.types = iconTypes;
IconButton.types = iconTypes;
export { Icon, IconButton };
