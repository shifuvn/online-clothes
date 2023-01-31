import React, { Fragment } from "react";
import { Datagrid, EditButton, List, TextField } from "react-admin";

const BrandList = () => {
  return (
    <Fragment>
      <List>
        <Datagrid>
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
