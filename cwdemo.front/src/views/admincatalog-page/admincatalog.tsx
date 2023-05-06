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


function preventDefault(event: React.MouseEvent) {
  event.preventDefault();
}

export default function AdminCatalog() {
  const { setTitle } = useContext(MainTitle);

  useEffect(() => {
    setTitle('Administration - Catalog');
  }, [setTitle]);

  return (
    <React.Fragment>
      <Grid container spacing={3}>
        {/* Chart */}
        <Grid item lg={12}>
          <Paper
            sx={{
              p: 2,
              display: 'flex',
              flexDirection: 'column',
            }}
          >
            <Title>Catalog List</Title>
           
            
          </Paper>
        </Grid>
      </Grid>
    </React.Fragment>
  );
}
