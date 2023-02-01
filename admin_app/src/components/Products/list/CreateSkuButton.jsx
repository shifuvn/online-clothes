import React from "react";
import { Button, useRecordContext } from "react-admin";
import { useNavigate } from "react-router-dom";

const CreateSkuButton = ({ source }) => {
  const record = useRecordContext();
  const navigate = useNavigate();

  const handleCreateSkuEvent = (e) => {
    const path = `/products/${record.id}/create-sku`;
    navigate(path);
  };

  return <Button label="Create SKU" onClick={handleCreateSkuEvent} />;
};

export default CreateSkuButton;
