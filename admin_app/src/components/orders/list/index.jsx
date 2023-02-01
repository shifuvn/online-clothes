import React, { Fragment } from "react";
import {
  ChipField,
  Datagrid,
  DateField,
  List,
  NumberField,
  TextField
} from "react-admin";

const OrderList = () => {
  return (
    <Fragment>
      <List>
        <Datagrid rowClick="show" isRowSelectable={(record) => false}>
          <TextField source="id" />
          <TextField source="email" />
          <ChipField source="state" />
          <NumberField source="totalPaid" />
          <DateField source="createdAt" />
        </Datagrid>
      </List>
    </Fragment>
  );
};

export default OrderList;
