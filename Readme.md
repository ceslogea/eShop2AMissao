**Metas do projeto:**


- Sistema baseado em microserviços.
- Todos os microserviços devem ser idependentes respeitando os princípios de microserviços, 
- Não devem compartilhar sua base de dados e devem possuir tolerante a falhas no caso de comunicação direta com outro microserviço (como mencionado acima).
- Todos os microserviços devem ser idependentes arquiteturalmente, podendo utilizar em sua demanda DDD, CQRS ou outras, de acordo com a necessidade..
- Todos os microserviços devem ser idependentes em seu contexto de negócio (sem domínio compartilhado).
- Integrações deverão utilizar "Event Source".
- Todos os microserviços devem contemplar testes unitários 
- Todos os microserviços devem contemplar testes de integração

- A base estrutural do ambiente deve ser compartilhada entre os microserviços, aproximando ao maximo formato da base dados, event source e recursos compartilhados atravéz de implementação abstrata da mesma ('eShop.common').
Esta abordagem tem como foco possibilitar benefícios como: 
    - Facilitar o uso de bibliotecas inclusa nestas, abstraindo sua complexibilidade e aumentando o foco na sua implementação, 
    - Possibilitar testes integrados com host 'in-memory', 
    - Proporcionar facil re-utilização da infraestrutura e base funcional dos projetos,
    - Hemogenia para resultados de BI, telemetria e afins.


**Testes**:
| Integration | Domain | Integration |
| ----------- | ------ | ----------- |
| Identiy     | 0%     | 0%          |
| Gateway     | 0%     | 0%          |
| Commun      | 0%     | 0%          |
| Catalog     | 0%     | 0%          |
| Basket      | 0%     | 0%          |
| Order       | 0%     | 0%          |


- Frameworks e tecnologias disponibilizadas na **eShop.Commun**

**Swagger**

```dotnetcli
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSwagger("Products", "v1");
    }
}
```


**MongoDB:**
- Config
 
```json
"mongo": {
    "connectionString": "mongodb://localhost:27017",
    "database": "eshop-services-catalog",
    "seed": false
  },
```


```dotnetcli
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMongoDB(Configuration);
    }
}
```

**RabbitMQ**:

- Config

```json
"rabbitmq": {
    "Username": "guest",
    "Password": "guest",
    "VirtualHost": "/",
    "Port": 5672,
    "Hostnames": [
      "localhost"
    ],
    "RequestTimeout": "00:00:10",
    "PublishConfirmTimeout": "00:00:01",
    "RecoveryInterval": "00:00:10",
    "PersistentDeliveryMode": true,
    "AutoCloseConnection": true,
    "AutomaticRecovery": true,
    "TopologyRecovery": true,
    "Exchange": {
      "Durable": true,
      "AutoDelete": true,
      "Type": "Topic"
    },
    "Queue": {
      "AutoDelete": true,
      "Durable": true,
      "Exclusive": true
    }
  },
```


- Add bus (***IBusClient***):

```dotnetcli
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRabbitMq(Configuration);
    }
}
```

- Subscribe Event Handler:
```dotnetcli
public class Program
{
    public static void Main(string[] args)
    {
            ServiceHost.Create<Startup>(args)
            .UseRabbitMq()
            .SubscribeToEvent<IEvent>()
            .Build()
            .Run();
    }
}
```

**Confluent Kafka**

- Config Producer:
   
```json
"producer": {
        "bootstrapservers": "localhost:9092"
    },
```

  
- Config Consumer:

```json
"consumer": {
    "bootstrapservers": "localhost:9092", //specify your kafka broker address
    "groupid": "csharp-consumer",
    "enableautocommit": true,
    "statisticsintervalms": 5000,
    "sessiontimeoutms": 6000,
    "autooffsetreset": 0,
    "enablepartitioneof": true,
    "SaslMechanism": 0, //0 for GSSAPI
    "SaslKerberosKeytab": "filename.keytab", //specify your keytab file here
    "SaslKerberosPrincipal": "youralias@DOMAIN.COM", //specify your alias here
    "SaslKerberosServiceName": "kafka",
    "SaslKerberosKinitCmd": "kinit -k -t %{sasl.kerberos.keytab} %{sasl.kerberos.principal}"
  }
```

- Add bus (***IKafkaProducer***):
```dotnetcli
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddKafkaProducer(Configuration);
    }
}
```

- Subscrive Event Handler:
```dotnetcli
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
            services.AddKafkaConsumerConfig(Configuration);
            services.AddKafkaConsumerEventHandlers<IEvent>(Configuration);
    }
}
```
