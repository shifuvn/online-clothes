import React, { Fragment } from "react";
import {
  ImageField,
  ImageInput,
  NumberInput,
  required,
  SaveButton,
  SimpleForm,
  SimpleShowLayout,
  TextField,
  TextInput,
  Toolbar
} from "react-admin";
import { useNavigate, useParams } from "react-router-dom";
import { HttpApiProvider } from "../../../services/ApiWrapper";
import SelectSizeTypeInput from "../../common/SelectSizeTypeInput";

const CreateSkuComponent = () => {
  const [product, setProduct] = React.useState();
  const navigate = useNavigate();
  const params = useParams();

  const productId = params.id;

  React.useEffect(() => {
    const fetchProduct = async () => {
      const result = await HttpApiProvider.get(`products/${productId}`);
      setProduct(result.data?.data);
    };

    fetchProduct();
  }, [productId]);

  const handleSubmit = async (params) => {
    let payload = {
      ProductId: productId,
      Sku: params.Sku,
      AddOnPrice: params.AddOnPrice,
      InStock: params.InStock,
      Size: params.Size,
      ImageFile: params.ImageFile?.rawFile
    };
    var result = await HttpApiProvider.createForm("skus", payload);
    if (result.data.status == 201) {
      const path = `/skus/${payload.Sku}/show`;
      navigate(path);
    }
  };

  return (
    <Fragment>
      <SimpleShowLayout record={product}>
        <TextField source="id" label="Product Id" />
        <TextField source="name" label="Product Name" />
      </SimpleShowLayout>
      <SimpleForm
        sanitizeEmptyValues
        record={product}
        onSubmit={handleSubmit}
        toolbar={<SaveButton />}
      >
        <TextInput source="Sku" validate={required()} />
        <NumberInput source="AddOnPrice" min={0} />
        <NumberInput source="InStock" min={0} />
        <SelectSizeTypeInput source="Size" />
        <ImageInput source="ImageFile">
          <ImageField source="src" />
        </ImageInput>
      </SimpleForm>
    </Fragment>
  );
};

export default CreateSkuComponent;
