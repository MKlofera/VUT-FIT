/**
 * author: Marek Klofera
 * login: xklofe01
 *
 * @param {props.UrlToGetData} string url to get medical logs from server
 * @param {props.Title} string Title of the table
 */

import { element } from "prop-types";
import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import Loading from "./Loading";
import Error from "./Error";
import ModalComponent from "./ModalComponent";
import TableWithCheckboxesComponent from "./TableWithCheckboxesComponent";

import { MdAddCircle, MdOutlineDeleteForever } from "react-icons/md";

export default function VetRecordsComponent(props) {
    const [DeleteModalShow, setDeleteModalShow] = useState(false);
    const [selectedRecords, setSelectedRecords] = useState([]);
    const [IsLoading, setIsLoading] = useState(true);
    const [Error, setError] = useState(null);
    const [MedicalLogs, setMedicalLogs] = useState();
    let IdFromUrl = useParams().id;

    const getMedicalLogs = async () => {
        try {
            const response = await fetch(`${props.UrlToGetData}${IdFromUrl}`);
            if (!response.ok) {
            }
            let actualData = await response.json();
            setMedicalLogs(changeTypeToEnumValue(actualData));
            setError(null);
        } catch (err) {
            setError(err.message);
            setMedicalLogs(null);
        } finally {
            setIsLoading(false);
        }
    };

    const DeleteMedicalLogHandler = () => {
        setDeleteModalShow(false);
        let data = [];
        if (selectedRecords.length > 0) {
            selectedRecords.forEach((id, index) => {
                data[index] = {
                    id: id,
                };
            });
            DeleteMedicalLogs();
            SubmitData(data);
        }
    };

    const DeleteMedicalLogs = () => {
        let newMedicalLogs= [];
        setIsLoading(true);
        MedicalLogs.forEach((element) => {
            if (!selectedRecords.includes(element.id)) {
                newMedicalLogs.push(element);
            }
        });
        setMedicalLogs(newMedicalLogs);
    }

    const SubmitData = async (data) => {
        if (data !== null) {
            try {
                const response = await fetch("/API/medicalLog/deleteLogs", {
                    method: "DELETE",
                    body: JSON.stringify(data),
                })
            } catch (err) {
                console.log(err.message);
            }
        }
    };

    const GetSelectedRowsFromTableComponent = (selectedRows) => {
        setSelectedRecords(selectedRows);
    };

    useEffect(() => {
        setIsLoading(false);
    }, [selectedRecords, MedicalLogs]);
    useEffect(() => {
        getMedicalLogs();
    }, []);
    return (
        <section className="DogVetRecords">
            {DeleteModalShow ? (
                <ModalComponent
                    show={DeleteModalShow}
                    onHide={() => setDeleteModalShow(false)}
                    onSubmit={() => DeleteMedicalLogHandler()}
                    Title={MedicalLogsDeleteModalText.Title}
                    Body={MedicalLogsDeleteModalText.Body}
                    SubmitBtnText={MedicalLogsDeleteModalText.SubmitBtnText}
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
                            window.location = `/vet-zaznam/new/${IdFromUrl}`;
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
                        "Veterinář",
                        "Typ zákroku",
                        "Provedeno",
                        "Detail zprávy",
                    ]}
                    Body={MedicalLogs !== null ? MedicalLogs : null}
                    SendToParentSelectedRows={GetSelectedRowsFromTableComponent}
                />
            )}
        </section>
    );
}

const changeTypeToEnumValue = (json) => {
    const EnumMedicalLogs = [
        "Očkování",
        "Operace",
        "Kastrace",
        "Kontrola",
        "Ošetření",
        "Ostatní",
    ];
    json.forEach((element) => {
        element.type = EnumMedicalLogs[element.type];
    });
    return json;
};
const MedicalLogsDeleteModalText = {
    Title: "Opravdu chcete smazat vybrané záznamy?",
    Body: "Tato akce je nevratná!",
    SubmitBtnText: "Smazat vybrané záznamy",
};
