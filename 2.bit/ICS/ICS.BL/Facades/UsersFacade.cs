using AutoMapper;
using ICS.BL.Models;
using ICS.DAL.Entity;
using ICS.DAL.UnitOfWork;

namespace ICS.BL.Facades;

public class UsersFacade : CRUDFacade<UsersEntity, UsersListModel, UsersDetailModel>
{
    public UsersFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
    }

    public async Task<UsersDetailModel> createNewUser(string Name, string Surname, string PhotoName)
    {
        UsersDetailModel user = new UsersDetailModel(Name, Surname, PhotoName);
        return await this.SaveAsync(user);
    }

    public async Task<UsersDetailModel?> updateUser(Guid Id, string Name, string Surname, string PhotoName)
    {
        UsersDetailModel? user = await this.GetAsync(Id);

        if (user != null)
        {
            user.Firstname = Name;
            user.Lastname = Surname;
            user.Photography = PhotoName;
            return await this.SaveAsync(user);
        }

        return null;
    }
    public async Task deleteUser(Guid Id)
    {
        await this.DeleteAsync(Id);
    }

}