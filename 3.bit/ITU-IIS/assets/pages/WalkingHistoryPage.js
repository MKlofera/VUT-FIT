//xsuman02

import React, { useEffect, useState } from "react";
import TableWithCheckboxesComponent from "../Components/Common/TableWithCheckboxesComponent";
import Loading from "../Components/Common/Loading";
import ErrorComponent from "../Components/Common/Error";

export default function ReservationPage() {
    let TableHeader = ["Jméno", "Plemeno", "Pohlaví", "Datum", "Od", "Do", "Stav"];
    const urlToFetchData = "/API/calendarEvent/walkingHistory";
    const [SelectedWalkingsIDs, setSelectedWalkingsIDs] = useState([]);
    const [Walkings, setWalkings] = useState(null);
    const [ErrorMsg, setErrorMsg] = useState(null);
    const [IsLoading, setIsLoading] = useState(true);

    
    const getWalkings = async () => {
        setIsLoading(true);
        try {
            let response = await fetch(urlToFetchData, {
                method: "GET",
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + sessionStorage.getItem("token"),
                }
            });
            if (!response.ok) {
                throw new Error(
                    `This is an HTTP error: The status is ${response.status}`
                );
            }
            let actualData = await response.json();
            console.log(actualData);
            setWalkings(changeStatusToEnumValue(actualData));
            setErrorMsg(null);
        } catch (err) {
            setErrorMsg(err.message);
            setWalkings(null);
        } finally {
            setIsLoading(false);
        }
    };
    const GetSelectedWalkingsFromChildComponent = (SelectedWalkingsIDs) => {
        setSelectedWalkingsIDs(SelectedWalkingsIDs);
    };

    useEffect(() => {
        getWalkings();
    }, []);
    return (
        <section className="container UserListPage">
            <div className="d-flex justify-content-between mt-3">
                <h2>Historie venčení</h2>
            </div>

            {IsLoading ? (
                <Loading />
            ) : ErrorMsg ? (
                <ErrorComponent Error={ErrorMsg} />
            ) : Walkings !== null ? (
                <TableWithCheckboxesComponent
                    Header={TableHeader}
                    Body={Walkings}
                    Links={[]}
                    ShowCheckBoxes={false}
                    SendToParentSelectedRows={GetSelectedWalkingsFromChildComponent}
                />
            ) : null}
        </section>
    );
}

const changeStatusToEnumValue = (json) => {
    const EnumReservationStatus = [
        "Zažádáno",
        "Schváleno",
        "Zamítnuto",
        "Dokončeno"
    ];
    const EnumSex = ["Pes", "Fena"];
    const EnumBreed = [
        "Afgánský chrt",
        "Anglický buldok",
        "Argentinská doga",
        "Belgický ovčák",
        "Bígl",
        "Border kolie",
        "Bulteriér",
        "Československý vlčák",
        "Dalmatin",
        "Dobrman",
        "Holandský ovčák",
        "Jezevčík",
        "Německý ovčák",
        "Shiba-Inu",
        "Zlatý retriever",
    ];


    json.forEach((element) => {
        if (typeof element.status === "number") {
            element.status = EnumReservationStatus[element.status];
        }
        if (typeof element.breed === "number") {
            element.breed = EnumBreed[element.breed];
        }
        if (typeof element.sex === "number") {
            element.sex = EnumSex[element.sex];
        }
    });
    console.log(json);
    return json;
};

