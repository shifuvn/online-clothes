import React, { Fragment } from "react";
import {
  BooleanInput,
  Edit,
  NumberInput,
  SimpleForm,
  TextInput
} from "react-admin";
import SelectTypeInput from "../../common/SelectTypeInput";

const ProductEdit = (props) => {
  return (
    <Fragment>
      <Edit>
        <SimpleForm sanitizeEmptyValues>
          <TextInput source="name" />
          <TextInput source="description" multiline />
          <NumberInput source="price" min={0} />
          <SelectTypeInput source="type" />
        </SimpleForm>
        {/*<BooleanInput source="isPublish" />*/}
      </Edit>
    </Fragment>
  );
};

export default ProductEdit;
