import React from 'react';
import TableCell from '@mui/material/TableCell';

const TableEmpty = ({ emptyText, columnsCount }) => (
  <tr>
    <TableCell align="center" colSpan={columnsCount} sx={{ borderBottom: 'none' }}>
      <div className="mt-5">{emptyText ?? 'No data'}</div>
    </TableCell>
  </tr>
);

export default TableEmpty;
