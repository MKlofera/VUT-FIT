// xsuman02

import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import Loading from "../Components/Common/Loading";

export default function MedicalRequestDetailPage() {
    const [Users, setUsers] = useState(null);
    const [MedicalRequestDetail, SetMedicalRequestDetail] = useState(null);
    const [EditedMedicalRequestDetailToServer, SetEditedMedicalRequestDetailToServer] =
        useState(null);
    const [DisplayEditingView, setDisplayEditingView] = useState(false);
    const [IsLoading, setIsLoading] = useState(true);
    const [Error, setError] = useState(null);
    const [ErrorMsg, setErrorMsg] = useState(null);

    let MedicalRequestIdFromUrl = useParams().id;

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

    const getMedicalRequestFromServer = async () => {
        try {
            const response = await fetch(
                `/API/medicalRequests/${MedicalRequestIdFromUrl}`
            );
            if (!response.ok) {
                console.log(response);
                return;
            }
            let actualData = await response.json();
            SetMedicalRequestDetail(actualData);
            setError(null);
        } catch (err) {
            setError(err.message);
            console.log(err);
            SetMedicalRequestDetail(null);
        } finally {
            setIsLoading(false);
        }
    };

    const SwitchToEditiginView = () => {
        setDisplayEditingView(!DisplayEditingView);
        getAllUsers();
    };

    const SaveEditedMedicalRequestHandler = () => {
        setDisplayEditingView(!DisplayEditingView);
        SetEditedMedicalRequestDetailToServer(MedicalRequestDetail);
    };

    const SendEditedMedicalRequestToServer = async (data) => {
        setIsLoading(true);
        try {
            const response = await fetch(
                `/API/medicalRequests/edit/${MedicalRequestIdFromUrl}`,
                {
                    method: "POST",
                    body: JSON.stringify({
                        id: data.id,
                        type: data.type,
                        text: data.text,
                        prio: data.prio,
                        vetId: data.vetId,
                        status: data.status
                    }),
                }
            );
        } catch (err) {
            console.log(err.message);
        }
        getMedicalRequestFromServer();
    };

    const MedicalRequestDescriptionHandler = (e) => {
        SetMedicalRequestDetail({ ...MedicalRequestDetail, text: e.target.value });
    };
    const MedicalRequestTypeHandler = (e) => {
        SetMedicalRequestDetail({ ...MedicalRequestDetail, type: e.target.value });
    };
    const MedicalRequestPrioHandler = (e) => {
        SetMedicalRequestDetail({ ...MedicalRequestDetail, prio: e.target.value });
    };
    const MedicalRequestVetHandler = (e) => {
        SetMedicalRequestDetail({ ...MedicalRequestDetail, vetId: e.target.value });
    };
    const MedicalRequestStatusHandler = (e) => {
        SetMedicalRequestDetail({ ...MedicalRequestDetail, status: e.target.value });
    };

    useEffect(() => {
        getMedicalRequestFromServer();
    }, []);

    useEffect(() => {
        if (EditedMedicalRequestDetailToServer !== null)
            SendEditedMedicalRequestToServer(EditedMedicalRequestDetailToServer);
    }, [EditedMedicalRequestDetailToServer]);

    return (
        <div className="container">
            <div className="my-3">
                <button
                    className={
                        DisplayEditingView
                            ? "btn btn-outline-warning col-6"
                            : "btn btn-warning col-6"
                    }
                    onClick={() => {
                        SwitchToEditiginView(!DisplayEditingView);
                    }}
                >
                    Upravit po??adavek
                </button>
                {DisplayEditingView ? (
                    <button
                        className="btn btn-success col-6"
                        onClick={SaveEditedMedicalRequestHandler}
                    >
                        Ulo??it po??adavek
                    </button>
                ) : null}
            </div>
            {IsLoading ? (
                <Loading />
            ) : ErrorMsg ? (
                <ErrorComponent Error={ErrorMsg} />
            ) : (
                <React.Fragment>
                    {DisplayEditingView ? (
                        <React.Fragment>
                            <div className="form-group">
                                <label for="createdTo">
                                    Vybrat veterin????e
                                </label>
                                <select
                                    className="form-control"
                                    id="createdTo"
                                    onChange={(e) => {
                                        MedicalRequestVetHandler(e);
                                    }}
                                >
                                    {Users.map((item, index) => {
                                        return (
                                            <React.Fragment key={index}>
                                                <option
                                                    value={item.user.id}
                                                    selected={
                                                        index ===
                                                        MedicalRequestDetail.vetId
                                                    }
                                                >
                                                    {item.user.text}
                                                </option>
                                            </React.Fragment>
                                        );
                                    })}
                                </select>
                            </div>
                            <div class="form-group">
                                <label for="exampleFormControlSelect1">
                                    Priorita (1-5)
                                </label>
                                <select
                                    class="form-control"
                                    id="exampleFormControlSelect1"
                                    onChange={(e) => {
                                        MedicalRequestPrioHandler(e);
                                    }}
                                >
                                    {EnumPriority.map((item, index) => {
                                        return (
                                            <React.Fragment key={index}>
                                                <option
                                                    value={index}
                                                    selected={
                                                        index ===
                                                        MedicalRequestDetail.priority
                                                    }
                                                >
                                                    {item}
                                                </option>
                                            </React.Fragment>
                                        );
                                    })}
                                </select>
                            </div>
                            <div class="form-group">
                                <label for="exampleFormControlSelect1">
                                    Typ z??kroku
                                </label>
                                <select
                                    class="form-control"
                                    id="exampleFormControlSelect1"
                                    onChange={(e) => {
                                        MedicalRequestTypeHandler(e);
                                    }}
                                >
                                    {EnumMedicalLogs.map((item, index) => {
                                        return (
                                            <React.Fragment key={index}>
                                                <option
                                                    value={index}
                                                    selected={
                                                        index ===
                                                        MedicalRequestDetail.type
                                                    }
                                                >
                                                    {item}
                                                </option>
                                            </React.Fragment>
                                        );
                                    })}
                                </select>
                            </div>
                            <div class="form-group">
                                <label for="inputAddress">Detail zpr??vy</label>
                                <textarea
                                    class="form-control"
                                    id="exampleFormControlTextarea1"
                                    rows="3"
                                    onChange={(e) => {
                                        MedicalRequestDescriptionHandler(e);
                                    }}
                                    value={MedicalRequestDetail.text}
                                ></textarea>
                            </div>
                            <div class="form-group">
                                <label for="exampleFormControlSelect1">
                                    Stav
                                </label>
                                <select
                                    class="form-control"
                                    id="exampleFormControlSelect1"
                                    onChange={(e) => {
                                        MedicalRequestStatusHandler(e);
                                    }}
                                >
                                    {EnumStatus.map((item, index) => {
                                        return (
                                            <React.Fragment key={index + 1}>
                                                <option
                                                    value={index + 1}
                                                    selected={
                                                        index + 1 ===
                                                        MedicalRequestDetail.status
                                                    }
                                                >
                                                    {item}
                                                </option>
                                            </React.Fragment>
                                        );
                                    })}
                                </select>
                            </div>
                        </React.Fragment>
                    ) : (
                        <React.Fragment>
                            <h4>Po??adavek vytvo??il</h4>
                            <p>{MedicalRequestDetail.worker.text}</p>
                            <h4>P??i??azen?? veterin????</h4>
                            <p>{MedicalRequestDetail.vet.text}</p>
                            <h4>Pes</h4>
                            <p>{MedicalRequestDetail.dog.text}</p>
                            <h4>Priorita</h4>
                            <p>{MedicalRequestDetail.prio}</p>
                            <h4>Typ z??kroku</h4>
                            <p>{EnumMedicalLogs[MedicalRequestDetail.type]}</p>
                            <h4>Detail zpr??vy</h4>
                            <p>{MedicalRequestDetail.text}</p>
                            <h4>Stav</h4>
                            <p>{EnumStatus[MedicalRequestDetail.status - 1]}</p>
                        </React.Fragment>
                    )}
                </React.Fragment>
            )}
        </div>
    );
}
const EnumMedicalLogs = [
    "O??kov??n??",
    "Operace",
    "Kastrace",
    "Kontrola",
    "O??et??en??",
    "Ostatn??",
];

const EnumPriority = [
    "1 - Nejvy??????",
    "2 - Vy??????",
    "3 - Pr??m??rn??",
    "4 - Ni??????",
    "5 - Nejni??????"
]

const EnumStatus = [
    "Vytvo??en",
    "Dokon??en"
]