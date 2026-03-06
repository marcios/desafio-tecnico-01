import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import Notificacao from "../../utils/Notificacao";
import type { Livro } from "../../models/Livro.interface";
import LivroService from "../../services/LivroService";
import type { Autor } from "../../models/Autor.interface";
import AutorService from "../../services/AutorService";
import GeneroService from "../../services/GeneroService";

export default function LivrosViews() {


    const [livros, setLivros] = useState<Livro[]>([]);
    const [autores, setAutores] = useState<Autor[]>([]);
    const [generos, setGeneros] = useState<Autor[]>([]);

    


    async function  obterAutores(){
        if(livros && !livros.length){
            const result = await AutorService.obtertTodos();
            if(result)
                setAutores(result);
        }
    }

     async function  obterGeneros(){
        if(livros && !livros.length){
            const result = await GeneroService.obtertTodos();
            if(result)
                setGeneros(result);
        }
    }


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
        obterAutores()
        obterGeneros();
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
                : <div>

                    <div className="alert alert-danger">Sem resultado</div>
                    {!generos.length ? <div className="alert alert-danger">Cadastre um novo gênero</div> : null}
                    {!autores.length ? <div className="alert alert-danger">Cadastre um novo autor</div> : null}
                    
                </div>
            }
        </div>





    </>
}