import React, { Fragment } from "react";
import { Admin, CustomRoutes, Resource } from "react-admin";
import { HttpApiProvider } from "../../services/ApiWrapper";
import {
  ProductList,
  ProductCreate,
  ProductEdit,
  CreateSkuComponent
} from "../products";
import { SkuDetailShow, SkuEdit, SkuList } from "../sku";
import { CategoryCreate, CategoryEdit, CategoryList } from "../categories";
import { BrandCreate, BrandEdit, BrandList } from "../brands";
import { Route } from "react-router-dom";
import { authProvider } from "../../services/Auth";
import OrderList from "../orders/list";
import OrderShow from "../orders/show";

const WrapperResource = () => {
  return (
    <Fragment>
      <Admin dataProvider={HttpApiProvider} authProvider={authProvider}>
        <CustomRoutes>
          <Route
            path="/products/:id/create-sku"
            element={<CreateSkuComponent />}
          />
        </CustomRoutes>
        <Resource
          name="products"
          options={{ label: "Product" }}
          list={ProductList}
          create={ProductCreate}
          edit={ProductEdit}
        />
        <Resource
          name="orders"
          options={{ label: "Order" }}
          list={OrderList}
          show={OrderShow}
        />
        <Resource
          name="skus"
          options={{ label: "Sku" }}
          list={SkuList}
          show={SkuDetailShow}
          edit={SkuEdit}
        />
        <Resource
          name="categories"
          options={{ label: "Category" }}
          list={CategoryList}
          create={CategoryCreate}
          edit={CategoryEdit}
        />
        <Resource
          name="brands"
          options={{ label: "Brand" }}
          list={BrandList}
          edit={BrandEdit}
          create={BrandCreate}
        />
      </Admin>
    </Fragment>
  );
};

export default WrapperResource;
