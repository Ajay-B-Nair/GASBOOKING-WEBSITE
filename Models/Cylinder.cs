namespace GASSBOOKING_WEBSITE.Models
{
    public class Cylinder
    {
        public int CylinderId { get; set; }
        public string CylinderType { get; set; }
        public int CylinderTotalStock { get; set; }
        public int FilledCylinder { get; set; }
        public int EmptyCylinder { get; set; }
        public decimal Amount { get; set; }
    }
}
