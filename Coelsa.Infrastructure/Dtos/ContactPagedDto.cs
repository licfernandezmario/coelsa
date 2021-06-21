using System.Collections.Generic;

namespace Coelsa.Infrastructure.Dtos
{
    public class ContactPagedDto : PagedDto
    {
        public int TotalItems { get; set; }
        public List<ContactDto> Items { get; set; }
    }
}
