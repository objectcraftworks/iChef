using Machine.Specifications;
using iChef.Domain;
using iChef.Tests.TestData;

namespace iChef.Tests
{
    public class Given_invalid_dish_selection_When_Chef_Processes_a_ticket
    {
        private Establish context = () =>
            {
                _ticket = new Ticket("morning", new[] { DishType.Unknown });
                _sut = ChefFactory.CreateChef();
            };

        private Because of = () =>
            {
                _order = _sut.ProcessOrder(_ticket);
            };


        It should_be_error = () =>
            {
                _order.ShouldNotBeNull();
                _order.Items.ShouldNotBeNull();
                _order.HasError.ShouldEqual(true);
            };

        static Chef _sut;
        static Ticket _ticket;
        static Order _order;
    }
}