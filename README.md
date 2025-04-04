# CRUD Pessoa e Empresa - Backend API

Este projeto é uma **API RESTful desenvolvida com .NET Core**, utilizando conceitos avançados como **CQRS**, **DDD** e **Event Sourcing** para gerenciar o estado da aplicação. A API permite o cadastro de clientes de maneira robusta, escalável e extensível, garantindo integridade e rastreabilidade dos dados.

---

## 🚀 Funcionalidades

- **Cadastro de Clientes**:
  - Registro de informações como Nome/Razão Social, CPF/CNPJ, data de nascimento, e contatos.
  - Validação de regras de negócio, como idade mínima e unicidade de CPF/CNPJ e e-mail.
  - Persistência de dados e eventos para reconstruir o estado do domínio.

- **CQRS (Command Query Responsibility Segregation)**:
  - Separação clara entre comandos (escrita) e consultas (leitura), garantindo uma arquitetura escalável e organizada.

- **Event Sourcing**:
  - Utiliza eventos para rastrear mudanças no estado da aplicação, possibilitando histórico completo e auditabilidade.

- **Princípios de DDD (Domain-Driven Design)**:
  - Modelagem baseada no domínio do problema, com foco em entidades e agregados.

---

## 🛠️ Tecnologias Utilizadas

- **.NET Core (v8.0)**:
  - Framework base para a API, oferecendo alto desempenho e suporte moderno.
- **EF Core**:
  - Mapeamento objeto-relacional para gerenciamento do banco de dados.
- **FluentValidation**:
  - Validação de comandos com regras personalizadas.
- **Mediator Pattern com MediatR**:
  - Gerenciamento de comandos e handlers de forma desacoplada.
- **Event Sourcing**:
  - Persistência de eventos no banco de dados para reconstrução do estado.
- **SQL Server**:
  - Banco de dados relacional para armazenamento persistente.

---

## 📂 Estrutura do Projeto

```plaintext
src/
├── Controllers/
│   ├── ClientesController.cs            # Controlador principal para gestão de clientes
├── Commands/
│   ├── CreateClienteCommand.cs          # Comando para criação de clientes
│   ├── Handlers/
│   │   ├── CreateClienteCommandHandler.cs # Handler do comando de criação
├── Domain/
│   ├── Cliente.cs                       # Entidade Cliente
│   ├── Events/
│   │   ├── Event.cs                     # Classe base para eventos de domínio
│   │   ├── EventEntity.cs               # Representação persistente de eventos
├── Infrastructure/
│   ├── PFPJDBContext.cs                 # Contexto do banco de dados (EF Core)
│   ├── Repositories/
│   │   ├── EventStore.cs                # Repositório para eventos


##✨ Vantagens da Arquitetura
Auditabilidade:

Graças ao Event Sourcing, é possível rastrear todas as mudanças no estado do domínio.

Escalabilidade:

A separação de comandos e consultas (CQRS) permite que cada parte seja escalada e otimizada de forma independente.

Organização e Manutenção:

Uso de DDD para manter uma estrutura clara e organizada, com foco no domínio do negócio.

Alta Extensibilidade:

Componentes desacoplados e baseados em padrões modernos, permitindo fácil evolução do sistema.

##🔧 Instalação e Configuração
Clone o repositório:

bash
git clone <url-do-repositorio>
Configure o banco de dados:

Certifique-se de que o SQL Server está configurado e rodando.

Atualize a string de conexão no arquivo appsettings.json:

json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=PessoaFisicaJuridicaDB;User Id=sa;Password=JF#123#teste;TrustServerCertificate=True"
}
Restaure as dependências:

bash
dotnet restore
Execute as migrações (caso necessário):

bash
dotnet ef database update
Inicie o servidor:

bash
dotnet run
Acesse a API:

plaintext
http://localhost:5000
##🎨 Exemplos de Endpoints
Criar Cliente:

POST /api/clientes

Corpo da requisição (JSON):

json
{
  "nomeRazaoSocial": "Empresa Exemplo",
  "cpfCnpj": "12345678901",
  "dataNascimento": "2000-01-01",
  "telefone": "11999999999",
  "email": "email@exemplo.com",
  "cep": "12345678",
  "endereco": "Rua Exemplo",
  "numero": "123",
  "bairro": "Centro",
  "cidade": "São Paulo",
  "estado": "SP",
  "pessoaFisica": true,
  "inscricaoEstadual": "isento"
}
Respostas possíveis:

200 OK: Cliente criado com sucesso.

422 Unprocessable Entity: Erros de validação nos dados enviados.

##📜 Scripts Disponíveis
dotnet build: Compila o projeto.

dotnet test: Executa os testes unitários.

dotnet run: Inicia o servidor da API.

##📝 Notas Adicionais
Este backend foi construído com foco em boas práticas de arquitetura e padrões modernos, como CQRS e DDD.

O projeto é totalmente extensível, permitindo a inclusão de novos domínios e funcionalidades.
