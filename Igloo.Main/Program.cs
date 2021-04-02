using ImageMagick;

namespace Igloo.Main
{
    class Program
    {
        static void Main()
        {
            using var imgBase = new MagickImage("./Assets/Base.png");
            
            using var step1 = new MagickImage("./Assets/Masks/Step1.png");
            using var step2 = new MagickImage("./Assets/Masks/Step2.png");
            using var step3 = new MagickImage("./Assets/Masks/Step3.png");
            using var step4 = new MagickImage("./Assets/Masks/Step4.png");
            using var step5 = new MagickImage("./Assets/Masks/Step5.png");

            // TODO: USE random texture OR colour
            using var faceBackFILL = new MagickImage(new MagickColor("#F1F1F1"), step1.Width, step1.Height);
            
            // TODO: USE random texture OR colour
            using var faceEdgeFILL = new MagickImage(new MagickColor("#000000"), step2.Width, step2.Height);
            
            // TODO: USE random texture OR colour
            using var noseFullFILL = new MagickImage(new MagickColor("#F7931E"), step3.Width, step3.Height);
            // TODO: USE random texture OR 
            // TODO: SHOULD compliment full nose
            using var noseTopFILL = new MagickImage(new MagickColor("#FFAC5A"), step4.Width, step4.Height);
            
            // TODO: USE random texture OR colour
            // TODO: Could change colours between blushes
            using var blushFILL = new MagickImage(new MagickColor("#F49CC5"), step5.Width, step5.Height);
            
            imgBase.SetWriteMask(step1);
            imgBase.Composite(faceBackFILL, CompositeOperator.Over);
            
            imgBase.SetWriteMask(step2);
            imgBase.Composite(faceEdgeFILL, CompositeOperator.Over);
            
            imgBase.SetWriteMask(step3);
            imgBase.Composite(noseFullFILL, CompositeOperator.Over);
            imgBase.SetWriteMask(step4);
            imgBase.Composite(noseTopFILL, CompositeOperator.Over);
            
            imgBase.SetWriteMask(step5);
            imgBase.Composite(blushFILL, CompositeOperator.Over);
            
            imgBase.Write("pengu.png");
        }
    }
}
