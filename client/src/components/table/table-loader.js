import React from 'react';
import TableCell from '@mui/material/TableCell';

import { Loader } from '../loader';

const TableLoader = ({ columnsCount }) => (
  <tr>
    <TableCell align="center" colSpan={columnsCount} sx={{ borderBottom: 'none' }}>
      <Loader />
    </TableCell>
  </tr>
);

export default TableLoader;
