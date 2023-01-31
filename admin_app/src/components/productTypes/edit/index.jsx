import React, { Fragment } from "react";
import { Edit, SimpleForm, TextInput } from "react-admin";
import { useNavigate } from "react-router-dom";
import { HttpApiProvider } from "../../../services/ApiWrapper";

const ProductTypeEdit = () => {
  const navigate = useNavigate();

  const handleSubmit = async (params) => {
    await HttpApiProvider.update("productTypes", params);
    navigate("/productTypes");
  };
  return (
    <Fragment>
      <Edit>
        <SimpleForm onSubmit={handleSubmit}>
          <TextInput source="name" />
        </SimpleForm>
      </Edit>
    </Fragment>
  );
};

export default ProductTypeEdit;
