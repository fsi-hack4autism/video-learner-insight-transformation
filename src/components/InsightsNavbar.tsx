import React from "react";
import { Navbar, Nav, NavDropdown } from 'react-bootstrap';
import '../styles/Navbar.css';

const InsightsNavbar = () => {
    return (
        <Navbar expand="lg" className="dash-navbar">
            <Navbar.Brand>Insight Center</Navbar.Brand>
            <Navbar.Toggle aria-controls="basic-navbar-nav" />
            <Navbar.Collapse id="basic-navbar-nav">
                <Nav className="mr-auto">
                    <Nav.Link href="/dashboard">Dashboard</Nav.Link>
                    <Nav.Link href="/video">Videos</Nav.Link>
                    <Nav.Link href="/upload">Upload Video</Nav.Link>
                </Nav>
                <Nav className="ml-auto">
                    <NavDropdown title="My Account" id="basic-nav-dropdown" >
                        <NavDropdown.Item href="/profile">Profile</NavDropdown.Item>
                        <NavDropdown.Item href="/insights">Detail Insights</NavDropdown.Item>
                        <NavDropdown.Item href="/settings">Settings</NavDropdown.Item>
                        <NavDropdown.Divider />
                        <NavDropdown.Item href="/logout">Logout</NavDropdown.Item>
                    </NavDropdown>
                </Nav>
            </Navbar.Collapse>
        </Navbar>
    )
}

export default InsightsNavbar;