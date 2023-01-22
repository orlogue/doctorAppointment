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
        return Result.Ok(_db.GetItemsList());
    }

	public Result<IEnumerable<Doctor?>> FindDoctorsBySpecialty(Specialty specialty)
	{
		var specialtyRes = specialty.IsValid();
		if (specialtyRes.IsFailure)
			return Result.Fail<IEnumerable<Doctor?>>("Invalid specialty: " + specialtyRes.Error);

		var res = _db.FindDoctorsBySpecialty(specialty);
		return res is not null ? Result.Ok(res)
			: Result.Fail<IEnumerable<Doctor?>>("Unable to find doctor");
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
        var result = _db.GetItem(id);
        if (result == null)
            return Result.Fail<Doctor>("Unable to find doctor");

		return _db.Delete(id) ? Result.Ok(result) :
			Result.Fail<Doctor>("Unable to delete doctor");
	}

    public Result<Doctor> GetItem(int id)
    {
        var result = _db.GetItem(id);
        return result != null ? Result.Ok(result) :
            Result.Fail<Doctor>("Unable to find doctor");
    }
}