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
       

        TbClient FindByAccountNumber(string accountNumber);
      

        void ClientDeposit(int clientId, decimal amount);

        void ClientWithdraw(int clientId, decimal amount);


    }
}
