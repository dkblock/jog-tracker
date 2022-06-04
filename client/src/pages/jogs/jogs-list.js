import React, { useEffect, useMemo } from 'react';
import { useDispatch, useSelector } from 'react-redux';

import { fetchJogs } from '../../actions';
import { SELECTORS } from '../../store';
import { Avatar, Table } from '../../components';

const columns = [
  {
    id: 'avatar',
    label: ' ',
    width: 64,
    renderCell: (row) => <Avatar firstName={row.user.firstName} lastName={row.user.lastName} />,
  },
  { id: 'fullName', label: 'Name' },
  { id: 'userName', label: 'Username' },
  { id: 'distance', label: 'Distance' },
  { id: 'date', label: 'Date' },
  { id: 'averageSpeed', label: 'Average speed' },
];

const prepareJogs = (jogs) =>
  jogs.map((jog) => ({
    ...jog,
    id: jog.id,
    fullName: `${jog.user.lastName}, ${jog.user.firstName}`,
    userName: jog.user.userName,
    distance: jog.distanceInMeters,
    date: new Date(jog.date).toLocaleDateString(),
    averageSpeed: jog.averageSpeedInMetersPerSecond,
  }));

const useJogs = () => {
  const jogs = useSelector(SELECTORS.JOGS.getJogs);
  const isFetching = useSelector(SELECTORS.JOGS.getIsFetching);

  return [jogs, isFetching];
};

const JogsList = () => {
  const dispatch = useDispatch();
  const [jogs, isFetching] = useJogs();

  useEffect(() => {
    dispatch(fetchJogs({}));
  }, []);

  const preparedJogs = useMemo(() => prepareJogs(jogs), [jogs]);
  return <Table columns={columns} data={preparedJogs} isFetching={isFetching} />;
};

export default JogsList;
