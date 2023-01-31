import React from "react";
import { SelectInput } from "react-admin";
import { HttpApiProvider } from "../../services/ApiWrapper";

const SelectTypeInput = (props) => {
  const [choices, setChoices] = React.useState([]);

  React.useEffect(() => {
    const fetchAllType = async () => {
      const result = await HttpApiProvider.get("productTypes/all");
      setChoices(result.data?.data);
    };

    fetchAllType();
  }, []);

  return <SelectInput source={props.source} choices={choices} />;
};

export default SelectTypeInput;
