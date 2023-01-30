import { useRecordContext } from "react-admin";

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
