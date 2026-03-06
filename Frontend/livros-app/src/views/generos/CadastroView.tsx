import { useEffect, useState } from 'react';
import { Link, useNavigate, useParams } from 'react-router-dom';
import type { Genero } from '../../models/Genero.Interface';
import GeneroService from '../../services/GeneroService';
import Notificacao from '../../utils/Notificacao';


export default function GeneroCadastroView() {

    const navigate = useNavigate();
    const { generoId } = useParams();
    const [genero, setGenero] = useState<Genero>();

    useEffect(() => {
        if (generoId && generoId.length)
            obterGenero();
    }, []);


    const obterGenero = async () => {
        const id = parseInt(generoId!);
        if (id <= 0) {
            setGenero({
                id,
                nome: "",
                ativo: true
            })
            return;
        }

        const result = await GeneroService.obterPorId(id)
        if (result != null)
            setGenero(result);
    }

    const handleSaveGenero = async () => {
        const erros = [];
        if (!genero?.nome.replace(/\s/gm, '').length)
            erros.push('Informe o nome do gênero');

        if (erros.length) {
            alert('tem erro')
            return;
        }

        const result = await GeneroService.Salvar(genero!);

        if (result.sucesso) {
            Notificacao.sucesso(result.mensagem, () => {
                navigate("/generos", { replace: true });
            })

        }else {
            Notificacao.erro(result.erros);
        }

    }

    function handleChangeNome(nome: string) {
        const prev = { ...genero } as Genero;
        prev.nome = nome;
        setGenero(prev);
    }
    function handleChangeStatus(ativo: boolean) {
        const prev = { ...genero } as Genero;
        prev.ativo = ativo;
        setGenero(prev);
    }

    return <>
        <div className='card'>
            <div className="card-header">
                Cadastro de gênero  {!genero?.id ? <span className="badge bg-success">Novo cadastro</span> : null}
            </div>
            <div className='card-body'>
                <form>
                    <input type='hidden' value={generoId} />
                    <div className="mb-3">
                        <label htmlFor="nome" className="form-label">Nome</label>
                        <input
                            type="text"
                            onChange={(e) => handleChangeNome(e.target.value)}
                            value={genero?.nome}
                            className="form-control" id="nome" aria-describedby="Nome do gênero" />

                    </div>

                    <div className="mb-3 form-check">
                        <input onChange={e => handleChangeStatus(e.target.checked)}
                            checked={genero?.ativo}
                            type="checkbox" className="form-check-input" id="exampleCheck1" />
                        <label className="form-check-label" htmlFor="exampleCheck1">
                            {genero?.ativo ? <span className='text-success'>Ativo</span> : <span className='text-danger'>Inativo</span>}
                        </label>
                    </div>
                    <Link className='btn btn-outline-info mr-1' to="/generos">Voltar</Link>
                    &nbsp;
                    <button type="button" onClick={handleSaveGenero} className="btn btn-success">Salvar</button>
                </form>
            </div>
        </div>

    </>
}