import React, { useEffect, useMemo } from 'react';
import { useDispatch, useSelector } from 'react-redux';

import { fetchUsers, showDeleteUserModal, showUpdateUserModal } from '../../redux/actions';
import { useCurrentUser } from '../../hooks';
import { SELECTORS } from '../../redux';
import roles from '../../constants/roles';
import UsersListToolbar from './users-list-toolbar';
import { Avatar, Icon, Table } from '../../components';

const roleItems = {
  [roles.ADMINISTRATOR]: 'Administrator',
  [roles.USER]: 'User',
};

const prepareActions = (currentUserId, onUserDelete, onUserEdit) => (row) =>
  [
    {
      label: 'Edit',
      icon: Icon.types.EDIT,
      onClick: (user) => onUserEdit(user),
    },
    {
      label: 'Delete',
      icon: Icon.types.DELETE,
      disabled: currentUserId === row.id,
      onClick: (user) => onUserDelete(user),
    },
  ];

const columns = [
  {
    id: 'avatar',
    label: ' ',
    width: 32,
    renderCell: (row) => <Avatar firstName={row.firstName} lastName={row.lastName} />,
  },
  { id: 'firstName', label: 'First Name' },
  { id: 'lastName', label: 'Last Name' },
  { id: 'userName', label: 'Username' },
  { id: 'role', label: 'Role', renderCell: ({ role }) => roleItems[role] },
  { id: 'totalJogs', label: 'Completed Jogs', align: 'center' },
];

const useUsers = () => {
  const users = useSelector(SELECTORS.USERS.getUsers);
  const totalCount = useSelector(SELECTORS.USERS.getTotalCount);
  const filter = useSelector(SELECTORS.USERS.getFilter);
  const { isFetching } = useSelector(SELECTORS.USERS.getFetching);

  return [users, totalCount, filter, isFetching];
};

const UsersList = () => {
  const dispatch = useDispatch();
  const { currentUser } = useCurrentUser();
  const [users, totalCount, filter, isFetching] = useUsers();
  const { searchText, role, pageIndex, pageSize, sortBy, sortDirection } = filter;

  useEffect(() => {
    handleSearch();
  }, []);

  const handleSearch = (params) => dispatch(fetchUsers({ ...filter, ...params }));

  const handleSort = ({ sortBy: orderBy, sortDirection: sortOrder }) =>
    handleSearch({ searchText, pageIndex, pageSize, sortBy: orderBy, sortDirection: sortOrder });

  const handlePageChange = (newPageIndex) => handleSearch({ pageIndex: newPageIndex });

  const handleDeleteUser = (user) => dispatch(showDeleteUserModal({ user }));
  const handleUpdateUser = (user) => dispatch(showUpdateUserModal({ user }));

  const actions = useMemo(
    () => prepareActions(currentUser.id, handleDeleteUser, handleUpdateUser),
    [currentUser, handleDeleteUser, handleUpdateUser],
  );

  return (
    <Table
      columns={columns}
      data={users}
      actions={actions}
      isFetching={isFetching}
      totalCount={totalCount}
      pageIndex={pageIndex}
      pageSize={pageSize}
      sortBy={sortBy}
      sortDirection={sortDirection}
      onSort={handleSort}
      onPageChange={handlePageChange}
      toolbar={<UsersListToolbar totalCount={totalCount} searchText={searchText} role={role} onSearch={handleSearch} />}
    />
  );
};

export default UsersList;
