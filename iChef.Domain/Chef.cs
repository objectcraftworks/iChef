using System.Collections.Generic;
using System.Linq;
namespace iChef.Domain
{
    public class Chef
    {
        readonly Menu _menu;

        public Chef(Menu menu)
        {
            _menu = menu;
        }

        public Order ProcessOrder(Ticket ticket)
        {
            var order = new Order();
            var menuItems = _menu.GetMenuItems(ticket.TimeOfDay);
            if (menuItems == null)
            {
                order.HasError = true;
                return order;
            }

            var selectionMenuItemMap = from selection in ticket.Dishes 
                        join menuItem in _menu.GetMenuItems(ticket.TimeOfDay) on selection equals menuItem.DishType into map
                        from item in map.DefaultIfEmpty()
                        select new {Selection =selection, MenuItem=item};
            selectionMenuItemMap = selectionMenuItemMap.ToList();
           
            
            var processedList = new Dictionary<DishType, OrderItem>();
            foreach (var selectedItem in selectionMenuItemMap)
            {
                if(selectedItem.MenuItem ==null)
                {
                    order.HasError = true;
                    break;
                }
                if( !CanAddItem(processedList, selectedItem.MenuItem.DishType, selectedItem.MenuItem))
                {
                    order.HasError = true;
                    break;
                }
            
                if (processedList.ContainsKey(selectedItem.Selection))
                    processedList[selectedItem.Selection].Quantity++;
                else
                {
                    processedList.Add(selectedItem.Selection,
                                      new OrderItem(selectedItem.Selection, selectedItem.MenuItem.Dish));
                }
            }

            foreach (var item in processedList)
            {
                order.AddItem(item.Value); 
            }
            return order;
        }

        bool CanAddItem(Dictionary<DishType, OrderItem> processedList,DishType dishType, MenuItem menuItem)
        {
            return processedList.Count(i => i.Key == dishType) < menuItem.AllowedItems;
        }

        public static Ticket CreateTicketFrom(string orderInput)
        {
            var split = orderInput.Split(',');
            var timeOfDay = split[0].Trim().ToLower();
            
            List<DishType> dishTypes = new List<DishType>();
            for (int i = 1; i < split.Length; i++)
            {
                int result;
                if (int.TryParse(split[i].Trim(), out result))
                {
                    dishTypes.Add(DishType.GetByCode(result));
                }
                else
                {
                    dishTypes.Add(DishType.Unknown);
                }
            }
            return new Ticket(timeOfDay, dishTypes);

        }

    }
}