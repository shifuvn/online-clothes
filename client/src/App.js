import React, { useState } from 'react';
import { Routes, Route, Outlet, Link } from "react-router-dom";
import DetailProduct from './pages/DetailProduct/DetailProduct';
import Home from './pages/Home/Home';
import PageProduct from './pages/PageProduct/PageProduct';
import { cartContext } from './Context/cartContext';
import Login from "./pages/Login/Login"
import Regeter from "./pages/Login/Regiter"
import Cart from "./pages/Cart/Cart"
import OrderDetails from './pages/OderDetail/OrderDetail';
import PersonalProfile from './pages/Profile/Profile';
function App() {
  const [cart, setCart] = useState([]);
  return (
    <div>
      <cartContext.Provider value={{ cart, setCart }}>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path='/detail' element={<DetailProduct />} />
          <Route path='/cart' element={<Cart />} />
          <Route path='/login' element={<Login />} />
          <Route path='/sigup' element={<Regeter />} />
          <Route path='/products' element={<PageProduct />} />
          <Route path='/products/:id' element={<DetailProduct />} />
          <Route path='/oderdetail' element={<OrderDetails />} />
          <Route path='/profile' element={<PersonalProfile />} />
        </Routes>
      </cartContext.Provider>
    </div>
  );
}

export default App;