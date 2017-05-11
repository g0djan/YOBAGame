using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YOBAGame
{
    public interface GameObject<T>
    {
        Tuple<float, float> Move(object sender, EventArgs args);
        T GenerateSomeObject(object sender, EventArgs args);
    }
}