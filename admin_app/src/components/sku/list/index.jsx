import React, { Fragment } from "react";
import {
  List,
  Datagrid,
  ImageField,
  TextField,
  NumberField,
  BooleanField,
  TextInput
} from "react-admin";

const skuFilter = [<TextInput source="keyword" label="SKU search" />];

const SkuList = () => {
  return (
    <Fragment>
      <List filters={skuFilter}>
        <Datagrid rowClick="show" isRowSelectable={(record) => false}>
          <TextField source="sku" />
          <TextField source="name" />
          <ImageField source="imageUrl" label="Image" />
          <NumberField source="totalPrice" />
          <NumberField source="inStock" />
          <BooleanField source="isDeleted" label="Deleted" />
        </Datagrid>
      </List>
    </Fragment>
  );
};

export default SkuList;
