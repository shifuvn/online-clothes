import React, { Fragment } from "react";
import { HttpApiProvider } from "../../../services/ApiWrapper";
import { useNavigate } from "react-router-dom";

import { Create, SimpleForm, TextInput } from "react-admin";

const ProductTypeCreate = (props) => {
  const navigate = useNavigate();

  const handleSubmit = async (params) => {
    await HttpApiProvider.create("productTypes", params);
    navigate("/productTypes");
  };

  return (
    <Fragment>
      <Create {...props}>
        <SimpleForm sanitizeEmptyValues onSubmit={handleSubmit}>
          <TextInput source="name" />
        </SimpleForm>
      </Create>
    </Fragment>
  );
};

export default ProductTypeCreate;
