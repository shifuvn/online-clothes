import React, { Fragment } from "react";
import { Datagrid, TextField, List, EditButton } from "react-admin";

const ProductTypeList = () => {
  return (
    <Fragment>
      <List>
        <Datagrid isRowSelectable={(record) => false}>
          <TextField source="id" />
          <TextField source="name" />
          <TextField source="createdAt" />
          <EditButton />
        </Datagrid>
      </List>
    </Fragment>
  );
};

export default ProductTypeList;
