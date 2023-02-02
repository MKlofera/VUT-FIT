/**
 * author: Marek Klofera
 * login: xklofe01
 */
import React from 'react'

import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';

export default function DogAddoptionModal(props) {
  return (
    <Modal
      {...props}
      size="lg"
      aria-labelledby="contained-modal-title-vcenter"
      centered
    >
      <Modal.Header closeButton>
        <Modal.Title id="contained-modal-title-vcenter">
          Adopce psa
        </Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <h4>Proces adopce</h4>
        <p>
          Nejprve je nutné si domluvit schůzku s praconíkem útulku. Po schůzce je nutné vyplnit a podepsat smlouvu o adopci. Po podepsání smlouvy je nutné zaplatit poplatek za adopci. Po zaplacení poplatku je možné psa vyzvednout.
        </p>
        <p>Schůzku si můžete domluvit na tomto telefonu: <strong>775839485</strong></p>
      </Modal.Body>
      <Modal.Footer>
        <Button onClick={props.onHide}>Close</Button>
      </Modal.Footer>
    </Modal>
  )
}
