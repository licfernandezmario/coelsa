using Coelsa.Core.Models;
using Coelsa.Infrastructure;
using Coelsa.Infrastructure.Dtos;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coelsa.Admin.Services
{
    public class ContactService : IContactService
    {
        private readonly ApplicationContext _db;

        public ContactService(ApplicationContext db)
        {
            _db = db;
        }      
                
        public async Task<IEnumerable<ContactDto>> GetAll()
        {
            return JsonConvert.DeserializeObject<List<ContactDto>>(JsonConvert.SerializeObject(await _db.Contacts.ToListAsync()));
        }

        public async Task<ContactPagedDto> GetAllPaged(int page, int size)
        {
            var contacts = await _db.Contacts
                                        .OrderBy(contact => contact.Business)
                                        .AsNoTracking()
                                        .Skip((page - 1) * size)
                                        .Take(size)
                                        .ToListAsync();
                        
            return new ContactPagedDto
            {
                PageNumber = page,
                PageSize = size,
                TotalItems = _db.Contacts.Count(),
                Items = contacts.Select(contact => new ContactDto
                {
                    Id = contact.Id,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Business = contact.Business,
                    Email = contact.Email,
                    PhoneNumber = contact.PhoneNumber
                }).ToList()
            };

        }

        public async Task<ContactDto> GetById(int id)
        {
            var contact = await _db.Contacts.FirstOrDefaultAsync(x => x.Id == id);

            return (contact != null) ? JsonConvert.DeserializeObject<ContactDto>(JsonConvert.SerializeObject(contact)) : null;
        }

        public async Task<ContactDto> Create(ContactDto dto)
        {
            var contact = JsonConvert.DeserializeObject<Contact>(JsonConvert.SerializeObject(dto));

            if (_db.Contacts.Any(x => x.LastName == dto.LastName && x.Email == dto.Email)) throw new Exception("The entered contact already exists");

            await _db.Contacts.AddAsync(contact);
            await _db.SaveChangesAsync();

            return JsonConvert.DeserializeObject<ContactDto>(JsonConvert.SerializeObject(contact));
        }

        public async Task<ContactDto> Update(ContactDto dto, int id)
        {
            var contact = JsonConvert.DeserializeObject<Contact>(JsonConvert.SerializeObject(dto));

            if (contact.Id != id) return null;

            _db.Contacts.Add(contact);
            _db.Entry(contact).State = EntityState.Modified;
            
            await _db.SaveChangesAsync();
            return JsonConvert.DeserializeObject<ContactDto>(JsonConvert.SerializeObject(contact));
        }

        public async Task<bool> Delete(int id)
        {
            var contact = _db.Contacts.FirstOrDefault(x => x.Id == id);
            if (contact != null)
            {
                _db.Contacts.Remove(contact);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
