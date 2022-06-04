import React from 'react';
import { Redirect as RouterRedirect } from 'react-router';

const Redirect = ({ to }) => <RouterRedirect to={to} />;

export default Redirect;
