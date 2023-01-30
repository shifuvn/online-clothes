import React from "react";

const WrapperPages = React.lazy(() => import("../components/WrapperResource"));

export const WrapperWildcard = "*";

// routers
const adminRoute = [
  {
    path: WrapperWildcard,
    name: "Exam",
    element: <WrapperPages></WrapperPages>
  }
];

const routes = {
  adminRoute
};

export default routes;
