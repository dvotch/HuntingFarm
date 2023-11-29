using System;
using System.IO;

namespace HuntingFarm.Models
{
    public partial class Animal
    {
        public string GetImage
        {
            get {
                if (Image is null)
                    return Directory.GetCurrentDirectory() + @"\Assets\Animals\default.png";
                return Directory.GetCurrentDirectory() + @"\Assets\Animals\" + Image.Trim();
            }
        }
    }
}
