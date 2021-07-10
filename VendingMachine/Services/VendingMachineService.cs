using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendingMachineApi.Models;
using static VendingMachineApi.Classes.Enumerations;

namespace VendingMachineApi.Services
{
    public class VendingMachineService
    {
        #region Constants

        const string ERROR_GET_VENDING_MACHINE = "Error: GetVendingMachine() - Failed to get vending machine. ";
        const string ERROR_GET_STOCK_DRINKS = "Error: GetStockDrinks() - Failed to create inventory for drink. ";
        const string ERROR_GET_STOCK_COINS = "Error: GetStockCoins() - Failed to create inventory for coins. ";

        #endregion
        #region Instance Variables
        #endregion
        #region Constructors
        #endregion
        #region Public Methods

        public Order ProcessOrder(ref VendingMachine vendingMachine,ref  Order order)
        {
            Drink currDrink= null;
            int cost = 0;
            int totalPaid = 0;
            int totalChangeOnHand = 0;
            try
            {

                totalChangeOnHand = calculateCoinOnHand(vendingMachine);

                // Determine their change
                foreach (Coin coinPaid in order.Coins)
                {
                    totalPaid += coinPaid.Quantity * coinPaid.Value;
                }

                if(totalPaid < totalChangeOnHand)
                {
                    // remove all the ordered drinks
                    foreach (Drink orderDrink in order.Drinks)
                    {
                        currDrink = vendingMachine.Drinks.Find(d => d.Name == orderDrink.Name);

                        currDrink.Quantity -= orderDrink.Order;

                        cost += orderDrink.Value * orderDrink.Order;
                    }

                    int change = totalPaid - cost;

                    if (totalPaid > 0)
                    {
                        order.Coins = new List<Coin>();

                        order.Coins.Add(ProcessCoinChange(CoinType.Quarter, 25, ref vendingMachine, ref change));
                        order.Coins.Add(ProcessCoinChange(CoinType.Dime, 10, ref vendingMachine, ref change));
                        order.Coins.Add(ProcessCoinChange(CoinType.Nickel, 5, ref vendingMachine, ref change));
                        order.Coins.Add(ProcessCoinChange(CoinType.Penny, 1, ref vendingMachine, ref change));
                    }
                }
                else
                {
                    order = null;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return order;
        }

        #endregion
        #region Private Methods

        private int calculateCoinOnHand(VendingMachine vendingMachine)
        {
            int total = 0;

            try
            {
                foreach(Coin coin in vendingMachine.Coins){
                    total += coin.Quantity * coin.Value;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return total;
        }

        private List<Drink> GetStockDrinks(string drinkName, int drinkPrice, int quantity)
        {
            List<Drink> retval = null;
            Drink drink = null;

            // Create a list of the requested drink
            try
            {
                drink = new Drink
                {
                    Name = drinkName,
                    Value = drinkPrice
                };

                for (int i = 0; i < quantity; i++)
                {
                    retval.Add(drink);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ERROR_GET_STOCK_DRINKS + ex);
            }

            return retval;
        }

        private Coin ProcessCoinChange(CoinType type, int coinValue,ref VendingMachine vendingMachine,ref int change)
        {
            Coin currCoin = null;
            int totalPossibleChangeForCoin = 0;
            Coin currVendingCoin = null;

            try
            {
                currCoin = new Coin
                {
                    CoinType = type,
                    Value = coinValue,
                    Quantity = 0
                };

                currVendingCoin = vendingMachine.Coins.Find(c => c.CoinType == type);
                // If we have the coins required to pay them back
                if (currVendingCoin.Quantity > 0)
                {
                    totalPossibleChangeForCoin = change / currCoin.Value;

                    // If we dont have enough coins that we could pay, just use as much as we can
                    if (totalPossibleChangeForCoin > currVendingCoin.Quantity)
                    {
                        currCoin.Quantity = currVendingCoin.Quantity;
                        currVendingCoin.Quantity -= currCoin.Quantity;
                    }
                    else
                    {
                        currCoin.Quantity = totalPossibleChangeForCoin;
                        currVendingCoin.Quantity -= totalPossibleChangeForCoin;
                    }
                }

                change -= currCoin.Quantity * currCoin.Value;

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return currCoin;
        }

        private List<Coin> GetStockCoins(CoinType coinType, int quantity, int value)
        {
            List<Coin> retval = null;
            Coin coin = null;

            // Create a list of the requested coin
            try
            {
                coin = new Coin
                {
                    CoinType = coinType,
                    Value = value
                };

                for (int i = 0; i < quantity; i++)
                {
                    retval.Add(coin);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ERROR_GET_STOCK_COINS + ex);
            }

            return retval;
        }

        #endregion
    }
}
