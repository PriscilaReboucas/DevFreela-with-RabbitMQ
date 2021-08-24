namespace DevFreela.Core.Services
{
    public interface IMessageBusService
    {        
        /// <summary>
        /// representa a publicação da mensagem
        /// </summary>
        /// <param name="queue">a fila e array de bytes que será a mensagem</param>
        /// <param name="message"> a mensagem</param>
        void Publish(string queue, byte[] message);
    }
}
