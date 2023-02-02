/**
 * author: Tomáš Szabó 
 * login: xszabo16
 */

import React from "react";
import { Card } from "react-bootstrap";
import { isUserRole, UserRoles } from '../utils/UserUtils';


const LogoutPage = () => {
    if (isUserRole(UserRoles.NOT_AUTHENTICATED)) window.location = "/404";
    sessionStorage.removeItem("token");
    window.location = "/";

    return (
        <Container>
            <Card>
             <Card.Body>Officially logged off!</Card.Body>
            </Card>
        </Container>
    );
}


export default LogoutPage;