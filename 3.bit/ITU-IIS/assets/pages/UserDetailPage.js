/**
 * author: Marek Klofera
 * login: xklofe01
 */
import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";

import Loading from "../Components/Common/Loading";
import Error from "../Components/Common/Error";
import ModalComponent from "../Components/Common/ModalComponent";
import Datetime from "react-datetime";

import { isUserGranted, isUserRole, UserRoles } from "../utils/UserUtils";

export default function UserDetailPage() {
    const [UserDetail, setUserDetail] = useState(null);
    const [UserEditedToServer, setUserEditedToServer] = useState(null);
    const [Error, setError] = useState(null);
    const [IsLoading, setIsLoading] = useState(true);
    const [DisplayEditingView, setDisplayEditingView] = useState(false);
    const [DeleteModalShow, setDeleteModalShow] = useState(false);

    const UserIdFromUrl = useParams().id;
    const getUserDetail = async (url) => {
        try {
            let response = await fetch(
                `/API/user/${UserIdFromUrl}`
            );
            if (!response.ok) {
                // window.location = "/404";
            }
            let actualData = await response.json();
            setUserDetail(actualData);
            setError(null);
        } catch (err) {
            setError(err.message);
            setUserDetail(null);
        } finally {
            setIsLoading(false);
        }
    };
    useEffect(() => {
        getUserDetail().then((r) => console.log(r));
    }, []);

    const EditUserHandler = () => {
        setDisplayEditingView(!DisplayEditingView);
    };

    function onInputchange(event) {
        setUserDetail({
            ...UserDetail,
            [event.target.name]: event.target.value,
        });
    }
    const DeleteUserHandler = async () => {
        setDeleteModalShow(false);
        setIsLoading(true);
        try {
            const response = await fetch(
                `/API/user/deleteUsers`,
                {
                    method: "DELETE",
                    body: JSON.stringify([{ id: UserIdFromUrl }]),
                }
            );
        } catch (err) {
            console.log(err.message);
        }
        window.location = "/user-list";
    };
    const SaveEditedUserHandler = () => {
        setDisplayEditingView(!DisplayEditingView);
        setUserEditedToServer(UserDetail);
    };
    const sendEditedUserToServer = async (data) => {
        setIsLoading(true);
        try {
            const response = await fetch(
                `/API/user/${UserEditedToServer.id}/edit`,
                {
                    method: "POST",
                    body: JSON.stringify(data),
                }
            );
        } catch (err) {
            console.log(err.message);
        }
        getUserDetail();
    };

    
    useEffect(() => {}, [DisplayEditingView, UserDetail]);
    useEffect(() => {
        if (UserEditedToServer !== null)
            sendEditedUserToServer(UserEditedToServer);
    }, [UserEditedToServer]);

    if (
        !isUserRole(UserRoles.ROLE_ADMIN) &&
        !isUserRole(UserRoles.ROLE_VET) &&
        !isUserRole(UserRoles.ROLE_SOCIAL_WORKER)
    ) {
        window.location = "/login";
    } else {
        return (
            <div className="container">
                {isUserRole(UserRoles.ROLE_ADMIN) ? (
                    <div className="row mt-3">
                        <ModalComponent
                            show={DeleteModalShow}
                            onHide={() => setDeleteModalShow(false)}
                            onSubmit={() => DeleteUserHandler()}
                            Title={UserDeleteModalText.Title}
                            Body={UserDeleteModalText.Body}
                            SubmitBtnText={UserDeleteModalText.SubmitBtnText}
                        />
                        <button
                            className="btn btn-danger col-6"
                            onClick={() => setDeleteModalShow(true)}
                        >
                            Smazat uživatele
                        </button>
                        <button
                            className={
                                DisplayEditingView
                                    ? "btn btn-outline-warning col-6"
                                    : "btn btn-warning col-6"
                            }
                            onClick={EditUserHandler}
                        >
                            Upravit uživatele
                        </button>
                        {DisplayEditingView ? (
                            <button
                                className="btn btn-success my-2"
                                onClick={SaveEditedUserHandler}
                            >
                                Uložit
                            </button>
                        ) : null}
                    </div>
                ) : null}
                {IsLoading ? (
                    <Loading />
                ) : Error ? (
                    <Error />
                ) : UserDetail !== null ? (
                    <React.Fragment>
                        {IsLoading ? (
                            <Loading />
                        ) : Error ? (
                            <Error />
                        ) : (
                            <React.Fragment>
                                {DisplayEditingView ? (
                                    <React.Fragment>
                                        <input
                                            type="text"
                                            class="form-control"
                                            name="Name"
                                            onChange={onInputchange}
                                            value={UserDetail.Name}
                                        />
                                        <div className="form-group p-1">
                                            <select
                                                className="form-select"
                                                name="Role"
                                                onChange={onInputchange}
                                            >
                                                {EnumRoles.map(
                                                    (element, index) => {
                                                        return (
                                                            <React.Fragment
                                                                key={index}
                                                            >
                                                                <option
                                                                    value={
                                                                        index
                                                                    }
                                                                    selected={
                                                                        index ===
                                                                        UserDetail.Role
                                                                    }
                                                                >
                                                                    {element}
                                                                </option>
                                                            </React.Fragment>
                                                        );
                                                    }
                                                )}
                                                ;
                                            </select>
                                        </div>
                                    </React.Fragment>
                                ) : (
                                    <div>
                                        <h1><strong>Jméno:</strong> {UserDetail.Name}</h1>
                                        <p> <strong>Role: </strong> {EnumRoles[UserDetail.Role]}</p>
                                        <p> <strong>Email: </strong> {UserDetail.Email}</p>
                                    </div>
                                )}
                            </React.Fragment>
                        )}
                    </React.Fragment>
                ) : null}
            </div>
        );
    }
}
const EnumRoles = [
    "Admin",
    "Veterinář",
    "Pečovatel",
    "Dobrovolník",
    "Nový uživatel",
    "Undefined",
];
const UserDeleteModalText = {
    Title: "Opravdu chcete smazat tohoto uživatele?",
    Body: "Tato akce je nevratná!",
    SubmitBtnText: "Smazat uživatele",
};
