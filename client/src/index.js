import React from 'react';
import { createRoot } from 'react-dom/client';

import App from './pages/__app__/app';
import './styles/index.scss';

const container = document.getElementById('root');
const root = createRoot(container);
root.render(<App />);
