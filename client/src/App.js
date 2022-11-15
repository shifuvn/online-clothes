import logo from './logo.svg';
//import './App.css';
import "bootstrap/dist/css/bootstrap.min.css"
// import { BrowserRouter, Routes, Route,Router } from "react-router-dom"
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";

import LogSign from "./components/User/LogSign.js";

//import Demo from './components/demo';
//import User from "./components/User"
//import a from "./a"
function App() {
  return (
    // <BrowserRouter>

    <Router>
      <div>
        <Routes>
          <Route path="/login" element={<LogSign />} />

        </Routes>
      </div>
    </Router>

  );
}
{/* </BrowserRouter> */ }

export default App;
