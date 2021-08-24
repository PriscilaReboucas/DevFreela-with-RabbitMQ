﻿using DevFreela.Core.DTOs;
using DevFreela.Core.Services;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IMessageBusService _messageBusService;
        private const string QUEUE_NAME = "Payments";
        public PaymentService(IMessageBusService messageBusService)
        {
            // recebendo por injeção de dependencia
            _messageBusService = messageBusService;
        }

        public void ProcessPayment(PaymentInfoDTO paymentInfoDTO)
        {
            //convert para string/json
            var paymentInfoJson = JsonSerializer.Serialize(paymentInfoDTO);
            // convert para bytes
            var paymentInfoBytes = Encoding.UTF8.GetBytes(paymentInfoJson);

            // chamando o método publish, passando a fila e os bytes (mensagem)
            _messageBusService.Publish(QUEUE_NAME, paymentInfoBytes);
        }
    }
}