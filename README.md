# .NET Mentoring Program Advanced 2022 Q3

Exercises for .NET Mentoring Program Advanced 2022 Q3



## F.A.Q.

Start RabbitMQ locally (see [this article](https://www.rabbitmq.com/download.html) for details):

```powershell
docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.11-management
```

RabbitMQ UI can be found here: [local RabbitMQ UI](http://localhost:15672/) with the next default credentials:
```
user: quest
pass: quest
```