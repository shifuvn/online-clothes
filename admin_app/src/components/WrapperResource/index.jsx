import React, { Fragment } from "react";
import { Admin, Resource } from "react-admin";
import { HttpApiProvider } from "../../services/ApiWrapper";
import { ProductList, ProductCreate } from "../products";
import { SkuDetailShow, SkuList } from "../sku";

const WrapperResource = () => {
  return (
    <Fragment>
      <Admin dataProvider={HttpApiProvider}>
        <Resource name="products" list={ProductList} create={ProductCreate} />
        <Resource name="skus" list={SkuList} show={SkuDetailShow} />
      </Admin>
    </Fragment>
  );
};

export default WrapperResource;
