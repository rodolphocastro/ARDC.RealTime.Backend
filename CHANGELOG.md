# Changelog - Realtime Backend

## 1.0.0 - 2020-01-22

Versão inicial da API.

### 1.0.0 - Adicionados

+ Endpoints:
  + Consultas a Broadcasts:
    + Todos
    + Último
    + Específico
  + Consultas a Mensagens:
    + Todas
    + Específica
  + Interações com Mensagens:
    + Criar
    + Deletar
+ Comandos e Queries:
  + Broadcasts:
    + Criar, Deletar e Editar
    + Listar, Consultar, Consultar o mais recente
  + Mensagens:
    + Criar e Deletar
    + Listar e Consultar
+ Hubs para Comunicação:
  + Broadcasts:
    + Notificar Criação, Remoção e Edição
  + Mensagens:
    + Notificar Criação, Remoção e Edição
    + Criar e Deletar
+ Serviços em Segundo plano:
  + Geração de Broadcasts aleatoriamente
+ Documentação
  + Swagger + Swagger UI

## Modelo

O modelo para este changelog é:

```markdown

## Versão - Data

### Versão - Adicionados

### Versão - Alterados

### Versão - Corrigidos

### Versão - Removidos

```