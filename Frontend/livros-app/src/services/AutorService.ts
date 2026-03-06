import type { Autor } from "../models/Autor.interface";
import type { Result } from "../models/Result";
import Api from "./Api"

const resultTemplate: Result = {
    erros: [],
    mensagem: "",
    sucesso: false
}

class AutorService {


    endpoint: string = "autores";
    constructor() {

    }
    async obterPorId(id: number): Promise<Genero | null> {
        const genero = await Api.get<Genero>(`${this.endpoint}/${id}`);

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

    async remover(id: number): Promise<Result> {
        let result = { ...resultTemplate }
        const response = await Api.remove<Genero>(`${this.endpoint}/${id}`);
        if (response) {
            result.sucesso = !response.notificacoes.length;
            result.mensagem = "Cadastro removido com sucesso";
            result.erros = response.notificacoes;
        }

        return result;
    }

    async Salvar(autor: Autor): Promise<Result> {
        let result = { ...resultTemplate }
        if (autor.id > 0) {

            const response = await Api.put<Autor>(`${this.endpoint}/${autor.id}`, autor);
            if (response) {
                result.sucesso = !response.notificacoes.length;
                result.mensagem = "Cadastro atualizado com sucesso";
                result.erros = response.notificacoes;
            }

        } else {
            const response = await Api.post<Autor>(this.endpoint, autor);
            if (response) {
                result.sucesso = !response.notificacoes.length;
                result.mensagem = "Cadastro criado com sucesso!";
                result.erros = response.notificacoes;
            }

        }

        return result;

    }

}




export default new AutorService();