import React, { Fragment } from "react";
import {
  BooleanInput,
  Create,
  ImageField,
  ImageInput,
  NumberInput,
  SimpleForm,
  TextInput
} from "react-admin";
import { useNavigate } from "react-router-dom";
import { HttpApiProvider } from "../../../services/ApiWrapper";
import SelectBrandTypeInput from "../../common/SelectBrandTypeInput";
import SelectCategoryInput from "../../common/SelectCategoryInput";
import SelectSizeTypeInput from "../../common/SelectSizeTypeInput";
import SelectTypeInput from "../../common/SelectTypeInput";

const ProductCreate = (props) => {
  const navigate = useNavigate();

  const handleSubmit = async (params) => {
    params.ImageFile = params.ImageFile?.rawFile ?? undefined;
    await HttpApiProvider.create("products", params);
    navigate("/products");
  };

  return (
    <Fragment>
      <Create {...props}>
        <SimpleForm sanitizeEmptyValues onSubmit={handleSubmit}>
          <TextInput source="Name" resettable />
          <TextInput
            source="Description"
            multiline
            resettable
            style={{ width: "50%" }}
          />
          <TextInput source="Sku" />
          <TextInput source="DisplaySkuName" />
          <NumberInput source="SkuInStock" min={0} label="Number" />
          <NumberInput source="Price" />
          <NumberInput source="SkuAddOnPrice" min={0} label="Add on price" />
          <SelectCategoryInput source="CategoryIds" />
          <SelectTypeInput source="Type" />
          <SelectSizeTypeInput source="SkuSize" />
          <SelectBrandTypeInput source="BrandId" />
          <ImageInput source="ImageFile" label="Image">
            <ImageField source="src" />
          </ImageInput>
          <BooleanInput
            source="IsPublish"
            label="Publish"
            defaultValue={true}
          />
        </SimpleForm>
      </Create>
    </Fragment>
  );
};

export default ProductCreate;
