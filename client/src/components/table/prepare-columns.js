import React from 'react';

import { IconButton } from '../icon';
import { Tooltip } from '../tooltip';

const getActionsCell = (getActionsFn, row) => {
  const handleClick = (e, onClick) => {
    e.stopPropagation();
    onClick(row);
  };

  const actions = getActionsFn(row);

  return (
    <div className="table-actions-cell">
      {actions.map((action) =>
        action.disabled ? (
          <IconButton key={action.label} type={action.icon} disabled />
        ) : (
          <Tooltip key={action.label} title={action.label} placement="bottom">
            <IconButton type={action.icon} onClick={(e) => handleClick(e, action.onClick)} />
          </Tooltip>
        ),
      )}
    </div>
  );
};

export const prepareColumns = (columns, getActionsFn, onSort) => {
  const preparedColumns = columns.map((column) => ({
    ...column,
    sortable: Boolean(onSort) && column.sortable !== false,
  }));

  if (getActionsFn)
    preparedColumns.push({
      id: 'TABLE_ACTIONS',
      label: '',
      align: 'center',
      width: getActionsFn({}).length,
      sortable: false,
      renderCell: (row) => getActionsCell(getActionsFn, row),
    });

  return preparedColumns;
};
