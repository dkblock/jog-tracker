import React from 'react';
import { makeStyles } from '@mui/styles';
import MuiAvatar from '@mui/material/Avatar';

import palette from '../constants/palette';

const useStyles = makeStyles(() => ({
  primary: {
    color: palette.primary.contrastText,
    backgroundColor: `${palette.primary.main} !important`,
    width: '36px !important',
    height: '36px !important',
    fontSize: '18px !important',
  },
}));

const Avatar = ({ className, firstName, lastName, onClick }) => {
  const classes = useStyles();
  const content = firstName && lastName ? `${firstName[0]}${lastName[0]}` : '?';

  return (
    <MuiAvatar className={`${className} ${classes.primary}`} onClick={onClick ?? null}>
      {content}
    </MuiAvatar>
  );
};

export default Avatar;
