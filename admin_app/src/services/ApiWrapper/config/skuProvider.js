export const handleSkuGetListResult = (result) => {
  const data = result?.data?.data?.items?.map((item, idx) => ({
    id: item.sku,
    ...item
  }));
  const total = result?.data?.data?.totalItems;
  return { data, total };
};
