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
import { Grid, Paper } from '@mui/material';

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
                    <Paper
                        sx={{
                            p: 2,
                            display: 'flex',
                            flexDirection: 'column',
                            height: 240,
                        }}
                    >
                        <Title>CW Demo React Site</Title>
                    </Paper>
                </Grid>

            </Grid>

        </React.Fragment>
    );
}
