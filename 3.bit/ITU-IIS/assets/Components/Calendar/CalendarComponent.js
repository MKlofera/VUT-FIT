/**
 * author: Tomáš Szabó 
 * login: xszabo16
 */
import React, {useEffect, useRef, useState} from "react";
import FullCalendar, { formatDate } from "@fullcalendar/react";
import timeGridPlugin from "@fullcalendar/timegrid";
import interactionPlugin from '@fullcalendar/interaction';
import momentPlugin from "@fullcalendar/moment";
import bootstrap5Plugin from "@fullcalendar/bootstrap5";
import { useParams } from "react-router-dom";
import ModalWindow from "../Common/GenericModalComponent";
import ModalCalendarAddComponent from "./ModalCalendarAddComponent";
import ModalCalendarInfo from "./ModalCalenderInfoComponent";
import {utc} from "moment";
import {tz} from "moment-timezone";

// styles
import "../../styles/app.scss";
import "react-datetime/css/react-datetime.css";
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-icons/font/bootstrap-icons.css'; // needs additional webpack config!

// utils
import { Col, Container, Row } from "react-bootstrap";
import { getTypeById, getColorByTypeId } from "../../utils/CalendarUtils";
import { isUserGranted, isUserRole, UserRoles } from "../../utils/UserUtils";
import GenericMessageComponent from "../Common/GenericMessageComponent";


const CalendarWindow = (props) => {
    const [events, setEvents] = useState([]);
    const [message, setMessage] = useState("");
    const [error, setError] = useState("");
    
    const [types, setTypes] = useState([]);
    const [dogs, setDogs] = useState([]);
    const [isEdit, setIsEdit] = useState(false);
    const [isAdd, setIsAdd] = useState(false);
    const [newInformation, setNewInformation] = useState(false);
    const dogSelected = useParams().id;
    const [loading, setLoading] = useState(false);
    const [eventInformation, setEventInformation] = useState({
        startStr: "",
        endStr: "",
        status: 0,
        type: 0,
        dog: 0,
    });
    const [modalShow, setModalShow] = useState(false);
    const refCalendar = useRef(null);

    // Gets Events from calendar and puts them in calendar
    const getEvents = async () => {
        try {
            setLoading(true);
            let response = await fetch("/API/calendarEvent/", {
                method: "GET",
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                },
            });
            const data = await response.json();
            setLoading(false);
            if (response.status === 200) {
                setMessage("Events loaded!");
                const parsedEvents = data.map((event) => {
                    return {
                        id: event.id,
                        title: getTypeById(event.Type) + " - " + event.OwnerName,
                        start: event.TimestampFrom,
                        end: event.TimestampTo,
                        allDay: false,
                        backgroundColor: getColorByTypeId(event.Type),
                        extendedProps: {
                            type: event.Type,
                            dog: event.DogId,
                            dogName: event.DogName,
                            owner: event.UserId,
                            ownerName: event.OwnerName,
                            status: event.Status,
                        },
                    }});
                setEvents(parsedEvents);
            
            } else {
                setError("Something went wrong, try it later.");
            }
        } catch (error) {
            console.log(error);
        }
    };

    // Adding event handler
    const handleDateSelect = (selectInfo) => {
        setIsAdd(true);
        setEventInformation({
            startStr: selectInfo.startStr,
            endStr: selectInfo.endStr,
            status: 0,
            type: null,
            dog: dogSelected ?? null,
        });
        setIsEdit(false);
        setModalShow(true);
        let calendarApi = selectInfo.view.calendar;
        calendarApi.unselect();
    }

    // Change of event information from modal
    const handleChangeModal = (event) => {
        setEventInformation(event);
    }
    
    // Close of modal 
    const handleModalClose = async (returnedEvent) => {
        // on close
        if (returnedEvent === undefined || returnedEvent.target.id === "modal-close") {
            setModalShow(false);
            return;
        }
        // on delete
        if (returnedEvent.target.id === "modal-delete") {
            deleteCalendarEvent();
        }
        else {
            // on submit
            setNewInformation(JSON.stringify({
                type: eventInformation.type,
                timestampFrom: eventInformation.startStr,
                timestampTo: eventInformation.endStr,
                dog: eventInformation.dog,
                status: eventInformation.status,
            }));
        }
    }

    /// USE EFFECTS - START ///
    // Updating values on frontend

    // GETS TYPES
    useEffect(() => {
        const getTypes = async () => {
            setTypes([
                { id: 1, name: "Walk" },
                { id: 2, name: "Vet" },
                { id: 3, name: "Grooming" },
                { id: 4, name: "Other" },
            ]);
        };
        getTypes().then(r => console.log(r));
    }, []);

    // GETS DOGS
    useEffect(() => {
        const getDogs = async () => {
            try {
                let response = await fetch("/API/dog/getDogList",{
                    method: "GET",
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    }
                });
                let data = await response.json();

                if (response.status === 200) {
                    setDogs(data);
                } else {
                    console.log(data);
                }
            } catch (err) {
                console.log(err);
            }
        }
        getDogs().then(r => console.log(r));
    }, []);

    // ADDS OR EDITS EVENT
    useEffect(() => {
        const addEventRequest = async () => {
            if (parseInt(eventInformation.id) != 0) {
                editCalendarEvent();
            } else {
                addCalendarEvent();
            } 
        }
        if (newInformation !== false) {
            addEventRequest();
            setNewInformation(false);
        }
    }, [newInformation]);


    useEffect(() => {
        getEvents().then(r => console.log(r));
    }, []);

    // Destroy message and error 
    useEffect(() => {
        setTimeout(()=>{
            setMessage("");
        }, 10000)
    }, [message]);

    useEffect(() => {
        setTimeout(()=>{
            setError("");
        }, 10000)
    }, [error]);
    

    useEffect(() => {console.log(events)}, [events]);
    useEffect(() => {console.log(isEdit)}, [isEdit]);
    /// USE EFFECTS - END ///

    // Edits calendar event asynchronously
    const editCalendarEvent = async () => {
        try {
            setLoading(true);

            let response = await fetch("/API/calendarEvent/edit/" + eventInformation.id , {
                method: "POST",
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + sessionStorage.getItem("token"),
                },
                body: JSON.stringify({
                    id: parseInt(eventInformation.id),
                    type: eventInformation.type,
                    timestampFrom: utc(eventInformation.startStr).tz("Europe/Prague").format(),
                    timestampTo: utc(eventInformation.endStr).tz("Europe/Prague").format(),
                    dog: eventInformation.dog,
                    status: eventInformation.status,
                }),
            });
            const data = await response.json();
            setLoading(false);
            window.scrollTo(0, 0);
            if (response.status === 200) {
                setMessage("Event " + eventInformation.id + "updated!");
                await getEvents();
                setModalShow(false);
            } else if (response.status === 401) {
                setError("You are not authorized to add events");
                sessionStorage.removeItem("token");
                window.location = "/login";
            } else {
                setError(data["error"]);
            }
        } catch (e) {
            setError(e.message);
        }
    }
    // Adds calendar event asynchronously
    const addCalendarEvent = async () => {
        try {
            setLoading(true);
            let response = await fetch("/API/calendarEvent/add", {
                method: "POST",
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + sessionStorage.getItem("token"),
                },
                body: JSON.stringify({
                    type: eventInformation.type,
                    timestampFrom: utc(eventInformation.startStr).tz("Europe/Prague").format(),
                    timestampTo: utc(eventInformation.endStr).tz("Europe/Prague").format(),
                    dog: eventInformation.dog,
                    status: eventInformation.status,
                }),
            });
            const data = await response.json();
            setLoading(false);
            window.scrollTo(0, 0);
            if (response.status === 200) {
                await getEvents();
                setMessage("Added event was successful!");
            } else if (response.status === 401) {
                setError("You are not authorized to add events");
                sessionStorage.removeItem("token");
                window.location = "/login";
            } else {
                setError(data["error"]);
            }
            setModalShow(false);
        } catch (e) {
            setError(e.message);
            setLoading(false);

        }
    }
    // Deletes calendar event asynchronously
    const deleteCalendarEvent = async () => {
        try {
            setLoading(true);

            let response = await fetch("/API/calendarEvent/delete/"+eventInformation.id, {
                method: "DELETE",
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + sessionStorage.getItem("token"),
                },
                body: JSON.stringify({
                    id: eventInformation.id
                }),
            });
            const data = await response.json();
            setLoading(false);

            window.scrollTo(0, 0);
            if (response.status === 200) {
                await getEvents();
                setMessage("Event " + eventInformation.id + " deleted!");
                setModalShow(false);
            } else if (response.status === 401) {
                setError("You are not authorized to remove events");
                sessionStorage.removeItem("token");
                window.location = "/login";
            } else {
                setError(data["error"]);
            }
        } catch (e) {
            setError(e.message);
            setLoading(false);

        }
    }

    // After click on an event will
    const handleEventClick = (clickInfo) => {
        setIsAdd(false);
        // Only Social worker, Admin and Vet can edit event
        setIsEdit(isUserGranted(UserRoles.ROLE_SOCIAL_WORKER));
        setEventInformation({
            id: clickInfo.event.id,
            startStr: clickInfo.event.startStr,
            endStr: clickInfo.event.endStr,
            status: clickInfo.event.title,
            type: clickInfo.event.extendedProps.type,
            dogName: clickInfo.event.extendedProps.dogName,
            dog: clickInfo.event.extendedProps.dog,
            ownerName: clickInfo.event.extendedProps.ownerName,
            ownerId: clickInfo.event.extendedProps.owner,
        });
        setModalShow(true);
    }

    const handleLoading = (e) => {
        setLoading(e);
    }


    // Event content
    const renderEventContent = (eventInfo) => {
        return (
            <>
                <div style={{cursor: 'pointer'}}>
                <i>{eventInfo.event.id}: {eventInfo.event.title}</i><br />
                <b>{eventInfo.timeText}</b>, <b>Dog Name: {eventInfo.event.extendedProps.dogName}</b>
                </div>
            </>
        );
    }

    // Front-End
    return (
        <Container>
            {/* messages */}
            {message !== "" ? <GenericMessageComponent variant="success" message={message} /> : ""}
            {error !== "" ? <GenericMessageComponent variant="danger" message={error} /> : ""}
            {loading ? <GenericMessageComponent variant="primary" message={"Loading..."} /> : ""}
            {isUserRole(UserRoles.NOT_AUTHENTICATED) ? <GenericMessageComponent variant="warning" message={"To add or edit calendar event you have to sign in first as eligible role."} /> : ""}
            {/* messages */}

            <Row>
                <Col md={8} sm={12}>
                <FullCalendar
                ref={refCalendar}
                plugins={[timeGridPlugin, interactionPlugin, momentPlugin, bootstrap5Plugin ]}
                themeSystem='bootstrap5'
                headerToolbar={{
                    left: 'prev,next today',
                    center: 'title',
                    right: 'timeGridWeek,timeGridDay'
                }}
                validRange={{
                    start: new Date().toISOString().slice(0, 10)
                }}
                initialView="timeGridDay"
                height="auto"
                handleWindowResize={true}
                selectable={sessionStorage.getItem("token") !== null}
                editable={sessionStorage.getItem("token") !== null}
                clickable={true}
                selectHelper={sessionStorage.getItem("token") !== null}
                select={handleDateSelect}
                eventContent={renderEventContent}
                eventClick={handleEventClick}
                events={events}
                timeZone='CET'
                slotLabelFormat={[
                    {
                        hour: '2-digit',
                        minute: '2-digit',
                        hour12: false
                    }
                ]}
            />
            </Col>
                <Col className="Calendar calendar" md={4} sm={12}>
                    <h1>How to use calendar</h1>
                    <section>
                        <h2>Unauthenticated User</h2>
                        <ul>
                            <li>Can show information about upcomming event.</li>
                            <li>To add events, you have to be logged in and approved!</li>
                        </ul>
                    </section>
                    <section>
                        <h2>Logged in and approved user</h2>
                        <ul>
                            <li>Can show information about upcomming event.</li>
                            <li>Can add new events</li>
                            <li>Can edit or delete existing events</li>
                        </ul>
                    </section>
                    <section>
                        <h2>Types of events</h2>
                        <ul>
                            <li>Walk is <span className="green">green</span></li>
                            <li>Vet is <span className="red">red</span></li>
                            <li>Grooming is <span className="brown">brown</span></li>
                            <li>Other is <span className="blue">blue</span></li>
                        </ul>
                    </section>
                </Col>
            </Row>

            <ModalWindow show={modalShow}
                        onHide={handleModalClose}
                        >
                {isEdit || isAdd
                    ? <ModalCalendarAddComponent
                        
                        onChange={handleChangeModal}
                        submitEvent={isAdd ? addCalendarEvent : editCalendarEvent}
                        deleteEvent={deleteCalendarEvent}
                        eventInformation={eventInformation}
                        DogId={dogSelected}
                        types={types}
                        dogs={dogs}
                        handleLoading={handleLoading}
                        loadingValue={loading}
                        isEdit={isEdit}
                        isAdd={isAdd}
                        onSubmit={handleModalClose}
                        title={isAdd ? "Add event" : "Edit event"}
                        submit={isAdd ? "Create Event" : "Save Changes"}
                        />
                    : <ModalCalendarInfo 
                        eventInformation={eventInformation} onHide={handleModalClose}/>
                }
            </ModalWindow>
        </Container>
    );
};


export default CalendarWindow;