using domain.Logic;
namespace domain.Classes;

public class Appointment
{
    public Int64 PatientId;
    public int DoctorId;
    public DateTime StartTime;
    public DateTime EndTime;

    public Appointment() : this(0, 0, DateTime.MinValue, DateTime.MinValue) {}

    public Appointment(Int64 patientId, int doctorId, DateTime startTime,
        DateTime endTime)
    {
        PatientId = patientId;
        DoctorId = doctorId;
        StartTime = startTime;
        EndTime = endTime;
    }

    public Result IsValid()
    {
        if (PatientId < 0)
            return Result.Fail("Invalid patient id");
        
        if (DoctorId < 0)
            return Result.Fail("Invalid doctor id");

        if (StartTime > EndTime)
            return Result.Fail("Invalid time");

        return Result.Ok();
    }
}