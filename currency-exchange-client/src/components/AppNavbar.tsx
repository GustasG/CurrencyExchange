import { useState } from "react";
import { Link } from "react-router-dom";
import { Container, Navbar, Nav } from "react-bootstrap";

export default function AppNavbar() {
  const [expanded, setExpanded] = useState(false);

  const closeNavbar = () => {
    setExpanded(false);
  };

  return (
    <Navbar collapseOnSelect bg="dark" variant="dark" expand="lg" onToggle={() => setExpanded(!expanded)} expanded={expanded}>
      <Container fluid>
        <Navbar.Brand onClick={closeNavbar} as={Link} to="/">Currency Converter</Navbar.Brand>
        <Navbar.Toggle/>
        <Navbar.Collapse className="justify-content-end">
          <Nav>
            <Nav.Link onClick={closeNavbar} as={Link} to="/convert">Convert Currency</Nav.Link>
            <Nav.Link onClick={closeNavbar} as={Link} to="/historical-convert">Convert Historical Currency</Nav.Link>
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
}