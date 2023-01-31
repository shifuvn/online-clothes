import React, { Fragment } from "react";
import {
  Edit,
  SimpleForm,
  NumberInput,
  ImageInput,
  ImageField
} from "react-admin";
import SelectSizeTypeInput from "../../common/SelectSizeTypeInput";
import { toPascalCase } from "../../../helpers/conventionCase";
import { HttpApiProvider } from "../../../services/ApiWrapper";

const SkuEdit = () => {
  const postField = ["sku", "addOnPrice", "inStock", "size", "imageFile"];

  const handleSubmit = async (params) => {
    params.imageFile = params.imageFile?.rawFile;

    let postObj = {};
    postField.forEach((key, idx) => {
      postObj[toPascalCase(key)] = params[key];
    });

    await HttpApiProvider.updateForm("skus", postObj);
  };

  return (
    <Fragment>
      <Edit>
        <SimpleForm sanitizeEmptyValues onSubmit={handleSubmit}>
          <NumberInput source="addOnPrice" />
          <NumberInput source="inStock" />
          <SelectSizeTypeInput source="size" />
          <ImageInput source="imageFile">
            <ImageField source="src" />
          </ImageInput>
        </SimpleForm>
      </Edit>
    </Fragment>
  );
};

export default SkuEdit;
