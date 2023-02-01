import React from "react";
import ReactDOM from "react-dom/client";
import GlobalStyles from "./styles";
import App from "./App";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  <React.StrictMode>
    <GlobalStyles>
      <ToastContainer />
      <App />
    </GlobalStyles>
  </React.StrictMode>
);
