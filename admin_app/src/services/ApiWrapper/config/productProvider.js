export const processProductQueryParams = (params) => {
  if (!params) {
    return undefined;
  }

  const paging = params.pagination;
  const sort = params.sort;
  let query = `pageIndex=${paging.page}&pageSize=${paging.perPage}&orderBy=${sort.order}&sortBy=${sort.field}`;
  return query;
};
