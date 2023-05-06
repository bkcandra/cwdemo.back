import * as React from 'react';
import Link from '@mui/material/Link';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Title from '../shared-components/Title';
import { useContext, useEffect, useCallback } from 'react';
import { MainTitle } from '../main-page/components/maintitle';
import { Grid, Paper } from '@mui/material';
import { IStore } from '../../utilities/api/interfaces/IStoreModel'
import { BackendAPI, useBackendApi } from '../../utilities/api/backendapi';

function preventDefault(event: React.MouseEvent) {
  event.preventDefault();
}

const AdminStoreList = (): React.ReactElement => {
  const { setTitle } = useContext(MainTitle);

  useEffect(() => {
    setTitle('Administration - Store');
  }, [setTitle]);

  const {
    request: requestGetStore,
    response: responseGetStore,
  } = useBackendApi<IStore[]>(BackendAPI.store.get);


  const renderTableRows = useCallback(() => {
    console.log(`called`);
    if (!responseGetStore || !responseGetStore.valid) {
      return null;
    }

    if (responseGetStore && responseGetStore.content) {
      return responseGetStore.content.map((store: IStore) => (
        <TableRow key={store.id}>
          <TableCell>{store.id}</TableCell>
          <TableCell>{store.name}</TableCell>
          <TableCell>{store.location}</TableCell>
        </TableRow>
      ));
    }
  }, [requestGetStore]);

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
            <Title>Store List</Title>
            <Table size="medium">
              <TableHead>
                <TableRow>
                  <TableCell>ID</TableCell>
                  <TableCell>Name</TableCell>
                  <TableCell>Description</TableCell>
                </TableRow>
              </TableHead>
              <TableBody>{renderTableRows()}</TableBody>
            </Table>
          </Paper>
        </Grid>
      </Grid>
    </React.Fragment>
  );
};

export default AdminStoreList;
