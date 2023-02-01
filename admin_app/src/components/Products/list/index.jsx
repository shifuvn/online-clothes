import React, { Fragment } from "react";
import {
  BooleanField,
  Button,
  Datagrid,
  EditButton,
  List,
  NumberField,
  TextField
} from "react-admin";
import { ProductPanelField, ThumbnailField } from "../_fields";

const ProductList = () => {
  return (
    <Fragment>
      <List>
        <Datagrid expand={<ProductPanelField />}>
          <TextField source="id" />
          <TextField source="name" />
          <ThumbnailField source="thumbnailUrl" label="Thumbnail" />
          <NumberField source="price" />
          <BooleanField source="isPublish" label="Published" />
          <EditButton />
        </Datagrid>
      </List>
    </Fragment>
  );
};

export default ProductList;
