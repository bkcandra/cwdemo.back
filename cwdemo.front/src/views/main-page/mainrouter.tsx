
import {
  createBrowserRouter,
  RouterProvider,
  useLocation,
  useParams,
} from "react-router-dom";
import CreateCatalog from "../admincatalog-page/catalogcreate";
import CatalogList from "../admincatalog-page/cataloglist";
import CreateStore from "../adminstore-page/storecreate";
import AdminStoreList from "../adminstore-page/storelist";
import Dashboard from "../dashboard-page/dashboard";
import ErrorPage from "../error-page/errorpage";
import OrderPage from "../order-page/orders";
import MainPage from "./mainpage";


const UpdateStoreWrapper = () => {
  const { id } = useParams<{ id: string }>();
  return <CreateStore mode="update" id={id} />;
};

const UpdateCatalogWrapper = () => {
  const { id } = useParams<{ id: string }>();
  return <CreateCatalog mode="update" id={id} />;
};



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
        element: <OrderPage />,
      },
      {
        path: "/admin/store",
        element: <AdminStoreList />,
      },
      {
        path: "/admin/store/create",
        element: <CreateStore mode="create" />,
      },
      {
        path: "/admin/store/update/:id",
        element: <UpdateStoreWrapper />,
      },
      {
        path: "/admin/catalog",
        element: <CatalogList />,
      },
      {
        path: "/admin/catalog/create",
        element: <CreateCatalog mode="create" />,
      },
      {
        path: "/admin/catalog/update/:id",
        element: <UpdateCatalogWrapper />,
      }
    ],
  },
]);


export default mainrouter;