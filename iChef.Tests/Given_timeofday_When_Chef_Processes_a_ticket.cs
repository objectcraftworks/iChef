using System.Linq;
using Machine.Specifications;
using iChef.Domain;
using iChef.Tests.TestData;

namespace iChef.Tests
{
    public class Given_timeofday_When_Chef_Processes_a_ticket
    {
        private Establish context = () =>
            {
                _ticket = new Ticket("night", new[] {DishType.Drink, DishType.Drink});
                //The test data factory doesnot have an entry for "night"
                _sut =  ChefFactory.CreateChef();
            };

        private Because of = () =>
            {
                _order = _sut.ProcessOrder(_ticket);
            };

        It should_serve_dishes_for_the_time_of_day = () =>
            {
                _order.Items.Count().ShouldEqual(1);
                _order.Items.ElementAt(0).DishName.ShouldEqual("Error");
            };

               
        static Chef _sut;
        static Ticket _ticket;
        static Order _order;
    }
}