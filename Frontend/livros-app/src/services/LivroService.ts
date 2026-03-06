
import type { Livro } from "../models/Livro.interface";
import type { Result } from "../models/Result";
import Api from "./Api"

const resultTemplate: Result = {
    erros: [],
    mensagem: "",
    sucesso: false
}

class LivroService {


    endpoint: string = "livros";
    constructor() {

    }
    async obterPorId(id: number): Promise<Livro | null> {
        const genero = await Api.get<Livro>(`${this.endpoint}/${id}`);

        if (genero?.data != null)
            return genero?.data;

        return null;
    }
    async obtertTodos(): Promise<Array<Livro>> {
        const result = await Api.get<Array<Livro>>(this.endpoint)
        if (result?.data)
            return result?.data;

        return [];
    }

    async remover(id: number): Promise<Result> {
        let result = { ...resultTemplate }
        const response = await Api.remove<Livro>(`${this.endpoint}/${id}`);
        if (response) {
            result.sucesso = !response.notificacoes.length;
            result.mensagem = "Cadastro removido com sucesso";
            result.erros = response.notificacoes;
        }

        return result;
    }

    async Salvar(livro: Livro): Promise<Result> {
        let result = { ...resultTemplate }
        if (livro.id > 0) {

            const response = await Api.put<Livro>(`${this.endpoint}/${livro.id}`, livro);
            if (response) {
                result.sucesso = !response.notificacoes.length;
                result.mensagem = "Cadastro atualizado com sucesso";
                result.erros = response.notificacoes;
            }

        } else {
            const response = await Api.post<Livro>(this.endpoint, livro);
            if (response) {
                result.sucesso = !response.notificacoes.length;
                result.mensagem = "Cadastro criado com sucesso!";
                result.erros = response.notificacoes;
            }

        }

        return result;

    }

}




export default new LivroService();