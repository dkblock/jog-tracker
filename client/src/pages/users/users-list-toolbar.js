import React from 'react';
import { useDebouncedCallback } from 'use-debounce';

import roles from '../../constants/roles';
import { Dropdown, Icon, Table, TextField } from '../../components';

const roleItems = [
  { value: roles.ANY, label: 'Any' },
  { value: roles.ADMINISTRATOR, label: 'Administrator' },
  { value: roles.USER, label: 'User' },
];

const UsersListToolbar = ({ totalCount, searchText, role, onSearch }) => {
  const handleSearchTextChange = useDebouncedCallback((value) => onSearch({ searchText: value }), 500);

  return (
    <Table.Toolbar title="Users" count={totalCount}>
      <Dropdown
        className="users-list__control"
        label="Role"
        items={roleItems}
        value={role}
        onChange={(value) => onSearch({ role: value })}
      />
      <TextField
        className="users-list__control"
        value={searchText}
        variant={TextField.variants.filled}
        icon={Icon.types.SEARCH}
        onChange={handleSearchTextChange}
      />
    </Table.Toolbar>
  );
};

export default UsersListToolbar;
