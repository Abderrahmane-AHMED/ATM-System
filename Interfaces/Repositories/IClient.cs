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


       

        TbClient FindByAccountNumber(string accountNumber);

        void ClientDeposit(int clientId, decimal amount);

        void ClientWithdraw(int clientId, decimal amount);


    }
}
