using System;
using ImageMagick;

namespace Igloo.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            using var imgBase = new MagickImage("./Assets/Base.png");
            using var step1 = new MagickImage("./Assets/Masks/Step1.png");
            using var step2 = new MagickImage("./Assets/Masks/Step2.png");

            // TODO: USE random texture OR colour
            using var faceBackFILL = new MagickImage(new MagickColor("#0000FF"), step1.Width, step1.Height);
            
            // TODO: USE random texture OR colour
            using var faceEdgeFILL = new MagickImage(new MagickColor("#000000"), step2.Width, step2.Height);
            
            imgBase.SetWriteMask(step1);
            imgBase.Composite(faceBackFILL, CompositeOperator.Over);
            
            imgBase.SetWriteMask(step2);
            imgBase.Composite(faceEdgeFILL, CompositeOperator.Over);
            
            imgBase.Write("test.png");
        }
    }
}
