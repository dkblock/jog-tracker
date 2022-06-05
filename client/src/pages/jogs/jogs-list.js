import React, { useEffect, useMemo, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';

import { fetchJogs, showDeleteJogModal, showUpdateJogModal } from '../../store/actions';
import { useCurrentUser } from '../../hooks';
import { SELECTORS } from '../../store';
import { normalizeTime } from '../../utils/time';
import JogsListToolbar from './jogs-list-toolbar';
import { Avatar, Icon, Table } from '../../components';

const prepareActions = (isAuthenticated, onJogDelete, onJogEdit) =>
  isAuthenticated
    ? (row) => [
        {
          label: 'Edit',
          icon: Icon.types.EDIT,
          disabled: !row.hasAccess,
          onClick: (jog) => onJogEdit(jog),
        },
        {
          label: 'Delete',
          icon: Icon.types.DELETE,
          disabled: !row.hasAccess,
          onClick: (jog) => onJogDelete(jog),
        },
      ]
    : null;

const prepareColumns = (showInKm) => [
  {
    id: 'avatar',
    label: ' ',
    width: 64,
    renderCell: (row) => <Avatar firstName={row.user.firstName} lastName={row.user.lastName} />,
  },
  { id: 'name', label: 'Name' },
  { id: 'userName', label: 'Username' },
  { id: 'date', label: 'Date' },
  { id: 'distance', label: `Distance (${showInKm ? 'km' : 'm'})`, align: 'center' },
  { id: 'elapsedTime', label: 'Elapsed time', align: 'center' },
  { id: 'averageSpeed', label: `Average speed (${showInKm ? 'km/h' : 'm/s'})`, align: 'center' },
];

const prepareJogs = (jogs, showInKm) =>
  jogs.map((jog) => ({
    ...jog,
    id: jog.id,
    name: `${jog.user.lastName}, ${jog.user.firstName}`,
    userName: jog.user.userName,
    date: new Date(jog.date).toLocaleDateString(),
    distance: showInKm ? jog.distanceInKilometers : jog.distanceInMeters,
    elapsedTime: normalizeTime(jog.elapsedTime),
    averageSpeed: showInKm ? jog.averageSpeedInKilometersPerHour : jog.averageSpeedInMetersPerSecond,
    hasAccess: jog.hasAccess,
    time: jog.elapsedTime,
  }));

const useJogs = () => {
  const jogs = useSelector(SELECTORS.JOGS.getJogs);
  const totalCount = useSelector(SELECTORS.JOGS.getTotalCount);
  const filter = useSelector(SELECTORS.JOGS.getFilter);
  const { isFetching } = useSelector(SELECTORS.JOGS.getFetching);

  return [jogs, totalCount, filter, isFetching];
};

const JogsList = () => {
  const dispatch = useDispatch();
  const { isAuthenticated } = useCurrentUser();
  const [jogs, totalCount, filter, isFetching] = useJogs();
  const [showInKm, setShowInKm] = useState(false);
  const { searchText, dateFrom, dateTo, showOnlyOwn, pageIndex, pageSize, sortBy, sortDirection } = filter;

  useEffect(() => {
    handleSearch();
  }, []);

  const handleSearch = (params) => dispatch(fetchJogs({ ...filter, ...params }));

  const handleSort = ({ sortBy: orderBy, sortDirection: sortOrder }) =>
    handleSearch({ searchText, pageIndex, pageSize, sortBy: orderBy, sortDirection: sortOrder });

  const handlePageChange = (newPageIndex) => handleSearch({ pageIndex: newPageIndex });

  const handleDeleteJog = (jog) => dispatch(showDeleteJogModal({ jog }));
  const handleUpdateJog = (jog) => dispatch(showUpdateJogModal({ jog }));

  const columns = useMemo(() => prepareColumns(showInKm), [showInKm]);
  const data = useMemo(() => prepareJogs(jogs, showInKm), [jogs, showInKm]);
  const actions = useMemo(
    () => prepareActions(isAuthenticated, handleDeleteJog, handleUpdateJog),
    [isAuthenticated, handleDeleteJog, handleUpdateJog],
  );

  return (
    <Table
      columns={columns}
      data={data}
      actions={actions}
      isFetching={isFetching}
      totalCount={totalCount}
      pageIndex={pageIndex}
      pageSize={pageSize}
      sortBy={sortBy}
      sortDirection={sortDirection}
      onSort={handleSort}
      onPageChange={handlePageChange}
      toolbar={
        <JogsListToolbar
          isAuthenticated={isAuthenticated}
          totalCount={totalCount}
          searchText={searchText}
          dateFrom={dateFrom}
          dateTo={dateTo}
          showOnlyOwn={showOnlyOwn}
          showInKm={showInKm}
          onSearch={handleSearch}
          onShowInKmChange={setShowInKm}
        />
      }
    />
  );
};

export default JogsList;
