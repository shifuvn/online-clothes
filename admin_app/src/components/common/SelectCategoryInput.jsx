import React from "react";
import { SelectArrayInput } from "react-admin";
import { HttpApiProvider } from "../../services/ApiWrapper";

const SelectCategoryInput = (props) => {
  const [choices, setChoices] = React.useState([]);

  React.useEffect(() => {
    const fetchAllCate = async () => {
      const result = await HttpApiProvider.get("categories/all");
      setChoices(result.data?.data);
    };

    fetchAllCate();
  }, []);

  return <SelectArrayInput choices={choices} source={props.source} />;
};

export default SelectCategoryInput;
