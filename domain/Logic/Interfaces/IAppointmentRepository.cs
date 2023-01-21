using domain.Classes;

namespace domain.Logic.Interfaces;

public interface IAppointmentRepository : IRepository<Appointment>
{
    bool SaveAppointment(Appointment appointment);
    IEnumerable<Appointment> GetFreeAppointments(Specialty specialization);
    IEnumerable<Appointment> GetAppointments(int doctorId);
}