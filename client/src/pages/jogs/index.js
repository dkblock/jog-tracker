import React from 'react';

import JogsList from './jogs-list';
import { Paper } from '../../components';
import './jogs.scss';

const Jogs = () => {
  return (
    <div className="jogs-container">
      <Paper className="jogs-list">
        <JogsList />
      </Paper>
    </div>
  );
};

export default Jogs;
