# cwdemo.back

CW Demo 

React Site for front end & .NET 7 for Backend

This is a simple React app that interacts with a backend API to manage stores, catalog and orders. The app has 3 main pages: Order, Store CRUD, and Catalog CRUD.
Data is stored on memory and always revert back when application is started.

Requirements:
Before using this app, make sure that the backend API is up and running on localhost:5000.
1. open cwdemo.back.sln with visual studio
2. run cwdemo.api project at port 5000
3. if run successfully you will see swagger page at https://localhost:5000/swagger/index.html

Order page:
The order page allows the user to place an order. The user can select a store and then add items to the cart. Once the user is done adding items, they can place the order. The order details will be sent to the backend API and a confirmation message will be displayed to the user.

Store CRUD page:
The store CRUD page allows the user to manage stores. The user can add a new store, edit an existing store, or delete a store.

Catalog CRUD page:
The catalog CRUD page allows the user to manage items in the catalog. The user can add a new item, edit an existing item, or delete an item.


How to use:
1. Clone the repository to your local machine.
2. Make sure that the backend API is up and running on localhost:5000.
3. Navigate to the project directory (cwdemo.back\cwdemo.front) in your terminal, i have merged both backend and frontend into 1 repository for demo purpose
4. Run yarn install to install the project dependencies.
5. Run yarn start to start the app.
6. The app should now be running in your browser at localhost:3000.
7. Navigate to the desired page (Order, Store CRUD, or Catalog CRUD) to start using the app.

Note: The app uses Material-UI for its UI components. The app uses Axios for making API requests.
