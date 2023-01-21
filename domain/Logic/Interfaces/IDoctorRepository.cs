using domain.Classes;

namespace domain.Logic.Interfaces;

public interface IDoctorRepository : IRepository<Doctor>
{
    IEnumerable<Doctor> GetAllDoctors();
    Doctor? FindDoctor(int id);
    Doctor? FindDoctor(Specialty specialty);
    new bool Create(Doctor doctor);
    new bool Delete(int id);
}