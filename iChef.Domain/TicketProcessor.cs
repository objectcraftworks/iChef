using System;
using System.Collections.Generic;
using System.Linq;

namespace iChef.Domain
{
    public class TicketProcessor
    {
        public Order ProcessTicket(Menu menu,Ticket ticket)
        {
            var orderItems = new List<OrderItem>();
            try
            {
                var selectionMenuItemMap = MapSelectionsWithTimeOfDayMenu(menu, ticket);
                foreach (var selectedItemMap in selectionMenuItemMap)
                {
                    ThrowIfSelectionIsUnknown(selectedItemMap);
                    var menuItem = selectedItemMap.MenuItem;
                    var selectedItem = selectedItemMap.DishType;
                    var itemInOrder = FindItem(orderItems,menuItem.DishType);
                    var orderHasItem = itemInOrder != null;
                    if (orderHasItem)
                    {
                        ThrowIfExceedesAllowedItems(itemInOrder, menuItem);
                        IncrementItem(itemInOrder);
                    }
                    else
                    {
                        AddItem(orderItems, new OrderItem(selectedItem, menuItem.Dish));
                    }
                }
            }
            catch
            {
                AddItem(orderItems,OrderItem.ErrorItem); //ErrorItem is a special condition, modeling a error card served
            }
            return  CreateOrderFor(orderItems);
        }

      

        IEnumerable<Map> MapSelectionsWithTimeOfDayMenu(Menu menu, Ticket ticket)
        {
            var menuItems = menu.GetMenuItems(ticket.TimeOfDay);
            ThrowIfTimeOfDayIsMissingMenu(menuItems);
            return (from selection in ticket.Dishes
                                       join menuItem in menuItems on selection equals
                                           menuItem.DishType into map
                                       from item in map.DefaultIfEmpty()
                                       select new Map( selection, item));
        }

        void ThrowIfTimeOfDayIsMissingMenu(IEnumerable<MenuItem> menuItems)
        {
            if (menuItems == null)
            {
                throw new Exception();
                    //for context specific exception, this could be a custom exception when a story pulls that.... for now YAGNI
            }
        }

        void ThrowIfSelectionIsUnknown(Map selectionMap)
        {
            if (selectionMap.MenuItem== null)
            {
                throw new Exception();
                    //for context specific exception, this could be a custom exception when a story pulls that.... for now YAGNI
            }
        }

        void IncrementItem(OrderItem itemInOrder)
        {
            if (itemInOrder != null)
            {
                itemInOrder.Quantity++;
            }
        }

        void ThrowIfExceedesAllowedItems(OrderItem itemInOrder, MenuItem menuItem)
        {
            if (itemInOrder != null)
            {
                if (itemInOrder.Quantity >= menuItem.AllowedItems)
                {
                    throw new Exception();
                }
            }
        }

        static void AddItem(ICollection<OrderItem> orderItems, OrderItem item)
        {
            orderItems.Add(item);
        }

        static OrderItem FindItem(IEnumerable<OrderItem> orderItems, DishType dishType)
        {
            return orderItems.FirstOrDefault(oi => oi.DishType == dishType);
        }
       
        static Order CreateOrderFor(IEnumerable<OrderItem> orderItems)
        {
            var order = new Order();
            order.AddItems(orderItems);
            return order;
        }
        private class Map
        {
            readonly DishType _dishType;
            readonly MenuItem _menuItem;

            public Map(DishType dishType, MenuItem menuItem)
            {
                _dishType = dishType;
                _menuItem = menuItem;
            }

            public DishType DishType
            {
                get { return _dishType; }
            }

            public MenuItem MenuItem
            {
                get { return _menuItem; }
            }
        }
    }
}