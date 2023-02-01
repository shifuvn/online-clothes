import React from "react";
import { SelectArrayInput, useRecordContext } from "react-admin";
import { HttpApiProvider } from "../../services/ApiWrapper";

const SelectCategoryInput = (props) => {
  const record = useRecordContext();
  const categoryIds = record?.category?.map((cate, idx) => cate.id) ?? [];

  const [choices, setChoices] = React.useState([]);

  React.useEffect(() => {
    const fetchAllCate = async () => {
      const result = await HttpApiProvider.get("categories/all");
      setChoices(result.data?.data);
    };

    fetchAllCate();
  }, []);

  return (
    <SelectArrayInput
      choices={choices}
      source={props.source}
      defaultValue={categoryIds}
    />
  );
};

export default SelectCategoryInput;
