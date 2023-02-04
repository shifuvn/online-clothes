import React from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { Login, Register } from "../components";
import { CartPage, HomePage, ProductDetail } from "../pages";

const Routers = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/product/:id" element={<ProductDetail />} />
        <Route path="cart" element={<CartPage />} />
      </Routes>
    </BrowserRouter>
  );
};

export default Routers;
