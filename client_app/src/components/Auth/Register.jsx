import React from "react";
import styled from "styled-components";
import { apiClient } from "../../services";

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
  flex-direction: column;
`;

const Input = styled.input`
  flex: 1;
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
  const handleSubmit = async (e) => {
    e.preventDefault();
    const target = e.target;
    const payload = {
      email: target.email.value,
      password: target.password.value,
      confirmPassword: target.confirmPassword.value,
      firstName: target.firstName.value,
      lastName: target.lastName.value
    };

    const result = await apiClient.post(`/accounts/sign-up`, payload);
    if (result.status === 201) {
      e.target.reset();
    }
  };

  return (
    <Container>
      <Wrapper>
        <Title>Create new account</Title>
        <Form onSubmit={handleSubmit}>
          <Input id="firstName" placeholder="name" />
          <Input id="lastName" placeholder="last name" />
          <Input id="email" placeholder="email" />
          <Input id="password" placeholder="password" type="password" />
          <Input
            id="confirmPassword"
            placeholder="confirm password"
            type="password"
          />
          <Agreement>
            By creating an account, I consent to the processing of my personal
            data in accordance with the <b>PRIVACY POLICY</b>
          </Agreement>
          <Button>Register</Button>
        </Form>
      </Wrapper>
    </Container>
  );
};

export default Register;
