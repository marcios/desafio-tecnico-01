import { useEffect, useState } from 'react';
import { Link, useNavigate, useParams } from 'react-router-dom';
import AutorService from '../../services/AutorService';
import Notificacao from '../../utils/Notificacao';
import type { Autor } from '../../models/Autor.interface';


export default function GeneroCadastroView() {

    const navigate = useNavigate();
    const { autorId } = useParams();
    const [autor, setAutor] = useState<Autor>();

    useEffect(() => {
        if (autorId && autorId.length)
            obterAutor();
    }, []);


    const obterAutor = async () => {
        const id = parseInt(autorId!);
        if (id <= 0) {
            setAutor({
                id,
                nome: "",
                ativo: true
            })
            return;
        }

        const result = await AutorService.obterPorId(id)
        if (result != null)
            setAutor(result);
    }

    const handleSalvar = async () => {
        const erros = [];
        if (!autor?.nome.replace(/\s/gm, '').length)
            erros.push('Informe o nome do autor');

        if (erros.length) {
            Notificacao.erro(erros);
            return;
        }

        const result = await AutorService.Salvar(autor!);

        if (result.sucesso) {
            Notificacao.sucesso(result.mensagem, () => {
                navigate("/autores", { replace: true });
            })

        }else {
            Notificacao.erro(result.erros);
        }

    }

    function handleChangeNome(nome: string) {
        const prev = { ...autor } as Autor;
        prev.nome = nome;
        setAutor(prev);
    }
    function handleChangeStatus(ativo: boolean) {
        const prev = { ...autor } as Autor;
        prev.ativo = ativo;
        setAutor(prev);
    }

    return <>
        <div className='card'>
            <div className="card-header">
                Cadastro de autor  {!autor?.id ? <span className="badge bg-success">Novo cadastro</span> : null}
            </div>
            <div className='card-body'>
                <form>
                    <input type='hidden' value={autorId} />
                    <div className="mb-3">
                        <label htmlFor="nome" className="form-label">Nome</label>
                        <input
                            type="text"
                            onChange={(e) => handleChangeNome(e.target.value)}
                            value={autor?.nome}
                            className="form-control" id="nome" aria-describedby="Nome do gênero" />

                    </div>

                    <div className="mb-3 form-check">
                        <input onChange={e => handleChangeStatus(e.target.checked)}
                            checked={autor?.ativo}
                            type="checkbox" className="form-check-input" id="exampleCheck1" />
                        <label className="form-check-label" htmlFor="exampleCheck1">
                            {autor?.ativo ? <span className='text-success'>Ativo</span> : <span className='text-danger'>Inativo</span>}
                        </label>
                    </div>
                    <Link className='btn btn-outline-info mr-1' to="/generos">Voltar</Link>
                    &nbsp;
                    <button type="button" onClick={handleSalvar} className="btn btn-success">Salvar</button>
                </form>
            </div>
        </div>

    </>
}