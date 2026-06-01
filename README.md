# 🚀 Universo Vagas — Sistema Fullstack de Gestão de Oportunidades

> Projeto desenvolvido como demonstração técnica focado em boas práticas de engenharia de software, arquitetura limpa, código desacoplado e cultura inclusiva.

---

## 🌍 Cultura & Propósito

Alinhado com a crença de um futuro com mais equidade e inovação social, este sistema foi desenhado respeitando a pluralidade e a diversidade. A modelagem de dados e a interface foram projetadas com foco em **ações afirmativas** e no respeito às individualidades, garantindo soluções mais humanas e conectadas com a realidade do mundo.

---

## 🛠️ Tecnologias e Ecossistema Técnico

O projeto foi construído utilizando as versões mais recentes e performáticas do ecossistema Microsoft, equilibrando qualidade de código e velocidade de entrega:

- **Backend:** C# 10 e .NET 10 (ASP.NET Core Web API)
- **Frontend:** Blazor WebAssembly (Atuação Fullstack nativa em C#) com CSS Isolado por Componente (Scoped CSS)
- **Persistência:** Entity Framework Core com Banco de Dados em Memória (In-Memory Database)
- **Validação de Negócio:** FluentValidation + DataAnnotations (Validação Dupla e Sincronizada)
- **Documentação:** OpenAPI e Swashbuckle (Swagger)
- **Testes Automatizados:** xUnit e Moq (Foco em regras de negócio e validação temporal)

---

## 📐 Princípios de Arquitetura e Engenharia (SOLID & Clean Code)

Para garantir escalabilidade, manutenibilidade e autonomia técnica, o projeto aplica rigorosamente os seguintes padrões:

- **S.O.L.I.D.:**
  - _Single Responsibility (SRP):_ Validadores, repositórios e controladores possuem responsabilidades estritamente isoladas. O Frontend separa a lógica de visualização da lógica de mutação de dados.
  - _Dependency Inversion (DIP):_ O controlador da API depende exclusivamente de abstrações (`IVagaRepository`), desacoplando a infraestrutura do motor de entrega.
- **Repository Pattern:** Isolamento completo das queries LINQ-to-SQL da camada de exposição HTTP.
- **Clean Code & W3C Standards:** Métodos expressivos, nomenclatura semântica, localização global para português (`pt-BR`) em tempo de execução e conformidade rígida às propriedades CSS padronizadas.
- **Separação por Camadas (Separation of Concerns):**
  - `Vaga.Domain`: Entidades ricas, propriedades temporais restritas e regras de validação.
  - `Vaga.Infra`: Persistência de dados e contratos de repositório.
  - `Vaga.API`: Pontos de extremidade RESTful protegidos contra concorrência de rastreamento do EF Core.
  - `Vaga.Blazor`: Interface SPA modular reativa através de subcomponentes declarativos.

---

## 📁 Arquitetura Estrutural do Backend

📁 src
└── 📁 Backend
    │
    ├── 📁 Vaga.Domain                      # Regras de Negócio Puras (Sem dependências externas)
    │   ├── 📁 Entities
    │   │   └── 📄 Vaga.cs                  # Entidade Rica (Construtor restrito + métodos de mutação)
    │   ├── 📁 Enums
    │   │   └── 📄 TipoVaga.cs              # Enum fortemente tipado (Remoto, Hibrido, Presencial)
    │   └── 📁 Validators
    │       └── 📄 VagaValidator.cs         # Validação de Negócio (FluentValidation com IsInEnum)
    │
    ├── 📁 Vaga.Infra                       # Infraestrutura de Acesso a Dados e Contratos
    │   ├── 📁 Context
    │   │   └── 📄 AppDbContext.cs          # DbContext (EF Core configurado em memória no Program.cs)
    │   ├── 📁 Interfaces
    │   │   └── 📄 IVagaRepository.cs       # Contratos/Interfaces das operações de dados
    │   └── 📁 Repositories
    │       └── 📄 VagaRepository.cs        # Implementação concreta do Repositório (Lê/Escreve no DbContext)
    │
    └── 📁 Vaga.API                         # Camada de Exposição HTTP (Endpoints RESTful)
        ├── 📁 Controllers
        │   └── 📄 VagasController.cs       # Controlador REST (Trata requisições através de DTOs)
        ├── 📁 DTOs
        │   └── 📄 VagaRequestDto.cs        # C# Record mutável/imutável para transporte leve do JSON
        ├── 📁 Properties
        │   └── 📄 launchSettings.json      # Perfis de inicialização (Configuração das portas HTTP 5194)
        ├── 📄 Program.cs                   # Pipeline HTTP, CORS, IoC/DI e JsonStringEnumConverter
        └── 📄 Vaga.API.csproj              # Arquivo de gerenciamento do projeto da API


## 📁 Arquitetura Estrutural do Frontend (Blazor SPA)

O projeto adota uma divisão modular profissional para evitar acoplamento visual e técnico na raiz do projeto:

```text
📁 src
└── 📁 Frontend
    └── 📁 Vaga.Blazor                      # Interface SPA Modular Reativa
        │
        ├── 📁 Components                   # Subcomponentes reaproveitáveis e modais
        │   ├── 📄 EditarVagaModal.razor    # Lógica e marcação estrutural do pop-up
        │   └── 📄 EditarVagaModal.razor.css # CSS isolado e alinhado com o pop-up
        │
        ├── 📁 Layout                       # Componentes globais de estrutura visual
        │   ├── 📄 MainLayout.razor         # Layout de casca principal da aplicação
        │   ├── 📄 MainLayout.razor.css     # CSS isolado da casca (Flexbox / Desktop vs Mobile)
        │   ├── 📄 NavMenu.razor            # Menu lateral com links das rotas
        │   └── 📄 NavMenu.razor.css        # CSS isolado do menu (Efeitos hover e link active)
        │
        ├── 📁 Models                       # Estruturas de dados exclusivas para a UI
        │   ├── 📄 VagaFormModel.cs         # Modelo mutável para capturar dados dos inputs
        │   └── 📄 VagaResponseDto.cs       # Record para desserializar as respostas da API
        │
        ├── 📁 Pages                        # Páginas SPA roteáveis do sistema
        │   ├── 📄 Home.razor               # Página inicial do sistema
        │   ├── 📄 Home.razor.css           # CSS isolado da página inicial (Cards de boas-vindas)
        │   ├── 📄 NotFound.razor           # Página de erro 404 customizada
        │   ├── 📄 NotFound.razor.css       # CSS isolado da página de erro 404
        │   ├── 📄 Sobre.razor              # Página institucional/informações do sistema
        │   ├── 📄 Sobre.razor.css          # CSS isolado da página Sobre
        │   │
        │   └── 📁 Vagas                    # Subpasta para gerenciamento de vagas
        │       ├── 📄 CadastrarVaga.razor  # Formulário de criação na rota "/cadastrar-vaga"
        │       ├── 📄 CadastrarVaga.razor.css # CSS isolado do formulário de cadastro
        │       ├── 📄 VagasPublicadas.razor # Listagem reativa e controle do modal de edição
        │       └── 📄 VagasPublicadas.razor.css # CSS isolado da grid e dos cards de vagas
        │
        ├── 📁 Properties
        │   └── 📄 launchSettings.json      # Configuração da porta de execução local (5162)
        │
        ├── 📁 wwwroot                      # Arquivos estáticos globais (Imagens, index.html)
        │   └── 📁 css
        │       └── 📄 app.css              # Estilos CSS globais da aplicação
        │
        ├── 📄 _Imports.razor               # Diretivas globais e imports (@using Vaga.Blazor.Components)
        ├── 📄 App.razor                    # Roteador principal do Blazor (Gerencia rotas e o NotFound)
        ├── 📄 Program.cs                   # Configuração de Cultura (pt-BR) e HttpClient (5194)
        └── 📄 Vaga.Blazor.csproj           # Arquivo de gerenciamento do projeto Blazor

```


## 📁 Arquitetura Estrutural do Teste

📁 tests
└── 📁 Vaga.Tests                           # Projeto de Testes Automatizados (xUnit)
    │
    ├── 📁 Domain                           # Testes das regras puras de domínio
    │   └── 📁 Entities
    │       └── 📄 VagaTests.cs             # Testes de unidade da Entidade (Muda estado correto?)
    │
    ├── 📁 Services                         # Testes dos fluxos e validadores (Sua pasta atual)
    │   └── 📄 VagaServiceTests.cs          # Testes do VagaValidator (FluentValidation + Enum)
    │
    ├── 📁 API                              # Testes de integração dos Endpoints (Opcional/Futuro)
    │   └── 📁 Controllers
    │       └── 📄 VagasControllerTests.cs  # Testes dos endpoints com DTOs e retornos HTTP
    │
    ├── 📁 MockData                         # Fábricas de objetos para apoiar os testes (Clean Code)
    │   └── 📄 VagaTestDataBuilder.cs       # Builder para gerar instâncias de Vaga válidas/inválidas
    │
    ├── 📄 Usings.cs                        # Imports globais de testes (Xunit, Moq, FluentValidation)
    └── 📄 Vaga.Tests.csproj                # Arquivo de gerenciamento do projeto de testes



---

## 💡 Regras de Negócio e Engenharia Aplicadas

1.  **Vigência Obrigatória:** Toda vaga necessita obrigatoriamente de uma **Data de Início** e uma **Data de Término** populadas e válidas.
2.  **Validação Temporal Lógica:** O sistema impede (via FluentValidation no Backend e DataAnnotations no Frontend) que uma vaga possua a data de encerramento menor ou anterior à sua data de abertura.
3.  **Controle Automático de Expirados:** A listagem de oportunidades desabilita visualmente através de estados opacos e insere tags descritivas em tempo real caso a vaga ultrapasse a data vigente com base no fuso local.
4.  **Isolamento Rígido de Estilos:** Zero poluição por classes utilitárias globais. Cada elemento possui sua folha de estilo atrelada ao ecossistema de escopo do Blazor.

---

## 🚀 Como Executar o Projeto Localmente

Certifique-se de possuir o **SDK do .NET 10** instalado em sua máquina.

### 1. Clonar o Repositório

```bash
git clone https://github.com
cd universo-vagas
```

### 2. Executar o Backend (API + Swagger)

Abra um terminal na pasta raiz e execute:

```bash
$env:ASPNETCORE_ENVIRONMENT="Development"; dotnet run --project src/Backend/Vaga.API/Vaga.API.csproj
```

A API iniciará localmente. Abra o navegador em: **`http://localhost:5194/swagger`** para acessar o painel visual do **Swagger** e testar os endpoints RESTful.

### 3. Executar o Frontend (Blazor WebAssembly)

Abra um segundo terminal na pasta raiz e execute:

```bash
dotnet run --project src/Frontend/Vaga.Blazor/Vaga.Blazor.csproj
```

O frontend estará disponível em: **`https://localhost:5162`** (ou a porta SSL indicada no console). Acesse para simular o ciclo completo de gerenciamento reativo.

---

## 🧪 Suíte de Testes Automatizados

Garantindo a estabilidade e a cobertura de regressão das regras corporativas, os testes unitários validam as restrições de tipos aceitos pelo domínio e o comportamento lógico das regras de consistência temporal:

```bash
dotnet test
```

A suíte cobre:

- `Vaga_DeveFalhar_QuandoTipoVagaForInvalido` (Restrição do domínio corporativo).
- `Vaga_DeveFalhar_QuandoDataFimForAnteriorADataInicio` (Consistência temporal).
- `Vaga_DevePassar_QuandoDadosEDatasForemValidos` (Garantia do Happy Path).

---

## 🖥️ Telas do Projeto e Recursos de Interface

O sistema é dividido em subgrupos visuais e técnicos bem definidos, mapeando todo o ecossistema da aplicação:

### 📑 1. Documentação e Integração (Backend)
*   **Swagger UI (API RESTful):** Endpoint centralizado de testes (`/swagger`) que serve como documentação viva. Permite a execução e auditoria imediata de payloads JSON para as operações de persistência e validação no servidor.
<img width="1875" height="766" alt="image" src="https://github.com/user-attachments/assets/355a356c-8e9b-4eab-b25f-c94d5450df48" />

### 🎨 2. Interface de Usuário (Blazor Frontend SPA)
*   **Página Inicial (Home):** Painel de apresentação do ecossistema técnico. Exibe cartões dinâmicos detalhando a stack (.NET 10, Blazor, SOLID) e o cartão profissional de autoria do projeto.
<img width="1895" height="850" alt="image" src="https://github.com/user-attachments/assets/e4d9634d-9bb4-4366-8c4e-34c39970dec0" />

*   **Painel Técnico (Sobre):** Tela de conformidade corporativa. Demonstra de forma analítica como os padrões de engenharia (SRP, DIP, Repository Pattern e FluentValidation) foram materializados no código fonte.
<img width="1881" height="1070" alt="image" src="https://github.com/user-attachments/assets/40799ce9-1dcb-456b-a0de-c87570e91210" />

*   **Cadastrar Vaga:** Formulário reativo integrado com `DataAnnotationsValidator`. Possui controle de digitação instantâneo em português (`ParsingErrorMessage`) que bloqueia o envio caso os campos obrigatórios ou as datas de vigência estejam incorretas.
<img width="1886" height="824" alt="image" src="https://github.com/user-attachments/assets/dd772212-1ba1-4523-845b-47dfbe0b003f" />

*   **Cadastrar Vaga:** Validação da data 
<img width="1887" height="843" alt="image" src="https://github.com/user-attachments/assets/7731bd23-5408-4397-8a6a-926ca5a3d4aa" />

*   **Vagas Publicadas (Listagem):** Painel de gerenciamento com suporte a ordenação dinâmica por Título, Data de Início ou Data de Término. Aplica opacidade e tags visuais de bloqueio (`🛑 Expirada`) em cards cuja data final seja menor que a data atual.
<img width="1870" height="866" alt="image" src="https://github.com/user-attachments/assets/3d9ba3b0-d938-4402-b587-347d58259f71" />

*   **Vagas Publicadas (Listagem):** Ordenado por data de término.  
<img width="1867" height="856" alt="image" src="https://github.com/user-attachments/assets/59c3faa2-a58a-4d4f-8834-0e7b2b685a44" />
  
*   **Edição da Vaga (Modal Isolado):** Componente especialista de mutação de dados carregado de forma declarativa via overlay. Mantém a mesma identidade visual e rigor de validação temporal do cadastro principal.
<img width="1185" height="770" alt="image" src="https://github.com/user-attachments/assets/364009a5-b70c-4c35-aab0-8762284c7e90" />

### 🧪 3. Suíte de Qualidade (Testes Unitários)
*   **Testes Automatizados (xUnit):** Camada desacoplada executada via CLI que audita a consistência lógica do negócio. Garante que qualquer alteração de código que viole as regras de tipos de vaga ou ordens de datas quebre o build de integração contínua (CI).
<img width="1048" height="593" alt="image" src="https://github.com/user-attachments/assets/ef6c8827-ce08-4b47-afd9-92166c12afb6" />
