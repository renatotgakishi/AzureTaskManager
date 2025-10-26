# AzureTaskManager

**AzureTaskManager** é uma aplicação moderna de gerenciamento de tarefas construída sobre a plataforma Microsoft Azure, com foco em escalabilidade, integração e produtividade. Utilizando **Azure Functions** como núcleo da arquitetura, o projeto oferece uma abordagem serverless para lidar com eventos, filas e notificações em tempo real.

## Visão Geral

O AzureTaskManager foi desenvolvido para atender equipes que precisam organizar tarefas em estilo **Kanban**, com recursos avançados que vão além do básico:

- Criação e movimentação de tarefas entre colunas (To Do, Doing, Done)
- Notificações automáticas por e-mail em eventos importantes
- Anexos de arquivos diretamente nas tarefas
- Integração com filas do Azure Storage para processamento assíncrono
- Envio de atualizações e alertas via Azure Functions
- Estrutura modular com separação clara entre Domain, Application, Infrastructure e Functions

## Tecnologias Utilizadas

- **Azure Functions** para lógica de backend e eventos
- **Azure Storage Queues** para processamento desacoplado
- **.NET 9** com arquitetura limpa e testável
- **xUnit + Moq** para testes automatizados
- **GitHub Actions** (em breve) para CI/CD

## Objetivo

O projeto tem como objetivo demonstrar como soluções baseadas em Azure podem ser utilizadas para construir aplicações robustas, escaláveis e com baixo custo operacional. Ele serve como base para produtos internos, MVPs ou soluções corporativas que exigem agilidade e integração com serviços em nuvem.

## Estrutura do Projeto

- `AzureTaskManager.Domain`: entidades e regras de negócio
- `AzureTaskManager.Application`: casos de uso e orquestração
- `AzureTaskManager.Infrastructure`: acesso a dados e serviços externos
- `AzureTaskManager.Functions`: funções serverless e integração com Azure
- `AzureTaskManager.Tests`: testes unitários e de integração

## Próximos Recursos

- Autenticação com Azure AD
- Dashboard com métricas em tempo real
- Webhooks para integração com outros sistemas
- Deploy automatizado via GitHub Actions

---

Este projeto está em evolução contínua e representa uma abordagem prática e eficiente para quem deseja explorar o potencial da nuvem com foco em produtividade e organização.
