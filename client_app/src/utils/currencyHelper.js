export const toVnd = (value) => {
  return (
    value !== undefined &&
    value.toLocaleString("it-IT", { style: "currency", currency: "VND" })
  );
};
