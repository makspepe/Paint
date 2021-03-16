using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Drawing;
    
namespace PluginInterface
{
    public interface IPlugin
    {
        string Name { get; }
        string Author { get; }
        Bitmap Transform(Bitmap app);
    }
    public class VersionAttribute : Attribute
    {
        public int Major { get; private set; } //версия X.0
        public int Minor { get; private set; } //версия 0.X
        public VersionAttribute(int major, int minor)
        {
            Major = major;
            Minor = minor;
        }
    }

}
