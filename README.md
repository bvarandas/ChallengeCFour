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
Command

Domain Notification

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

![image](https://github.com/bvarandas/ChallengeCrf/assets/13907905/29aeeb46-4fe7-4632-961d-26ba4b5881cd)

![image](https://github.com/bvarandas/ChallengeCrf/assets/13907905/719581d1-c4a5-429b-8a9e-44ece8ccb475)


![arq_crf_gif](https://github.com/bvarandas/ChallengeCrf/assets/13907905/b99b25a9-6302-4a65-af4e-0f6af976e668)


