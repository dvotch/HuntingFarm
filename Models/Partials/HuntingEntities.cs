using System.Data.Entity;
namespace HuntingFarm.Models
{
    public partial class HuntEntities : DbContext
    {
        private static HuntEntities _context;
        public static HuntEntities GetContext()
        {
            if (_context == null) _context = new HuntEntities();
            return _context;
        }
    }
}