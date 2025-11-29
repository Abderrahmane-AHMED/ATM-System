# ATM System

This ATM System is fully integrated with my previous **Bank System** project, using the same database.  
All operations performed here (Quick Withdraw, Normal Withdraw, Deposit, and Check Balance) directly affect the bank system in real-time.  

- **Quick Withdraw:** Withdraw preset amounts (10, 20, 50, 100, 200, 300, 400, 500) instantly.  
- **Normal Withdraw:** Allows custom withdrawals, validating multiples of 10 only.  
- **Deposit:** Add funds directly to the same bank account.  
- **Check Balance:** Always displays the real-time bank balance.  

This system acts as both a standalone ATM interface and a complementary extension to the main bank system. Every transaction here is reflected immediately in the bank database.  

**Technologies Used:**  
- ASP.NET Core MVC  
- Entity Framework Core  
- Async Interface Repository Service and Controller  
- Razor Views with validation and real-time error handling
