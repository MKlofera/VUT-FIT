/**
 * author: Marek Klofera 
 * login: xklofe01
 */
import React, { useEffect, useState } from "react";

import { isUserGranted, isUserRole, UserRoles } from "../utils/UserUtils";

// Components
import TableWithCheckboxesComponent from "../Components/Common/TableWithCheckboxesComponent";
import UserList from "../Components/User/UserList";
import Loading from "../Components/Common/Loading";
import ErrorComponent from "../Components/Common/Error";
import { Row } from "react-bootstrap";

export default function UserListPage() {
    let TableHeader = ["Jméno", "Typ uživatele", "Email"];
    const urlToFetchData = [
        "/API/user/allNewRegistratedUsers",
        "/API/user/allActiveUsers",
    ];
    const [SelectedUsersIDs, setSelectedUsersIDs] = useState([]);
    const [ActiveUserListView, setActiveUserListView] = useState(true); // if false then new user view is shown
    const [Users, setUsers] = useState(null);
    const [ErrorMsg, setErrorMsg] = useState(null);
    const [IsLoading, setIsLoading] = useState(true);

    const ActiveUserListViewToTrue = () => {
        setActiveUserListView(true);
    };
    const ActiveUserListViewToFalse = () => {
        setActiveUserListView(false);
    };

    const getAllUsers = async (url) => {
        setIsLoading(true);
        try {
            let response = await fetch(urlToFetchData[url]);
            if (!response.ok) {
                throw new Error(
                    `This is an HTTP error: The status is ${response.status}`
                );
            }
            let actualData = await response.json();
            setUsers(changeTypeToEnumValue(actualData));
            setErrorMsg(null);
        } catch (err) {
            setErrorMsg(err.message);
            setUsers(null);
        } finally {
            setIsLoading(false);
        }
    };

    useEffect(() => {
        setIsLoading(true);
        if (ActiveUserListView) {
            getAllUsers(1);
        } else {
            getAllUsers(0);
        }
    }, [ActiveUserListView]);

    const GetSelectedUsersFromChildComponent = (SelectedUsers) => {
        setSelectedUsersIDs(SelectedUsers);
    };

    const submitChangedUserRole = async (data) => {
        setIsLoading(true);
        try {
            const response = await fetch(
                `/API/user/changeUserRole`,
                {
                    method: "POST",
                    body: JSON.stringify(data),
                }
            );
        } catch (err) {
            console.log(err.message);
        }
        if(ActiveUserListView){
            getAllUsers(1);
        } else {
            getAllUsers(0);
        }
    };

    const ChangeUserRoleHandler = (ChangeRoleTo) => {
        let data = [];
        if (SelectedUsersIDs.length > 0) {
            SelectedUsersIDs.forEach((id, index) => {
                data[index] = {
                    id: id,
                    role: ChangeRoleTo,
                };
            });
            submitChangedUserRole(data);
        }
    };

    const DeleteUsersHandler = () => {
        let data = [];
        if (SelectedUsersIDs.length > 0) {
            SelectedUsersIDs.forEach((id, index) => {
                data[index] = {
                    id: id,
                };
            });
            SubmitDeleteSelectedUsers(data);
        }
    };
    const SubmitDeleteSelectedUsers = async (data) => {
        setIsLoading(true);
        try {
            const response = await fetch(
                `/API/user/deleteUsers`,
                {
                    method: "DELETE",
                    body: JSON.stringify(data),
                }
            );
        } catch (err) {
            console.log(err.message);
        }
        if (ActiveUserListView) {
            getAllUsers(1);
        } else {
            getAllUsers(0);
        }
    };

    
    if (
        !isUserGranted(UserRoles.ROLE_ADMIN) &&
        !isUserGranted(UserRoles.ROLE_SOCIAL_WORKER) &&
        !isUserRole(UserRoles.ROLE_VET) &&
        !isUserRole(UserRoles.ROLE_VOLUNTEER)
    ) {
        window.location = "/login";
    } else {
        return (
            <section className="container UserListPage">
                <div className="d-flex justify-content-between my-3">
                    <div>
                        {isUserRole(UserRoles.ROLE_ADMIN) ? (
                            <button
                                className="btn btn-danger m-2"
                                onClick={() => DeleteUsersHandler()}
                            >
                                Smazat
                            </button>
                        ) : null}
                    </div>
                    <div>
                        {isUserRole(UserRoles.ROLE_ADMIN) ? (
                            <button
                                className="btn btn-dark m-2"
                                onClick={() => ChangeUserRoleHandler(0)}
                            >
                                Admin
                            </button>
                        ) : null}
                        {isUserRole(UserRoles.ROLE_ADMIN) ||
                        isUserRole(UserRoles.ROLE_SOCIAL_WORKER) ? (
                            <React.Fragment>
                                <button
                                    className="btn btn-primary m-2"
                                    onClick={() => ChangeUserRoleHandler(1)}
                                >
                                    Veterinář
                                </button>
                                <button
                                    className="btn btn-warning m-2"
                                    onClick={() => ChangeUserRoleHandler(3)}
                                >
                                    Pečovatel
                                </button>
                                <button
                                    className="btn btn-info m-2"
                                    onClick={() => ChangeUserRoleHandler(2)}
                                >
                                    Dobrovolník
                                </button>
                            </React.Fragment>
                        ) : null}
                    </div>
                </div>
                <div className="d-flex justify-content-between">
                    <h2
                        className={
                            ActiveUserListView ? "ActiveTitle" : "DeactiveTitle"
                        }
                        onClick={ActiveUserListViewToTrue}
                    >
                        Stálí uživatelé
                    </h2>
                    <h2
                        className={
                            ActiveUserListView ? "DeactiveTitle" : "ActiveTitle"
                        }
                        onClick={ActiveUserListViewToFalse}
                    >
                        Noví uživatelé
                    </h2>
                </div>

                {IsLoading ? (
                    <Loading />
                ) : ErrorMsg ? (
                    <ErrorComponent Error={ErrorMsg} />
                ) : Users !== null ? (
                    ActiveUserListView ? (
                        <TableWithCheckboxesComponent
                            Header={TableHeader}
                            Body={Users}
                            Links={[]}
                            SendToParentSelectedRows={
                                GetSelectedUsersFromChildComponent
                            }
                        />
                    ) : (
                        <TableWithCheckboxesComponent
                            Header={TableHeader}
                            Body={Users}
                            SendToParentSelectedRows={
                                GetSelectedUsersFromChildComponent
                            }
                        />
                    )
                ) : null}
            </section>
        );
    }
}
const changeTypeToEnumValue = (json) => {
    const EnumUserRole = [
        "Admin",
        "Veterinář",
        "Dobrovolník",
        "Pečovatel",
        "Nový uživatel",
        "Undefined",
    ];
    json.forEach((element) => {
        if (typeof element.role === "number") {
            element.role = EnumUserRole[element.role];
        }
    });
    return json;
};
