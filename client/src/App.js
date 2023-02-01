import React, { useState } from 'react';
import { Routes, Route, Outlet, Link } from "react-router-dom";
import DetailProduct from './pages/DetailProduct/DetailProduct';
import Home from './pages/Home/Home';
import PageProduct from './pages/PageProduct/PageProduct';
import { cartContext } from './Context/cartContext';
import LogSign from './pages/Login/Login';

function App() {
  const [cart, setCart] = useState([]);
  return (
    <div>
      <cartContext.Provider value={{ cart, setCart }}>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path='/detail' element={<DetailProduct />} />
          <Route path='/login' element={<LogSign />} />
          <Route path='/products' element={<PageProduct />} />
          <Route path='/products/:id' element={<DetailProduct />} />
        </Routes>
      </cartContext.Provider>
    </div>
  );
}

export default App;