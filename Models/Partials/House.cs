using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntingFarm.Models
{
    public partial class House
    {
        public string GetImage
        {
            get
            {
                if (Image is null)
                    return Directory.GetCurrentDirectory() + @"\Assets\Images\default.png";
                return Directory.GetCurrentDirectory() + @"\Assets\Images\" + Image.Trim();
            }
        }
    }
}
