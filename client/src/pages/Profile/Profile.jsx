import React from "react";
import "./Profile.css";
import Header from "../../components/Header/Header"
import { Navigate, useNavigate } from "react-router-dom";

const Profile = () => {
  const navigate = useNavigate();
  const handleLogOut = (e) => {
    e.preventDefault();


    navigate('/login');


  }
  return (
    <div>
      <link
        rel="stylesheet"
        href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css"
      />
      <Header />
      <div className="card">
        <img src="https://img.lovepik.com/free-png/20210926/lovepik-cartoon-avatar-png-image_401440426_wh1200.png" alt="John" style={{ width: "50%", padding: "20px" }} />
        <h1>Nguyen Van An</h1>
        <p className="title">CEO &amp; Founder, Example</p>
        <p>Đại Học Quốc Gia Thành Phố Hồ Chí Minh</p>
        <p>annv@gmail.com</p>
        <a href="#">
          <i className="fa fa-dribbble" />
        </a>
        <a href="#">
          <i className="fa fa-twitter" />
        </a>
        <a href="#">
          <i className="fa fa-linkedin" />
        </a>
        <a href="#">
          <i className="fa fa-facebook" />
        </a>
        <p>
          <button onClick={handleLogOut}>Log Out</button>
        </p>
      </div>
    </div>
  );
};
export default Profile;