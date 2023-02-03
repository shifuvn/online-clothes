import styled from "styled-components";

import { Link } from "react-router-dom";
import axios from "axios";
import { useState } from "react";
import { TokenManage } from "../../services";
import { Navigate, useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

const Container = styled.div`
  width: 100vw;
  height: 100vh;
  background: linear-gradient(
      rgba(255, 255, 255, 0.5),
      rgba(255, 255, 255, 0.5)
    ),
    url("https://images.pexels.com/photos/6984650/pexels-photo-6984650.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940")
      center;
  background-size: cover;
  display: flex;
  align-items: center;
  justify-content: center;
`;

const Wrapper = styled.div`
  width: 25%;
  padding: 20px;
  background-color: white;
  
`;

const Title = styled.h1`
  font-size: 24px;
  font-weight: 300;
`;

const Form = styled.form`
  display: flex;
  flex-direction: column;
`;

const Input = styled.input`
  flex: 1;
  min-width: 40%;
  margin: 10px 0;
  padding: 10px;
`;

const Button = styled.button`
  width: 40%;
  border: none;
  padding: 15px 20px;
  background-color: teal;
  color: white;
  cursor: pointer;
  margin-bottom: 10px;
`;

const Linkk = styled.a`
  margin: 5px 0px;
  font-size: 12px;
  text-decoration: underline;
  cursor: pointer;
`;

const Login = () => {
  const navigate = useNavigate();

  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const handleLogin = (e) => {
    e.preventDefault();

    axios.post('https://localhost:9443/api/v1/Accounts/sign-in', { email, password })
      .then((res) => {
        console.log("day la res", res);
        TokenManage.setAccessToken(res.data.data.accessToken);
        toast.success("login successs");
        navigate('/');
      })
      .catch((err) => {

        console.log("day la err", err);
        toast.error(err.response.data.message);
      })

  }
  return (
    <Container>
      <Wrapper>
        <Title>SIGN IN</Title>
        <Form>
          <Input type="text" required value={email} onChange={(e) => setEmail(e.target.value)} placeholder="email" />
          <Input type="password" required value={password} onChange={(e) => setPassword(e.target.value)} placeholder="password" />
          <Button onClick={handleLogin}>LOGIN</Button>
          <Linkk style={{ "margin": "5px 0px", "fontSize": "12px", "textDecoration": "underline", "cursor": "pointer" }}>DO NOT YOU REMEMBER THE PASSWORD?</Linkk>
          <Link to='/sigup' style={{ "margin": "5px 0px", "fontSize": "12px", "textDecoration": "underline", "cursor": "pointer" }}>CREATE A NEW ACCOUNT</Link>
        </Form>
      </Wrapper>
    </Container>
  );
};

export default Login;