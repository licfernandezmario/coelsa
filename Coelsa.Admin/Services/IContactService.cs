using Coelsa.Infrastructure.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coelsa.Admin.Services
{
    public interface IContactService
    {
        Task<IEnumerable<ContactDto>> GetAll();
        Task<ContactPagedDto> GetAllPaged(int page, int size);
        Task<ContactDto> GetById(int id);
        Task<ContactDto> Create(ContactDto dto);
        Task<ContactDto> Update(ContactDto dto, int id);
        Task<bool> Delete(int id);
    }
}
