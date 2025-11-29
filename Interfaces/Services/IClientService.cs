using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Services
{
    public interface IClientService
    {


        Task<TbClient> FindByAccountNumberAsync(string accountNumber);
        Task ClientDepositAsync(string accountNumber, decimal amount);
        Task ClientWithdrawAsync(string accountNumber, decimal amount);



    }
}
