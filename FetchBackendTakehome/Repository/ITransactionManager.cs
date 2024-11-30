namespace FetchBackendTakehome.Repository
{
    using System;
    using System.Collections.Generic;
    using FetchBackendTakehome.Models;

    public interface ITransactionManager
    {
        //Add a transaction with payer, points, and date
        void AddTransaction(Transaction transaction);

        //Spend points and return a list of changes (payer and points deducted)
        List<object> SpendPoints(int points);

        //Get current balances of all payers
        Dictionary<string,int> GetBalance();
    }

}
