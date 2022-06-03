import React, { useEffect, useMemo } from 'react';
import { useDispatch, useSelector } from 'react-redux';

import { SELECTORS } from '../../store';
import { Table } from '../../components/table';
import { fetchJogs } from '../../actions/jogs';

const columns = [
  { id: 'fullName', label: 'Name' },
  { id: 'username', label: 'Username' },
  { id: 'distance', label: 'Distance' },
  { id: 'date', label: 'Date' },
  { id: 'averageSpeed', label: 'Average speed' },
];

const prepareJogs = (jogs) =>
  jogs.map((jog) => ({
    id: jog.id,
    fullName: `${jog.user.lastName}, ${jog.user.firstName}`,
    username: jog.user.username,
    distance: jog.distanceInMeters,
    date: new Date(jog.date).toLocaleDateString(),
    averageSpeed: jog.averageSpeedInMetersPerSecond,
  }));

const useJogs = () => {
  const jogs = useSelector(SELECTORS.JOGS.getJogs);
  const isFetching = useSelector(SELECTORS.JOGS.isFetching);

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
