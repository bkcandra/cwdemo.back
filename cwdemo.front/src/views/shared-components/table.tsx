import * as React from 'react';
import { DataGrid, GridColDef, GridApi, GridKeyValue } from '@mui/x-data-grid';
import Button from '@mui/material/Button';
import { IAction, ICatalog } from '../../utilities/api/interfaces/ICatalogModel';
import { BackendAPI, useBackendApi } from '../../utilities/api/backendapi';
import { Link } from 'react-router-dom';
import { Box, Tooltip } from '@mui/material';

interface TableProps {
    headers: string[];
    data: any[];
    config: { [key: string]: (data: any) => React.ReactNode };
}

const DataTable = ({ headers, data, config }: TableProps) => {
    const rows = data.map((row) => ({
        id: row.id,
        ...headers.reduce((acc, header) => {
            return {
                ...acc,
                [header]: config[header](row),
            };
        }, {}),
    }));


    const columns: GridColDef[] = headers.map((header) => ({

        field: header,
        headerName: header,
        flex: header === 'Actions' ? 1 : undefined,
    }));



    return <DataGrid rows={rows} columns={columns} />;
};

export default DataTable;
