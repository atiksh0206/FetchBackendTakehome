using FetchBackendTakehome.Models;

namespace FetchBackendTakehome.Repository
{
    public class TransactionManager : ITransactionManager
    {
        //stores the transactions with the payer, points and time stamp ordered by time
        private static List<Transaction> _transactions  = new List<Transaction>();

        
        void ITransactionManager.AddTransaction(Transaction transaction)
        {
            //add transaction to list of transactions
            _transactions.Add(transaction);
            _transactions.Sort((t1, t2) => t1.Timestamp.CompareTo(t2.Timestamp));
        }

        Dictionary<string,int> ITransactionManager.GetBalance()
        {
            //dictionary which stores total points for each payer
            Dictionary<string, int> balance = new Dictionary<string, int>();
            //for each transaction if the payer is in the dictionary update the points 
            foreach (var t in _transactions.ToList())
            {
                if (!balance.ContainsKey(t.Payer))
                {
                    //adds the new transaction to the dictionary
                    balance[t.Payer] = 0;
                }
                balance[t.Payer] += t.Points;

            }
            return balance;
        }

        List<object> ITransactionManager.SpendPoints(int points)
        {   
            //gets the total points
            int totalAvailablePoints = _transactions.Sum(t => t.Points);
            //checks if we have sufficient points
            if (totalAvailablePoints < points)
            {
                throw new InvalidOperationException("Insufficient points.");
            }
            //keeps track of where points are being spent by which payer
            var spentPoints = new List<object>();
            foreach (var transaction in _transactions.ToList())
            {
                //calculates the points available for spending
                int pointsAvailable = Math.Min(transaction.Points, points);

                //subtracts the points from the transaction
                transaction.Points -= pointsAvailable;
                points -= pointsAvailable;
                //adds the payer and piints to the list
                spentPoints.Add(new { payer = transaction.Payer, points = -pointsAvailable });
                
            }

            return spentPoints;

        }
    }
}
