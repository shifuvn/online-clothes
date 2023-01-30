import React, { Fragment } from "react";
import { Admin, Resource } from "react-admin";
import { HttpApiProvider } from "../../services/ApiWrapper";
import { ProductList, ProductCreate } from "../products";

const WrapperResource = () => {
  return (
    <Fragment>
      <Admin dataProvider={HttpApiProvider}>
        <Resource name="products" list={ProductList} create={ProductCreate} />
      </Admin>
    </Fragment>
  );
};

export default WrapperResource;
