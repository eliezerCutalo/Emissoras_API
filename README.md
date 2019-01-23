# Emissora_API

### Requisitos
- SDK .NET Core 2.2 - https://dotnet.microsoft.com/download/dotnet-core/2.2


### Configurações iniciais e execução da aplicação

- Crie um banco de dados e rode o arquivo `` script.sql ``;
- Abra a solução, entre em ```Core_MVC.API/appsettings.json```;
- Execute a aplicação.

### Documentação

- ``<localhost>/swagger`` ou ``<localhost>/swagger/v1/swagger.json``.

### Criando um CRUD

- Crie a classe de entidade em ``Core_MVC.Domain.Models``, a mesma será os campos do banco de dados;
- Implemente o contexto da entidade criada no arquivo ``Core_MVC.Data/Context.cs`` para indicar qual tabela do banco a mesma se refere;
- Crie a interface do repositório em ``Core_MVC.Domain.Repository`` para fazer o CRUD;
- Implemente a interface do repositório em ``Core_MVC.Infra.Repository``;
- Mapeie a Interface com a classe do repositório no arquivo ``Core_MVC.IoC.Mapping/Map.cs``;
- Caso exista alguma consulta com uma resposta com join ou visualização customizada, crie uma classe em ``Core_MVC.Domain/CustomerResponses.cs``;
- Crie uma classe estática que valide os campos de sua entidade em ``Core_MVC.Domain.Validators``;
- Finalmente, crie o seu controller em ``Core_MVC.Data.Controllers`` para controlar as respostas e os status da aplicação.
