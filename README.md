# ChallengeCrf
Projeto proposto para desafio.
---

Projeto se propoe a fazer um CRUD com patterns de mercado e mecanismos usados em soluções no mercado de capitais.

Requisito arquitetural relevantes: 
* O serviço de controle de lançamento não deve ficar indisponível se o sistema de consolidado diário cair.
* O serviço recebe 500 requisições por segundos, com no máximo 5% de perda de requisições

---

A escolha por essa arquitetura de mensageria foi efetuada pelos requisitos arquiteturais propostos no desafio.

Arquitetura - Message/Event Driven e alguns elementos de Clean Architecture.
* Command-> Event
* Query-> Reply

Usando  **Filas do RabbiMQ** para coreografia do ambiente - Importante na quantidade de mensagens (recebimento e entrega), e também inportante na alta disponibilidade e resiliência da entrega, pois foi implementado, Ack e NAck no consumer.

**Protobuf** para compactação na camada de transporte entre serviços (Array de Bytes)- Importante na compactação das mensagens para transporte e para melhor armazenamento e envio pelo RabbitMQ.

**SignalR** no response do para o client/Angular.(Tela) - Importante para recebimento assincrono das informações de Consolidado Diário e  Lançamento de Fluxo de caixa na tela.

**Essa abordagem também restringe que cada serviço tenha sua responsabilidade seapradamente, garantindo a coesão da programação e também mantendo suas lógicas desacopladas.**

**Entity Framework** - 
---

Modelo da arquitetura C4


![arq_crf_gif](https://github.com/bvarandas/ChallengeCrf/assets/13907905/2397d92e-5341-4899-90b0-a3f48ef0d13e)

---

Padrões Criacionais usados:
* Singleton - para usar somente uma instancia de cada módulo

Padrões Comportmentais
* Command
* Mediator 

Mais Parterns
* **Domain Notification** - para notificações centralizdas, validando os lançamentos na application
* **CQRS - com Coreografia** - para leitura ficar separadamente da gravação, melhorando assim a performance da applicação
* Injeção de depedencia - Injetando as dependências, para usarmos as interfaces dos objetos, desacoplando as chamadas dos métodos entre os objetos.
* Unit of Work

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



