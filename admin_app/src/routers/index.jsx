import React from "react";
import routes from "./routes";
import { Route, Routes } from "react-router-dom";

const loading = (
  <div>
    <h3>Loading ...</h3>
  </div>
);

const AppRouters = () => {
  return (
    <React.Suspense fallback={loading}>
      <Routes>
        {routes.adminRoute.map((route, idx) => {
          return (
            route.element && (
              <Route
                index
                key={idx}
                path={route.path}
                name={route.name}
                element={route.element}
              />
            )
          );
        })}
      </Routes>
    </React.Suspense>
  );
};

export default AppRouters;
