import React, { Fragment } from "react";
import {
  ArrayField,
  ChipField,
  SingleFieldList,
  EditButton,
  ImageField,
  NumberField,
  Show,
  SimpleShowLayout,
  TextField,
  BooleanField
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
          <NumberField source="addOnPrice" />
          <NumberField source="inStock" />
          <TextField source="size" />
          <TextField source="type.name" />
          <TextField source="brand.name" />
          <ArrayField source="categories">
            <SingleFieldList linkType={false}>
              <ChipField source="name" />
            </SingleFieldList>
          </ArrayField>
          <BooleanField source="isDeleted" />
          <TextField source="createdAt" /> {/*TODO: parse time*/}
          <ImageField source="imageUrl" label="Image" />
          <EditButton />
        </SimpleShowLayout>
      </Show>
    </Fragment>
  );
};

export default SkuDetailShow;
