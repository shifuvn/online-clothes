import React, { Fragment } from "react";
import { Create, SimpleForm, TextInput } from "react-admin";
import { HttpApiProvider } from "../../../services/ApiWrapper";
import { useNavigate } from "react-router-dom";

const BrandCreate = (props) => {
  const navigate = useNavigate();
  const handleSubmit = async (params) => {
    await HttpApiProvider.create("brands", params);
    navigate("/brands");
  };

  return (
    <Fragment>
      <Create {...props}>
        <SimpleForm sanitizeEmptyValues onSubmit={handleSubmit}>
          <TextInput source="name" />
          <TextInput source="description" />
          <TextInput source="contactEmail" label="Email" />
        </SimpleForm>
      </Create>
    </Fragment>
  );
};

export default BrandCreate;
