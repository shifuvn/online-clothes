import React, { Fragment } from "react";
import { Create, SimpleForm, TextInput } from "react-admin";
import { useNavigate } from "react-router-dom";
import { HttpApiProvider } from "../../../services/ApiWrapper";

const CategoryCreate = (props) => {
  const navigate = useNavigate();

  const handleSubmit = async (params) => {
    await HttpApiProvider.create("categories", params);
    navigate("/categories");
  };

  return (
    <Fragment>
      <Create {...props}>
        <SimpleForm onSubmit={handleSubmit}>
          <TextInput source="name" />
          <TextInput source="description" multiline />
        </SimpleForm>
      </Create>
    </Fragment>
  );
};

export default CategoryCreate;
