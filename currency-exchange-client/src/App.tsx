import { Container } from "react-bootstrap";
import { ToastContainer } from "react-toastify";
import { Route, Routes, Navigate } from "react-router-dom";

import Home from "./pages/Home";
import Exchange from "./pages/Exchange";
import HistoricalExchange from "./pages/HistoricalExchange";

import AppNavbar from "./components/AppNavbar";

function Page() {
  return (
    <Container fluid className="pt-4">
      <Routes>
        <Route path="/" element={<Home/>} />
        <Route path="/convert" element={<Exchange/>} />
        <Route path="/historical-convert" element={<HistoricalExchange/>} />
        <Route path="*" element={<Navigate to="/"/>} />
      </Routes>
    </Container>
  );
}

export default function App() {
  return (
    <>
      <header>
        <AppNavbar/>
      </header>
      <main>
       <Page/>
      </main>
      <ToastContainer autoClose={2500} pauseOnFocusLoss={false} pauseOnHover={false} position="bottom-right"/>
    </>
  );
}