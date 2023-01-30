import { Fragment } from "react";
import { BrowserRouter } from "react-router-dom";
import AppRouters from "./routers";
import { ThemeProvider, createTheme } from "@mui/material/styles";

const theme = createTheme({
  typography: {
    fontFamily: ["Montserrat", "cursive"].join(","),
    fontSize: 20,
    lineHeight: 20
  }
});

function App() {
  return (
    <Fragment>
      <ThemeProvider theme={theme}>
        <BrowserRouter>
          <AppRouters />
        </BrowserRouter>
      </ThemeProvider>
    </Fragment>
  );
}

export default App;
