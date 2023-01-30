import React, { Fragment } from "react";
import {
  EditButton,
  ImageField,
  NumberField,
  Show,
  SimpleShowLayout,
  TextField
} from "react-admin";

const SkuDetailShow = () => {
  return (
    <Fragment>
      <Show>
        <SimpleShowLayout>
          <TextField source="sku" />
          <TextField source="productId" />
          <TextField source="name" />
          <TextField source="description" />
          <NumberField source="price" />
          <NumberField source="inStock" />
          <TextField source="createdAt" /> {/*TODO: parse time*/}
          <ImageField source="imageUrl" label="Image" />
          <EditButton />
        </SimpleShowLayout>
      </Show>
    </Fragment>
  );
};

export default SkuDetailShow;
