import React, { useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';

import { generateReport, refreshReport } from '../../redux/actions';
import { useCurrentUser } from '../../hooks';
import { SELECTORS } from '../../redux';
import JogsReport from './jogs-report';
import { Button, DateRangePicker, Modal, Switch } from '../../components';

const GenerateReportModal = ({ onClose }) => {
  const dispatch = useDispatch();
  const { report, hasError } = useSelector(SELECTORS.REPORTS.getReport);
  const isFetching = useSelector(SELECTORS.REPORTS.getFetching);
  const { currentUser } = useCurrentUser();

  const [dateFrom, setDateFrom] = useState(new Date(new Date().setDate(new Date().getDate() - 7)));
  const [dateTo, setDateTo] = useState(new Date());
  const [showInKm, setShowInKm] = useState(false);

  const handleDateChange = ({ startValue, endValue }) => {
    setDateFrom(startValue);
    setDateTo(endValue);
  };

  const handleClose = () => {
    dispatch(refreshReport());
    onClose();
  };

  return (
    <Modal.Create
      title="Report"
      createButtonText="OK"
      size={Modal.sizes.large}
      onClose={handleClose}
      onCreate={handleClose}
      actions={
        report && (
          <Switch
            className="jogs-report__actions"
            checked={showInKm}
            label="Show in km"
            onChange={(value) => setShowInKm(value)}
          />
        )
      }
    >
      <div className="jogs-report">
        <div className="jogs-report__head">
          <DateRangePicker
            startValue={dateFrom}
            endValue={dateTo}
            startLabel="Start date"
            endLabel="End date"
            onChange={handleDateChange}
          />
          <Button
            color={Button.colors.success}
            isLoading={isFetching}
            onClick={() => dispatch(generateReport({ dateFrom, dateTo }))}
          >
            Generate
          </Button>
          {hasError && <span>There is no data for the specified period</span>}
        </div>
        {report && <JogsReport report={report} currentUser={currentUser} showInKm={showInKm} />}
      </div>
    </Modal.Create>
  );
};

export default GenerateReportModal;
