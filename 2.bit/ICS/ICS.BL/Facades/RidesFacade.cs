using AutoMapper;
using ICS.BL.Models;
using ICS.DAL.Entity;
using ICS.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ICS.BL.Facades;

public class RidesFacade : CRUDFacade<RidesEntity, RidesListModel, RidesDetailModel>
{
    public RidesFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
    {
    }

    public new async Task<RidesDetailModel> SaveAsync(RidesDetailModel ride)
    {
        ride.Duration = (uint)(ride.EndTime - ride.StartTime).TotalMinutes;
        return await base.SaveAsync(ride);
    }

    public async Task deleteRide(Guid Id)
    {
        await this.DeleteAsync(Id);
    }

    public async Task<IEnumerable<RidesListModel>> FilterRides(
        string? StartDestination,
        DateTime? StartTime,
        string? EndDestination,
        DateTime? EndTime,
        Guid? Driver,
        Guid? excludedUser,
        bool defaultStartInFuture
    )
    {
        const int timeEpsilon = 1;
        await using var uow = _unitOfWorkFactory.Create();
        var query = uow.GetRepository<RidesEntity>().Get();

        if (StartDestination != null)
            query = query.Where(e => e.startDestination.Contains(StartDestination));
        if (EndDestination != null)
            query = query.Where(e => e.endDestination.Contains(EndDestination));
        if (StartTime != null)
        {
            DateTime st = (DateTime)StartTime;
            query = query.Where(e => e.startTime > st.AddHours(-timeEpsilon) && e.startTime < st.AddHours(timeEpsilon));
        }
        else if (defaultStartInFuture)
        {
            query = query.Where(e => e.startTime > DateTime.Now);
        }
        if (EndTime != null)
        {
            DateTime et = (DateTime)EndTime;
            query = query.Where(e => e.endTime > et.AddHours(-timeEpsilon) && e.endTime < et.AddHours(timeEpsilon));
        }
        if (Driver != null)
            query = query.Where(e => e.DriverId == Driver);
        if (excludedUser != null)
        {
            query = query.Where(e => e.DriverId != excludedUser);
            query = query.Where(e => !e.Carpoolers.Any(c => c.CodriverId == excludedUser));
        }

        query = query.OrderByDescending(e => e.startTime);

        return await _mapper.ProjectTo<RidesListModel>(query).ToArrayAsync().ConfigureAwait(false);
    }

    public async Task<bool> HasUserFreeTimeAsync(Guid UserId, DateTime StartTime, DateTime EndTime)
    {
        var uow = _unitOfWorkFactory.Create();
        var query = uow.GetRepository<RidesEntity>().Get();
        query = query.Where(e => e.DriverId == UserId || e.Carpoolers.Any(c => c.CodriverId == UserId));
        query = query.Where(e => e.endTime > StartTime && e.startTime < EndTime);

        var result = await _mapper.ProjectTo<RidesListModel>(query).ToArrayAsync().ConfigureAwait(false);

        if (result == null || result.Any())
        {
            return false;
        }
        return true;
    }
}