using domain.Classes;
namespace domain.Logic.Interfaces;

public interface IScheduleRepository : IRepository<Schedule>
{
    IEnumerable<Schedule> GetScheduleByDoctor(Doctor doctor);
}