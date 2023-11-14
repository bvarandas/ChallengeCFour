# ChallengeCrf
Projeto de desafio porposto pelo time desenvolvimento do Banco Carrefour.

Projeto se propoe a fazer um crud com patterns de mercado e mecanismos usados em soluções no mercado de capitais, 
como mesageria e bibliotecas de compressão de dados para camada de transporte como protobuf.

Arquitetura - Message/Event Driven e alguns elementos de Clean Architecture.
Command-> Event
Query-> Reply
Usando  Filas do RabbiMQ para coreografia do ambiente.
Protobuf para compactação na camada de transporte.
SignalR no response do para o client/Angular.

Padrões Criacionais usados:
Factory
Singleton

Padrões Comportmentais
Command
Mediator 

Mais
Ioc - Inversão de controle
CQRS - com Coreografia
Injeção de depedencia
Unit of Work
Event Sourcing (removido)
Alguns conceitos de solid tbm foram usados.

Instruções para rodar
Na pasta ChallengeCrf rodar o docker-compose up

Irá subir os containers de:
rabbitmq-server - RabbitMQ
mongo - MongoDB
challengecrf.api - Api de requisições para Controle de lançamentos e consolidado diário
challengecrf.queue - Worker para Producer e consumer para o serviço 
angularcontainer - Front End em angular para efetuar o cadastro. http//localhost:4200/cashflow


Modelo da arquitetura.
![Arq_ChallengeB3](https://github.com/bvarandas/ChallengeB3/assets/13907905/3f5e852c-e0f7-4f5d-a46d-289813343551)

Dados Cadastrados
![image](https://github.com/bvarandas/ChallengeB3/assets/13907905/a59d4ac7-1746-4cf3-9f0b-198cc4578028)





