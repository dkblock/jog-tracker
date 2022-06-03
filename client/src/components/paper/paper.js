import React from 'react';
import { Paper as MuiPaper } from '@mui/material';

import './paper.scss';

const Paper = ({ className, children }) => <MuiPaper className={`${className} paper`}>{children}</MuiPaper>;

export default Paper;
