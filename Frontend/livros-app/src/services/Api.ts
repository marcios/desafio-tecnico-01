import axios, { AxiosError } from 'axios';
import type { ApiResponseResult } from '../models/responses/ApiResponseResult';

const instance = axios.create({
    baseURL: 'https://localhost:7113/api/v1/',
});


function tratarResponseError<T>(error:any) : ApiResponseResult<T> | null {
 if (axios.isAxiosError(error) && error.response) {
            // TypeScript knows error is an AxiosError and has a response object
            return error.response.data as ApiResponseResult<T>;
        } else {
            // Handle generic errors
            console.error(error);
            return null;
        }
}

async function get<T>(endpoint: string) {
    try {
        const response = await instance.get<ApiResponseResult<T>>(endpoint)
        return response.data;
    } catch (error) {
        console.error(error);
        return null;
    }
}
async function put<T>(endpoint: string, data: T) {
    try {
        const response = await instance.put<ApiResponseResult<T>>(endpoint, data);
        return response.data;
    } catch (error) {
        return tratarResponseError(error);
    }
}

async function remove<T>(endpoint: string) {
    try {
        const response = await instance.delete<ApiResponseResult<T>>(endpoint);
        return response.data;
    } catch (error) {
        return tratarResponseError(error);
    }
}

async function post<T>(endpoint: string, data: T) {
    try {
        const response = await instance.post<ApiResponseResult<T>>(endpoint, data);
        return response.data;
    } catch (error) {
         return tratarResponseError(error);

    }
}


export default {
    get,
    put,
    post,
    remove
}