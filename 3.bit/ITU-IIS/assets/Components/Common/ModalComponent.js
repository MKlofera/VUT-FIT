/**
 * @Author: Marek Klofera
 * @Login: xklofe01
 * @param {props.Title} string with title of modal
 * @param {props.Body} string with body of modal
 * @param {props.SubmitBtnColor} string with color of submit button
 * @param {props.SubmitBtnText} string with text of submit button
 * @param {onHide} function to hide modal
 * @param {onSubmit} function to submit modal
 */

import React from "react";
import Button from "react-bootstrap/Button";
import Modal from "react-bootstrap/Modal";

export default function ModalComponent(props) {
    return (
        <Modal
            {...props}
            size="lg"
            aria-labelledby="contained-modal-title-vcenter"
            centered
        >
            <Modal.Header closeButton>
                <Modal.Title id="contained-modal-title-vcenter">
                    {props.Title}
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>{props.Body}</Modal.Body>
            <Modal.Footer>
                <Button onClick={props.onHide}>Close</Button>
                <Button onClick={props.onSubmit}>{props.SubmitBtnText}</Button>
            </Modal.Footer>
        </Modal>
    );
}
