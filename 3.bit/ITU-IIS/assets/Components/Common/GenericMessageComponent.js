/**
 * author: Tomáš Szabó 
 * login: xszabo16
 */
import React from "react";
import { Alert, Fade } from "react-bootstrap";

export default function GenericMessageComponent(props) {
    return (
        <Alert variant={props.variant} transition={Fade}>
            {props.message}
        </Alert>
    );
}
