import React, { useContext, useEffect, useState } from 'react';
import { Grid, Paper } from '@mui/material';
import { MainTitle } from '../main-page/components/maintitle';
import Title from '../shared-components/Title';
import { useFormik } from 'formik';
import * as Yup from 'yup';
import TextField from '@mui/material/TextField';
import Checkbox from '@mui/material/Checkbox';
import FormControlLabel from '@mui/material/FormControlLabel';
import Button from '@mui/material/Button';
import { useNavigate } from 'react-router-dom';

import { ICreateStore, IStore } from '../../utilities/api/interfaces/IStoreModel';
import { useBackendApi, BackendAPI } from '../../utilities/api/backendapi';
import { ICreateCatalog } from '../../utilities/api/interfaces/ICatalogModel';


const validationSchema = Yup.object({
    name: Yup.string().required('Name is required'),
    location: Yup.string().required('Location is required'),
});

interface CreateStoreProps {
    mode: 'create' | 'update';
    id?: string | null;
}
const CreateStore = ({ mode = 'create', id }: CreateStoreProps): React.ReactElement => {
    const { setTitle } = useContext(MainTitle);
    const navigate = useNavigate();
    const [isEditMode, setIsEditMode] = useState(!!id);


    const { request: requestCreateStore } = useBackendApi<ICreateStore>(BackendAPI.store.create, 'POST');
    const { request: requestGetSingle,  response: responseGetSingle } = useBackendApi<ICreateStore>(BackendAPI.store.get, 'GET');
    const { request: requestUpdateStore } = useBackendApi<ICreateStore>(BackendAPI.store.update, 'PUT');

    const initialValues: IStore = {
        id: 0,
        name: '',
        location: '',
        active: true,
    };
    
    const handleSubmit = async (values: any) => {
        if (isEditMode) {
            requestUpdateStore(values, true, values.id.toString(), (response) => {
                if (response.valid) {
                    navigate(-1);
                }
            });
        } else {
            requestCreateStore(values, true, '', (response) => {
                if (response.valid) {
                    navigate(-1);
                }
            });
        }
    };


    const formik = useFormik({
        initialValues: initialValues,
        validationSchema,
        onSubmit: handleSubmit,
    });

    const handleReset = () => {
        formik.resetForm();
    };

    

    useEffect(() => {
        setTitle(isEditMode ? 'Edit Store' : 'Create Store');
        if (isEditMode && id) {
            setIsEditMode(true);
            requestGetSingle('', true, id, (response) => {
                if (response.valid) {
                    formik.setValues(response.content);
                }
            });
        }
    }, []);
    

    
    return (
        <React.Fragment>
            <Grid container spacing={3}>
                <Grid item lg={12}>
                    <Paper sx={{ p: 2, display: 'flex', flexDirection: 'column' }}>
                        <Title> {isEditMode ? 'Edit' : 'Create'} Store</Title>

                        <form onSubmit={formik.handleSubmit}>
                            {isEditMode && <input type="hidden" name="id" value={formik.values.id} />}
                            <TextField
                                fullWidth
                                id="name"
                                name="name"
                                label="Name"
                                value={formik.values.name}
                                onChange={formik.handleChange}
                                error={formik.touched.name && Boolean(formik.errors.name)}
                                helperText={formik.touched.name && formik.errors.name}
                                sx={{ mb: 2 }}
                            />
                            <TextField
                                fullWidth
                                id="location"
                                name="location"
                                label="Location"
                                value={formik.values.location}
                                onChange={formik.handleChange}
                                error={formik.touched.location && Boolean(formik.errors.location)}
                                helperText={formik.touched.location && formik.errors.location}
                                sx={{ mb: 2 }}
                            />
                            <FormControlLabel
                                control={
                                    <Checkbox
                                        checked={formik.values.active}
                                        onChange={formik.handleChange}
                                        name="active"
                                    />
                                }
                                label="Active"
                            />
                            <br></br>
                            <Button type="submit" variant="contained" sx={{ mr: 2, mt: 2 }}>
                                {isEditMode ? 'Edit' : 'Create'}
                            </Button>
                            <Button type="button" variant="outlined" onClick={() => navigate(-1)} sx={{ mt: 2 }}>
                                Back
                            </Button>
                        </form>
                    </Paper>
                </Grid>
            </Grid>
        </React.Fragment>
    );
};

export default CreateStore;
