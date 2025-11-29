using DataAccess.DbContext.Data;
using Domain;
using Interfaces.Repositories;
using Interfaces.Services;
using Microsoft.Extensions.Logging;
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

        public async Task<TbClient> FindByAccountNumberAsync(string accountNumber)
        {
            return await _clientRepository.FindByAccountNumberAsync(accountNumber);
        }

        public async Task ClientDepositAsync(string accountNumber, decimal amount)
        {
            await _clientRepository.ClientDepositAsync(accountNumber, amount);
        }

        public async Task ClientWithdrawAsync(string accountNumber, decimal amount)
        {
            await _clientRepository.ClientWithdrawAsync(accountNumber, amount);
        }
    }
}
