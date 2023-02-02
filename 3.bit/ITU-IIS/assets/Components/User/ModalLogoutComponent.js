/**
 * author: Tomáš Szabó 
 * login: xszabo16
 */
import React from 'react';
import { Container, Modal, Button } from "react-bootstrap";

const ModalLogoutComponent = ({onClose, onLogoff}) => {
    // Front-End
    return (
        <Container>
                <Modal.Header closeButton>
                    <Modal.Title>{"You have been quite!"}</Modal.Title>
                </Modal.Header>

                <Modal.Body>
                    <p>
                    You are seeing this message, because you were inactive for some time now. Confirm you want to continue being logged in.
                    Otherwise, you will be logged off automatically.
                    </p>
                </Modal.Body>

                <Modal.Footer>
                <Button variant="secondary" id="modal-close" onClick={onLogoff}>
                    Log off
                </Button>
                <Button variant="secondary" id="modal-close" onClick={onClose}>
                    Stay logged-in
                </Button>
            </Modal.Footer>
        </Container>
    );
}

export default ModalLogoutComponent;