import { useEffect, useState } from 'react';
import { ServiceResponse } from './interfaces/IBackendResponse';




export const BackendAPI = {
    store: {
        get: 'https://localhost:5000/api/Store',
        create: '/api/stores',
        update: '/api/stores/{id}',
        delete: '/api/stores/{id}'
    },
    product: {
        get: '/api/products',
        create: '/api/products',
        update: '/api/products/{id}',
        delete: '/api/products/{id}'
    }
};

export function useBackendApi<T>(url: string, method: string = 'GET', payload?: any): { request: boolean, response: ServiceResponse<T> } {
    const [request, setRequest] = useState<boolean>(false);
    const [response, setResponse] = useState<ServiceResponse<T>>(new ServiceResponse());

    useEffect(() => {
        setRequest(true);
        const options: RequestInit = {
            method: method,
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json'
            }
        };
        if (payload) {
            options.body = JSON.stringify(payload);
        }
        fetch(url, options)
            .then(res => res.json())
            .then((data: ServiceResponse<T>) => setResponse(data))
            .catch((error: Error) => {
                setResponse(new ServiceResponse<T>(false, [error.message], undefined));

            })
            .finally(() => setRequest(false));
    }, [url, method, payload]);

    return { request, response };
}
