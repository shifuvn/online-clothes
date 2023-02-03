import React, { Fragment } from "react";
import { Edit, SimpleForm, TextInput } from "react-admin";
import { HttpApiProvider } from "../../../services/ApiWrapper";

const CategoryEdit = () => {
  const handleSubmit = async (params) => {
    await HttpApiProvider.update("categories", params);
  };

  return (
    <Fragment>
      <Edit>
        <SimpleForm onSubmit={handleSubmit}>
          <TextInput source="name" />
          <TextInput source="description" multiline />
        </SimpleForm>
      </Edit>
    </Fragment>
  );
};

export default CategoryEdit;
