using domain.Logic.Interfaces;
using domain.Logic;
using domain.Classes;

public class DoctorService
{
	private readonly IDoctorRepository _db;
	public DoctorService(IDoctorRepository db)
	{
		_db = db;
	}

    public Result<IEnumerable<Doctor>> GetAllDoctors()
    {
        return Result.Ok(_db.GetAllDoctors());
    }

	public Result<Doctor> FindDoctor(int id)
	{
		if (id < 0)
			return Result.Fail<Doctor>("Invalid id");

		var res = _db.FindDoctor(id);
		return res is not null ? Result.Ok(res)
			: Result.Fail<Doctor>("Unable to find doctor");
	}

    public Result<Doctor> FindDoctor(Specialty specialty)
    {
		var specialtyResult = specialty.IsValid();
		if (specialtyResult.IsFailure)
			return Result.Fail<Doctor>("Invalid specialty: "
				+ specialtyResult.Error.ToLower());

        var doctor = _db.FindDoctor(specialty);
        return doctor != null ? Result.Ok(doctor)
            : Result.Fail<Doctor>("Unable to find doctor");
    }

    public Result<Doctor> Create(Doctor doctor)
	{
		var result = doctor.IsValid();
		if (result.IsFailure)
			return Result.Fail<Doctor>("Invalid doctor: " + result.Error.ToLower());

		return _db.Create(doctor) ? Result.Ok(doctor)
			: Result.Fail<Doctor>("Unable to create doctor");
	}

    public Result<Doctor> Delete(int id)
	{
        var result = FindDoctor(id);
        if (result.IsFailure)
            return Result.Fail<Doctor>(result.Error);

		return _db.Delete(id) ? result :
			Result.Fail<Doctor>("Unable to delete doctor");
	}

}