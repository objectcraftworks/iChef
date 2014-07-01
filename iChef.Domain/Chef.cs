using System;
using System.Collections.Generic;
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
            //Ticket processing could be delegateed to someone else(hint:strategy pattern) 
            // if and when chef can not handle or need different expertise,
            //for now chef handles ticket processing 
            // OCP to extend the processTicket
            _processTicket = processTicket ?? ProcessTicket;
        }

        public Order ProcessOrder(Ticket ticket)
        {
            return  _processTicket(_menu,ticket);
        }

        private static Order ProcessTicket(Menu menu,Ticket ticket)
        {
            var orderItems = new List<OrderItem>();
            try
            {
                var menuItems = menu.GetMenuItems(ticket.TimeOfDay);
                if (menuItems == null)
                {
                    throw new Exception(); //for context specific exception, this could be a custom exception when a story pulls that.... for now YAGNI
                }
                var selectionMenuItemMap = from selection in ticket.Dishes
                                           join menuItem in menuItems on selection equals
                                               menuItem.DishType into map
                                           from item in map.DefaultIfEmpty()
                                           select new {Selection = selection, MenuItem = item};
                foreach (var selectedItem in selectionMenuItemMap.ToList())
                {
                    var menuItem = selectedItem.MenuItem;
                   
                    if (menuItem == null)
                    {
                        throw new Exception(); //for context specific exception, this could be a custom exception when a story pulls that.... for now YAGNI
                    }

                    var itemInOrder = orderItems.FirstOrDefault(oi => oi.DishType == menuItem.DishType);

                    if (itemInOrder != null)
                    {
                        if (itemInOrder.Quantity < menuItem.AllowedItems)
                        {
                            itemInOrder.Quantity++;
                        }
                        else
                        {
                            throw new Exception();
                        }
                        continue;
                    }

                    orderItems.Add(new OrderItem(selectedItem.Selection, selectedItem.MenuItem.Dish));
                }
            }
            catch
            {
                orderItems.Add(OrderItem.ErrorItem);//ErrorItem is a special condition, modeling a error card served
            }
            var order = new Order();
            order.AddItems(orderItems);
            return order;
        }
    }
}