using DaisyStudy.Data;
using DaisyStudy.Models.Catalog.Contact;
using DaisyStudy.Models.Common;

namespace DaisyStudy.Application.Catalog.Contacts;
public interface IContactService
{
    Task<bool> Create(Contact request);
    Task<bool> Delete(int ContactID);
    Task<Contact> GetById(int ContactID);
    Task<PagedResult<Contact>> GetAllPaging(GetAllPagingRequest request);
}