import type { Genero } from "../models/Genero.Interface";
import Api from "./Api"

class GeneroService {
    endpoint: string = "generos";
    constructor() {

    }

    async obtertTodos(): Promise<Array<Genero>> {
        const result = await Api.get<Array<Genero>>(this.endpoint)
        return result.data.data;
    }
}

export default new GeneroService();