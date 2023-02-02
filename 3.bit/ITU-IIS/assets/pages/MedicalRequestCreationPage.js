// xsuman02

import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";

import Loading from "../Components/Common/Loading";
import Error from "../Components/Common/Error";

import { isUserGranted, isUserRole, UserRoles } from "../utils/UserUtils";


export default function MedicalRequestCreationPage() {
    const [Users, setUsers] = useState(null);
    const [MedicalRequest, SetMedicalRequest] = useState(null);
    const [MedicalRequestVetId, SetMedicalRequestVetId] = useState(null);
    const [MedicalRequestWorkerId, SetMedicalRequestWorkerId] = useState(null);
    const [MedicalRequestType, SetMedicalRequestType] = useState(null);
    const [MedicalRequestDescription, SetMedicalRequestDescription] = useState(null);
    const [MedicalRequestPriority, SetMedicalRequestPriority] = useState(null);
    const [ErrorMsg, setErrorMsg] = useState(null);
    const [IsLoading, setIsLoading] = useState(true);
    let DogIdFromUrl = useParams().id;

    const getAllUsers = async () => {
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
            setErrorMsg(null);
        } catch (err) {
            setErrorMsg(err.message);
            setUsers(null);
        } finally {
            setIsLoading(false);
        }
    };

    const saveMedicalRequestHandler = () => {
        SetMedicalRequest({
            dogId: DogIdFromUrl,
            type: MedicalRequestType,
            description: MedicalRequestDescription,
            vetId: MedicalRequestVetId,
            workerId: MedicalRequestWorkerId,
            priority: MedicalRequestPriority,
        });
    };

    const SubmitMedicalRequest = async (data) => {
        if (data !== null) {
            setIsLoading(true);
            try {
                const response = await fetch(
                    `/API/medicalRequests/new`,
                    {
                        method: "POST",
                        body: JSON.stringify({
                            vetId: data.vetId,
                            workerId: data.workerId,
                            dogId: data.dogId,
                            type: data.type,
                            priority: data.priority,
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
    useEffect(() => { }, [
        MedicalRequestWorkerId,
        MedicalRequestVetId,
        MedicalRequestType,
        MedicalRequestPriority,
        MedicalRequestDescription,
    ]);
    useEffect(() => {
        SubmitMedicalRequest(MedicalRequest);
    }, [MedicalRequest]);

    if (
        isUserRole(UserRoles.ROLE_ADMIN) ||
        isUserRole(UserRoles.ROLE_VET) ||
        isUserRole(UserRoles.ROLE_SOCIAL_WORKER)
    ) {
        return (
            <div className="container">
                <h1>Nový veterinární požadavek</h1>
                {IsLoading ? (
                    <Loading />
                ) : ErrorMsg ? (
                    <ErrorComponent Error={ErrorMsg} />
                ) : Users !== null ? (
                    <div className="form-group">
                        <label for="createdBy">
                            Požadavek vytvořil
                        </label>
                        <select
                            className="form-control"
                            id="createdBy"
                            onChange={(e) => {
                                SetMedicalRequestWorkerId(e.target.value);
                            }}
                        >
                            {Users.map((item, index) => {
                                return (
                                    <React.Fragment key={index}>
                                        <option
                                            value={item.user.id}
                                            defaultValue="0"
                                        >
                                            {item.user.text}
                                        </option>
                                    </React.Fragment>
                                );
                            })}
                        </select>
                    </div>
                ) : null}
                {IsLoading ? (
                    <Loading />
                ) : ErrorMsg ? (
                    <ErrorComponent Error={ErrorMsg} />
                ) : Users !== null ? (
                    <div className="form-group">
                        <label for="createdTo">
                            Vybrat veterináře
                        </label>
                        <select
                            className="form-control"
                            id="createdTo"
                            onChange={(e) => {
                                SetMedicalRequestVetId(e.target.value);
                            }}
                        >
                            {Users.map((item, index) => {
                                return (
                                    <React.Fragment key={index}>
                                        <option
                                            value={item.user.id}
                                            defaultValue="1"
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
                    <label for="type">Typ zákroku</label>
                    <select
                        className="form-control"
                        id="type"
                        onChange={(e) => {
                            SetMedicalRequestType(e.target.value);
                        }}
                    >
                        {EnumMedicalRequests.map((item, index) => {
                            return (
                                <React.Fragment key={index}>
                                    <option value={index}>{item}</option>
                                </React.Fragment>
                            );
                        })}
                    </select>
                </div>
                <div className="form-group">
                    <label for="exampleFormControlSelect1">Priorita (1-5)</label>
                    <select
                        className="form-control"
                        id="exampleFormControlSelect1"
                        onChange={(e) => {
                            SetMedicalRequestPriority(e.target.value);
                        }}
                    >
                        {EnumPriority.map((item, index) => {
                            return (
                                <React.Fragment key={index+1}>
                                    <option value={index+1}>{item}</option>
                                </React.Fragment>
                            );
                        })}
                    </select>
                </div>
                <div className="form-group">
                    <label for="inputAddress">Detail zprávy</label>
                    <textarea
                        className="form-control"
                        id="inputAddress"
                        rows="5"
                        onChange={(e) => {
                            SetMedicalRequestDescription(e.target.value);
                        }}
                    ></textarea>
                </div>
                <div className="row">
                    <button
                        className="btn btn-success"
                        onClick={() => {
                            saveMedicalRequestHandler();
                        }}
                    >
                        Uložit požadavek
                    </button>
                </div>
            </div>
        );
    }
    else {
        window.location = "/login";
    }
}
const EnumMedicalRequests = [
    "Očkování",
    "Operace",
    "Kastrace",
    "Kontrola",
    "Ošetření",
    "Ostatní",
];

const EnumPriority = [
    "1 - Nejvyšší",
    "2 - Vyšší",
    "3 - Průměrná",
    "4 - Nižší",
    "5 - Nejnižší"
]