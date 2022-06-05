import BorderlessModal from './borderless-modal';
import CreateModal from './create-modal';
import DeleteModal from './delete-modal';
import InfoModal from './info-modal';
import sizes from '../constants/sizes';

const Modal = () => ({});

Modal.Borderless = BorderlessModal;
Modal.Create = CreateModal;
Modal.Delete = DeleteModal;
Modal.Info = InfoModal;
Modal.sizes = sizes;

export { Modal };
