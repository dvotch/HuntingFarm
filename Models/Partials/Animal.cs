using System;
using System.IO;

namespace HuntingFarm.Models
{
    public partial class Animal
    {
        public string GetAnimalImage
        {
            get {
                if (Image is null)
                    return Directory.GetCurrentDirectory() + @"\Images\default.png";
                return Directory.GetCurrentDirectory() + @"\Images\Animals\" + Image.Trim();
            }
        }
    }
}
