# SIGEV

SIGEV - Sistema Inteligente de Gestão da Evasão é um aplicativo desktop WinUI 3 para apoiar gestores na aprovação e priorização de causas de evasão estudantil. A interface usa termos operacionais, como participantes, aprovação das causas, peso dos grupos e ranking final; os métodos AHP e TOPSIS permanecem como base técnica interna.

## O que o app faz

1. Gerencia projetos por instituição e curso.
2. Mantém grupos participantes e causas possíveis por projeto.
3. Exporta scripts para Google Apps Script, usados para gerar questionários no Google Forms.
4. Importa respostas baixadas do Google Forms em CSV, sem exigir que o arquivo seja renomeado.
5. Aprova causas com base em mediana mínima, concordância mínima e notas positivas configuradas.
6. Calcula os pesos dos grupos participantes a partir das comparações.
7. Calcula o ranking final das causas aprovadas a partir das respostas finais.
8. Exporta relatório em CSV e copia um resumo textual para a área de transferência.

## Fluxo de uso

1. Crie ou abra um projeto escolar.
2. Cadastre os participantes e as causas possíveis.
3. Gere o questionário de validação das causas.
4. Cole o script no Google Apps Script e execute `criarFormulario`.
5. Colete as respostas no Google Forms e baixe o CSV.
6. Importe esse CSV em `Importar respostas de aprovação`.
7. Revise mediana, concordância, causas aprovadas e peso dos grupos.
8. Gere o questionário final, colete as respostas e importe o CSV em `Importar respostas finais`.
9. Atualize o relatório, revise o ranking final e exporte o CSV quando necessário.

## Build e execução

Build de desenvolvimento:

```powershell
dotnet build PrimeiraTelaWinUI.csproj -c Debug
```

Abrir o app antes de gerar instalador, para testar os ajustes:

```powershell
dotnet run --project PrimeiraTelaWinUI.csproj -c Debug
```

Gerar o instalador final:

```powershell
.\build-installer.ps1
```

Se o Inno Setup ainda não estiver instalado na máquina:

```powershell
.\build-installer.ps1 -InstallInnoSetup
```

Arquivo final para instalação:

```text
dist\SIGEV-Setup-x64.exe
```

Esse é o arquivo único que deve ser enviado para usuários finais. Ele abre o assistente de instalação, instala o SIGEV, cria atalhos e registra o app em Programas e Recursos.

O executável interno publicado fica em:

```text
dist\SIGEV-Distribuicao\PrimeiraTelaWinUI.exe
```

Esse arquivo é usado pelo instalador. Para distribuição normal, envie apenas `dist\SIGEV-Setup-x64.exe`.

## Estrutura principal

- `PrimeiraTelaWinUI/`: código C# principal do app.
- `Views/`: XAML das telas.
- `AppAssets/`: assets, recursos empacotados e dependências locais usadas pelo app.
- `installer/`: script Inno Setup usado para gerar `SIGEV-Setup-x64.exe`.
- `Samples/`: amostras de CSV e scripts de questionários.
- `docs/`: documentação operacional e técnica.
- `documentos_inpi/`: minutas LaTeX e roteiro para registro de software e avaliação de depósito de patente junto ao INPI.

## Observações

Os arquivos internos `causas.csv`, `q1.csv` e `q2.csv` continuam existindo dentro da pasta de cada projeto para manter compatibilidade e persistência. Na interface, o usuário trabalha com nomes amigáveis: lista de causas, respostas de aprovação e respostas finais.

O build pode emitir avisos de nulidade/código gerado que não impedem a execução.
