/**
 * author: Tomáš Szabó 
 * login: xszabo16
 */
import React, { useEffect, useState } from 'react';
import { Container, Modal, Button } from "react-bootstrap";

import { getTypeById } from "../../utils/CalendarUtils";


const ModalCalendarAdd = (props) => {
    // Front-End
    return (
        <Container>
                <Modal.Header closeButton>
                    <Modal.Title>{"Event information"}</Modal.Title>
                </Modal.Header>

                <Modal.Body>
                    <p>Type: {getTypeById(props.eventInformation.type)}</p>
                    <p>Dog: {props.eventInformation.dogName}</p>
                    <p>Owner of reservation: {props.eventInformation.ownerName}</p>
                    <p>From: {props.eventInformation.startStr}</p>
                    <p>To: {props.eventInformation.endStr}</p>
                </Modal.Body>

                <Modal.Footer>
                <Button variant="secondary" id="modal-close" onClick={props.onHide}>
                    Close
                </Button>
            </Modal.Footer>
        </Container>
    );
}

export default ModalCalendarAdd;