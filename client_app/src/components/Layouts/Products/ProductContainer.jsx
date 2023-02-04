import React from "react";
import styled from "styled-components";
import { apiClient } from "../../../services";
import ProductItem from "./ProductItem";
import { Pagination } from "@mui/material";
import { Container, Grid } from "@material-ui/core";

//const Container = styled.div`
//  padding: 20px;
//  display: flex;
//  flex-wrap: wrap;
//  justify-content: space-between;
//`;

const ProductContainer = () => {
  const [products, setProducts] = React.useState([]);
  const [page, setPage] = React.useState(1);
  const [pageCount, setPageCount] = React.useState(1);

  React.useEffect(() => {
    const prepareGetProductQuery = () => {
      return `/Products?PageIndex=${page}`;
    };
    const fetchData = async () => {
      const data = await apiClient.get(prepareGetProductQuery());
      const paging = {
        page: data.data.currentPage,
        count: data.data.pages
      };
      setProducts(data?.data?.items);
      setPage(paging.page);
      setPageCount(paging.count);
    };

    fetchData();
  }, [page]);

  const handleChange = (e, page) => {
    setPage(page);
  };

  return (
    <Container style={{ marginTop: " 40px" }}>
      <Grid container>
        {products &&
          products.map((item, idx) => {
            return (
              <Grid xs={3} key={idx}>
                <ProductItem source={item} />
              </Grid>
            );
          })}
      </Grid>
      <div
        style={{
          width: "90%",
          display: "flex",
          justifyContent: "center",
          margin: "40px 0"
        }}
      >
        <Pagination
          count={pageCount}
          page={page}
          variant="outlined"
          shape="rounded"
          onChange={handleChange}
        />
      </div>
    </Container>
  );
};

export default ProductContainer;
