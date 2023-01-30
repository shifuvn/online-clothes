import React, { Fragment } from "react";
import {
  Datagrid,
  EditButton,
  List,
  NumberField,
  TextField
} from "react-admin";
import { ThumbnailField } from "../_fields";

const ProductList = () => {
  return (
    <Fragment>
      <List>
        <Datagrid rowClick="edit">
          <TextField source="id" />
          <TextField source="name" />
          <ThumbnailField source="thumbnailUrl" />
          <NumberField source="price" />
          <EditButton></EditButton>
        </Datagrid>
      </List>
    </Fragment>
  );
};

export default ProductList;
