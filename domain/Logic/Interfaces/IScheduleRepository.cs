using domain.Classes;
namespace domain.Logic.Interfaces;

public interface IScheduleRepository : IRepository<Schedule>
{
    Task<IEnumerable<Schedule>> GetScheduleByDoctor(int id);
}