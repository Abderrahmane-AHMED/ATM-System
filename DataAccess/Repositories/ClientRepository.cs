using DataAccess.DbContext.Data;
using Domain;
using Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ClientRepository : IClient
    {
        private readonly ATMSystemContext _context;
        private readonly ILogger<ClientRepository> _logger;
        public ClientRepository(ATMSystemContext context, ILogger<ClientRepository> logger)
        {
            _context = context;
            _logger = logger;
        }




 
        public TbClient FindByAccountNumber(string accountNumber)
        {
            return _context.Clients.FirstOrDefault(c => c.AccountNumber == accountNumber);
        }
 
        public void ClientDeposit(int clientId, decimal amount)
        {
            var existingClient = _context.Clients.FirstOrDefault(c => c.ClientId == clientId);
            if (existingClient == null)
                throw new ArgumentException("Client not found");

            existingClient.Balance += amount;
            _context.SaveChanges();
        }

        public void ClientWithdraw(int clientId , decimal amount)
        {
            var existingClient = _context.Clients.FirstOrDefault(c => c.ClientId == clientId);

            if (existingClient == null)
                throw new ArgumentException("Client not found");

            if (amount <= 0)
                throw new ArgumentException("Invalid withdraw amount.");

            if (existingClient.Balance < amount)
                throw new InvalidOperationException("Insufficient balance.");

            existingClient.Balance -= amount;
            _context.SaveChanges();


        }
        
    }
}
