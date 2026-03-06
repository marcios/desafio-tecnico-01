import { useEffect, useState } from "react";
import AutorService from "../../services/AutorService";
import { Link } from "react-router-dom";
import Notificacao from "../../utils/Notificacao";
import type { Autor } from "../../models/Autor.interface";

export default function AutoresViews() {


    const [autores, setAutores] = useState<Autor[]>([]);


    async function obterAutores() {
        const result = await AutorService.obtertTodos();
        if (result != null)
            setAutores(result);
        else {
            setAutores([])
        }
    }

    useEffect(() => {
        obterAutores();
    }, [])


    function handleDeletar(autor:Autor) {
        const msg = `Deseja realmente apagar o autor ${autor.nome}?`;

        const remover = async ()=> {
            const result = await AutorService.remover(autor.id);
            if(result.sucesso) {
                Notificacao.sucesso(result.mensagem, obterAutores)
            }else {
                Notificacao.erro(result.erros);
            }
        }
        Notificacao.confirmacao(msg,remover)
    }



    return <>

        <div className="card">
            <div className="card-header">
                <span>Lista de Autores</span>
                <Link to="cadastro/0" className="btn btn-sm btn-primary float-end ">Novo</Link>
            </div>
        </div>
        <div className="card-body">
            {autores && autores.length ?
                <table border={0} className="table table-bordered table-striped">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Nome</th>
                            <th>Total de livros</th>
                            <th style={{width:"10rem"}}>#</th>
                        </tr>
                    </thead>
                    <tbody>
                        {autores.map(item => <tr key={item.id}>
                            <td>
                                {item.id}
                            </td>
                            <td>
                                {item.nome}
                            </td>
                            <td>
                                {item.totalLivros}
                            </td>
                            <td>
                                <button className="btn btn-sm btn-danger mx-1" type="button" onClick={()=>handleDeletar(item)}>Deletar</button>
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