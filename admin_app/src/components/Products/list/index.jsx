import React, { Fragment } from "react";
import {
  Button,
  Datagrid,
  EditButton,
  List,
  NumberField,
  ShowButton,
  TextField
} from "react-admin";
import { ProductPanelField, ThumbnailField } from "../_fields";

const ProductList = () => {
  return (
    <Fragment>
      <List>
        <Datagrid rowClick="show" expand={<ProductPanelField />}>
          <TextField source="id" />
          <TextField source="name" />
          <ThumbnailField source="thumbnailUrl" label="Thumbnail" />
          <NumberField source="price" />
          <EditButton />
        </Datagrid>
      </List>
    </Fragment>
  );
};

export default ProductList;
