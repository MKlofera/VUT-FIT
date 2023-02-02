//xklofe01, xsuman02

import { element } from "prop-types";
import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import Loading from "./Loading";
import Error from "./Error";
import ModalComponent from "./ModalComponent";
import TableWithCheckboxesComponent from "./TableWithCheckboxesComponent";

import { MdAddCircle, MdOutlineDeleteForever } from "react-icons/md";

export default function MedicalRequestsComponent(props) {
    const [DeleteModalShow, setDeleteModalShow] = useState(false);
    const [selectedRequests, setselectedRequests] = useState([]);
    const [IsLoading, setIsLoading] = useState(true);
    const [Error, setError] = useState(null);
    const [MedicalRequests, setMedicalRequests] = useState();
    let IdFromUrl = useParams().id;

    const getMedicalRequests = async () => {
        try {
            const response = await fetch(
                `${props.UrlToGetData}${IdFromUrl}`
            );
            if (!response.ok) {
            }
            let actualData = await response.json();
            setMedicalRequests(changeTypeToEnumValue(actualData));
            setError(null);
        } catch (err) {
            setError(err.message);
            setMedicalRequests(null);
        } finally {
            setIsLoading(false);
        }
    };


    const DeletemedicalRequestsHandler = () => {
        setDeleteModalShow(false);
        let data = [];
        if (selectedRequests.length > 0) {
            selectedRequests.forEach((id, index) => {
                data[index] = {
                    id: id,
                };
            });
            DeleteMedicalLogs();
            SubmitData(data);
        }
    };

    const DeleteMedicalLogs = () => {
        let newMedicalRequests= [];
        setIsLoading(true);
        MedicalRequests.forEach((element) => {
            if (!selectedRequests.includes(element.id)) {
                newMedicalRequests.push(element);
            }
        });
        setMedicalRequests(newMedicalRequests);
    }

    const SubmitData = async (data) => {
        if (data !== null) {
            try {
                const response = await fetch("/API/medicalRequests/deleteRequests", {
                    method: "DELETE",
                    body: JSON.stringify(data),
                });
            } catch (err) {
                console.log(err.message);
            }
            getMedicalRequests();
        }

    };

    const GetSelectedRowsFromTableComponent = (selectedRows) => {
        setselectedRequests(selectedRows);
    };

    useEffect(() => { }, [selectedRequests, MedicalRequests]);
    useEffect(() => {
        getMedicalRequests();
    }, []);
    return (
        <section className="DogVetRequests">
            {DeleteModalShow ? (
                <ModalComponent
                    show={DeleteModalShow}
                    onHide={() => setDeleteModalShow(false)}
                    onSubmit={() => DeletemedicalRequestsHandler()}
                    Title={MedicalRequestsDeleteModalText.Title}
                    Body={MedicalRequestsDeleteModalText.Body}
                    SubmitBtnText={MedicalRequestsDeleteModalText.SubmitBtnText}
                />
            ) : null}
            <div className="d-flex justify-content-between">
                <h1 className="mt-3">{props.Title}</h1>
                <div className="d-flex align-items-center">
                    <MdAddCircle
                        className="icon"
                        color="green"
                        fontSize="2.5em"
                        onClick={() => {
                            window.location = `/vet-pozadavek/new/${IdFromUrl}`;
                        }}
                    />
                    <MdOutlineDeleteForever
                        onClick={() => setDeleteModalShow(true)}
                        className="icon"
                        color="red"
                        fontSize="2.5em"
                    />
                </div>
            </div>
            {IsLoading ? (
                <Loading />
            ) : (
                <TableWithCheckboxesComponent
                    Header={[
                        "Pečovatel",
                        "Typ zákroku",
                        "Priorita",
                        "Vytvořeno",
                        "Detail požadavku",
                    ]}
                    Body={MedicalRequests !== null ? MedicalRequests : null}
                    SendToParentSelectedRows={GetSelectedRowsFromTableComponent}
                />
            )}
        </section>
    );
}

const changeTypeToEnumValue = (json) => {
    const EnumMedicalRequests = [
        "Očkování",
        "Operace",
        "Kastrace",
        "Kontrola",
        "Ošetření",
        "Ostatní",
    ];
    json.forEach((element) => {
        element.type = EnumMedicalRequests[element.type];
    });
    return json;
};
const MedicalRequestsDeleteModalText = {
    Title: "Opravdu chcete smazat vybrané požadavky?",
    Body: "Tato akce je nevratná!",
    SubmitBtnText: "Smazat vybrané požadavky",
};
