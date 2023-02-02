/**
 * authors: Marek Klofera, Tomáš Szabó, Jan Šuman
 */
import React, { Component } from "react";
import ReactDOM from "react-dom/client";

import { BrowserRouter as Router, Routes, Route, Link } from "react-router-dom";

import "./styles/app.scss";
import "react-datetime/css/react-datetime.css";


//Components
import NavBar from "./Components/NavBar";

//pages
import MainPage from "./pages/index";
import DogDetailPage from "./pages/DogDetailPage";
import CalendarPage from "./pages/CalendarPage";
import AboutPage from "./pages/AboutPage";
import SupportPage from "./pages/SupportPage";
import NoPage from "./pages/NoPage";
import UserListPage from "./pages/UserListPage";
import WalkingHistoryPage from "./pages/WalkingHistoryPage";
import ReservationPage from "./pages/ReservationPage";
import VetRequestsPage from "./pages/VetRequestsPage";
import UserDetailPage from "./pages/UserDetailPage";
import LoginPage from "./pages/LoginPage";
import LogoutPage from "./pages/LogoutPage";
import RegistrationPage from "./pages/RegistrationPage";
import MedicalLogDetailPage from "./pages/MedicalLogDetailPage";
import MedicalLogCreationPage from "./pages/MedicalLogCreationPage";
import DogCreationPage from "./pages/DogCreationPage";
import MedicalRequestDetailPage from "./pages/MedicalRequestDetailPage";
import MedicalRequestCreationPage from "./pages/MedicalRequestCreationPage";


import { AutoLogout, isUserGranted, UserRoles } from "./utils/UserUtils";

class App extends Component {
    render() {
        return (
            <div>
{/* xszabo16 */}
                {isUserGranted(UserRoles.ROLE_USER) ? <AutoLogout /> : ""}
{/* xszabo16 */}
                <Router>
                    <NavBar />
                    <Routes>
                        {/* <Route element={<NavBar />}> */}
                        <Route path="/" element={<MainPage />} />
                        <Route path="/dog/:id" element={<DogDetailPage />} />
{/* xszabo16 */}
                        <Route path="/calendar">
                            <Route path=":id" element={<CalendarPage />} />
                            <Route path="" element={<CalendarPage />} />
                        </Route>
{/* xszabo16 */}
                        <Route path="/about" element={<AboutPage />} />
                        <Route path="/support" element={<SupportPage />} />
                        <Route path="/user-list" element={<UserListPage />} />
                        <Route path="/login" element={<LoginPage />} />
                        <Route path="/signup" element={<RegistrationPage />} />
                        <Route path="/historie-venceni" element={<WalkingHistoryPage />} />
                        <Route path="/veterinarni-pozadavky" element={<VetRequestsPage />} />
                        <Route path="/rezervace" element={<ReservationPage />} />
                        <Route path="/user/:id" element={<UserDetailPage />} />
                        <Route path="/vet-zaznam/:id" element={<MedicalLogDetailPage />} />
                        <Route path="/vet-zaznam/new/:id" element={<MedicalLogCreationPage />} />
                        <Route path="/vet-pozadavek/:id" element={<MedicalRequestDetailPage />} />
                        <Route path="/vet-pozadavek/new/:id" element={<MedicalRequestCreationPage />} />
                        <Route path="/novy-pes" element={<DogCreationPage />} />
                        <Route path="/logout" element={<LogoutPage />} />
                        <Route path="*" element={<NoPage />} />
                        <Route path="/" element={<MainPage />} />
                        {/* </Route> */}
                    </Routes>
                </Router>
            </div>
        );
    }
}

const root = ReactDOM.createRoot(document.getElementById("app"));
root.render(
    <React.StrictMode>
        <App />
    </React.StrictMode>
);
