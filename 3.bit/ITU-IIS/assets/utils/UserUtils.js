/**
 * author: Tomáš Szabó 
 * login: xszabo16
 */

import React, { useEffect, useState } from 'react';
import { setTimeout } from 'core-js';
import jwt_decode from 'jwt-decode';
import ModalLogoutComponent from '../Components/User/ModalLogoutComponent';
import ModalWindow from '../Components/Common/GenericModalComponent';


// Gets token from session and compares the roles
export const isUserGranted = (role) => {
    const token = sessionStorage.getItem("token");
    if (token !== null) {
        const decoded = jwt_decode(token);
        if (decoded.roles === null) return false;
        const userRole = decoded.roles[0];
        return role >= UserRoles[userRole];
    } 
    return UserRoles.NOT_AUTHENTICATED === role;
};


// Gets token from session and compares the roles
export const isUserRole = (role) => {
    const token = sessionStorage.getItem("token");
    if (token !== null) {
        const decoded = jwt_decode(token);
        if (decoded.roles === null) return false;
        const userRole = decoded.roles[0];
        return UserRoles[userRole] === role;
    }

    return UserRoles.NOT_AUTHENTICATED === role;
};

// Gets user email from token
export const getUserEmail = () => {
    const token = sessionStorage.getItem("token");
    if (token !== null) {
        const decoded = jwt_decode(token);
        return decoded.email;
    }

    return "";
};

// Implementation of existing solution, modified for our needs - START
// resource: https://github.com/LuminousIT/auth-protected-route/tree/auto-logout-feature
export const AutoLogout = () => {
    let timer;
    const [isWarningModalOpen, setWarningModalOpen] = useState(false);

    useEffect(() => {
        Object.values(events).forEach((item) => {
            window.addEventListener(item, () => {
                resetTimer();
                timer = firstTimeout();
            });
        });
    }, []);

    const logoutAction = () => {
        sessionStorage.clear();
        window.location.pathname = "/login";
    };

    const firstTimeout = () => setTimeout(() =>{
        setWarningModalOpen(true);
    }, 30000);

    const resetTimer = () => {
        if (timer) clearTimeout(timer);
    };

    const secondTimeout = () => {
        timer = setTimeout(() => {
            resetTimer();
            Object.values(events).forEach((item) => {
                window.removeEventListener(item, resetTimer);
            });
            logoutAction();
        }, 5000);
    };

    const listener = () => {
        if(!isWarningModalOpen){
            resetTimer(timer)
            timer = firstTimeout();
        }
    }

    useEffect(() => {
        timer = isWarningModalOpen ? secondTimeout : firstTimeout;
        addEventListeners(listener);

        return () => {
            removeEventListeners(listener);
            resetTimer(timer);
        }
    }, [isWarningModalOpen]);

    return (
        <div>
          {isWarningModalOpen && (<ModalWindow show={isWarningModalOpen} onHide={() => setWarningModalOpen(false)}>
                <ModalLogoutComponent
                    onClose={() => setWarningModalOpen(false)}
                    onLogoff={logoutAction}
                />
                </ModalWindow>
            )
          }
        </div>
      )
};

export const addEventListeners = (listener) => {
    events.forEach((type) => {
      window.addEventListener(type, listener, false)
    })
}
export const removeEventListeners = (listener) => {
    if (listener) {
    events.forEach((type) => {
        window.removeEventListener(type, listener, false)
      })
    }
}

const events = [
    "load",
    "mousemove",
    "mousedown",
    "click",
    "scroll",
    "keypress",
  ];
// Implementation of existing solution, modified for our needs - END
// resource: https://github.com/LuminousIT/auth-protected-route/tree/auto-logout-feature

// ENUMS
export const UserRoles = {
 	ROLE_ADMIN: 0,
 	ROLE_VET: 1,
 	ROLE_SOCIAL_WORKER: 2,
    ROLE_VOLUNTEER: 3,
    ROLE_REQUESTING: 4,
 	ROLE_USER: 5,
    NOT_AUTHENTICATED: 6
 }

