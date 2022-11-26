
import './App.css';
import Header from "./components/Header.js"
import { BrowserRouter as Router } from "react-router-dom"
import Slider from './components/Slider';
import Categories from './components/Categories';
//import LogSign from './component/layout/LogSign';
const App = () => {
  return (<><Header />
    <Slider />
    <Categories />
  </>);

}

export default App;
