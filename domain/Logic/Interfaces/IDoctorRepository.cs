using domain.Classes;

namespace domain.Logic.Interfaces;

public interface IDoctorRepository : IRepository<Doctor>
{
    IEnumerable<Doctor?> FindDoctorsBySpecialty(int specialtyId);
    Doctor? FindDoctor(int specialtyId);
}