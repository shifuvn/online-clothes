import React, { Fragment } from "react";
import { Edit, SimpleForm, TextInput } from "react-admin";
import { HttpApiProvider } from "../../../services/ApiWrapper";

const BrandEdit = () => {
  const handleSubmit = async (params) => {
    await HttpApiProvider.update("brands", params);
  };

  return (
    <Fragment>
      <Edit>
        <SimpleForm sanitizeEmptyValues onSubmit={handleSubmit}>
          <TextInput source="name" />
          <TextInput source="description" />
          <TextInput source="contactEmail" />
        </SimpleForm>
      </Edit>
    </Fragment>
  );
};

export default BrandEdit;
