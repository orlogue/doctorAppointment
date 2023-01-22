using domain.Classes;

namespace domain.Logic.Interfaces;

public interface IAppointmentRepository : IRepository<Appointment>
{
    IEnumerable<DateTime> GetFreeAppointments(Specialty specialization);
    IEnumerable<Appointment> GetAppointments(int doctorId);
}