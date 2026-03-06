import { useEffect, useState } from 'react';
import { Link, useNavigate, useParams } from 'react-router-dom';
import AutorService from '../../services/AutorService';
import Notificacao from '../../utils/Notificacao';
import type { Autor } from '../../models/Autor.interface';
import type { Livro } from '../../models/Livro.interface';
import LivroService from '../../services/LivroService';
import type { Genero } from '../../models/Genero.Interface';
import GeneroService from '../../services/GeneroService';


export default function LivroCadastroView() {

    const navigate = useNavigate();
    const { livroId } = useParams();
    const [livro, setLivro] = useState<Livro>();
    const [nomeLivro, setNomeLivro] = useState('');
    const [autores, setAutores] = useState<Autor[]>([])
    const [generos, setGeneros] = useState<Genero[]>([])
    const [selectedAutor, setSelectedAutor] =useState(0);


    useEffect(() => {
        if (livroId && livroId.length) {
            obterAutores();
            obterGeneros();
            obterLivro();

        }
    }, []);

    const obterGeneros = async () => {
        const result = await GeneroService.obtertTodos();
        if (result)
            setGeneros(result);

    }

    const obterAutores = async () => {
        const result = await AutorService.obtertTodos();
        if (result)
            setAutores(result);

    }
    const obterLivro = async () => {
        const id = parseInt(livroId!);
        if (id <= 0) {
            setLivro({
                id,
                nome: "",
                ativo: true,
                autorId: 0,
                generoId: 0
            })
            return;
        }

        const result = await LivroService.obterPorId(id)
        
        if (result != null)
        {
            setLivro(result);
            setSelectedAutor(result.autorId)
        }
    }

    const handleSalvar = async () => {
        const erros = [];        
        if (!livro?.nome.replace(/\s/gm, '').length)
            erros.push('Informe o nome do autor');

        if(livro?.autorId!=null && livro?.autorId <=0) {
            erros.push('Informe o autor');
        }

        if(livro?.generoId!=null && livro?.generoId <=0) {
            erros.push('Informe o gênero');
        }

        if (erros.length) {
            Notificacao.erro(erros);
            return;
        }

        const result = await LivroService.Salvar(livro!);

        if (result.sucesso) {
            Notificacao.sucesso(result.mensagem, () => {
                navigate("/livros", { replace: true });
            })

        } else {
            Notificacao.erro(result.erros);
        }

    }

    function handleChangeNome(nome: string) {
        const prev = { ...livro } as Livro;
        prev.nome = nome;
        setLivro(prev);
        setNomeLivro(nome);
    }
    function handleChangeStatus(ativo: boolean) {
        const prev = { ...livro } as Livro;
        prev.ativo = ativo;
        setLivro(prev);
    }

    function handleChangeAutor(autorId: string) {
        
        if (autorId !=null && autorId.length) {
            const id = parseInt(autorId!);
            setSelectedAutor(id);
            const prev = {...livro} as Livro;
            prev.autorId = id;
            setLivro(prev);
        }

    }

      function handleChangeGenero(generoId: string) {        
        
        if (generoId!=null && generoId.length) {
            const id = parseInt(generoId!);
            const prev = {...livro} as Livro;
            prev.generoId = id;
            setLivro(prev);
        }

    }

    return <>
        <div className='card'>
            <div className="card-header">
                Cadastro de livro  {!livro?.id ? <span className="badge bg-success">Novo cadastro</span> : null}
            </div>
            <div className='card-body'>
                <form>
                    <input type='hidden' value={livroId} />
                    <div className="mb-3">
                        <label htmlFor="nome" className="form-label">Nome</label>
                        <input
                            type="text"
                            onChange={(e) => handleChangeNome(e.target.value)}
                            value={livro?.nome}
                            className="form-control" id="nome" aria-describedby="Nome do livro" />

                    </div>

                    <div className="mb-3">
                        <label htmlFor="autor" className="form-label">Autor</label>
                        <select className="form-select"
                            onChange={e => handleChangeAutor(e.target.value)}
                            value={livro?.autorId}
                            aria-label="Selecionar o autor">
                            <option value={0}>Selecione autor</option>
                            {autores.map(item => <option 
                                selected={item.id == livro?.autorId}
                            key={item.id} value={item.id}>{item.nome}</option>)}
                        </select>

                    </div>

                    <div className="mb-3">
                        <label htmlFor="autor" className="form-label">Gênero</label>
                        <select className="form-select"
                            value={livro?.generoId}
                            onChange={e=>handleChangeGenero(e.target.value)}
                        aria-label="Selecionar o gênero">
                            <option value={0}>Selecione gênero</option>
                            {generos.map(item => <option 
                                 selected={item.id == livro?.generoId}
                            key={item.id} value={item.id}>{item.nome}</option>)}
                        </select>

                    </div>

                    <div className="mb-3 form-check">
                        <input onChange={e => handleChangeStatus(e.target.checked)}
                            checked={livro?.ativo}
                            type="checkbox" className="form-check-input" id="chkStatus" />
                        <label className="form-check-label" htmlFor="chkStatus">
                            {livro?.ativo ? <span className='text-success'>Ativo</span> : <span className='text-danger'>Inativo</span>}
                        </label>
                    </div>
                    <Link className='btn btn-outline-info mr-1' to="/livros">Voltar</Link>
                    &nbsp;
                    <button type="button" onClick={handleSalvar} className="btn btn-success">Salvar</button>
                </form>
            </div>
        </div>

    </>
}