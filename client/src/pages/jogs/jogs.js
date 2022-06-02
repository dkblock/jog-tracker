import React from 'react';

import { Paper } from '../../components/paper';
import JogsList from './jogs-list';
import './jogs.scss';

const Jogs = () => {
  return (
    <div className="jogs-container">
      <Paper>
        <JogsList />
      </Paper>
    </div>
  );
};

export default Jogs;
