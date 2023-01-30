import React, { Fragment } from "react";
import { List, Datagrid, ImageField, TextField } from "react-admin";

const SkuList = () => {
  return (
    <Fragment>
      <List>
        <Datagrid rowClick="show">
          <TextField source="sku" />
          <TextField source="type" />
          <ImageField source="imageUrl" />
          <TextField source="isDeleted" />
        </Datagrid>
      </List>
    </Fragment>
  );
};

export default SkuList;
