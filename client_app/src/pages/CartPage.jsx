import { Input, TextField } from "@material-ui/core";
import React from "react";
import styled from "styled-components";
import { Footer, Navbar } from "../components";
import { apiClient } from "../services";
import { toVnd } from "../utils";
import { Add, Remove } from "@material-ui/icons";

const Container = styled.div``;

const Wrapper = styled.div`
  padding: 20px;
`;

const Title = styled.h1`
  font-weight: 300;
  text-align: center;
`;

const Top = styled.div`
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 20px;
`;

const TopButton = styled.button`
  padding: 10px;
  font-weight: 600;
  cursor: pointer;
  border: ${(props) => props.type === "filled" && "none"};
  background-color: ${(props) =>
    props.type === "filled" ? "black" : "transparent"};
  color: ${(props) => props.type === "filled" && "white"};
`;

const TopTexts = styled.div``;
const TopText = styled.span`
  text-decoration: underline;
  cursor: pointer;
  margin: 0px 10px;
`;

const Bottom = styled.div`
  display: flex;
  justify-content: space-between;
`;

const Info = styled.div`
  flex: 3;
`;

const Summary = styled.div`
  flex: 1;
  border: 0.5px solid lightgray;
  border-radius: 10px;
  padding: 20px;
  height: 50vh;
`;

const SummaryTitle = styled.h1`
  font-weight: 200;
`;

const SummaryItem = styled.div`
  margin: 30px 0px;
  display: flex;
  justify-content: space-between;
  font-weight: ${(props) => props.type === "total" && "500"};
  font-size: ${(props) => props.type === "total" && "24px"};
`;

const SummaryItemText = styled.span``;

const SummaryItemPrice = styled.span``;

const Button = styled.button`
  width: 100%;
  padding: 10px;
  background-color: black;
  color: white;
  font-weight: 600;
`;

const Product = styled.div`
  display: flex;
  justify-content: space-between;
`;

const ProductDetail = styled.div`
  flex: 2;
  display: flex;
`;

const Image = styled.img`
  width: 200px;
`;

const Details = styled.div`
  padding: 20px;
  display: flex;
  flex-direction: column;
  justify-content: space-around;
`;

const ProductName = styled.span``;

const ProductSize = styled.span``;

const ProductId = styled.span``;

const ProductColor = styled.div`
  width: 20px;
  height: 20px;
  border-radius: 50%;
  background-color: ${(props) => props.color};
`;

const ProductPrice = styled.div`
  font-size: 30px;
  font-weight: 200;
`;

const PriceDetail = styled.div`
  flex: 1;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
`;

const ProductAmountContainer = styled.div`
  display: flex;
  align-items: center;
  margin-bottom: 20px;
`;

const ProductAmount = styled.div`
  font-size: 24px;
  margin: 5px;
`;

const Hr = styled.hr`
  background-color: #eee;
  border: none;
  height: 1px;
`;

const CartItem = ({ productSku, quantity }) => {
  const [itemQuantity, setItemQuantity] = React.useState(quantity);

  const handleClickQuantity = async (value) => {
    const newValue = itemQuantity + value;
    if (newValue < 1) {
      setItemQuantity(1);
      return;
    }
    setItemQuantity(newValue);
    const result = await apiClient.put(`carts`, {
      sku: productSku.sku,
      number: newValue
    });
  };

  return (
    <Product>
      <ProductDetail>
        <Image src={productSku?.imageUrl} />
        <Details>
          <ProductName>
            <b>Product:</b> {productSku?.name}
          </ProductName>
          <ProductId>
            <b>ID:</b> {productSku?.sku}
          </ProductId>
        </Details>
      </ProductDetail>
      <PriceDetail>
        <ProductAmountContainer>
          <Add
            onClick={(e) => {
              handleClickQuantity(1);
            }}
          />
          <ProductAmount>{itemQuantity}</ProductAmount>
          <Remove
            onClick={(e) => {
              handleClickQuantity(-1);
            }}
          />
        </ProductAmountContainer>
        <ProductPrice>Price: {toVnd(productSku?.totalPrice)}</ProductPrice>
      </PriceDetail>
      <Hr />
    </Product>
  );
};

const CartPage = () => {
  const [cartItems, setCartItems] = React.useState([]);
  const [orderAddress, setOrderAddress] = React.useState("");

  React.useEffect(() => {
    const fetchData = async () => {
      const result = await apiClient.get(`carts`);
      setCartItems(result.data);
    };

    fetchData();
  }, []);

  const calcTotal = () => {
    return cartItems.reduce(
      (sum, item) => sum + item.productSku.totalPrice * item.quantity,
      0
    );
  };

  const handleCheckout = async () => {
    const payload = {
      address: orderAddress
    };

    const result = await apiClient.post(`Orders/checkout`, payload);
  };

  const handleChangeAddress = (e) => {
    setOrderAddress(e.target.value);
  };

  return (
    <Container>
      <Navbar />
      <Wrapper>
        <Title>Your cart</Title>
        <Bottom>
          <Info>
            {cartItems &&
              cartItems.map((item, idx) => (
                <CartItem
                  key={idx}
                  productSku={item.productSku}
                  quantity={item.quantity}
                />
              ))}
          </Info>
          <Summary>
            <SummaryTitle>Order</SummaryTitle>
            <TextField
              placeholder="Address"
              style={{ width: "100%", padding: "12px 0" }}
              onChange={handleChangeAddress}
              value={orderAddress}
            />
            <SummaryItem type="total">
              <SummaryItemText>Total</SummaryItemText>
              <SummaryItemPrice>{toVnd(calcTotal())}</SummaryItemPrice>
            </SummaryItem>
            <Button style={{ marginTop: "16px" }} onClick={handleCheckout}>
              Checkout
            </Button>
          </Summary>
        </Bottom>
      </Wrapper>
      <Footer />
    </Container>
  );
};

export default CartPage;
