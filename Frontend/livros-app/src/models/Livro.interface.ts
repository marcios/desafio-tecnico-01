export interface Livro {
    id:number;
    nome:string;
    ativo: boolean;
    autorId:number
    autor?:string;
    generoId:number;
    genero?:string
}