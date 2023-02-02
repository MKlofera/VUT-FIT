/**
 * author: Tomáš Szabó 
 * login: xszabo16
 */

import React from "react";
import RegistrationWindow from "../Components/User/RegistrationComponent";
import { isUserGranted, isUserRole, UserRoles } from '../utils/UserUtils';

const RegistrationPage = () => {
    if (isUserGranted(UserRoles.ROLE_USER)) window.location = "/404";
    return (
        <section>
            <RegistrationWindow></RegistrationWindow>
        </section>
    );
}

export default RegistrationPage;