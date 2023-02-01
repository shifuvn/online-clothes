import React, { Fragment } from "react";
import {
  ArrayField,
  BooleanField,
  Datagrid,
  DateField,
  NumberField,
  RichTextField,
  Show,
  SimpleShowLayout,
  TextField
} from "react-admin";
import { OrderStepperField } from "../_field";

const OrderShow = () => {
  return (
    <Fragment>
      <Show>
        <SimpleShowLayout>
          <OrderStepperField />
          <TextField source="id" label="Order id" />
          <TextField source="email" label="Customer email" />
          <TextField source="address" label="Address" />
          <RichTextField source="note" />
          <NumberField source="totalPaid" />
          <BooleanField source="isPaid" />
          <DateField source="createdAt" />
          <ArrayField source="items">
            <Datagrid isRowSelectable={(record) => false} onSelect={() => {}}>
              <TextField source="productSkuId" label="Sku" />
              <NumberField source="quantity" />
              <NumberField source="price" />
            </Datagrid>
          </ArrayField>
        </SimpleShowLayout>
      </Show>
    </Fragment>
  );
};

export default OrderShow;
