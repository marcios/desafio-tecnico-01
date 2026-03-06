
Rodar  api
Entrar na pasta do projeto, acessar o pasta raiz do projeto: (onde se encontram todas a pastas da api
LivrosWebApi, LivrosWebApi.Application, etc..)

**0 - Baixar o projeto do repositorio**
https://github.com/marcios/desafio-tecnico-01

os 2 projetos estão no mesmo repositorio

1. Frontend/livros-app
2. WebApi



**Baixar o projeto e seguir os passos abaixo**

API

1 - Executar o combando para "buildar" acessar a pasta raiz do projeto e no local onde se encontra o arquivo 'LivrosWebApi.sln'
executar o comando:

```dotnet build```

2 - Executar a migration


```dotnet ef database update --project .\LivrosWebApi.Data --startup-project .\LivrosWebApi```


3 - Rodar o projeto

 ```dotnet run --project .\LivrosWebApi\LivrosWebApi.csproj -lp https```
 
 vai gerar o endereço:  https://localhost:7113

4 - Caso queira acessar o swagger
https://localhost:7113/swagger


**Configurar o frontend**


1 - Criar ou verificar o arquivo .env se esta com o endereço da api
caso não esteja adicionar a chave:
VITE_API=https://localhost:7113/api/v1/

2 - Instalar os pacotes
acessar a pasta livro-app e rodar o comando:
```npm init```


3 - apos instalação rodar o comando
```npm run dev```
com isso deve iniciar o servidor local
