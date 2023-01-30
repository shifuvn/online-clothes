import React from "react";
import { SelectInput } from "react-admin";

const SelectTypeInput = (props) => {
  const choices = [
    { id: 0, name: "Unknown" },
    { id: 1, name: "Hat" },
    { id: 2, name: "Shirt" },
    { id: 3, name: "Pant" }
  ];

  return <SelectInput source={props.source} choices={choices} />;
};

export default SelectTypeInput;
