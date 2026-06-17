# Arquivos de Dados

## Arquivos por projeto

Cada projeto usa uma pasta própria em `%LocalAppData%\\SIGEV\\Projects\\<NomeDoProjeto>`.

Arquivos mais relevantes:

- `groups.json`: grupos participantes cadastrados.
- `causas.json`: causas persistidas internamente.
- `causas.csv`: cópia padronizada da lista de causas importada/exportada.
- `q1.csv`: cópia interna das respostas da etapa de aprovação das causas e peso dos grupos.
- `q2.csv`: cópia interna das respostas finais usadas no ranking.
- relatório exportado: CSV salvo pelo usuário com nome sugerido `relatorio_sigev_<projeto>.csv`.

## Regras práticas de importação

- O usuário pode importar qualquer arquivo `.csv` baixado do Google Forms; não é necessário renomear.
- O app copia o arquivo para o nome interno esperado na pasta do projeto.
- A lista de causas precisa ter uma coluna `Causa`.
- As respostas de aprovação precisam conter colunas de causas de evasão e, para pesos dos grupos, as colunas de comparação entre participantes.
- As respostas finais precisam conter colunas de causas aprovadas.

## Lista de causas

Formato mínimo:

```csv
Causa
Exemplo de causa
```

A amostra em `Samples/causas.csv` lista 17 causas ligadas à evasão, cobrindo temas como desempenho acadêmico, trabalho e estudo, motivação, saúde mental, infraestrutura e currículo.

## Respostas de aprovação

No código, esse arquivo é armazenado como `q1.csv`.

Conteúdo esperado:

1. Perfil do respondente.
2. Escala Likert de 1 a 5 para as causas de evasão.
3. Comparações entre grupos participantes para calcular os pesos.

Amostra atual em `Samples/q1.csv`:

- `10` respostas no total.
- `5` respostas de `(Ex)coordenação`.
- `5` respostas de `Docentes`.

O script base do formulário está em `Samples/q1.txt`.

## Respostas finais

No código, esse arquivo é armazenado como `q2.csv`.

Conteúdo esperado:

1. Perfil do respondente.
2. Escala Likert de 1 a 5 para as causas aprovadas.

Amostra atual em `Samples/q2.csv`:

- `100` respostas no total.
- `10` respostas de `(Ex-)coordenação`.
- `30` respostas de `Docentes`.
- `60` respostas de `Discentes`.

O script base do formulário está em `Samples/q2.txt`.

## Exportações sugeridas

O app sugere nomes amigáveis ao salvar arquivos:

- `modelo_causas_evasao_<projeto>.csv`
- `formulario_validacao_causas_<projeto>.txt`
- `formulario_ranking_final_<projeto>.txt`
- `relatorio_sigev_<projeto>.csv`
