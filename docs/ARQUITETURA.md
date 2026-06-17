# Arquitetura

## Visão geral

O app é uma aplicação WinUI 3 desktop com navegação simples entre uma tela de lista de projetos e uma tela de detalhes do projeto.

## Camadas principais

### Inicialização

- `PrimeiraTelaWinUI/App.cs`: cria a janela principal, define o título do app e navega para `MainPage`.
- `PrimeiraTelaWinUI/Program.cs`: ponto de entrada e inicialização do ambiente WinUI.

### Interface

- `Views/MainPage.xaml` e `PrimeiraTelaWinUI/Views/MainPage.cs`: tela inicial com CRUD de projetos escolares.
- `Views/ProjectDetailsPage.xaml` e `PrimeiraTelaWinUI/Views/ProjectDetailsPage.cs`: tela principal do projeto, com participantes, causas, aprovação, peso dos grupos, ranking final e relatórios.

### Persistência

- `PrimeiraTelaWinUI/Data/ProjectStorage.cs`: resolve e cria as pastas locais do app e migra dados legados de `SADMAT` para `SIGEV`.
- `PrimeiraTelaWinUI/Data/ProjectRepository.cs`: persiste metadados de projetos em `projects.json`.
- `PrimeiraTelaWinUI/Data/ProjectGroupsRepository.cs`: persiste os grupos participantes em `groups.json`.
- `PrimeiraTelaWinUI/Data/ProjectCausesRepository.cs`: persiste causas em `causas.json` e importa/exporta a lista de causas em CSV.

## Cálculos e importações

Grande parte da lógica de importação e cálculo está concentrada em `PrimeiraTelaWinUI/Views/ProjectDetailsPage.cs`.

Esse arquivo concentra:

1. Leitura das respostas da etapa de aprovação, armazenadas internamente como `q1.csv`.
2. Cálculo agregado das respostas Likert para aprovação das causas.
3. Cálculo da mediana discreta das notas de 1 a 5.
4. Cálculo da concordância com base nas notas positivas selecionadas.
5. Leitura das comparações entre grupos participantes a partir do arquivo de aprovação.
6. Cálculo dos pesos dos grupos por matriz de comparação pareada.
7. Cálculo da razão de consistência global.
8. Leitura das respostas finais, armazenadas internamente como `q2.csv`.
9. Montagem da matriz de decisão do ranking final.
10. Cálculo do ranking final das causas aprovadas.
11. Geração do relatório exportável em CSV.

## Nomes internos e nomes de usuário

Os nomes internos `q1.csv` e `q2.csv` são mantidos apenas para compatibilidade da persistência. A interface usa:

- respostas de aprovação.
- respostas finais.
- lista de causas.
- relatório do SIGEV.

O app aceita CSV com qualquer nome escolhido pelo Google Forms e copia o conteúdo para o arquivo interno correspondente.

## Distribuição

O script `build-installer.ps1` publica o app em `dist\SIGEV-Distribuicao` e usa o Inno Setup para gerar:

```text
dist\SIGEV-Setup-x64.exe
```

Esse instalador é o artefato final para envio aos usuários.
