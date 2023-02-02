/**
 * author: Tomáš Szabó 
 * login: xszabo16
 */

import React from "react";
import LoginWindow from "../Components/User/LoginComponent";
import { isUserGranted, isUserRole, UserRoles } from '../utils/UserUtils';


const LoginPage = () => {
    if (isUserGranted(UserRoles.ROLE_USER)) window.location = "/404";
    return (
        <section>
            <LoginWindow></LoginWindow>
        </section>
    );
}


export default LoginPage;