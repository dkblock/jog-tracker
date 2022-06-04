import React from 'react';
import { useDispatch } from 'react-redux';
import Dialog from '@mui/material/Dialog';
import DialogTitle from '@mui/material/DialogTitle';
import DialogContent from '@mui/material/DialogContent';
import DialogActions from '@mui/material/DialogActions';

import { hideModal } from '../../actions';
import sizes from '../constants/sizes';
import { Button } from '../button';
import { IconButton } from '../icon';
import './modal.scss';

const CreateModal = ({
  children,
  createButtonText = 'Create',
  size = sizes.small,
  title,
  isCreating,
  actions,
  onCreate,
}) => {
  const dispatch = useDispatch();
  const handleClose = () => dispatch(hideModal());

  return (
    <Dialog maxWidth={size} onClose={handleClose} fullWidth open>
      <DialogTitle className="modal-common__header">
        <span className="modal-common__title">{title}</span>
        <IconButton type={IconButton.types.close} onClick={handleClose} />
      </DialogTitle>
      <DialogContent className="modal-common__content" dividers>
        {children}
      </DialogContent>
      <DialogActions>
        <div className="modal-common__actions">
          {!!actions && actions}
          <div className="modal-common__actions-buttons">
            <Button type={Button.types.text} onClick={handleClose}>
              Cancel
            </Button>
            <Button color={Button.colors.success} isLoading={isCreating} onClick={onCreate}>
              {createButtonText}
            </Button>
          </div>
        </div>
      </DialogActions>
    </Dialog>
  );
};

export default CreateModal;
