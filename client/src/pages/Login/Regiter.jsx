import styled from "styled-components";
import React, { useState } from "react"
import { ToastContainer, toast } from 'react-toastify';
import { Navigate, useNavigate } from "react-router-dom";
import "./Register.css";
import axios from "axios"

const Container = styled.div`
  width: 100vw;
  height: 100vh;
  background: linear-gradient(
      rgba(255, 255, 255, 0.5),
      rgba(255, 255, 255, 0.5)
    ),
    url("https://images.pexels.com/photos/6984661/pexels-photo-6984661.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940")
      center;
  background-size: cover;
  display: flex;
  align-items: center;
  justify-content: center;
`;

const Wrapper = styled.div`
  width: 40%;
  padding: 20px;
  background-color: white;

`;

const Title = styled.h1`
  font-size: 24px;
  font-weight: 300;
`;

const Form = styled.form`
  display: flex;
  flex-wrap: wrap;
`;

const Input = styled.input`
  flex: 1;
  min-width: 40%;
  margin: 20px 10px 0px 0px;
  padding: 10px;
`;

const Agreement = styled.span`
  font-size: 12px;
  margin: 20px 0px;
`;

const Button = styled.button`
  width: 40%;
  border: none;
  padding: 15px 20px;
  background-color: teal;
  color: white;
  cursor: pointer;
`;
const Register = () => {
  const navigate = useNavigate();

  const [firstName, setFirstName] = useState("")
  const [lastName, setLastName] = useState("")
  const [password, setPwd] = useState("")
  const [userName, setUserName] = useState("")
  const [email, setEmail] = useState("")
  const [confirmPassword, setConfirmPassword] = useState("")



  const handleRegister = (e) => {
    e.preventDefault();
    axios.post('https://localhost:9443/api/v1/Accounts/sign-up', { firstName, lastName, email, password, confirmPassword })
      .then((res) => {
        console.log(res);
        toast.info("vui lòng xác nhận email");
        navigate("/login");
      })
      .catch((err) => {
        err.response.data.Message ?
          toast.error(err.response.data.Message) :
          toast.error(err.response.data.message)
        console.log("day la error")
        console.log(err);
      })

  }
  const naviLogin = (e) => {
    navigate('/login');
  }
  return (
    <Container>
      <Wrapper>
        <Title>CREATE AN ACCOUNT</Title>
        <Form onSubmit={handleRegister}>
          <Input type="text" required value={firstName} onChange={(e => setFirstName(e.target.value))} placeholder="first name" />
          <Input type="text" required value={lastName} onChange={(e => setLastName(e.target.value))} placeholder="last name" />
          <Input type="email" required value={email} onChange={(e => setEmail(e.target.value))} placeholder="email" />
          <Input type="password" minLength="6" required value={password} onChange={(e => setPwd(e.target.value))} placeholder="password" />
          <Input type="password" minLength="6" required value={confirmPassword} onChange={(e => setConfirmPassword(e.target.value))} placeholder="confirm password" />
          <Agreement>
            By creating an account, I consent to the processing of my personal
            data in accordance with the <b>PRIVACY POLICY</b>
          </Agreement>
          <div className="exist-account" onClick={naviLogin}>Already account? Login</div>
          <Button>CREATE</Button>

        </Form>

      </Wrapper>
    </Container>
  );
};

export default Register;