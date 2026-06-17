# SIGEV

SIGEV - Sistema Inteligente de Gestão da Evasão é um aplicativo desktop WinUI 3 para análise, aprovação e priorização de causas de evasão estudantil. A interface usa termos operacionais para gestores, mantendo internamente os métodos AHP e TOPSIS para cálculo dos pesos dos grupos e do ranking final.

## O que o app faz

1. Gerencia projetos por instituição/curso.
2. Mantém grupos de atores e causas de evasão por projeto.
3. Exporta scripts para Google Apps Script, usados para gerar questionários no Google Forms.
4. Importa `causas.csv`, `q1.csv` e `q2.csv`.
5. Aprova causas com base em mediana mínima e concordância mínima.
6. Calcula pesos dos grupos participantes a partir das comparações.
7. Calcula o ranking final das causas aprovadas a partir das respostas finais.
8. Exporta um resumo em CSV e copia um resumo textual para a área de transferência.

## Build e execução

Build de desenvolvimento:

```powershell
dotnet build PrimeiraTelaWinUI.csproj -c Debug
```

Abrir o app antes de gerar instalador, para testar os ajustes:

```powershell
dotnet run --project PrimeiraTelaWinUI.csproj -c Debug
```

Publicação para distribuição:

```powershell
dotnet publish PrimeiraTelaWinUI.csproj -c Release -r win-x64 --self-contained true -o dist\SIGEV-Distribuicao
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

Esse é o arquivo único que deve ser enviado para os usuários finais. Ele abre o assistente de instalação, instala o SIGEV, cria atalhos e registra o app em Programas e Recursos.

O executável interno gerado pela publicação fica em:

```text
dist\SIGEV-Distribuicao\PrimeiraTelaWinUI.exe
```

Esse `PrimeiraTelaWinUI.exe` é usado pelo instalador e deve permanecer junto com os arquivos da pasta publicada.

Observação: o build passa, mas ainda emite avisos de nulidade/código gerado que não impedem a execução.

## Estrutura principal

- `PrimeiraTelaWinUI/`: código C# principal do app.
- `Views/`: XAML das telas.
- `AppAssets/`: assets, recursos empacotados e dependências locais usadas pelo app.
- `installer/`: script Inno Setup usado para gerar `SIGEV-Setup-x64.exe`.
- `Samples/`: amostras de `causas.csv`, `q1.csv`, `q2.csv` e scripts `q1.txt`/`q2.txt`.
- `NOTES.md`: notas do projeto.

## Documentação adicional

- `docs/FLUXO_DE_USO.md`
- `docs/ARQUITETURA.md`
- `docs/ARQUIVOS_DE_DADOS.md`
