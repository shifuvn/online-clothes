import React, { Fragment } from "react";
import {
  BooleanInput,
  Edit,
  NumberInput,
  SimpleForm,
  SimpleShowLayout,
  TextInput
} from "react-admin";
import { useNavigate } from "react-router-dom";
import { HttpApiProvider } from "../../../services/ApiWrapper";
import SelectBrandTypeInput from "../../common/SelectBrandTypeInput";
import SelectCategoryInput from "../../common/SelectCategoryInput";
import SelectTypeInput from "../../common/SelectTypeInput";
import SingleLineImageList from "../_fields/SingleImageList";

const ProductEdit = (props) => {
  const navigate = useNavigate();

  const handleSubmit = async (params) => {
    const putPayload = {
      id: params.id,
      name: params.name,
      description: params.description,
      price: params.price,
      typeId: params.type?.id,
      brandId: params.brand?.id,
      categoryIds: params.categories,
      isPublish: params.isPublish
    };

    await HttpApiProvider.update("products", putPayload);
    navigate("/products");
  };

  return (
    <Fragment>
      <Edit>
        <SimpleForm
          title="Edit info"
          sanitizeEmptyValues
          onSubmit={handleSubmit}
        >
          <TextInput source="name" />
          <TextInput source="description" multiline />
          <NumberInput source="price" min={0} />
          <SelectTypeInput source="type.id" />
          <SelectBrandTypeInput source="brand.id" />
          <SelectCategoryInput source="categories" />
          <BooleanInput source="isPublish" />
        </SimpleForm>
        <SimpleShowLayout>
          <SingleLineImageList />
        </SimpleShowLayout>
      </Edit>
    </Fragment>
  );
};

export default ProductEdit;
