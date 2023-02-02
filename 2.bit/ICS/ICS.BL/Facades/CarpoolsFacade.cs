using AutoMapper;
using ICS.BL.Models;
using ICS.DAL.Entity;
using ICS.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ICS.BL.Facades;

public class CarpoolsFacade : CRUDFacade<CarpoolsEntity, CarpoolsListModel, CarpoolsDetailModel>
{
    public CarpoolsFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
    }

    public async Task<CarpoolsDetailModel> createNewCarpool(Guid CodriverId, Guid RideId)
    {
        CarpoolsDetailModel carpool = new CarpoolsDetailModel(CodriverId, RideId);
        return await this.SaveAsync(carpool);
    }


    public async Task<IEnumerable<CarpoolsListModel>> FilterCarpools(Guid? RideId, Guid? CodriverId)
    {
        await using var uow = _unitOfWorkFactory.Create();
        var query = uow.GetRepository<CarpoolsEntity>().Get();


        if (RideId != null)
            query = query.Where(e => e.RideId == RideId);
        if (CodriverId != null)
            query = query.Where(e => e.CodriverId == CodriverId);

        query = query.OrderByDescending(e => e.Ride.startTime);

        return await _mapper.ProjectTo<CarpoolsListModel>(query).ToArrayAsync().ConfigureAwait(false);
    }

}