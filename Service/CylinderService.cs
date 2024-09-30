using GASSBOOKING_WEBSITE.Interface;
using GASSBOOKING_WEBSITE.Models;
using GASSBOOKING_WEBSITE.Repositories;

namespace GASSBOOKING_WEBSITE.Services
{
    public class CylinderService : ICylinderService
    {
        private readonly CylinderRepository _cylinderRepository;

        public CylinderService(CylinderRepository cylinderRepository)
        {
            _cylinderRepository = cylinderRepository;
        }

        public IEnumerable<Cylinder> GetAllCylinders()
        {
            return _cylinderRepository.GetAllCylinders();
        }

        public Cylinder GetCylinderById(int id)
        {
            return _cylinderRepository.GetCylinderById(id);
        }

        public void AddCylinder(Cylinder cylinder)
        {
            _cylinderRepository.AddCylinder(cylinder);
        }

        public void UpdateCylinder(Cylinder cylinder)
        {
            _cylinderRepository.UpdateCylinder(cylinder);
        }

        public void DeleteCylinder(int id)
        {
            _cylinderRepository.DeleteCylinder(id);
        }

        public async Task<IEnumerable<Cylinder>> GetCylinderTypesAsync()
        {
            return await _cylinderRepository.GetCylinderTypesAsync();
        }

        public async Task<Cylinder> GetCylinderByIdAsync(int cylinderId) // Implement the method
        {
            return await _cylinderRepository.GetCylinderByIdAsync(cylinderId);
        }
    }

}
