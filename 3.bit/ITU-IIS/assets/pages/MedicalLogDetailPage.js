/**
 * author: Marek Klofera
 * login: xklofe01
 */
import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";

import Loading from "../Components/Common/Loading";
import Error from "../Components/Common/Error";
import ModalComponent from "../Components/Common/ModalComponent";

export default function MedicalLogDetailPage() {
    const [MedicalLogDetail, SetMedicalLogDetail] = useState(null);
    const [EditedMedicalLogDetailToServer, SetEditedMedicalLogDetailToServer] =
        useState(null);
    const [DisplayEditingView, setDisplayEditingView] = useState(false);
    const [IsLoading, setIsLoading] = useState(true);
    const [Error, setError] = useState(null);

    let MedicalLogIdFromUrl = useParams().id;

    // get medical log detail from server
    const getMedicalLogFromServer = async () => {
        try {
            const response = await fetch(
                `/API/medicalLog/${MedicalLogIdFromUrl}` // TODO change to production API
            );
            if (!response.ok) {
                console.log(response);
                // window.location = "/404";
                return;
            }
            let actualData = await response.json();
            SetMedicalLogDetail(actualData);
            setError(null);
        } catch (err) {
            setError(err.message);
            console.log(err);
            SetMedicalLogDetail(null);
        } finally {
            setIsLoading(false);
        }
    };

    const SaveEditedMedicalLogHandler = () => {
        setDisplayEditingView(!DisplayEditingView);
        SetEditedMedicalLogDetailToServer(MedicalLogDetail);
    };

    // send edited medical log to server
    const SendEditedMedicalLogToServer = async (data) => {
        setIsLoading(true);
        try {
            const response = await fetch(
                `/API/medicalLog/edit/${MedicalLogIdFromUrl}`, // TODO change to production API
                {
                    method: "POST",
                    body: JSON.stringify({
                        id: data.id,
                        type: data.type,
                        text: data.text,
                    }),
                }
            );
        } catch (err) {
            console.log(err.message);
        }
        getMedicalLogFromServer();
    };
    const MedicalLogDescriptionHandler = (e) => {
        SetMedicalLogDetail({ ...MedicalLogDetail, text: e.target.value });
    };
    const MedicalLogTypeHandler = (e) => {
        SetMedicalLogDetail({ ...MedicalLogDetail, type: e.target.value });
    };

    useEffect(() => {
        getMedicalLogFromServer();
    }, []);

    useEffect(() => {
        if (EditedMedicalLogDetailToServer !== null)
            SendEditedMedicalLogToServer(EditedMedicalLogDetailToServer);
    }, [EditedMedicalLogDetailToServer]);

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
                        setDisplayEditingView(!DisplayEditingView);
                    }}
                >
                    Upravit záznam
                </button>
                {DisplayEditingView ? (
                    <button
                        className="btn btn-success col-6"
                        onClick={SaveEditedMedicalLogHandler}
                    >
                        Uložit záznam
                    </button>
                ) : null}
            </div>
            {IsLoading ? (
                <Loading />
            ) : Error ? (
                <Error />
            ) : (
                <React.Fragment>
                    {DisplayEditingView ? (
                        <React.Fragment>
                            <div class="form-group">
                                <label for="inputAddress">Detail zprávy</label>
                                <textarea
                                    class="form-control"
                                    id="exampleFormControlTextarea1"
                                    rows="3"
                                    onChange={(e) => {
                                        MedicalLogDescriptionHandler(e);
                                    }}
                                    value={MedicalLogDetail.text}
                                ></textarea>
                            </div>
                            <div class="form-group">
                                <label for="exampleFormControlSelect1">
                                    Typ zákroku
                                </label>
                                <select
                                    class="form-control"
                                    id="exampleFormControlSelect1"
                                    onChange={(e) => {
                                        MedicalLogTypeHandler(e);
                                    }}
                                >
                                    {EnumMedicalLogs.map((item, index) => {
                                        return (
                                            <React.Fragment key={index}>
                                                <option
                                                    value={index}
                                                    selected={
                                                        index ===
                                                        MedicalLogDetail.type
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
                            <h4>zákrok provedl</h4>
                            <p>{MedicalLogDetail.user.text}</p>
                            <h4>pes</h4>
                            <p>{MedicalLogDetail.dog.text}</p>
                            <h4>typ zákroku</h4>
                            <p>{EnumMedicalLogs[MedicalLogDetail.type]}</p>
                            <h4>Detail zprávy</h4>
                            <p>{MedicalLogDetail.text}</p>
                        </React.Fragment>
                    )}
                </React.Fragment>
            )}
        </div>
    );
}
const EnumMedicalLogs = [
    "Očkování",
    "Operace",
    "Kastrace",
    "Kontrola",
    "Ošetření",
    "Ostatní",
];
const MedicalLogsDeleteModalText = {
    Title: "Opravdu chcete smazat vybrané záznamy?",
    Body: "Tato akce je nevratná!",
    SubmitBtnText: "Smazat vybrané záznamy",
};
