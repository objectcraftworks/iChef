using Machine.Specifications;
using iChef.Domain;

namespace iChef.Tests
{
    public class When_Taking_Order
    {

        It should_be_create_ticket = () =>
            {
                _orderInput = "morning,1,3,2,5";
                _ticket = Chef.CreateTicketFrom(_orderInput);
                _ticket.TimeOfDay.ShouldEqual("morning");
                _ticket.Dishes.ShouldEqual(new[] { DishType.Entree, DishType.Drink, DishType.Side, DishType.Unknown });

            };

        It should_allow_case_insentive = () =>
            {
                _orderInput = "Morning,1,3,2";
                _ticket = Chef.CreateTicketFrom(_orderInput);
                _ticket.TimeOfDay.ShouldEqual("morning");

            };

        static Ticket _ticket;
        static string _orderInput;
    }
}