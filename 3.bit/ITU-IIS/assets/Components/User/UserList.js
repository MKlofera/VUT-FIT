/**
 * author: Marek Klofera 
 * login: xklofe01
 */
import React from "react";
import { useEffect, useState } from "react";
import Loading from "../Common/Loading";
import TableWithCheckboxesComponent from "../Common/TableWithCheckboxesComponent";

export default function UserList(props) {
    const [SelectedUsers, setSelectedUsers] = useState([]);
    const [Users, setUsers] = useState([]);
    const [Error, setError] = useState(null);
    const [IsLoading, setIsLoading] = useState(true);

    
    // Get data from API
    useEffect(() => {
        const getAllUsers = async () => {
            try {
                let response = await fetch(props.url);
                if (!response.ok) {
                    throw new Error(
                        `This is an HTTP error: The status is ${response.status}`
                    );
                }
                let actualData = await response.json();
                // reset the state
                setUsers(actualData);
                setError(null);
            } catch (err) {
                setError(err.message);
                setUsers(null);
            } finally {
                setIsLoading(false);
            }
        };
        getAllUsers().then((r) => console.log(r));
    }, []);

    function GetSelectedUsersFromChildComponent(SelectedUsers) {
        setSelectedUsers(SelectedUsers);
    }

    useEffect(() => {
    }, [Users]);

    useEffect(() => {}, [SelectedUsers]);

    return (
        <React.Fragment>
            {IsLoading ? (
                <Loading />
            ) : Error ? (
                <Error />
            ) : Users !== null ? (
                <TableWithCheckboxesComponent
                    Header={props.Header}
                    Body={props.Users}
                    SendToParentSelectedRows={
                        GetSelectedUsersFromChildComponent
                    }
                />
            ) : null}
        </React.Fragment>
    );
}
