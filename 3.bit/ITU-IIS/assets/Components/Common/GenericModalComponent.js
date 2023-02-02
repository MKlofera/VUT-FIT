/**
 * author: Tomáš Szabó 
 * login: xszabo16
 */
import React from 'react';
import '../../styles/app.scss';
import { Modal } from 'react-bootstrap';

const ModalWindow = (props) => {
    return (
        <Modal show={props.show} onHide={props.onHide}>
            {props.children}
        </Modal>
    )
};

export default ModalWindow;