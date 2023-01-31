import React, { Fragment } from "react";
import {
  List,
  Datagrid,
  ImageField,
  TextField,
  NumberField,
  BooleanField
} from "react-admin";

const SkuList = () => {
  return (
    <Fragment>
      <List>
        <Datagrid rowClick="show">
          <TextField source="sku" />
          <TextField source="name" />
          <ImageField source="imageUrl" label="Image" />
          <NumberField source="price" />
          <NumberField source="inStock" />
          <BooleanField source="isDeleted" label="Deleted" />
        </Datagrid>
      </List>
    </Fragment>
  );
};

export default SkuList;
