import React from "react";
import { Link } from "react-router-dom";
import Container from "react-bootstrap/Container";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";
import NavDropdown from "react-bootstrap/NavDropdown";

// Utils
import {
    isUserRole,
    UserRoles,
    getUserEmail,
    isUserGranted,
} from "../utils/UserUtils";

const NavBar = () => {
    return (
        <Navbar className="NavBarComponent" bg="light" expand="lg" sticky="top">
            <Container>
                <Navbar.Brand>
                    <Link to="/">IIS-ITUtulek</Link>
                </Navbar.Brand>
                <Navbar.Toggle aria-controls="basic-navbar-nav" />
                <Navbar.Collapse id="basic-navbar-nav">
                    <Nav className="mx-auto">
                        <Nav.Link>
                            <Link className="navBarLink" to="/calendar">
                                Kalendář
                            </Link>
                        </Nav.Link>
                        <Nav.Link>
                            <Link className="navBarLink" to="/support">
                                Podpořte Nás
                            </Link>
                        </Nav.Link>
                        <Nav.Link>
                            <Link className="navBarLink" to="/about">
                                O Nás
                            </Link>
                        </Nav.Link>
                        {/* xszabo16 */}
                        {isUserRole(UserRoles.NOT_AUTHENTICATED) ? (
                            <Nav.Link>
                                <Link className="navBarLink" to="/login">
                                    Login
                                </Link>
                            </Nav.Link>
                        ) : (
                            ""
                        )}
                        {isUserRole(UserRoles.NOT_AUTHENTICATED) ? (
                            <Nav.Link>
                                <Link className="navBarLink" to="/signup">
                                    SignUp
                                </Link>
                            </Nav.Link>
                        ) : (
                            ""
                        )}
                        {/* xszabo16 */}

                    </Nav>
                    {isUserRole(UserRoles.NOT_AUTHENTICATED) ? null : (
                        <Nav className="mr-auto">
                            <NavDropdown
                                title="Login funkce"
                                id="basic-nav-dropdown"
                            >
                                <NavDropdown.Item>
                                    <Link to="/Historie-venceni">
                                        Historie venčení
                                    </Link>
                                </NavDropdown.Item>
                                {isUserRole(UserRoles.ROLE_ADMIN) ||
                                    isUserRole(UserRoles.ROLE_SOCIAL_WORKER) ||
                                    isUserRole(UserRoles.ROLE_VET) ? (
                                    <NavDropdown.Item>
                                        <Link to="/Veterinarni-pozadavky">
                                            Veterinární požadavky
                                        </Link>
                                    </NavDropdown.Item>
                                ) : null}
                                {isUserRole(UserRoles.ROLE_ADMIN) ||
                                    isUserRole(UserRoles.ROLE_SOCIAL_WORKER) ? (
                                    <React.Fragment>
                                        <NavDropdown.Item>
                                            <Link to="/rezervace">
                                                Rezervace
                                            </Link>
                                        </NavDropdown.Item>

                                        <NavDropdown.Item>
                                            <Link to="/novy-pes">
                                                Přidat psa
                                            </Link>
                                        </NavDropdown.Item>
                                    </React.Fragment>
                                ) : null}
                                <NavDropdown.Divider />
                                <NavDropdown.Item>
                                    <Link to="/user-list">Uživatelé</Link>
                                </NavDropdown.Item>
                            </NavDropdown>
                            {/* xszabo16 */}
                            <Nav.Link>
                                <Link className="navBarLink" to="/">
                                    {getUserEmail()}
                                </Link>
                            </Nav.Link>
                            <Nav.Link>
                                <Link className="navBarLink" to="/logout">
                                    Log out
                                </Link>
                            </Nav.Link>
                            {/* xszabo16 */}
                        </Nav>
                    )}
                </Navbar.Collapse>
            </Container>
        </Navbar>
    );
};
export default NavBar;
