# Fluxo de Uso

## Tela inicial

A tela inicial lista os projetos cadastrados e permite criar, abrir, editar nome e excluir projetos escolares.

Ao criar um projeto, o app pede instituição e curso e cria automaticamente uma pasta local para armazenar os dados.

## Fluxo recomendado dentro do projeto

1. Cadastre os participantes.
2. Cadastre ou importe as causas possíveis.
3. Gere o questionário de validação das causas.
4. Cole o script salvo no Google Apps Script e execute `criarFormulario`.
5. Compartilhe o Google Forms com os participantes.
6. Depois da coleta, baixe as respostas em CSV no Google Forms.
7. Importe o CSV em `Importar respostas de aprovação`.
8. Revise mediana mínima, concordância mínima, notas positivas e causas aprovadas.
9. Verifique os pesos dos grupos e a consistência das comparações.
10. Gere o questionário final.
11. Depois da segunda coleta, baixe as respostas finais em CSV e importe em `Importar respostas finais`.
12. Atualize o relatório, revise o ranking final e exporte o CSV se necessário.

O app não exige que os CSVs baixados do Google Forms tenham nomes específicos. Ele salva cópias internas padronizadas na pasta do projeto.

## Seções da página de detalhes

### Relatório e ranking final

- Mostra o status das últimas importações.
- Exibe a causa mais prioritária e o top 10 do ranking final.
- Permite importar respostas finais, limpar ranking, atualizar relatório, exportar CSV e copiar resumo.

### Peso dos grupos

- Exibe o peso calculado para cada grupo participante.
- Mostra a consistência das comparações entre grupos.
- Permite ajustar o limite de inconsistência percentual.
- Permite gerar o questionário final.

### Aprovação das causas

- Importa respostas da etapa de aprovação.
- Calcula a mediana das notas de 1 a 5.
- Calcula a concordância usando as notas positivas selecionadas.
- Aplica a regra de aprovação por mediana mínima e concordância mínima.
- Marca cada causa como aprovada ou reprovada.

Padrões atuais:

- mediana mínima: `3`.
- concordância mínima: `70%`.
- notas positivas: `3`, `4` e `5`.

### Causas possíveis

- Lista as causas do projeto.
- Permite adicionar, editar, excluir e limpar causas.
- Permite importar lista de causas em CSV.
- Permite baixar um modelo de lista de causas.
- Permite gerar o questionário de validação das causas.

### Participantes

- Lista os grupos participantes do projeto.
- Permite adicionar, editar, excluir e limpar participantes.
- Todos os grupos cadastrados entram nos questionários e nos cálculos.

## Armazenamento local

Os dados do app ficam em `%LocalAppData%\\SIGEV`, com um `projects.json` na raiz e uma pasta para cada projeto em `%LocalAppData%\\SIGEV\\Projects`.

Se houver dados antigos em `%LocalAppData%\\SADMAT`, o app copia esses dados automaticamente na primeira execução para preservar projetos existentes.
