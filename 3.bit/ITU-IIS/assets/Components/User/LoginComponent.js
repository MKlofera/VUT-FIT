// Tomáš Szabó, xszabo16

import React, { useState, useEffect } from 'react';
import { Form, Button, Container, Nav, Row, Col } from 'react-bootstrap';
import '../../styles/app.scss';
import GenericMessageComponent from '../Common/GenericMessageComponent';

// utils

const LoginWindow = () => {
    // initialization
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [message, setMessage] = useState('');
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(false);

    // Login
    const sendLogin = async (e) => {
        e.preventDefault();
        try {
            setLoading(true)
            const response = await fetch(
                `/API/login_check`, {
                method: "POST",
                headers: {
                    Accept: 'application/json',
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    email: email.trim(),
                    password: password,
                }),
            });
            let { token } = await response.json();
            setLoading(false);
            if (response.status === 200) {
                sessionStorage.setItem("token", token);
                setMessage("User authenticated successfully!");
                window.location = "/";
            } else {
              setError("Wrong email or password.");
            }
        } catch (err) {
            setError(err.message);
        }
    }

    // Destroy message and error 
    useEffect(() => {
        setTimeout(()=>{
            setMessage("");
        }, 10000)
    }, [message]);

    useEffect(() => {
        setTimeout(()=>{
            setError("");
        }, 10000)
    }, [error]);

    //Front-End
    return (
        <Container className='form-card LoginWindowComponent'>
            {/* messages */}
            {message ? <GenericMessageComponent message={message} variant="primary" /> : ""}
            {error ? <GenericMessageComponent message={error} variant="danger" /> : ""}
            {loading ? <GenericMessageComponent variant="primary" message={"Loading..."} /> : ""}
            {/* messages */}

            <h3 className='login-title'>Login</h3>
            <Form onSubmit={sendLogin}>
                {/* Email */}
                <Form.Group className="mb-3 mx-3" controlId="email">
                    <Form.Label>Email address</Form.Label>
                    <Form.Control type="email" value={email} onChange={(e) => {setEmail(e.target.value)}} placeholder="Enter email" required/>
                </Form.Group>
                {/* Email */}
                {/* Password */}
                <Form.Group className="mb-3 mx-3" controlId="password">
                    <Form.Label>Password</Form.Label>
                    <Form.Control type="password" value={password} onChange={(e) => {setPassword(e.target.value)}} placeholder="Password" required/>
                </Form.Group>
                {/* Password */}
                <Container>
                    <Row>
                        <Col className='center'>
                            <Button variant="primary mx-3" type="submit">
                                Log in
                            </Button>
                        </Col>
                    </Row>
                    <Row className='small-text center'>
                        <Nav>
                            <Col>
                                <Nav.Link href="/signup">Sign In</Nav.Link>
                            </Col>
                        </Nav>
                    </Row>
                </Container>
            </Form>
        </Container>
    )
};

export default LoginWindow;