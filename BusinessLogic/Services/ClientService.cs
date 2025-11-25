using DataAccess.DbContext.Data;
using Domain;
using Interfaces.Repositories;
using Interfaces.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class ClientService : IClientService
    {

        private readonly IClient _clientRepository;
        private readonly ILogger<ClientService> _logger;
        public ClientService(IClient clientRepository, ILogger<ClientService> logger)
        {
            _clientRepository = clientRepository;
            _logger = logger;
        }


    
      
        public TbClient FindByAccountNumber(string accountNumber)
        {
            return _clientRepository.FindByAccountNumber(accountNumber);
        }

        public void ClientDeposit(int clientId, decimal amount) => _clientRepository.ClientDeposit(clientId , amount);

        public void ClientWithdraw(int clientId, decimal amount) => _clientRepository.ClientWithdraw(clientId, amount);

    }
}
