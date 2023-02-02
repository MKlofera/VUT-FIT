/**
 * author: Marek Klofera
 * login: xklofe01
 *
 * This page is used to create new medical log and takes the parametr from url to know which dog is the medical log for.
 */
import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";

import Loading from "../Components/Common/Loading";
import Error from "../Components/Common/Error";
import DatePicker from "react-datepicker";

import { isUserGranted, isUserRole, UserRoles } from "../utils/UserUtils";

export default function VetRequestCreationPage() {
    let DogIdFromUrl = useParams().id;

    const [Users, setUsers] = useState(null);
    const [MedicalRequest, SetMedicalRequest] = useState(null);
    const [RequestToServer, SetRequestToServer] = useState(null);
    const [FormData, setFormData] = useState({
        CreatedBy: 0,
        DogId: DogIdFromUrl,
        VetId: 0,
        Type: 0,
        Description: "",
        Priority: 0,
        Status: 0,
    });
    const [ErrorMsg, setErrorMsg] = useState(null);
    const [IsLoading, setIsLoading] = useState(true);
    const getAllUsers = async (url) => {
        setIsLoading(true);
        try {
            let response = await fetch("/API/user/allActiveUsers");
            if (!response.ok) {
                throw new Error(
                    `This is an HTTP error: The status is ${response.status}`
                );
            }
            let actualData = await response.json();
            setUsers(actualData);
            setErrorMsg(null);
        } catch (err) {
            setErrorMsg(err.message);
            setUsers(null);
        } finally {
            setIsLoading(false);
        }
    };

    const saveMedicalLogHandler = () => {
        SetRequestToServer(FormData);
    };

    const SubmitMedicalLog = async (data) => {
        if (data !== null) {
            setIsLoading(true);
            try {
                const response = await fetch(
                    `/API/medicalRequests/new`,
                    {
                        method: "POST",
                        body: JSON.stringify(data),
                    }
                );
            } catch (err) {
                console.log(err.message);
            }
            window.location = `/dog/${DogIdFromUrl}`;
        }
    };

    useEffect(() => {
        getAllUsers();
    }, []);
    useEffect(() => {
        SubmitMedicalLog(RequestToServer);
    }, [RequestToServer]);
    if (
        isUserRole(UserRoles.ROLE_ADMIN) ||
        isUserRole(UserRoles.ROLE_VET) ||
        isUserRole(UserRoles.ROLE_SOCIAL_WORKER)
    ) {
        return (
            <div className="container">
                <h1>Nový veterinární požadavek na psa</h1>
                {IsLoading ? (
                    <Loading />
                ) : ErrorMsg ? (
                    <ErrorComponent Error={ErrorMsg} />
                ) : Users !== null ? (
                    <React.Fragment>
                        <div className="form-group">
                            <label for="exampleFormControlSelect1">
                                Požadavek na veterináře:
                            </label>
                            <select
                                className="form-control"
                                id="exampleFormControlSelect1"
                                onChange={(e) => {
                                    setFormData({
                                        ...FormData,
                                        VetId: e.target.value,
                                    });
                                }}
                            >
                                {Users.map((item, index) => {
                                    return (
                                        <React.Fragment key={index}>
                                            <option
                                                value={item.user.id}
                                                selected="0"
                                            >
                                                {item.user.text}
                                            </option>
                                        </React.Fragment>
                                    );
                                })}
                            </select>
                        </div>
                        <div className="form-group">
                            <label for="exampleFormControlSelect1">
                                Vytvořeno uživatelem:
                            </label>
                            <select
                                className="form-control"
                                id="exampleFormControlSelect1"
                                onChange={(e) => {
                                    setFormData({
                                        ...FormData,
                                        CreatedBy: e.target.value,
                                    });
                                }}
                            >
                                {Users.map((item, index) => {
                                    return (
                                        <React.Fragment key={index}>
                                            <option
                                                value={item.user.id}
                                                selected="0"
                                            >
                                                {item.user.text}
                                            </option>
                                        </React.Fragment>
                                    );
                                })}
                            </select>
                        </div>
                    </React.Fragment>
                ) : null}
                <div className="form-group">
                    <label for="exampleFormControlSelect1">Typ zákroku</label>
                    <select
                        className="form-control"
                        id="exampleFormControlSelect1"
                        onChange={(e) => {
                            setFormData({ ...FormData, Type: e.target.value });
                        }}
                    >
                        {EnumMedicalLogs.map((item, index) => {
                            return (
                                <React.Fragment key={index}>
                                    <option value={index}>{item}</option>
                                </React.Fragment>
                            );
                        })}
                    </select>
                </div>
                <div className="form-group">
                    <label for="exampleFormControlSelect1">
                        Priorita zákroku
                    </label>
                    <select
                        className="form-control"
                        id="exampleFormControlSelect1"
                        onChange={(e) => {
                            setFormData({
                                ...FormData,
                                Status: e.target.value,
                            });
                        }}
                    >
                        {EnumPriority.map((item, index) => {
                            return (
                                <React.Fragment key={index}>
                                    <option value={index}>{index}</option>
                                </React.Fragment>
                            );
                        })}
                    </select>
                </div>
                <div className="form-group">
                    <label for="inputAddress">Detail zprávy</label>
                    <textarea
                        className="form-control"
                        id="exampleFormControlTextarea1"
                        rows="5"
                        onChange={(e) => {
                            setFormData({
                                ...FormData,
                                Description: e.target.value,
                            });
                        }}
                    ></textarea>
                </div>
                <div className="row">
                    <button
                        className="btn btn-success"
                        onClick={() => {
                            saveMedicalLogHandler();
                        }}
                    >
                        Uložit záznam
                    </button>
                </div>
            </div>
        );
    } else {
        window.location = "/login";
    }
}
const EnumMedicalLogs = [
    "Očkování",
    "Operace",
    "Kastrace",
    "Kontrola",
    "Ošetření",
    "Ostatní",
];
const EnumPriority = ["1", "2", "3", "4"];
