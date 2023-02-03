import React, { Fragment } from "react";
import {
  BooleanField,
  Datagrid,
  EditButton,
  List,
  NumberField,
  TextField,
  TextInput
} from "react-admin";
import { ProductPanelField, ThumbnailField } from "../_fields";
import CreateSkuButton from "./CreateSkuButton";

const productFilter = [
  <TextInput source="id" />,
  <TextInput source="keyword" />
];

const ProductList = (props) => {
  return (
    <Fragment>
      <List {...props} filters={productFilter}>
        <Datagrid
          expand={<ProductPanelField />}
          isRowSelectable={(record) => false}
        >
          <TextField source="id" />
          <TextField source="name" />
          <ThumbnailField source="thumbnailUrl" label="Thumbnail" />
          <NumberField source="price" />
          <BooleanField source="isPublish" label="Published" />
          <EditButton />
          <CreateSkuButton />
        </Datagrid>
      </List>
    </Fragment>
  );
};

export default ProductList;
