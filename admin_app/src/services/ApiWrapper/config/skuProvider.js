export const handleSkuGetListResult = (result, isFilterById) => {
  if (isFilterById) {
    const resultData = result?.data?.data;
    const data =
      resultData === undefined ? [] : [{ id: resultData.sku, ...resultData }];
    return { data, total: 1 };
  }

  const data = result?.data?.data?.items?.map((item, idx) => ({
    id: item.sku,
    ...item
  }));
  const total = result?.data?.data?.totalItems;
  return { data, total };
};
