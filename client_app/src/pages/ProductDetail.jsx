import React from "react";
import { Add, Remove } from "@material-ui/icons";
import styled from "styled-components";
import { Navbar, Footer, FilterSkuOptions } from "../components";
import { Container } from "@mui/material";
import { useParams } from "react-router-dom";
import { apiClient } from "../services";
import { toVnd } from "../utils";

const Wrapper = styled.div`
  padding: 50px;
  display: flex;
`;

const ImgContainer = styled.div`
  flex: 1;
`;

const Image = styled.img`
  width: 100%;
  height: 90vh;
  object-fit: cover;
`;

const InfoContainer = styled.div`
  flex: 1;
  padding: 0px 50px;
`;

const Title = styled.h1`
  font-weight: 200;
`;

const Desc = styled.p`
  margin: 20px 0px;
`;

const Price = styled.span`
  font-weight: 100;
  font-size: 40px;
`;

const FilterContainer = styled.div`
  width: 50%;
  margin: 30px 0px;
  display: flex;
  justify-content: space-between;
`;

//const Filter = styled.div`
//  display: flex;
//  align-items: center;
//`;

//const FilterTitle = styled.span`
//  font-size: 20px;
//  font-weight: 200;
//`;

//const FilterColor = styled.div`
//  width: 20px;
//  height: 20px;
//  border-radius: 50%;
//  background-color: ${(props) => props.color};
//  margin: 0px 5px;
//  cursor: pointer;
//`;

//const FilterSize = styled.select`
//  margin-left: 10px;
//  padding: 5px;
//`;

const AddContainer = styled.div`
  width: 50%;
  display: flex;
  align-items: center;
  justify-content: space-between;
`;

const AmountContainer = styled.div`
  display: flex;
  align-items: center;
  font-weight: 700;
`;

const Amount = styled.span`
  width: 30px;
  height: 30px;
  border-radius: 10px;
  border: 1px solid teal;
  display: flex;
  align-items: center;
  justify-content: center;
  margin: 0px 5px;
`;

const Button = styled.button`
  padding: 15px;
  border: 2px solid teal;
  background-color: white;
  cursor: pointer;
  font-weight: 500;
  &:hover {
    background-color: #f8f4f4;
  }
`;

const ProductDetail = () => {
  const params = useParams();
  const [product, setProduct] = React.useState({});
  const [allSkus, setAllSkus] = React.useState([]);
  const [quantity, setQuantity] = React.useState(1);

  React.useEffect(() => {
    const fetchData = async () => {
      const productResult = await apiClient.get(`/Skus/${params.id}`);
      const productId = productResult?.data?.productId;
      const skuIdsResult = await apiClient.get(`/Skus/others/${productId}`);
      setProduct(productResult?.data);
      setAllSkus(skuIdsResult?.data);
    };

    fetchData();
  }, [params.id]);

  const handleClickQuantity = (value) => {
    const newValue = quantity + value;
    if (newValue < 1) {
      setQuantity(1);
      return;
    }
    setQuantity(newValue);
  };

  const handleAddCart = async (e) => {
    const payload = {
      sku: params.id,
      number: quantity
    };

    const result = await apiClient.put(`Carts`, payload);
  };

  return (
    <Container>
      <Navbar />
      <Wrapper>
        <ImgContainer>
          <Image src={product?.imageUrl} />
        </ImgContainer>
        <InfoContainer>
          <Title>{product?.name}</Title>
          <Desc>{product?.description}</Desc>
          <Price>{toVnd(product?.totalPrice)}</Price>
          <FilterSkuOptions skus={allSkus} />
          <AddContainer>
            <AmountContainer>
              <Remove
                onClick={(e) => {
                  handleClickQuantity(-1);
                }}
              />
              <Amount>{quantity}</Amount>
              <Add
                onClick={(e) => {
                  handleClickQuantity(1);
                }}
              />
            </AmountContainer>
            <Button onClick={handleAddCart}>Add cart</Button>
          </AddContainer>
        </InfoContainer>
      </Wrapper>
      <Footer />
    </Container>
  );
};

export default ProductDetail;
