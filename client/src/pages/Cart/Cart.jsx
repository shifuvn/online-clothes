import "./Cart.css";
import { useState } from "react";
import CartItem from "../../components/CartItem/CartItem";
import Header from "../../components/Header/Header";
const Cart = () => {


  return (
    <>
      <Header />

      <h1>danh sach san pham</h1>
      <div className="cart-container">
        <div className="listItem">
          <CartItem />
        </div>
        <div className="total-price">
          <div>
            <h3>Tổng tiền</h3>
            <div style={{ fontSize: "25px" }}> 300000</div>
            <button className="cart-order">Đặt hàng</button>
          </div>
        </div>
      </div>
    </>
  );
};
export default Cart;
