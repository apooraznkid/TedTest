using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TedTest.Models;
using System.Web.SessionState;

namespace TedTest.Controllers
{
    public class InventoryController : Controller
    {
        //add item
        public Notification AddItem(Item item, Inventory inventory)
        {

            Error error = Validation(item, inventory);
            Notification notification = new Notification();

            //if there is no error, try to add item
            if (error.error == false)
            {
                try
                {
                    inventory.items.Add(item);
                    notification.message = item.label + " added";                
                    return notification;
                }
                //for unexpected errors such as invalid input
                catch (Exception ex)
                {
                    notification.message = ex.ToString();
                    return notification;
                }
            }
            else
            {
                
                notification.message = error.errorMessage;
                return notification;
            }
            
        }

        //remove item
        public Notification RemoveItem(string label, Inventory inventory)
        {
            Notification notification = new Notification();
            try
            {
                //finds first item with matching label
                Item item = inventory.items.Where(x => x.label == label).FirstOrDefault();
                
                if (item != null)
                {
                    inventory.items.Remove(item);
                    notification.message= label + " item removed";
                    return notification;
                }
                else
                {
                    notification.message = label + " item does not exist";
                    return notification;
                }
            }
            catch(Exception ex)
            {
                
                notification.message = ex.ToString();
                return notification;
            }


        }

        //check expiration
        public ExpirationNotification CheckExpiration(Inventory inventory)
        {
            ExpirationNotification notification = new ExpirationNotification();
            notification.messages = new List<string>();
            foreach (Item item in inventory.items)
            {
                if (DateTime.Now > item.expiration)
                {
                    notification.messages.Add("Item expired:" + item.label + " expiration date:" + item.expiration.ToString() + ".");
                }
            }
            return notification;
        }

        //error validation. checks for any pre existing matching labels and valid expiration date
        public Error Validation(Item item, Inventory inventory)
        {
            Error error = new Error();
            error.error = false;
            error.errorMessage = "";
            for(int i = 0; i < inventory.items.Count; i++)
            {
                if (item.label.ToUpper() == inventory.items[i].label.ToUpper())
                {
                    error.error = true;
                    error.errorMessage = item.label + " already exists in inventory.";
                }
                if (item.expiration <= DateTime.Now)
                {
                    error.error = true;
                    error.errorMessage = "Expiration date already passed.";
                }
            }
            return error;
        }
    }
}
