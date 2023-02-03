import React, { Fragment } from "react";
import { Datagrid, EditButton, List, TextField, TextInput } from "react-admin";

const brandFilter = [<TextInput source="id" label="Id" />];

const BrandList = () => {
  return (
    <Fragment>
      <List filters={brandFilter}>
        <Datagrid isRowSelectable={(record) => false}>
          <TextField source="id" />
          <TextField source="name" />
          <TextField source="description" />
          <TextField source="contactEmail" label="Email" />
          <EditButton />
        </Datagrid>
      </List>
    </Fragment>
  );
};

export default BrandList;
