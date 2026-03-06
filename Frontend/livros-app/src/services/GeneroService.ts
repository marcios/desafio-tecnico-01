import type { Genero } from "../models/Genero.Interface";
import type { Result } from "../models/Result";
import Api from "./Api"

const resultTemplate: Result = {
            erros: [],
            mensagem: "",
            sucesso: false
        }
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

    async remover(generoId:number) : Promise<Result> {
        let result = {...resultTemplate}
        const response = await Api.remove<Genero>(`${this.endpoint}/${generoId}`);
            if (response) {
                result.sucesso = !response.notificacoes.length;
                result.mensagem ="Cadastro removido com sucesso";
                result.erros = response.notificacoes;
            }

            return result;
    }

    async Salvar(genero: Genero): Promise<Result> {
        let result = {...resultTemplate}
        if (genero.id > 0) {

            const response = await Api.put<Genero>(`${this.endpoint}/${genero.id}`, genero);
            if (response) {
                result.sucesso = !response.notificacoes.length;
                result.mensagem ="Cadastro atualizado com sucesso";
                result.erros = response.notificacoes;
            }

        } else {
            genero.ativo=true;
            const response = await Api.post<Genero>(this.endpoint, genero);
             if (response) {
                result.sucesso = !response.notificacoes.length;
                result.mensagem ="Cadastro criado com sucesso!";
                result.erros = response.notificacoes;
            }

        }

        return result;

    }

}




export default new GeneroService();