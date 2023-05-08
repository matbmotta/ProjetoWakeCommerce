
# Projeto WakeCommerce

Este projeto consiste em uma API desenvolvida em .NET 6 utilizando Entity Framework, para realizar o CRUD de produtos.




## Funcionalidades

- Criar um produto
- Atualizar um produto
- Listar os produtos
- Deletar um produto

## Instalação

Para executar a aplicação, é necessário ter o .NET 6 instalado em sua máquina. Além disso, é necessário clonar o repositório para a sua máquina local. Para clonar o repositório, utilize o seguinte comando:

```bash
  git clone https://github.com/matbmotta/ProjetoWakeCommerce.git
```    
## Executando a aplicação

Para executar a aplicação, abra o terminal na pasta raiz do projeto e execute o seguinte comando:

```bash
  dotnet run --project  WakeCommerce
```
Ou caso tenha o Visual Studio Instalado, abra a solution e rode o projeto.

## Utilizando o Entity Framework

 Neste projeto, utilizamos o code-first approach, ou seja, criamos as entidades em classes C# e o Entity Framework cria as tabelas no banco de dados automaticamente. Para criar o banco de dados, basta executar o seguinte comando no terminal:

 ```bash
  dotnet ef database update 
```

Lembrando que é necessário ter o SQL Server instalado na maquina, e também trocar a connection string no appSetting, realizando o apontamento para o seu banco de dados Local.


 ```bash
  "ConnectionStrings": {
    "DefaultConnection": "SUA CONNECTION STRING"
  },
```
## Rodando os testes

Abra o projeto que contém os testes no Visual Studio.

1. No menu superior, selecione "Teste" > "Executar" > "Todos os Testes" (ou use o atalho de teclado Ctrl + R, A).
2. Os testes serão executados e o resultado será exibido no "Gerenciador de Testes", que pode ser acessado pelo menu "Teste" > "Gerenciador de Testes".


## Licença

[MIT](https://choosealicense.com/licenses/mit/)

