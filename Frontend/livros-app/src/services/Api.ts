import axios, { type AxiosResponse } from 'axios';
import type { Result } from '../models/responses/Result.interface';

const instance = axios.create({
 baseURL: 'https://localhost:7113/api/v1/',
});




function get<T>(endpoint:string) {
    return instance.get<Result<T>>(endpoint)
}


export default {
    get
}