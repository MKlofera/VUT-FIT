// Tomáš Szabó, xszabo16

import React, { useState, useEffect } from 'react';
import { Form, Button, Container, Nav, Row, Col } from 'react-bootstrap';
import '../../styles/app.scss';

import GenericMessageComponent from '../Common/GenericMessageComponent';

const RegistrationWindow = () => {
    const [email, setEmail] = useState('');
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [message, setMessage] = useState('');
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(false);
    //
    const sendSignUp = async (e) => {
        e.preventDefault();
        try {
            setLoading(true);
            const response = await fetch(
                `/API/user/signup`, {
                method: "POST",
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    email: email.trim(),
                    username: username.trim(),
                    password: password,
                }),
            });
            const data = await response.json();
            setLoading(false);
            if (response.status === 200) {
                setMessage("User created successfully!");
                window.location = "/login";
            } else {
                setError(data['error']);
            }
        } catch (err) {
            setError(err.message);
        }
    }

/// USE EFFECT - START ///
    // Destroy message and error 
    useEffect(() => {
        setTimeout(()=>{
            setMessage("");
        }, 5000)
    }, [message]);

    useEffect(() => {
        setTimeout(()=>{
            setError("");
        }, 5000)
    }, [error]);
/// USE EFFECT - END ///

    return (
        <Container className='form-card RegistrationWindowComponent'>
            <h3 className='registration-title'>Registration</h3>
            {message ? <GenericMessageComponent message={message} variant="primary" /> : ""}
            {error ? <GenericMessageComponent message={error} variant="danger" /> : ""}
            {loading ? <GenericMessageComponent variant="primary" message={"Loading..."} /> : ""}

            <Form onSubmit={sendSignUp}>
                <Form.Group className="mb-3 mx-3" controlId="email">
                    <Form.Label>Email address</Form.Label>
                    <Form.Control type="email" value={email} onChange={(e) => setEmail(e.target.value)} placeholder="Enter email" required/>
                </Form.Group>

                <Form.Group className="mb-3 mx-3" controlId="name">
                    <Form.Label>Your name</Form.Label>
                    <Form.Control placeholder="Enter your name" value={username} onChange={(e) => setUsername(e.target.value)} required/>
                </Form.Group>

                <Form.Group className="mb-3 mx-3" controlId="password">
                    <Form.Label>Password</Form.Label>
                    <Form.Control type="password" placeholder="Password" value={password} onChange={(e) => setPassword(e.target.value)} required/>
                </Form.Group>

                <Container fluid>
                    <Row>
                        <Col className='center'>
                            <Button variant="primary mx-3" type="submit">
                                Sign In
                            </Button>
                        </Col>
                    </Row>
                    <Row className='small-text center'>
                        <Nav>
                            <Col>
                                <Nav.Link href="/login">Login</Nav.Link>
                            </Col>
                        </Nav>
                    </Row>
                </Container>
            </Form>
        </Container>
    )
};

export default RegistrationWindow;