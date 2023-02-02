/**
 * author: Marek Klofera
 * login: xklofe01
 */

import React from "react";
import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";

import { isUserGranted, isUserRole, UserRoles } from "../utils/UserUtils";

//components
import DogDetailInfoComponent from "../Components/Dog/DogDetailInfoComponent";
import DogDetailEditInfoComponent from "../Components/Dog/DogDetailEditInfoComponent";
import VetRecordsComponent from "../Components/Common/VetRecordsComponent";
import MedicalRequestsComponent from "../Components/Common/MedicalRequestsComponent";
import Loading from "../Components/Common/Loading";
import ModalComponent from "../Components/Common/ModalComponent";

const DogDetailPage = () => {
    const [DogDetail, SetDogDetail] = useState(null);
    const [DogDetailEdited, SetDogDetailEdited] = useState(null);
    const [DogObjectToServer, setDogObjectToServer] = useState(null);
    const [IsLoading, setIsLoading] = useState(true);
    const [Error, setError] = useState(null);
    const [DisplayEditingView, setDisplayEditingView] = useState(false);
    
    //Modals
    const [DeleteModalShow, setDeleteModalShow] = useState(false);

    let DogIdFromUrl = useParams().id;
    const getDetailDogFromServer = async () => {
        try {
            const response = await fetch(
                `/API/dog/detail/${DogIdFromUrl}`
            );
            if (!response.ok) {
                window.location = "/404";
                return;
            }
            let actualData = await response.json();

            SetDogDetail(actualData);
            setError(null);
        } catch (err) {
            setError(err.message);
            SetDogDetail(null);
        } finally {
            setIsLoading(false);
        }
    };

    const EditDogHandler = () => {
        setDisplayEditingView(!DisplayEditingView);
    };

    const DogDescriptionHandler = (e) => {
        SetDogDetail({ ...DogDetail, Description: e.target.value });
    };

    const GetEditedDogDetailFromChildComponent = (editedDogDetail) => {
        SetDogDetailEdited(editedDogDetail);
    };

    const SaveEditedDogHandler = () => {
        setDisplayEditingView(!DisplayEditingView);
        DogDetailEdited.Description = DogDetail.Description;
        setDogObjectToServer(DogDetailEdited);
        SetDogDetail(DogDetailEdited);
    };

    const DeleteDogHandler = async () => {
        setDeleteModalShow(false);

        const responce = await fetch(`/API/dog/${DogDetail.id}`, {
            method: "DELETE",
        })
            .then((res) => {
                res.json();
            })
            .catch((err) => {
                console.log(err.message);
            });
        window.location = "/";
    };

    const SendEditedDogToServer = async (data) => {
        setIsLoading(true);
        try {
            const response = await fetch(`/API/dog/edit`, {
                method: "POST",
                body: JSON.stringify(data),
            });
        } catch (err) {
            console.log(err.message);
        }
        getDetailDogFromServer();
    };
    useEffect(() => {
        getDetailDogFromServer();
    }, []);
    useEffect(() => {}, [DogDetail, DisplayEditingView, DogDetailEdited]);
    useEffect(() => {
        if (DogObjectToServer !== null)
            SendEditedDogToServer(DogObjectToServer);
        getDetailDogFromServer();
    }, [DogObjectToServer]);

    return (
        <React.Fragment>
            <div className="container">
                {isUserRole(UserRoles.ROLE_SOCIAL_WORKER) ||
                isUserRole(UserRoles.ROLE_ADMIN) ? (
                    <div className="row mt-3">
                        {DeleteModalShow ? (
                            <ModalComponent
                                show={DeleteModalShow}
                                onHide={() => setDeleteModalShow(false)}
                                onSubmit={() => DeleteDogHandler()}
                                Title={DogDeleteModalText.Title}
                                Body={DogDeleteModalText.Body}
                                SubmitBtnText={DogDeleteModalText.SubmitBtnText}
                            />
                        ) : null}
                        <button
                            className="btn btn-danger col-6"
                            onClick={() => setDeleteModalShow(true)}
                        >
                            Smazat psa
                        </button>
                        <button
                            className={
                                DisplayEditingView
                                    ? "btn btn-outline-warning col-6"
                                    : "btn btn-warning col-6"
                            }
                            onClick={EditDogHandler}
                        >
                            Upravit psa
                        </button>
                        {DisplayEditingView ? (
                            <button
                                className="btn btn-success my-2"
                                onClick={SaveEditedDogHandler}
                            >
                                Uložit
                            </button>
                        ) : null}
                    </div>
                ) : (
                    ""
                )}
                {IsLoading ? (
                    <Loading />
                ) : Error ? (
                    <Error />
                ) : (
                    <React.Fragment>
                        {DisplayEditingView ? (
                            <DogDetailEditInfoComponent
                                DogDetail={DogDetail}
                                SendToParentEditedDog={
                                    GetEditedDogDetailFromChildComponent
                                }
                                DisplayEditingView={DisplayEditingView}
                            />
                        ) : (
                            <DogDetailInfoComponent DogDetail={DogDetail} />
                        )}
                    </React.Fragment>
                )}

                {isUserRole(UserRoles.ROLE_VET) ||
                isUserRole(UserRoles.ROLE_SOCIAL_WORKER) ||
                isUserRole(UserRoles.ROLE_ADMIN) ? (
                    <div>
                        <VetRecordsComponent
                            Title={"Veterinární záznamy"}
                            UrlToGetData={"/API/dog/medicalLogs/"}
                        />
                        {/*Implementoval Jan šuman */}
                        <MedicalRequestsComponent
                            Title={"Veterinární požadavky"}
                            UrlToGetData={"/API/dog/medicalRequests/"}
                        />
                        {/*---------------------------------------------*/}
                    </div>
                ) : null}

                <h1>O mně</h1>
                {IsLoading ? (
                    <Loading />
                ) : Error ? (
                    <Error />
                ) : (
                    <React.Fragment>
                        {DisplayEditingView ? (
                            <div className="form-group">
                                <textarea
                                    className="form-control"
                                    id="exampleFormControlTextarea1"
                                    rows="10"
                                    onChange={(e) => {
                                        DogDescriptionHandler(e);
                                    }}
                                    value={DogDetail.Description}
                                ></textarea>
                            </div>
                        ) : (
                            <p>{DogDetail.Description}</p>
                        )}
                    </React.Fragment>
                )}
            </div>
        </React.Fragment>
    );
};

export default DogDetailPage;

const DogDeleteModalText = {
    Title: "Opravdu chcete smazat tohoto psa?",
    Body: "Tato akce je nevratná!",
    SubmitBtnText: "Smazat psa",
};
