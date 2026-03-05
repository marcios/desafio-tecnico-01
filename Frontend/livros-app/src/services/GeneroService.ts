import type { Genero } from "../models/Genero.Interface";
import Api from "./Api"

class GeneroService {


    endpoint: string = "generos";
    constructor() {

    }
    async obterPorId(generoId: number): Promise<Genero | null> {
        const genero = await Api.get<Genero>(`${this.endpoint}/${generoId}`);

        if (genero?.data != null)
            return genero?.data;

        return null;
    }
    async obtertTodos(): Promise<Array<Genero>> {
        const result = await Api.get<Array<Genero>>(this.endpoint)
        if (result?.data)
            return result?.data;

        return [];
    }

    async Salvar(genero: Genero): Promise<Genero | null> {

        if (genero.id > 0) {
            
            const response = await Api.put<Genero>(`${this.endpoint}/${genero.id}`, genero);
            if (response != null)
                return response.data;
        }

        return null;

    }
}

export default new GeneroService();