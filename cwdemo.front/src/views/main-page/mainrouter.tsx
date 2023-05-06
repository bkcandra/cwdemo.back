
import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";
import AdminCatalog from "../admincatalog-page/admincatalog";
import AdminStoreList from "../adminstore-page/storelist";
import Dashboard from "../dashboard-page/dashboard";
import ErrorPage from "../error-page/errorpage";
import Orders from "../order-page/orders";
import MainPage from "./mainpage";


const mainrouter = createBrowserRouter([
  {
    path: "/",
    element: <MainPage />,
    errorElement: <ErrorPage />,
    children: [
      {
        path: "/dashboard",
        element: <Dashboard />,
      },


      {
        path: "/orders",
        element: <Orders />,
      },
      {
        path: "/admin/store",
        element: <AdminStoreList />,
      },

      {
        path: "/admin/catalog",
        element: <AdminCatalog />,
      },



    ],
  },
]);


export default mainrouter;