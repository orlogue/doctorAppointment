using domain.Classes;

namespace domain.Logic.Interfaces;

public interface IAppointmentRepository : IRepository<Appointment>
{
    Task<IEnumerable<DateTime>> GetFreeAppointments(Specialty specialization);
    Task<IEnumerable<Appointment>> GetAppointments(int doctorId);
}