import React from 'react';
import { Paper as MuiPaper } from '@mui/material';

const Paper = ({ className, children }) => <MuiPaper className={className}>{children}</MuiPaper>;

export default Paper;
