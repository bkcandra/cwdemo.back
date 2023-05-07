import qs from 'qs';
import { useCallback, useEffect, useState } from 'react';
import { ServiceResponse } from './interfaces/IBackendResponse';




export const BackendAPI = {
  store: {
    get: 'https://localhost:5000/api/Store',
    create: 'https://localhost:5000/api/store',
    update: 'https://localhost:5000/api/store',
    delete: 'https://localhost:5000/api/store'
  },
  catalog: {
    get: 'https://localhost:5000/api/catalog',
    create: 'https://localhost:5000/api/catalog',
    update: 'https://localhost:5000/api/catalog',
    delete: 'https://localhost:5000/api/catalog'
  }
};



export function useBackendApi<T>(
  endpoint: string,
  method: string = 'GET',
  payload?: any | undefined
): {
  request: (
    currentPayload?: any | undefined,
    alert?: boolean,
    slug?: string,
    callback?: (success: any) => any
  ) => void; response: ServiceResponse<T>
} {
  const [response, setResponse] = useState<ServiceResponse<T>>(
    new ServiceResponse()
  );




  const request = useCallback(
    (currentPayload?: any, alert?: boolean, slug?: string | undefined, callback?: (success: any) => any) => {
      setResponse(new ServiceResponse<T>(true));
      let fetchOptions: RequestInit = {
        method,
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json',
        },
      };

      if (currentPayload) {
        console.log(`current payload ${currentPayload}`)
        fetchOptions.body = JSON.stringify(currentPayload);
      }

      let newEndpoint = endpoint;
      if (slug) {
        console.log(slug)
        newEndpoint += `/${slug}`;
        
      }


      fetch(newEndpoint, fetchOptions)
        .then(async (response) => {
          if (response.ok) {
            const data: ServiceResponse<T> = await response.json();
            setResponse(new ServiceResponse<T>(true, data.message, data.content));
            if (callback) {
              callback(data);
            }
            if (alert) {
              // show success notification
            }
          } else {
            const error = await response.json();
            setResponse(new ServiceResponse<T>(false, error));
            if (alert) {
              // show error notification
            }
          }
        })
        .catch((error) => {
          setResponse(new ServiceResponse<T>(false, error));
          if (alert) {
            // show error notification
          }
        });
    },
    [endpoint, method]
  );

  useEffect(() => {
    request();
  }, []);

  return { request, response };
}

