using DataAccess.DbContext.Data;
using Domain;
using Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
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

        public async Task<TbClient> FindByAccountNumberAsync(string accountNumber)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.AccountNumber == accountNumber);
        }

        public async Task ClientDepositAsync(string accountNumber, decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be greater than zero.");

            var client = await _context.Clients.FirstOrDefaultAsync(c => c.AccountNumber == accountNumber);
            if (client == null)
                throw new ArgumentException("Client not found");

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                client.Balance += amount;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                _logger.LogInformation($"Deposit of {amount:C} successful for account {accountNumber}");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, $"Deposit failed for account {accountNumber}");
                throw;
            }
        }

        public async Task ClientWithdrawAsync(string accountNumber, decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Withdraw amount must be greater than zero.");

            if (amount % 10 != 0)
                throw new ArgumentException("You can only withdraw multiples of 10.");

            var client = await _context.Clients.FirstOrDefaultAsync(c => c.AccountNumber == accountNumber);
            if (client == null)
                throw new ArgumentException("Client not found");

            if (client.Balance < amount)
                throw new InvalidOperationException("Insufficient balance.");

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                client.Balance -= amount;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                _logger.LogInformation($"Withdrawal of {amount:C} successful for account {accountNumber}");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, $"Withdrawal failed for account {accountNumber}");
                throw;
            }
        }
    }
}
