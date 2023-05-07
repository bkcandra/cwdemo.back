
import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";
import CreateCatalog from "../admincatalog-page/catalogcreate";
import CatalogList from "../admincatalog-page/cataloglist";
import CreateStore from "../adminstore-page/storecreate";
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
        path: "/admin/store/create",
        element: <CreateStore />,
      },
      {
        path: "/admin/store/update",
        element: <CreateStore />,
      },
      {
        path: "/admin/catalog",
        element: <CatalogList />,
      },
      {
        path: "/admin/catalog/create",
        element: <CreateCatalog />,
      },
      {
        path: "/admin/catalog/update",
        element: <CreateCatalog />,
      }
    ],
  },
]);


export default mainrouter;