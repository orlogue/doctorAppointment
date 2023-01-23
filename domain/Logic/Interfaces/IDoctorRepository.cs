using domain.Classes;

namespace domain.Logic.Interfaces;

public interface IDoctorRepository : IRepository<Doctor>
{
    Task<IEnumerable<Doctor?>> FindDoctorsBySpecialty(int specialtyId);
    Task<Doctor?> FindDoctor(int specialtyId);
}