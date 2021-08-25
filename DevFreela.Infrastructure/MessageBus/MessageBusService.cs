using DevFreela.Core.Services;
using RabbitMQ.Client;

namespace DevFreela.Infrastructure.MessageBus
{
    public class MessageBusService : IMessageBusService
    {
        private readonly ConnectionFactory _factory;

        public MessageBusService()
        {
            _factory = new ConnectionFactory
            {
                HostName = "localhost"                
            };
        }
        /// <summary>
        /// Publicando a mensagem com RabbitMQ
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="message"></param>
        public void Publish(string queue, byte[] message)
        {
            //inicializando a conexao.
            using (var connection = _factory.CreateConnection())
            {
                // criando canal de comunicações
                using (var channel = connection.CreateModel())
                {
                    //Garantir que a fila esteja criada.
                    channel.QueueDeclare(
                        queue: queue,
                        durable: false,
                        exclusive: false, // apos conexao acabar, dele a fila
                        autoDelete: false, // permite varias conexoes, mas quando terminarem deleto a fila.
                        arguments: null);

                    //Publicar a mensagem
                    channel.BasicPublish(
                        exchange: "",
                        routingKey: queue,
                        basicProperties: null,
                        body: message);
                }
            }
        }
    }
}
