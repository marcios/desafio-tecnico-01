import { useState } from "react"
import { type Genero } from "../models/Genero.Interface"


export default function Livros(){

    const [generos,setGeneros] = useState<Genero[]>([]);

    console.log('generors', generos);

    
    return <h1>Livros</h1>
}