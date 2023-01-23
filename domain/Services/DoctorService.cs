using domain.Logic.Interfaces;
using domain.Logic;
using domain.Classes;

namespace domain.Services;

public class DoctorService
{
	private readonly IDoctorRepository _db;
	public DoctorService(IDoctorRepository db)
	{
		_db = db;
	}

    public async Task<Result<IEnumerable<Doctor>>> GetAllDoctors()
    {
        return Result.Ok(await _db.GetItemsList());
    }

	public async Task<Result<IEnumerable<Doctor?>>> FindDoctorsBySpecialty(int specialtyId)
	{
        if (specialtyId < 0)
            return Result.Fail<IEnumerable<Doctor?>>("Invalid specialty id");

        var res = await _db.FindDoctorsBySpecialty(specialtyId);
		return res is not null ? Result.Ok(res)
			: Result.Fail<IEnumerable<Doctor?>>("Unable to find doctor");
	}

    public async Task<Result<Doctor>> FindDoctor(int specialtyId)
    {
		if (specialtyId < 0)
			return Result.Fail<Doctor>("Invalid specialty id");

        var doctor = await _db.FindDoctor(specialtyId);
        return doctor != null ? Result.Ok(doctor)
            : Result.Fail<Doctor>("Unable to find doctor");
    }

    public async Task<Result<Doctor>> Create(Doctor doctor)
	{
		var result = doctor.IsValid();
		if (result.IsFailure)
			return Result.Fail<Doctor>("Invalid doctor: " + result.Error.ToLower());

		return await _db.Create(doctor) ? Result.Ok(doctor)
			: Result.Fail<Doctor>("Unable to create doctor");
	}

    public async Task<Result<Doctor>> Delete(int id)
	{
        var result = await _db.GetItem(id);
        if (result == null)
            return Result.Fail<Doctor>("Unable to find doctor");

		return await _db.Delete(id) ? Result.Ok(result) :
			Result.Fail<Doctor>("Unable to delete doctor");
	}

    public async Task<Result<Doctor>> GetItem(int id)
    {
        var result = await _db.GetItem(id);
        return result != null ? Result.Ok(result) :
            Result.Fail<Doctor>("Unable to find doctor");
    }
}