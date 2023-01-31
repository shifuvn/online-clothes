import React from "react";
import { SelectInput } from "react-admin";
import { HttpApiProvider } from "../../services/ApiWrapper";

const SelectBrandTypeInput = (props) => {
  const [choices, setChoices] = React.useState([]);

  React.useEffect(() => {
    const fetchAllBrands = async () => {
      var result = await HttpApiProvider.get("brands/all");
      setChoices(result.data?.data);
    };

    fetchAllBrands();
  }, []);

  return <SelectInput source={props.source} choices={choices} />;
};

export default SelectBrandTypeInput;
