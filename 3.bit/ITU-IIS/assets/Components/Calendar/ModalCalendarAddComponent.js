/**
 * author: Tomáš Szabó 
 * login: xszabo16
 */
import React, { useEffect, useState } from 'react';
import { Container, Form, Modal, Button } from "react-bootstrap";
import { utc } from "moment";
import { tz } from "moment-timezone";
import Datetime from "react-datetime";
import GenericMessageComponent from "../Common/GenericMessageComponent";
import moment from 'moment';

const ModalCalendarAddComponent = (props) => {
    // Form values
    const [startDate, setStartDate] = useState(props.eventInformation.startStr);
    const [endDate, setEndDate] = useState(props.eventInformation.endStr);
    const [dog, setDog] = useState(props.eventInformation.dog ?? props.DogId );
    const [type, setType] = useState(props.eventInformation.type ?? 0);

    // Validators
    const [validDog, setValidDog] = useState(true);
    const [validType, setValidType] = useState(true);
    const [validStartTime, setValidStartTime] = useState(true);
    const [validEndTime, setValidEndTime] = useState(true);

    // Change handlers
    const handleStartChange = (e) => {
        setStartDate(e);
    }
    const handleEndChange = (e) => {
        setEndDate(e);
    }
    const handleTypeChange = (e) => {
        setType(e);
    }
    const handleDogChange = (e) => {
        setDog(e)
    }
    // Change handlers

    // Form validation on submit
    const isFormValid = () => {
        let valid = true;
        console.log(dog);
        if ((parseInt(dog) === 0 || dog === null || dog === undefined)) {
            valid = false;
            setValidDog(false);
        } else {
            setValidDog(true);
        }
        if ((parseInt(type) === 0 || type === null || type === undefined)) {
            valid = false;
            setValidType(false);
        }else {
            setValidType(true);
        }
        if (moment.utc(endDate).valueOf() < moment.utc(startDate).valueOf()) {
            valid = false;
            setValidStartTime(false);
            setValidEndTime(false);
        } else {
            setValidStartTime(true);
            setValidEndTime(true);
        }
        return valid;
    }

    // Submit function
    const handleOnSubmit = async (e) => {
        e.preventDefault();
        props.handleLoading(true);
        if (!isFormValid()) {
            props.handleLoading(false);
            e.stopPropagation();
        } else {
            props.submitEvent();
        }
    }

    useEffect(() => {
        const handleChange = () => {
            props.onChange({
                id: props.eventInformation.id ?? 0,
                startStr: startDate,
                endStr: endDate,
                type: parseInt(type),
                status: 0,
                dog: parseInt(dog),
            })
        }
        handleChange();
    }, [startDate, endDate, dog, type]);

    // Front-End
    return (
        <Container>
            <Form  onSubmit={(e) => handleOnSubmit(e)}>
                <Modal.Header closeButton>
                    <Modal.Title>{props.title ?? "Title"}</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                {/* Type */}
                <Form.Group className="mb-3" controlId="type">
                    <Form.Label>Select event type</Form.Label>
                    <Form.Select value={type} onChange={(e) => handleTypeChange(e.target.value)} aria-label="Select event type" required>
                        <option value={0}>Select type of event</option>
                        {props.types.map((type) => {
                            return <option value={type.id}>{type.name}</option>
                            })
                        }
                    </Form.Select>
                    <small style={!validType ? { color: "red", display: 'inline' } : {display: 'none'}}>Choose a valid type option!</small>
                </Form.Group>
                {/* Type */}
                {/* Dog */}
                <Form.Group className="mb-3" controlId="dog">
                    <Form.Label>Doggo</Form.Label>
                    <Form.Select value={dog} onChange={(e) => handleDogChange(e.target.value)} aria-label="Select a dog" required>
                        return <option value={0}>Select a dog</option>
                        {props.dogs.map((doggo, index) => {
                            return <option value={doggo.id}>{doggo.Name}</option>
                        })
                        }
                    </Form.Select>
                    <small style={!validDog ? { color: "red", display: 'inline' } : {display: 'none'}}>Choose a valid dog option!</small>
                </Form.Group>
                {/* Dog */}
                {/* StartDate */}
                <Form.Group className="mb-3" controlId="startDate">
                    <Form.Label>From</Form.Label>
                    <Datetime
                        inputProps={{ id: "startDate", required: true }}
                        value={startDate ? startDate : props.eventInformation.startStr}
                        closeOnSelect={true}
                        onChange={(e) => handleStartChange(e)}
                        dateFormat="YYYY-MM-DD"
                        timeFormat="HH:mm"
                        displayTimeZone={tz.guess()}

                        isValidDate={
                            (current) => {
                                return current.isSameOrAfter(utc().subtract(1, "day"));
                            }
                        }
                        required
                    />
                    <small style={!validStartTime ? { color: "red", display: 'inline' } : {display: 'none'}}>Choose a valid date time!</small>
                </Form.Group>
                {/* StartDate */}
                {/* EndDate */}
                <Form.Group className="mb-3" controlId="endDate">
                    <Form.Label>To</Form.Label>
                    <Datetime
                        inputProps={{ id: "endDate", required: true }}
                        value={endDate ? endDate : props.eventInformation.endStr}
                        closeOnSelect={true}
                        onChange={(e) => handleEndChange(e)}
                        dateFormat="YYYY-MM-DD"
                        timeFormat="HH:mm"
                        displayTimeZone={tz.guess()}
                        isValidDate={
                            (current) => {
                                return current.isSameOrAfter(utc(startDate));
                            }
                        }
                        required
                    />
                    <small style={!validEndTime ? { color: "red", display: 'inline' } : {display: 'none'}}>Choose a valid date time!</small>
                </Form.Group>
                </Modal.Body>
                {/* EndDate */}

                <Modal.Footer>
                    {props.loadingValue ? <GenericMessageComponent variant="primary" message={"Loading..."} /> : ""}
                    <Button variant="secondary" id="modal-close" onClick={props.onSubmit}>
                        {props.close ?? "Close"}
                    </Button>
                    {props.isEdit || props.isAdd ?
                    <Button variant="primary" type="submit" onClick={props.onHide}>
                        {props.submit ?? "Save"}
                    </Button> : ""}
                    {props.isEdit && !props.isAdd ?
                        <Button variant="danger" id="modal-delete" onClick={props.deleteEvent}>
                            {props.delete ?? "Delete"}
                        </Button> : ""}
                </Modal.Footer>
            </Form>
        </Container>
    );
}

export default ModalCalendarAddComponent;