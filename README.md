# ChallengeCrf
Projeto proposto para desafio.

Projeto se propoe a fazer um crud com patterns de mercado e mecanismos usados em soluções no mercado de capitais, 
como mesageria e bibliotecas de compressão de dados para camada de transporte como protobuf.

Arquitetura - Message/Event Driven e alguns elementos de Clean Architecture.
Command-> Event
Query-> Reply
Usando  Filas do RabbiMQ para coreografia do ambiente.
Protobuf para compactação na camada de transporte.
SignalR no response do para o client/Angular.


Modelo da arquitetura C4

![arq_crf_gif](https://github.com/bvarandas/ChallengeCrf/assets/13907905/b99b25a9-6302-4a65-af4e-0f6af976e668)


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

![image](https://github.com/bvarandas/ChallengeCrf/assets/13907905/765b8d2e-0ea2-4ee3-a30c-b0ef5e55f36e)

![image](https://github.com/bvarandas/ChallengeCrf/assets/13907905/1d6c8ed2-35fe-4f59-8778-28b585246803)


Instruções para rodar

Setar o visual Studio para rodar o docker compose
![image](https://github.com/bvarandas/ChallengeCrf/assets/13907905/f291af70-68bf-4391-9af9-b83890a04676)

![image](https://github.com/bvarandas/ChallengeCrf/assets/13907905/88e23b2b-814c-4e3e-a182-b8075c4e2234)

F5

Irá subir os seguintes containers

![image](https://github.com/bvarandas/ChallengeCrf/assets/13907905/d4553f77-03b2-4128-b6c1-31221a0b52ef)



Na visual studio Code, entrar na pasta 

![image](https://github.com/bvarandas/ChallengeCrf/assets/13907905/2925361b-7de8-4b1b-b632-806a4d31d1e8)

![image](https://github.com/bvarandas/ChallengeCrf/assets/13907905/9b7e4af0-b126-4ffa-a91c-231fcb563313)

Abrir um terminal e digitar ng serve

Acessar no navegador http://localhost:4200/


Irá subir os containers de:
rabbitmq-server - RabbitMQ
mongo - MongoDB
challengecrf.api - Api de requisições para Controle de lançamentos e consolidado diário
challengecrf.queue - Worker para Producer e consumer para o serviço 
angularcontainer - Front End em angular para efetuar o cadastro. http//localhost:4200/cashflow



