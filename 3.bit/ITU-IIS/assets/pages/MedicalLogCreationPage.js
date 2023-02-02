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


export default function MedicalLogCreationPage() {
    const [Users, setUsers] = useState(null);
    const [MedicalLog, SetMedicalLog] = useState(null);
    const [MedicalLogType, SetMedicalLogType] = useState(0);
    const [MedicalLogDate, SetMedicalLogDate] = useState(
        new Date("2015-03-25 1:2:2")
    );
    const [MedicalLogVetId, SetMedicalLogVetId] = useState(null);
    const [MedicalLogDescription, SetMedicalLogDescription] = useState("");
    const [ErrorMsg, setErrorMsg] = useState(null);
    const [IsLoading, setIsLoading] = useState(true);
    let DogIdFromUrl = useParams().id;

    const getAllUsers = async (url) => {
        setIsLoading(true);
        try {
            let response = await fetch(
                "/API/user/allActiveUsers"
            );
            if (!response.ok) {
                throw new Error(
                    `This is an HTTP error: The status is ${response.status}`
                );
            }
            let actualData = await response.json();
            setUsers(actualData);
            SetMedicalLogVetId(actualData[0].id);
            setErrorMsg(null);
        } catch (err) {
            setErrorMsg(err.message);
            setUsers(null);
        } finally {
            setIsLoading(false);
        }
    };

    const saveMedicalLogHandler = () => {
        SetMedicalLog({
            dogId: DogIdFromUrl,
            type: MedicalLogType,
            description: MedicalLogDescription,
            vetId: MedicalLogVetId,
            date: MedicalLogDate,
        });
    };

    const SubmitMedicalLog = async (data) => {
        if (data !== null) {
            setIsLoading(true);
            try {
                const response = await fetch(
                    `/API/medicalLog/new`, // TODO change to production API
                    {
                        method: "POST",
                        body: JSON.stringify({
                            vetId: data.vetId,
                            dogId: data.dogId,
                            type: data.type,
                            date: data.date,
                            description: data.description,
                        }),
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
    useEffect(() => {}, [
        MedicalLogDescription,
        MedicalLogVetId,
        MedicalLogType,
        MedicalLogDate,
    ]);
    useEffect(() => {
        SubmitMedicalLog(MedicalLog);
    }, [MedicalLog]);

    if (
        isUserRole(UserRoles.ROLE_ADMIN) ||
        isUserRole(UserRoles.ROLE_VET) ||
        isUserRole(UserRoles.ROLE_SOCIAL_WORKER)
    ) {
        return (
            <div className="container">
                <h1>Nový veterinární záznam o psovi</h1>
                {IsLoading ? (
                    <Loading />
                ) : ErrorMsg ? (
                    <ErrorComponent Error={ErrorMsg} />
                ) : Users !== null ? (
                    <div className="form-group">
                        <label for="exampleFormControlSelect1">
                            Zákrok provedl
                        </label>
                        <select
                            className="form-control"
                            id="exampleFormControlSelect1"
                            defaultValue={Users[0].id}
                            onChange={(e) => {
                                SetMedicalLogVetId(e.target.value);
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
                ) : null}
                <div className="form-group">
                    <label for="exampleFormControlSelect1">Typ zákroku</label>
                    <select
                        className="form-control"
                        id="exampleFormControlSelect1"
                        defaultValue={0}
                        onChange={(e) => {
                            SetMedicalLogType(e.target.value);
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
                    <label for="inputAddress">Detail zprávy</label>
                    <textarea
                        className="form-control"
                        id="exampleFormControlTextarea1"
                        rows="5"
                        onChange={(e) => {
                            SetMedicalLogDescription(e.target.value);
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
    }
    else {
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
