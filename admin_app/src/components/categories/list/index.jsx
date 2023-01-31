import React, { Fragment } from "react";
import {
  Datagrid,
  EditButton,
  List,
  RichTextField,
  TextField
} from "react-admin";

const CategoryListPanelField = () => {
  return <RichTextField source="description" />;
};

const CategoryList = () => {
  return (
    <Fragment>
      <List>
        <Datagrid expand={CategoryListPanelField}>
          <TextField source="id" />
          <TextField source="name" />
          <TextField source="createdAt" />
          <EditButton />
        </Datagrid>
      </List>
    </Fragment>
  );
};

export default CategoryList;
