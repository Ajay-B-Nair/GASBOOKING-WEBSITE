using GASSBOOKING_WEBSITE.Models;

namespace GASSBOOKING_WEBSITE.Interface
{
    public interface ICylinderService
    {
        IEnumerable<Cylinder> GetAllCylinders();
        Cylinder GetCylinderById(int id);
        void AddCylinder(Cylinder cylinder);
        void UpdateCylinder(Cylinder cylinder);
        void DeleteCylinder(int id);
        Task<IEnumerable<Cylinder>> GetCylinderTypesAsync();
        Task<Cylinder> GetCylinderByIdAsync(int cylinderId);


    }
}
