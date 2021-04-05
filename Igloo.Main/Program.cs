using System.IO;
using System.Threading.Tasks;
using ImageMagick;

namespace Igloo.Main
{
	class Program
	{
		private static readonly MagickImage Step1 = new MagickImage("./Assets/Masks/Step1.png");
		private static readonly MagickImage Step2 = new MagickImage("./Assets/Masks/Step2.png");
		private static readonly MagickImage Step3 = new MagickImage("./Assets/Masks/Step3.png");
		private static readonly MagickImage Step4 = new MagickImage("./Assets/Masks/Step4.png");
		private static readonly MagickImage Step5 = new MagickImage("./Assets/Masks/Step5.png");
		private static readonly MagickImage ImgBaseSize = new MagickImage("./Assets/Base.png");
		private static readonly MagickColor NoseFullFillColour = new MagickColor("#F7931E");
		private static readonly MagickColor NoseTopFillColour = new MagickColor("#FFAC5A");
		private static readonly MagickColor BlushFillColour = new MagickColor("#F49CC5");
		
		public static async Task Main()
		{
			PenguletsList pengulets = Config.Read();

			var tasks = new Task[pengulets.Pengulets.Count];

			for (var i = 0; i < pengulets.Pengulets.Count; i++)
			{
				var pengulet = pengulets.Pengulets[i];
				tasks[i] = WritePenguletAsync(pengulet);
			}

			await Task.WhenAll(tasks);
			Dispose();
		}

		public static async Task WritePenguletAsync(Pengulet pengulet)
		{
			int height = ImgBaseSize.Height;
			int width = ImgBaseSize.Width;

			// TODO: Switch between file or image IF the "Value" contain "#"
			var imgBase = new MagickImage(new MagickColor(pengulet.Steps[0].Value), width, height);

			// TODO: Switch between file or image IF the "Value" contain "#"
			using var faceBackFill = new MagickImage(new MagickColor(pengulet.Steps[1].Value), Step1.Width, Step1.Height);

			// TODO: Switch between file or image IF the "Value" contain "#"
			using var faceEdgeFill = new MagickImage(new MagickColor(pengulet.Steps[2].Value), Step2.Width, Step2.Height);

			// TODO: USE random texture OR colour
			using var noseFullFill = new MagickImage(NoseFullFillColour, Step3.Width, Step3.Height);
			// TODO: USE random texture OR 
			// TODO: SHOULD compliment full nose
			using var noseTopFill = new MagickImage(NoseTopFillColour, Step4.Width, Step4.Height);

			// TODO: USE random texture OR colour
			// TODO: Could change colours between blushes
			using var blushFill = new MagickImage(BlushFillColour, Step5.Width, Step5.Height);

			imgBase.SetWriteMask(Step1);
			imgBase.Composite(faceBackFill, CompositeOperator.Over);

			imgBase.SetWriteMask(Step2);
			imgBase.Composite(faceEdgeFill, CompositeOperator.Over);

			imgBase.SetWriteMask(Step3);
			imgBase.Composite(noseFullFill, CompositeOperator.Over);
			imgBase.SetWriteMask(Step4);
			imgBase.Composite(noseTopFill, CompositeOperator.Over);

			imgBase.SetWriteMask(Step5);
			imgBase.Composite(blushFill, CompositeOperator.Over);

			var streamNormal = new FileStream($"{pengulet.Index.ToString()}.png", FileMode.Create);
			await imgBase.WriteAsync(streamNormal, MagickFormat.Png);
			
			imgBase.Resize(350, 350);
			
			var streamSmall = new FileStream($"{pengulet.Index.ToString()}.opensea.png", FileMode.Create);
			await imgBase.WriteAsync(streamSmall, MagickFormat.Png);
		}

		public static void Dispose()
		{
			Step1?.Dispose();
			Step2?.Dispose();
			Step3?.Dispose();
			Step4?.Dispose();
			Step5?.Dispose();
			ImgBaseSize?.Dispose();
		}
	}
}