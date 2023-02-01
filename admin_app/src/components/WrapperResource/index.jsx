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
import {
  ProductTypeCreate,
  ProductTypeEdit,
  ProductTypeList
} from "../productTypes";
import { CategoryCreate, CategoryEdit, CategoryList } from "../categories";
import { BrandCreate, BrandEdit, BrandList } from "../brands";
import { Route } from "react-router-dom";
import { authProvider } from "../../services/Auth";

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
        <Resource
          name="productTypes"
          options={{ label: "Product Type" }}
          list={ProductTypeList}
          create={ProductTypeCreate}
          edit={ProductTypeEdit}
        />
      </Admin>
    </Fragment>
  );
};

export default WrapperResource;
