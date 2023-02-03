import { Checkbox, FormControlLabel } from "@mui/material";
import { Button, SimpleShowLayout, useRecordContext } from "react-admin";
import { useNavigate } from "react-router-dom";
import React from "react";

import styles from "./field.module.scss";

export const ThumbnailField = ({ source }) => {
  const record = useRecordContext();
  return (
    <object data="" type="image/png">
      <img
        className={styles.thumbnail}
        src={record && record[source]}
        alt="No Img"
      />
    </object>
  );
};

export const ProductPanelField = ({ source }) => {
  const record = useRecordContext();
  const skuList = record.skus;

  return (
    <SimpleShowLayout>
      <div>
        {record &&
          skuList &&
          skuList.map((sku, idx) => {
            return <ProductSkuField key={idx} sku={sku} />;
          })}
      </div>
    </SimpleShowLayout>
  );
};

const ProductSkuField = (props) => {
  const skuId = props.sku.id;
  const navigate = useNavigate();

  const handleClickDetail = (e) => {
    const path = `/skus/${skuId}/show`;
    console.log("path", path);
    navigate(path);
  };
  return (
    <div className={styles.blockField}>
      <span style={{ minWidth: "100px" }}>{skuId}</span>
      <FormControlLabel
        style={{ marginLeft: "30px" }}
        label="Deleted"
        control={<Checkbox checked={props.sku.isDeleted} disabled />}
      />

      <div className={styles.productPanelButton}>
        <Button onClick={handleClickDetail} name="Detail" label="Detail" />
      </div>
    </div>
  );
};
