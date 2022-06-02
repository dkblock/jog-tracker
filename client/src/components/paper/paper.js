import React from 'react';
import { Paper as MuiPaper } from '@mui/material';

import './paper.scss';

const Paper = ({ children }) => <MuiPaper className="paper">{children}</MuiPaper>;

export default Paper;
