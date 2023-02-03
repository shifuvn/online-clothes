import React, { Fragment } from "react";
import {
  Edit,
  SimpleForm,
  NumberInput,
  ImageInput,
  ImageField,
  BooleanInput,
  TextInput
} from "react-admin";
import SelectSizeTypeInput from "../../common/SelectSizeTypeInput";
import { toPascalCase } from "../../../helpers/conventionCase";
import { HttpApiProvider } from "../../../services/ApiWrapper";
import { useNavigate } from "react-router-dom";

const SkuEdit = () => {
  const navigate = useNavigate();
  const postField = [
    "sku",
    "displaySkuName",
    "addOnPrice",
    "inStock",
    "size",
    "imageFile"
  ];

  const handleSubmit = async (params) => {
    params.imageFile = params.imageFile?.rawFile;

    let postObj = {};
    postField.forEach((key, idx) => {
      postObj[toPascalCase(key)] = params[key];
    });

    await HttpApiProvider.updateForm("skus", postObj);
    navigate("/skus");
  };

  return (
    <Fragment>
      <Edit>
        <SimpleForm sanitizeEmptyValues onSubmit={handleSubmit}>
          <TextInput source="displaySkuName" />
          <NumberInput source="addOnPrice" />
          <NumberInput source="inStock" />
          <SelectSizeTypeInput source="size" />
          <BooleanInput source="isDeleted" />
          <ImageInput source="imageFile">
            <ImageField source="src" />
          </ImageInput>
        </SimpleForm>
      </Edit>
    </Fragment>
  );
};

export default SkuEdit;
