export const processSkyQueryParams = (params) => {
  if (!params || params == undefined) {
    return undefined;
  }

  let query;
  const paging = params.pagination;
  if (paging) {
    query += `pageIndex=${paging.page}&pageSize=${paging.perPage}`;
  }

  const sort = params.sort;
  if (sort) {
    query += `&orderBy=${sort.order}& sortBy=${sort.field}`;
  }

  return query;
};
