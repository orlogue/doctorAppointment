using domain.Classes;
namespace domain.Logic.Interfaces;

public interface IScheduleRepository : IRepository<Schedule>
{
    IEnumerable<Schedule> GetSchedule(Doctor doctor);
    bool CreateSchedule(Doctor doctor, Schedule schedule);
    bool UpdateSchedule(Doctor doctor, Schedule schedule);
}