using Machine.Specifications;
using iChef.Domain;

namespace iChef.Tests
{
    public class When_Taking_Order
    {

        It should_create_ticket = () =>
            {
                _orderInput = "morning,1,3,2";
                _ticket = Ticket.CreateTicketFrom(_orderInput);
                _ticket.TimeOfDay.ShouldEqual("morning");
                _ticket.Dishes.ShouldEqual(new[] { DishType.Entree, DishType.Drink, DishType.Side  });

            };

        It should_create_unknown_dish_types_ = () =>
        {
            // 15 unknown dish type,
            _orderInput = "morning,1,15,ab";
            _ticket = Ticket.CreateTicketFrom(_orderInput);
            _ticket.TimeOfDay.ShouldEqual("morning");
            _ticket.Dishes.ShouldEqual(new[] { DishType.Entree, DishType.Unknown,DishType.Unknown });

        };
        It should_allow_case_insentive_input = () =>
            {
                _orderInput = "Morning,1,3,2";
                _ticket = Ticket.CreateTicketFrom(_orderInput);
                _ticket.TimeOfDay.ShouldEqual("morning");

            };

        static Ticket _ticket;
        static string _orderInput;
    }
}