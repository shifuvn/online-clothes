import React from "react";
import { useNavigate } from "react-router-dom";
import styled from "styled-components";
import { apiClient } from "../../services";
import { TokenManage } from "../../services/TokenManage";

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

const Link = styled.a`
  margin: 5px 0px;
  font-size: 12px;
  text-decoration: underline;
  cursor: pointer;
`;

const Login = () => {
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    const target = e.target;
    const payload = {
      email: target.email.value,
      password: target.password.value
    };
    const result = await apiClient.post("/Accounts/sign-in", payload);
    if (result.status === 200) {
      TokenManage.setAccessToken(result.data.data.accessToken);
      navigate("/");
    }
  };
  return (
    <Container>
      <Wrapper>
        <Title>SIGN IN</Title>
        <Form onSubmit={handleSubmit}>
          <Input id="email" placeholder="email" />
          <Input id="password" placeholder="password" type="password" />
          <Button>Login</Button>
          <Link>Forgot password?</Link>
          <Link href="/register">Create new account</Link>
        </Form>
      </Wrapper>
    </Container>
  );
};

export default Login;
