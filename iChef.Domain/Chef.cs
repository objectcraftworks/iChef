using System;
using System.Linq;
namespace iChef.Domain
{
    public delegate Order ProcessTicket(Menu menu,Ticket ticket);

    public class Chef
    {
        readonly Menu _menu;
        readonly ProcessTicket _processTicket;

        public Chef(Menu menu,ProcessTicket processTicket=null)
        {
            _menu = menu;
            //ProcessTicket could be a strategy if this needs to modified, for now chef provides the default implementation
            // OCP to extend the processTicket
            _processTicket = processTicket ?? ProcessTicket;
        }

        public Order ProcessOrder(Ticket ticket)
        {
            return  _processTicket(_menu,ticket);
        }

        private static Order ProcessTicket(Menu menu,Ticket ticket)
        {
            var hasError = false;
            var processedList = new OrderItemCollection();
            try
            {
                var menuItems = menu.GetMenuItems(ticket.TimeOfDay);
                if (menuItems == null)
                {
                    throw new Exception(); //for those purists, this could be a custom exception when a story pulls that.... for now YAGNI
                }
                var selectionMenuItemMap = from selection in ticket.Dishes
                                           join menuItem in menu.GetMenuItems(ticket.TimeOfDay) on selection equals
                                               menuItem.DishType into map
                                           from item in map.DefaultIfEmpty()
                                           select new {Selection = selection, MenuItem = item};

                foreach (var selectedItem in selectionMenuItemMap.ToList())
                {
                    var menuItem = selectedItem.MenuItem;
                    if (menuItem == null)
                    {
                        throw new Exception(); //for those purists, this could be a custom exception when a story pulls that.... for now YAGNI
                    }
                    processedList.Add(new OrderItem(selectedItem.Selection, selectedItem.MenuItem.Dish,
                                                    menuItem.AllowedItems));
                }
            }
            catch
            {
                hasError = true; //if client needs Error Details, exception could be carried to Order. for now YAGNI.
            }
            var order = new Order {HasError = hasError};
            order.AddItems(processedList.ToList().AsReadOnly());
            return order;
        }
    }
}