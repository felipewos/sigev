# Notas do Projeto

## Visão geral

SIGEV - Sistema Inteligente de Gestão da Evasão é um aplicativo WinUI 3 focado em análise, aprovação e priorização de causas de evasão estudantil. O vocabulário visível prioriza gestores leigos: participantes, causas possíveis, aprovação das causas, peso dos grupos, ranking final e relatório.

## Estrutura relevante

- `PrimeiraTelaWinUI/`: código principal da aplicação.
- `Views/`: definição das telas.
- `AppAssets/`: recursos, dependências e arquivos empacotados usados pela aplicação.
- `Samples/`: exemplos de lista de causas, respostas e scripts do Google Forms.
- `docs/`: documentação de uso, arquivos e arquitetura.
- `documentos_inpi/`: minutas LaTeX para orientação de registro de software e avaliação de depósito de patente.

## Pontos de entrada

- `PrimeiraTelaWinUI/App.cs`
- `PrimeiraTelaWinUI/Program.cs`
- `PrimeiraTelaWinUI/Views/MainPage.cs`
- `PrimeiraTelaWinUI/Views/ProjectDetailsPage.cs`

## Build

Comando de desenvolvimento:

```powershell
dotnet build PrimeiraTelaWinUI.csproj -c Debug
```

Abrir o app para revisar alterações antes de gerar instalador:

```powershell
dotnet run --project PrimeiraTelaWinUI.csproj -c Debug
```

Comando de distribuição:

```powershell
.\build-installer.ps1
```

Se o Inno Setup ainda não estiver instalado:

```powershell
.\build-installer.ps1 -InstallInnoSetup
```

Entrega final:

```text
dist\SIGEV-Setup-x64.exe
```

Use `dist\SIGEV-Setup-x64.exe` como arquivo final para instalação pelos usuários. O `PrimeiraTelaWinUI.exe` em `dist\SIGEV-Distribuicao\` é o executável interno publicado e é empacotado pelo Inno Setup.

## Nomes de arquivos

O app aceita CSV baixado do Google Forms sem exigir renomeação. Dentro da pasta do projeto, os dados seguem nomes internos para compatibilidade:

- `causas.csv`: cópia padronizada da lista de causas.
- `q1.csv`: respostas da etapa de aprovação das causas e peso dos grupos.
- `q2.csv`: respostas finais usadas no ranking.

## Próximos ajustes possíveis

- reduzir avisos de nulidade no build.
- consolidar recursos locais do app.
- validar o pacote INPI com NIT/procurador antes de qualquer protocolo formal.
