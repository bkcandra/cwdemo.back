import * as React from 'react';
import Link from '@mui/material/Link';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Title from '../shared-components/Title';
import { useContext, useEffect } from 'react';
import { MainTitle } from '../main-page/components/maintitle';
import { Grid, Paper, Typography } from '@mui/material';

// Generate Order Data
function createData(
    id: number,
    date: string,
    name: string,
    shipTo: string,
    paymentMethod: string,
    amount: number,
) {
    return { id, date, name, shipTo, paymentMethod, amount };
}



function preventDefault(event: React.MouseEvent) {
    event.preventDefault();
}

export default function Dashboard() {
    const { setTitle } = useContext(MainTitle);

    useEffect(() => {
        setTitle('Dashboard');
    }, [setTitle]);

    return (
        <React.Fragment>

            <Grid container spacing={1}>
                {/* Chart */}
                <Grid item xs={12} md={8} lg={12}>
                    <Paper sx={{ p: 2, display: 'flex', flexDirection: 'column' }}>
                        <Title>CW Demo React Site</Title>
                        <Typography sx={{ mb: 2 }}>
                            This is a simple React app that interacts with a backend API to manage stores, catalog and orders. The app has 3 main pages: Order, Store CRUD, and Catalog CRUD. <br/>
                            Data is stored on memory and always revert back when application is started.
                        </Typography>

                        <Typography variant="h6" sx={{ mb: 1 }}>Requirements:</Typography>
                        <Typography sx={{ mb: 2 }}>
                            Before using this app, make sure that the backend API is up and running on localhost:5000.<br />
                            1. open cwdemo.back.sln with visual studio<br />
                            2. run <strong>cwdemo.api</strong> project at port 5000<br />
                            3. if run successfully you will see swagger page at <strong>https://localhost:5000/swagger/index.html</strong><br />
                            
                        </Typography>

                        <Typography variant="h6" sx={{ mb: 1 }}>Order page:</Typography>
                        <Typography sx={{ mb: 2 }}>
                            The order page allows the user to place an order. The user can select a store and then add items to the cart. Once the user is done adding items, they can place the order. The order details will be sent to the backend API and a confirmation message will be displayed to the user.
                        </Typography>

                        <Typography variant="h6" sx={{ mb: 1 }}>Store CRUD page:</Typography>
                        <Typography sx={{ mb: 2 }}>
                            The store CRUD page allows the user to manage stores. The user can add a new store, edit an existing store, or delete a store.
                        </Typography>

                        <Typography variant="h6" sx={{ mb: 1 }}>Catalog CRUD page:</Typography>
                        <Typography sx={{ mb: 2 }}>
                            The catalog CRUD page allows the user to manage items in the catalog. The user can add a new item, edit an existing item, or delete an item.
                        </Typography>

                        <Typography variant="h6" sx={{ mb: 1 }}>How to use:</Typography>
                        <Typography sx={{ mb: 2 }}>
                            1. Clone the repository to your local machine.<br />
                            2. Make sure that the backend API is up and running on localhost:5000.<br />
                            3. Navigate to the project directory (cwdemo.back\cwdemo.front) in your terminal, i have merged both backend and frontend into 1 repository for demo purpose<br />
                            4. Run <strong>yarn install</strong> to install the project dependencies.<br />
                            5. Run <strong>yarn start</strong> to start the app.<br />
                            6. The app should now be running in your browser at localhost:3000.<br />
                            7. Navigate to the desired page (Order, Store CRUD, or Catalog CRUD) to start using the app.
                        </Typography>

                        <Typography sx={{ fontStyle: 'italic' }}>
                            Note: The app uses Material-UI for its UI components. The app uses Axios for making API requests.
                        </Typography>
                    </Paper>

                </Grid>

            </Grid>

        </React.Fragment>
    );
}
