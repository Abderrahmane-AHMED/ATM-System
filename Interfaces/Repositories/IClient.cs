using System;
using Domain;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Repositories
{
    public interface IClient
    {




        Task<TbClient> FindByAccountNumberAsync(string accountNumber);
        Task ClientDepositAsync(string accountNumber, decimal amount);
        Task ClientWithdrawAsync(string accountNumber, decimal amount);


    }
}
