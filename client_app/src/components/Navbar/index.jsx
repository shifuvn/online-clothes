import React from "react";
import styled from "styled-components";
import { Search, ShoppingCartOutlined } from "@material-ui/icons";
import { Badge, Container } from "@material-ui/core";
import { useNavigate } from "react-router-dom";
import { TokenManage } from "../../services/TokenManage";
import { UserManage } from "../../services/UserManage";
import UserDropDown from "./UserDropDown";

const Wrapper = styled.div`
  padding: 10px 20px;
  display: flex;
  align-items: center;
  justify-content: space-between;
`;

const Left = styled.div`
  flex: 1;
  display: flex;
  align-items: center;
`;

const SearchContainer = styled.div`
  border: 0.5px solid lightgray;
  display: flex;
  align-items: center;
  margin-left: 25px;
  padding: 5px;
`;

const Input = styled.input`
  border: none;
  font-size: 16px;
`;

const Center = styled.div`
  flex: 1;
  text-align: center;
`;

const Logo = styled.h1`
  font-weight: bold;
`;
const Right = styled.div`
  flex: 1;
  display: flex;
  align-items: center;
  justify-content: flex-end;
`;

const MenuItem = styled.div`
  font-size: 14px;
  cursor: pointer;
  margin-left: 25px;
`;

const Navbar = () => {
  const navigate = useNavigate();

  const clickRegister = (e) => {
    navigate("/register");
  };

  const clickLogin = (e) => {
    navigate("/login");
  };

  const clickCart = (e) => {
    navigate("/cart");
  };

  return (
    <Container>
      <Wrapper>
        <Left>
          <SearchContainer>
            <Input placeholder="Search" />
            <Search style={{ color: "gray", fontSize: 16, padding: "4px" }} />
          </SearchContainer>
        </Left>
        <Center>
          <Logo>ONLINE CLOTHE.</Logo>
        </Center>
        <Right>
          {TokenManage.isAuth() ? (
            <UserDropDown />
          ) : (
            <>
              <MenuItem onClick={clickRegister}>Register</MenuItem>
              <MenuItem onClick={clickLogin}>Sign in</MenuItem>
            </>
          )}
          <MenuItem onClick={clickCart}>
            <Badge badgeContent={0} color="primary">
              <ShoppingCartOutlined />
            </Badge>
          </MenuItem>
        </Right>
      </Wrapper>
    </Container>
  );
};

export default Navbar;
