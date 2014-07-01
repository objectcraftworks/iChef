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

        public Chef(Menu menu,ProcessTicket processTicket)
        {
            _menu = menu;
            _processTicket = processTicket;
        }

        public Order ProcessOrder(Ticket ticket)
        {
            return  _processTicket(_menu,ticket);
        }
    }
}