using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntingFarm.Models
{
    public partial class User
    {
        public string GetPhoto
        {
            get 
            {
                if (Image == null) return Directory.GetCurrentDirectory() + @"\Images\default.png";
                if (RoleId == 1) return Directory.GetCurrentDirectory() + @"\Images\Clients\" + Image.Trim();
                if (RoleId == 2) return Directory.GetCurrentDirectory() + @"\Images\Administrators\" + Image.Trim();
                return Directory.GetCurrentDirectory() + @"\Images\default.png";
            }
        }
    }
}
