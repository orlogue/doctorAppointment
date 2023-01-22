using domain.Classes;

namespace domain.Logic.Interfaces;

public interface IDoctorRepository : IRepository<Doctor>
{
    IEnumerable<Doctor?> FindDoctorsBySpecialty(Specialty specialty);
    Doctor? FindDoctor(Specialty specialty);
}