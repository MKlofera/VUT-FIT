//xsuman02

import React, { useEffect, useState } from "react";
import TableWithCheckboxesComponent from "../Components/Common/TableWithCheckboxesComponent";
import ReservationFilterComponent from "../Components/Filters/BasicFilterComponent";
import Loading from "../Components/Common/Loading";
import ErrorComponent from "../Components/Common/Error";

export default function VetRequestsPage() {
    let TableHeader = ["Veterinář", "Pes", "Typ zákroku", "Priorita", "Vytvořeno", "Detail požadavku"];
    const urlToFetchData = [
        "/API/medicalRequests/openRequests",
        "/API/medicalRequests/closedRequests",
    ];
    const [SelectedRequestsIDs, setSelectedRequestsIDs] = useState([]);
    const [OpenRequestsView, setOpenRequestsView] = useState(true);
    const [Requests, setRequests] = useState(null);
    const [ErrorMsg, setErrorMsg] = useState(null);
    const [IsLoading, setIsLoading] = useState(true);
    const [Filters_set, setFilters_set] = useState(false);
    const [Filters, SetFilters] = useState({
        FormInput: null,
        WhichRequests: null,
    });

    const getFilters = (formInput) => {
        if (formInput == "") {
            setFilters_set(false);
        }
        else {
            setFilters_set(true);
        }
        SetFilters({
            FormInput: formInput,
            WhichRequests: OpenRequestsView,
        });
    };

    const OpenRequestsViewToTrue = () => {
        setOpenRequestsView(true);
        SetFilters({
            FormInput: null,
            WhichRequests: true,
        });
    };
    const OpenRequestsViewToFalse = () => {
        setOpenRequestsView(false);
        SetFilters({
            FormInput: null,
            WhichRequests: false,
        });
    };

    const getRequests = async (url) => {
        setIsLoading(true);
        try {
            if (Filters_set) {
                let response = await fetch("/API/medicalRequests/filtered", {
                    method: "POST",
                    body: JSON.stringify(Filters),
                });
                if (!response.ok) {
                    throw new Error(
                        `This is an HTTP error: The status is ${response.status}`
                    );
                }
                let actualData = await response.json();
                setRequests(changeTypeToEnumValue(actualData));
                setErrorMsg(null);
            } else {
                let response = await fetch(urlToFetchData[url]);
                if (!response.ok) {
                    throw new Error(
                        `This is an HTTP error: The status is ${response.status}`
                    );
                }
                let actualData = await response.json();
                setRequests(changeTypeToEnumValue(actualData));
                setErrorMsg(null);
            }
        } catch (err) {
            setErrorMsg(err.message);
            setRequests(null);
        } finally {
            setIsLoading(false);
        }
    };
    const GetSelectedRequestsFromChildComponent = (
        SelectedRequests
    ) => {
        setSelectedRequestsIDs(SelectedRequests);
    };
    const DeleteRequestsHandler = () => {
        let data = [];
        if (SelectedRequestsIDs.length > 0) {
            SelectedRequestsIDs.forEach((id, index) => {
                data[index] = {
                    id: id,
                };
            });
            SubmitDeleteSelectedRequests(data);
        }
    };
    const SubmitDeleteSelectedRequests = async (data) => {
        setIsLoading(true);
        try {
            const response = await fetch(
                `/API/medicalRequests/deleteRequests`,
                {
                    method: "DELETE",
                    body: JSON.stringify(data),
                }
            );
        } catch (err) {
            console.log(err.message);
        }
        if (OpenRequestsView) {
            getRequests(0);
        } else {
            getRequests(1);
        }
    };

    useEffect(() => {
        if (OpenRequestsView) {
            getRequests(0);
        } else {
            getRequests(1);
        }
    }, [OpenRequestsView, Filters]);
    return (
        <section className="container UserListPage">
            <div className="d-flex justify-content-between my-3">
                <div>
                    <button
                        className="btn btn-danger m-2"
                        onClick={() => DeleteRequestsHandler()}
                    >
                        Smazat
                    </button>
                </div>

            </div>
            <div className="d-flex justify-content-between">
                <h2
                    className={
                        OpenRequestsView ? "ActiveTitle" : "DeactiveTitle"
                    }
                    onClick={OpenRequestsViewToTrue}
                >
                    Nevyřízené požadavky
                </h2>
                <h2
                    className={
                        OpenRequestsView ? "DeactiveTitle" : "ActiveTitle"
                    }
                    onClick={OpenRequestsViewToFalse}
                >
                    Vyřízené požadavky
                </h2>
            </div>

            {IsLoading ? (
                <Loading />
            ) : ErrorMsg ? (
                <ErrorComponent Error={ErrorMsg} />
            ) : Requests !== null ? (
                OpenRequestsView ? (
                    <TableWithCheckboxesComponent
                        Header={TableHeader}
                        Body={Requests}
                        Links={[]}
                        SendToParentSelectedRows={
                            GetSelectedRequestsFromChildComponent
                        }
                    />
                ) : (
                    <TableWithCheckboxesComponent
                        Header={TableHeader}
                        Body={Requests}
                        SendToParentSelectedRows={
                            GetSelectedRequestsFromChildComponent
                        }
                    />
                )
            ) : null}
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

