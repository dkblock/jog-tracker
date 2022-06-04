import React from 'react';
import { createTheme, ThemeProvider } from '@mui/material';
import CircularProgress from '@mui/material/CircularProgress';

import palette from '../constants/palette';
import colors from '../constants/colors';
import sizes from '../constants/sizes';
import './loader.scss';

const theme = createTheme({ palette });

const Loader = ({ className, color = colors.primary, size = sizes.medium }) => (
  <ThemeProvider theme={theme}>
    <CircularProgress className={`${className} loader--${size}`} color={color} />
  </ThemeProvider>
);

Loader.colors = colors;
Loader.sizes = sizes;
export default Loader;
