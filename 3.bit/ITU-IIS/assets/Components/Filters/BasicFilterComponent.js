//xsuman02

import React from "react";
import Form from "react-bootstrap/Form";
import { useState } from "react";
import { Col, Row } from "react-bootstrap";

export default function FilterComponent(props) {
    const [FormInput, SetFormInput] = useState(null);
    const sendToParent = () => {
        props.getFilters(
            FormInput
        );
    };

    return (
        <Row>
            <Col>
                <Form.Control
                    type="text"
                    placeholder="Uživatel"
                    onChange={(e) => SetFormInput(e.target.value)}
                />
            </Col>
            <Col>
                <button className="button active" onClick={sendToParent}>
                    {" "}
                    Vyhledat
                </button>
            </Col>
        </Row>
    );
}
