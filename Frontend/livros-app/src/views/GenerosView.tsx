import { useEffect, useState } from "react";
import type { Genero } from "../models/Genero.Interface";
import GeneroService from "../services/GeneroService";

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




    return <>
        <h1>Genero</h1>

        {generos && generos.length ?
            <table border={0} className="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Nome</th>
                        <th>Total de livros</th>
                        <th>#</th>
                    </tr>
                </thead>
                <tbody>
                    {generos.map(genero=> <tr key={genero.id}>
                            <td>
                                {genero.id}
                            </td>
                            <td>
                                {genero.nome}
                            </td>
                             <td>
                                {genero.totalLivros}
                            </td>
                            <td>
                                <a>link</a>
                            </td>

                    </tr>)}
                </tbody>
            </table>
            : <div>
                Sem resultado
            </div>
        }


    </>
}