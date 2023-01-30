import { Button, SimpleShowLayout, useRecordContext } from "react-admin";
import { useNavigate } from "react-router-dom";

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
  const skuId = props.sku;
  const navigate = useNavigate();

  const handleClickDetail = (e) => {
    const path = `/skus/${skuId}/show`;
    console.log("path", path);
    navigate(path);
  };
  return (
    <div className={styles.blockField}>
      {skuId}
      <div className={styles.productPanelButton}>
        <Button onClick={handleClickDetail} name="Detail" label="Detail" />
      </div>
    </div>
  );
};
