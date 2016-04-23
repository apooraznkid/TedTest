using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using TedTest.Controllers;
using TedTest.Models;
using System.Web.SessionState;

namespace TedTest
{
    public class Program
    {
        
        

        public static void Main(string[] args)
        {
            InventoryController inventoryController = new InventoryController();
            Inventory inventory = new Inventory();

            inventory.items = new List<Item>();

            //prepopulate items
            Item item1 = new Item("xxx-000","Dairy",DateTime.Now.AddDays(2));
            inventory.items.Add(item1);
           
            Item item2 = new Item("xxx-001","Dairy",DateTime.Now.AddDays(1));
            inventory.items.Add(item2);

            Item item3 = new Item("xxx-002","Meat",DateTime.Now.AddSeconds(12));
            inventory.items.Add(item3);

            Item item4 = new Item("xxx-003","Fruit",DateTime.Now.AddDays(-2));
            inventory.items.Add(item4);
            
            //item to add
            Item itemToAdd = new Item("xxx-004","car",DateTime.Now.AddYears(2));

            //remove item
            Notification notification1 = inventoryController.RemoveItem("xxx-000", inventory);
            Console.WriteLine(notification1.message);

            //add item
            Notification notification = inventoryController.AddItem(itemToAdd, inventory);
            Console.WriteLine(notification.message);

            //list items in inventory
            for (int i = 0; i < inventory.items.Count; i++)
            {
                Console.WriteLine(inventory.items[i].label);
            }

            //Background thread that checks for expired items in inventory every 10 seconds
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                while (1 == 1)
                {
                    ExpirationNotification exNotificaiton = inventoryController.CheckExpiration(inventory);
                    if (exNotificaiton.messages != null)
                    {
                        foreach (string message in exNotificaiton.messages)
                        {
                            Console.WriteLine(message);
                        }
                    }
                    Thread.Sleep(10000);
                }
            }).Start();
            Console.ReadKey();

            
        }
    }

}
