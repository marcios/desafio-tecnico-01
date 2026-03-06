import { useEffect, useState } from "react";
import type { Genero } from "../../models/Genero.Interface";
import GeneroService from "../../services/GeneroService";
import { Link } from "react-router-dom";
import Notificacao from "../../utils/Notificacao";

export default function GenerosView() {


    const [generos, setGeneros] = useState<Genero[]>([]);


    async function obterGeneros() {
        const result = await GeneroService.obtertTodos();
        if (result != null)
            setGeneros(result);
    }

    useEffect(() => {
        obterGeneros();
    }, [])


    function handleDeletar(genero: Genero) {
        const msg = `Deseja realmente apagar o genero ${genero.nome}?`;

        const remover = async () => {
            const result = await GeneroService.remover(genero.id);
            if (result.sucesso) {
                Notificacao.sucesso(result.mensagem, obterGeneros)
            } else {
                Notificacao.erro(result.erros);
            }
        }
        Notificacao.confirmacao(msg, remover)
    }



    return <>

        <div className="card">
            <div className="card-header">
                <span>Lista de Generos</span>
                <Link to="cadastro/0" className="btn btn-sm btn-primary float-end ">Novo</Link>
            </div>
        </div>
        <div className="card-body">
            {generos && generos.length ?
                <table border={0} className="table table-bordered table-striped">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Nome</th>
                            <th>Status</th>
                            <th>Total de livros</th>
                            <th style={{ width: "10rem" }}>#</th>
                        </tr>
                    </thead>
                    <tbody>
                        {generos.map(item => <tr key={item.id}>
                            <td>
                                {item.id}
                            </td>
                            <td>
                                {item.nome}
                            </td>
                            <td>
                                {
                                    item.ativo ? <span className="badge bg-success">Ativo</span> 
                                : <span className="badge bg-danger">Inativo</span>
                                }
                            </td>
                            <td>
                                {item.totalLivros}
                            </td>
                            <td>
                                <button className="btn btn-sm btn-danger mx-1" type="button" onClick={() => handleDeletar(item)}>Deletar</button>
                                <Link className="btn btn-sm btn-warning" to={`cadastro/${item.id}`} >Editar</Link>
                            </td>

                        </tr>)}
                    </tbody>
                </table>
                : <div>

                    <span className="text-danger">
                        Sem resultado
                    </span>
                </div>
            }
        </div>





    </>
}