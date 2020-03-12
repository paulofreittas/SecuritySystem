# SecuritySystem-backend
 Projeto de implementação do caso de uso 'Manter Sistemas' (Backend).

### Tecnologias utilizadas?

* .NET Core Web Api
* Entity Framework (ORM)
* Fluent Validation (Validação de dados)
* SQL Server (Banco de dados)

### O que é necessário para executar?

* .NET Core 3.1 SDK
* Banco de dados SQL Server

### Como configurar o banco de dados?

 O projeto foi realizado usando o banco de dados relacional SQL Server. 
 Você precisa configurar a connection string dentro do arquivo 'appsettings.json'.
 Para gerar o banco de dados caso você esteja usando o Visual Studio execute: ``` Update-Database ```
 Caso esteja usando o CLI do .net core: ``` dotnet ef database update ```
 
### Como executar?

 Caso esteja o CLI do .net core, execute ``` dotnet run ``` na raiz do projeto.

