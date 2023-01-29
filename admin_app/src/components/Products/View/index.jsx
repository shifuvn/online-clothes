import React, { Fragment } from "react";
import { Datagrid, List, TextField } from "react-admin";

const ProductList = () => {
  return (
    <Fragment>
      <List>
        <Datagrid rowClick="edit">
          <TextField source="id" />
          <TextField source="name" />
        </Datagrid>
      </List>
    </Fragment>
  );
};

export default ProductList;
