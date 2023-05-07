import * as React from 'react';
import { useState, useContext, useEffect, useCallback } from 'react';
import Button from '@mui/material/Button';
import Title from '../shared-components/Title';
import { MainTitle } from '../main-page/components/maintitle';
import { Grid, Paper } from '@mui/material';
import { ICatalog, ICreateCatalog } from '../../utilities/api/interfaces/ICatalogModel';
import { BackendAPI, useBackendApi } from '../../utilities/api/backendapi';
import DataTable from '../shared-components/table';
import { CatalogType } from '../../utilities/Enums/catalogTypeEnum';
import CreateCatalog from './catalogcreate';
import { Link, RouterProvider } from 'react-router-dom';

function preventDefault(event: React.MouseEvent) {
  event.preventDefault();
}


const CatalogList = (): React.ReactElement => {
  const { setTitle } = useContext(MainTitle);


  const { request, response: responseGetCatalog } = useBackendApi<ICatalog[]>(
    BackendAPI.catalog.get
  );

  useEffect(() => {
    setTitle('Catalog List');
    request()
  }, [setTitle, request]);


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
            <Grid container justifyContent="space-between" alignItems="left">
              <Button variant="contained" component={Link} to="/admin/catalog/create">
                Create Catalog
              </Button>
            </Grid>

            <DataTable
              headers={['ID', 'Name', 'Description', 'Type', 'Store Name']}
              data={responseGetCatalog?.content || []}
              config={{
                'ID': (catalog: ICatalog) => catalog.id,
                'Name': (catalog: ICatalog) => catalog.name,
                'Description': (catalog: ICatalog) => catalog.description,
                'Type': (catalog: ICatalog) => CatalogType[catalog.type],
                'Store Name': (catalog: ICatalog) => catalog.storeName
              }}
            />
          </Paper>
        </Grid>
      </Grid>

    </React.Fragment>
  );
};

export default CatalogList;
