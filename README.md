# ChallengeCrf
Projeto de desafio porposto para teste.

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

De dentro da pasta do projeto ChallengeCrf rodar :

kind create cluster

kubectl apply -f .



Irá subir os containers de:
rabbitmq-server - RabbitMQ
mongo - MongoDB
challengecrf.api - Api de requisições para Controle de lançamentos e consolidado diário
challengecrf.queue - Worker para Producer e consumer para o serviço 
angularcontainer - Front End em angular para efetuar o cadastro. http//localhost:4200/cashflow

Modelo da arquitetura C4

[![image](https://github.com/bvarandas/ChallengeCrf/assets/13907905/76c11216-d9b7-4d2a-bee2-ec5f4855334b)]

![image](https://github.com/bvarandas/ChallengeCrf/assets/13907905/43b306e1-df1d-443a-8199-dbeefc4915e8)

![image](https://github.com/bvarandas/ChallengeCrf/assets/13907905/6a10558d-eb43-4842-acba-58ea77442767)

