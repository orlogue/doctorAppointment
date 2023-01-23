using domain.Classes;
using domain.Logic.Interfaces;

namespace domain.Logic.Interfaces;

public interface ISpecialtyRepository : IRepository<Specialty>
{
	Task<Specialty?> GetByName(string name);
}
