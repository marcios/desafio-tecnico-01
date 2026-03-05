import axios, { type AxiosResponse } from 'axios';
import type { Result } from '../models/responses/Result.interface';

const instance = axios.create({
    baseURL: 'https://localhost:7113/api/v1/',
});




async function get<T>(endpoint: string) {
    try {
        const response =  await instance.get<Result<T>>(endpoint)
        return response.data;
    } catch (error) {
        console.error(error);
        return null;
    }
}
async function put<T>(endpoint: string,  data:T) {
    try {
        const response = await instance.put<Result<T>>(endpoint, data);
        return response.data;
    } catch (error) {
        return null;

    }
}


export default {
    get,
    put
}