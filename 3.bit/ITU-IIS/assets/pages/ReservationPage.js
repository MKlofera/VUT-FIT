//xsuman02

import React, { useEffect, useState } from "react";
import TableWithCheckboxesComponent from "../Components/Common/TableWithCheckboxesComponent";
import ReservationFilterComponent from "../Components/Filters/BasicFilterComponent";
import Loading from "../Components/Common/Loading";
import ErrorComponent from "../Components/Common/Error";

export default function ReservationPage() {
    let TableHeader = ["Uživatel", "Pes", "Od", "Do", "Stav"];
    const urlToFetchData = [
        "/API/calendarEvent/openReservations",
        "/API/calendarEvent/closedReservations",
    ];
    const [SelectedReservationsIDs, setSelectedReservationsIDs] = useState([]);
    const [OpenReservationsView, setOpenReservationsView] = useState(true);
    const [Reservations, setReservations] = useState(null);
    const [ErrorMsg, setErrorMsg] = useState(null);
    const [IsLoading, setIsLoading] = useState(true);
    const [Filters_set, setFilters_set] = useState(false);
    const [Filters, SetFilters] = useState({
        FormInput: null,
        WhichReservations: null,
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
            WhichReservations: OpenReservationsView,
        });
    };

    const openReservationsViewToTrue = () => {
        setOpenReservationsView(true);
        SetFilters({
            FormInput: null,
            WhichReservations: true,
        });
    };
    const openReservationsViewToFalse = () => {
        setOpenReservationsView(false);
        SetFilters({
            FormInput: null,
            WhichReservations: false,
        });
    };

    const getReservations = async (url) => {
        setIsLoading(true);
        try {
            if (Filters_set) {
                let response = await fetch("/API/calendarEvent/filtered", {
                    method: "POST",
                    body: JSON.stringify(Filters),
                });
                if (!response.ok) {
                    throw new Error(
                        `This is an HTTP error: The status is ${response.status}`
                    );
                }
                let actualData = await response.json();
                setReservations(changeStatusToEnumValue(actualData));
                setErrorMsg(null);
            } else {
                let response = await fetch(urlToFetchData[url]);
                if (!response.ok) {
                    throw new Error(
                        `This is an HTTP error: The status is ${response.status}`
                    );
                }
                let actualData = await response.json();
                setReservations(changeStatusToEnumValue(actualData));
                setErrorMsg(null);
            }
        } catch (err) {
            setErrorMsg(err.message);
            setReservations(null);
        } finally {
            setIsLoading(false);
        }
    };
    const GetSelectedReservationsFromChildComponent = (
        SelectedReservations
    ) => {
        setSelectedReservationsIDs(SelectedReservations);
    };
    const submitChangedStatus = async (data) => {
        setIsLoading(true);
        try {
            const response = await fetch(`/API/calendarEvent/changeStatus`, {
                method: "POST",
                body: JSON.stringify(data),
            });
        } catch (err) {
            console.log(err.message);
        }
        if (OpenReservationsView) {
            getReservations(0);
        } else {
            getReservations(1);
        }
    };
    const ChangeStatusHandler = (newStatus) => {
        let data = [];
        if (SelectedReservationsIDs.length > 0) {
            SelectedReservationsIDs.forEach((id, index) => {
                data[index] = {
                    id: id,
                    status: newStatus,
                };
            });
            submitChangedStatus(data);
        }
    };
    const DeleteReservationsHandler = () => {
        let data = [];
        if (SelectedReservationsIDs.length > 0) {
            SelectedReservationsIDs.forEach((id, index) => {
                data[index] = {
                    id: id,
                };
            });
            SubmitDeleteSelectedReservations(data);
        }
    };
    const SubmitDeleteSelectedReservations = async (data) => {
        setIsLoading(true);
        try {
            const response = await fetch(
                `/API/calendarEvent/deleteReservations`,
                {
                    method: "DELETE",
                    body: JSON.stringify(data),
                }
            );
        } catch (err) {
            console.log(err.message);
        }
        if (OpenReservationsView) {
            getReservations(0);
        } else {
            getReservations(1);
        }
    };

    useEffect(() => {
        if (OpenReservationsView) {
            getReservations(0);
        } else {
            getReservations(1);
        }
    }, [OpenReservationsView, Filters]);
    return (
        <section className="container UserListPage">
            <div className="d-flex justify-content-between my-3">
                <div>
                    <button
                        className="btn btn-success m-2"
                        onClick={() => ChangeStatusHandler(1)}
                    >
                        Povolit
                    </button>
                    <button
                        className="btn btn-danger m-2"
                        onClick={() => ChangeStatusHandler(2)}
                    >
                        Zamítnout
                    </button>
                    <button
                        className="btn btn-primary m-2"
                        onClick={() => ChangeStatusHandler(3)}
                    >
                        Vrácen(a)
                    </button>
                    <button
                        className="btn btn-danger m-2"
                        onClick={() => DeleteReservationsHandler()}
                    >
                        Smazat
                    </button>
                </div>
            </div>
            <div className="d-flex justify-content-between">
                <h2
                    className={
                        OpenReservationsView ? "ActiveTitle" : "DeactiveTitle"
                    }
                    onClick={openReservationsViewToTrue}
                >
                    Nevyřízené rezervace
                </h2>
                <h2
                    className={
                        OpenReservationsView ? "DeactiveTitle" : "ActiveTitle"
                    }
                    onClick={openReservationsViewToFalse}
                >
                    Vyřízené rezervace
                </h2>
            </div>

            {IsLoading ? (
                <Loading />
            ) : ErrorMsg ? (
                <ErrorComponent Error={ErrorMsg} />
            ) : Reservations !== null ? (
                OpenReservationsView ? (
                    <TableWithCheckboxesComponent
                        Header={TableHeader}
                        Body={Reservations}
                        Links={[]}
                        SendToParentSelectedRows={
                            GetSelectedReservationsFromChildComponent
                        }
                    />
                ) : (
                    <TableWithCheckboxesComponent
                        Header={TableHeader}
                        Body={Reservations}
                        SendToParentSelectedRows={
                            GetSelectedReservationsFromChildComponent
                        }
                    />
                )
            ) : null}
        </section>
    );
}

const changeStatusToEnumValue = (json) => {
    const EnumReservationStatus = [
        "Nevyřízeno",
        "Schváleno",
        "Zamítnuto",
        "Vrácen(a)",
    ];
    json.forEach((element) => {
        if (typeof element.status === "number") {
            element.status = EnumReservationStatus[element.status];
        }
    });
    return json;
};
