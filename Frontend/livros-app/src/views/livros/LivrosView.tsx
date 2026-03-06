import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import Notificacao from "../../utils/Notificacao";
import type { Livro } from "../../models/Livro.interface";
import LivroService from "../../services/LivroService";

export default function LivrosViews() {


    const [livros, setLivros] = useState<Livro[]>([]);


    async function obterLivros() {
        const result = await LivroService.obtertTodos();
        if (result != null)
            setLivros(result);
        else {
            setLivros([])
        }
    }

    useEffect(() => {
        obterLivros();
    }, [])


    function handleDeletar(livro: Livro) {
        const msg = `Deseja realmente apagar o livro ${livro.nome}?`;

        const remover = async () => {
            const result = await LivroService.remover(livro.id);
            if (result.sucesso) {
                Notificacao.sucesso(result.mensagem, obterLivros)
            } else {
                Notificacao.erro(result.erros);
            }
        }
        Notificacao.confirmacao(msg, remover)
    }



    return <>

        <div className="card">
            <div className="card-header">
                <span>Lista de Livros</span>
                <Link to="cadastro/0" className="btn btn-sm btn-primary float-end ">Novo</Link>
            </div>
        </div>
        <div className="card-body">
            {livros && livros.length ?
                <table border={0} className="table table-bordered table-striped">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Título</th>
                            <th>Autor</th>
                            <th>Genero</th>
                            <th>Status</th>
                            <th style={{ width: "10rem" }}>#</th>
                        </tr>
                    </thead>
                    <tbody>
                        {livros.map(item => <tr key={item.id}>
                            <td>
                                {item.id}
                            </td>
                            <td>
                                {item.nome}
                            </td>
                            <td>
                                {item.autor}
                            </td>
                            <td>
                                {item.genero}
                            </td>
                            <td>
                                {
                                    item.ativo ? <span className="badge bg-success">Ativo</span>
                                        : <span className="badge bg-danger">Inativo</span>
                                }
                            </td>
                          
                            <td>
                                <button className="btn btn-sm btn-danger mx-1" type="button" onClick={() => handleDeletar(item)}>Deletar</button>
                                <Link className="btn btn-sm btn-warning" to={`cadastro/${item.id}`} >Editar</Link>
                            </td>

                        </tr>)}
                    </tbody>
                </table>
                : <div className="alert alert-danger">
                    Sem resultado
                </div>
            }
        </div>





    </>
}