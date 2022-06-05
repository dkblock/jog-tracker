import React, { useState } from 'react';
import { useDispatch } from 'react-redux';
import { useDebouncedCallback } from 'use-debounce';
import Popover from '@mui/material/Popover';

import { showCreateJogModal, showGenerateReportModal } from '../../store/actions';
import { Button, DateRangePicker, Icon, IconButton, Switch, Table, TextField, Tooltip } from '../../components';

const JogsListToolbar = ({
  isAuthenticated,
  totalCount,
  searchText,
  dateFrom,
  dateTo,
  showOnlyOwn,
  showInKm,
  onSearch,
  onShowInKmChange,
}) => {
  const dispatch = useDispatch();
  const handleSearchTextChange = useDebouncedCallback((value) => onSearch({ searchText: value }), 500);

  const [anchorEl, setAnchorEl] = useState(null);

  const handleMenuOpen = (event) => setAnchorEl(event.currentTarget);
  const handleMenuClose = () => setAnchorEl(null);

  return (
    <Table.Toolbar title="Jog Results" count={totalCount}>
      <Button
        color={Button.colors.primary}
        endIcon={Icon.types.FLAG}
        onClick={() => dispatch(showGenerateReportModal())}
      >
        Generate report
      </Button>
      {isAuthenticated && (
        <>
          <Button
            color={Button.colors.success}
            startIcon={Icon.types.PLUS}
            onClick={() => dispatch(showCreateJogModal())}
          >
            Create jog
          </Button>
          <Switch
            label="Show only my jogs"
            checked={showOnlyOwn}
            onChange={(value) => onSearch({ showOnlyOwn: value })}
          />
        </>
      )}
      <Switch label="Show in km" checked={showInKm} onChange={(value) => onShowInKmChange(value)} />
      <Tooltip title="Date range" placement="top">
        <IconButton type={IconButton.types.CALENDAR} onClick={handleMenuOpen} />
      </Tooltip>
      <Popover
        id="app-header-popover"
        open={Boolean(anchorEl)}
        anchorEl={anchorEl}
        onClose={handleMenuClose}
        anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
        transformOrigin={{ vertical: 'top', horizontal: 'right' }}
      >
        <div className="jogs-list__date-picker">
          <DateRangePicker
            startValue={dateFrom}
            endValue={dateTo}
            startLabel="Start date"
            endLabel="End date"
            onChange={({ startValue, endValue }) => onSearch({ dateFrom: startValue, dateTo: endValue })}
          />
        </div>
      </Popover>
      <TextField
        className="jogs-list__search"
        value={searchText}
        variant={TextField.variants.filled}
        icon={Icon.types.SEARCH}
        onChange={handleSearchTextChange}
      />
    </Table.Toolbar>
  );
};

export default JogsListToolbar;
