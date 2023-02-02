using AutoMapper;
using ICS.BL.Models;
using ICS.DAL.Entity;
using ICS.DAL.UnitOfWork;
using ICS.Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace ICS.BL.Facades;

public class CarsFacade : CRUDFacade<CarsEntity, CarsListModel, CarsDetailModel>
{
    public CarsFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {

    }

    public async Task<CarsDetailModel> createNewCar(
        Guid OwnerId,
        string OwnerName,
        string Brand,
        string Model,
        VehicleType Type,
        DateTime RegistrationDate,
        string Photography
        )
    {
        CarsDetailModel car = new CarsDetailModel(
            OwnerId,
            OwnerName,
            Brand,
            Model,
            Type,
            RegistrationDate,
            Photography
        );
        return await this.SaveAsync(car);
    }

    public async Task<CarsDetailModel?> updateCar(
        Guid Id,
        Guid OwnerId,
        string OwnerName,
        string Brand,
        string Model,
        VehicleType Type,
        DateTime RegistrationDate,
        string Photography
    )
    {
        CarsDetailModel? car = await this.GetAsync(Id);

        if (car != null)
        {
            car.OwnerId = OwnerId;
            car.OwnerName = OwnerName;
            car.Brand = Brand;
            car.Model = Model;
            car.Type = Type;
            car.RegistrationDate = RegistrationDate;
            car.Photography = Photography;

            return await this.SaveAsync(car);
        }

        return null;
    }


    public async Task<IEnumerable<CarsListModel>> FilterCars(Guid? OwnerId)
    {
        await using var uow = _unitOfWorkFactory.Create();
        var query = uow.GetRepository<CarsEntity>().Get();

        if (OwnerId != null)
            query = query.Where(e => e.OwnerId == OwnerId);

        return await _mapper.ProjectTo<CarsListModel>(query).ToArrayAsync().ConfigureAwait(false);
    }


}