import React, { Fragment } from "react";
import { SelectInput } from "react-admin";

const choices = [
  { id: 0, name: "No size" },
  { id: 1, name: "S" },
  { id: 2, name: "M" },
  { id: 3, name: "L" },
  { id: 4, name: "XL" },
  { id: 5, name: "XXL" }
];

const SelectSizeTypeInput = (props) => {
  return (
    <Fragment>
      <SelectInput source={props.source} choices={choices} />
    </Fragment>
  );
};

export default SelectSizeTypeInput;
