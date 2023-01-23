using domain.Classes;
using domain.Logic;
using domain.Logic.Interfaces;

namespace domain.Services;

public class AppointmentService
{
	private readonly IAppointmentRepository _db;

	public AppointmentService(IAppointmentRepository db)
	{
		_db = db;
	}

    public Result<Appointment> SaveAppointment(Schedule schedule,
        Appointment appointment)
    {
        var result = schedule.IsValid();
        if (result.IsFailure)
            return Result.Fail<Appointment>("Invalid schedule: " + result.Error.ToLower());

        result = appointment.IsValid();
        if (result.IsFailure)
            return Result.Fail<Appointment>("Invalid appointment: " + result.Error.ToLower());
        
        if (schedule.WorkdayStartTime > appointment.StartTime ||
            schedule.WorkdayEndTime < appointment.EndTime)
            return Result.Fail<Appointment>("Appointment out of schedule");

        var appointmentsList = _db.GetAppointments(appointment.DoctorId).ToList();
        appointmentsList.Sort((a, b) => {
            return  b.StartTime < a.StartTime ? 1 : -1 ;
        });

        var position = appointmentsList.FindLastIndex(
            a => a.EndTime < appointment.StartTime);

        if (appointmentsList.Count > position + 1)
        {
            if (appointmentsList[position + 1].StartTime < appointment.EndTime)
                return Result.Fail<Appointment>("This time already taken");
        }

        return _db.Create(appointment) ? Result.Ok(appointment) :
            Result.Fail<Appointment>("Unable to save the appointment");

    }

    public Result<IEnumerable<DateTime>> GetFreeAppointments(Specialty
        specialty)
    {
        var result = specialty.IsValid();
        if (result.IsFailure)
            return Result.Fail<IEnumerable<DateTime>>
                ("Invalid specialization: " + result.Error.ToLower());

        return Result.Ok(_db.GetFreeAppointments(specialty));
    }
}