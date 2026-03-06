export interface ApiResponseResult<T>{
    notificacoes:Array<string>;
    mensagem:string;
    data:T
}