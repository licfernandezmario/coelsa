using APICoelsa.Controllers;
using Coelsa.Admin.Services;
using Coelsa.Infrastructure.Dtos;
using Moq;
using Xunit;

namespace Coelsa.Test
{
    public class ContactControllerTest
    {
        [Fact]
        public async void GetById_BadRequest()
        {            
            var mockContactService = new Mock<IContactService>();
            
            ContactDto item = new ContactDto();
                        
            ContactController controller = new ContactController(mockContactService.Object);
                        
            var result = await controller.GetById(0);

            Assert.Equal("Please enter an id is a required data", ((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result).Value);
        }


        [Fact]
        public async void GetById_StatusCode200()
        {
            var mockContactService = new Mock<IContactService>();

            ContactDto item = new ContactDto
            {
                Id = 1,
                FirstName = "Ivan",
                LastName = "Wassaf",
                Email = "test@test.com.ar",
                PhoneNumber = "11 4236-4598",
                Business = "COA"
            };

            mockContactService.Setup(x => x.GetById(1)).ReturnsAsync(item);

            ContactController controller = new ContactController(mockContactService.Object);

            var result = await controller.GetById(1);

            Assert.Equal(item, ((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result).Value);            
        }
    }
}
