#region Copyright (c) 2002-2006 EConTech JSC., All Rights Reserved
/* ---------------------------------------------------------------------*
*                           EConTech JSC.,                              *
*              Copyright (c) 2002-2006 All Rights reserved              *
*                                                                       *
*                                                                       *
* This file and its contents are protected by Vietnam and               *
* International copyright laws.  Unauthorized reproduction and/or       *
* distribution of all or any portion of the code contained herein       *
* is strictly prohibited and will result in severe civil and criminal   *
* penalties.  Any violations of this copyright will be prosecuted       *
* to the fullest extent possible under law.                             *
*                                                                       *
* THE SOURCE CODE CONTAINED HEREIN AND IN RELATED FILES IS PROVIDED     *
* TO THE REGISTERED DEVELOPER FOR THE PURPOSES OF EDUCATION AND         *
* TROUBLESHOOTING. UNDER NO CIRCUMSTANCES MAY ANY PORTION OF THE SOURCE *
* CODE BE DISTRIBUTED, DISCLOSED OR OTHERWISE MADE AVAILABLE TO ANY     *
* THIRD PARTY WITHOUT THE EXPRESS WRITTEN CONSENT OF ECONTECH JSC.,     *
*                                                                       *
* UNDER NO CIRCUMSTANCES MAY THE SOURCE CODE BE USED IN WHOLE OR IN     *
* PART, AS THE BASIS FOR CREATING A PRODUCT THAT PROVIDES THE SAME, OR  *
* SUBSTANTIALLY THE SAME, FUNCTIONALITY AS ANY ECONTECH JSC. PRODUCT.   *
*                                                                       *
* THE REGISTERED DEVELOPER ACKNOWLEDGES THAT THIS SOURCE CODE           *
* CONTAINS VALUABLE AND PROPRIETARY TRADE SECRETS OF ECONTECH JSC.,     *
* THE REGISTERED DEVELOPER AGREES TO EXPEND EVERY EFFORT TO             *
* INSURE ITS CONFIDENTIALITY.                                           *
*                                                                       *
* THE END USER LICENSE AGREEMENT (EULA) ACCOMPANYING THE PRODUCT        *
* PERMITS THE REGISTERED DEVELOPER TO REDISTRIBUTE THE PRODUCT IN       *
* EXECUTABLE FORM ONLY IN SUPPORT OF APPLICATIONS WRITTEN USING         *
* THE PRODUCT.  IT DOES NOT PROVIDE ANY RIGHTS REGARDING THE            *
* SOURCE CODE CONTAINED HEREIN.                                         *
*                                                                       *
* THIS COPYRIGHT NOTICE MAY NOT BE REMOVED FROM THIS FILE.              *
* --------------------------------------------------------------------- *
*/
#endregion Copyright (c) 2002-2006 EConTech JSC., All Rights Reserved

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace EConTech.Windows.MACUI
{
	/// <summary>
	/// Summary description for DrawMACStyleHelper.
	/// </summary>
	public static /*sealed*/ class DrawMACStyleHelper
	{
		///// <summary>
		///// The contructor 
		///// </summary>
		//private DrawMACStyleHelper()
		//{
		//	//
		//	// TODO: Add constructor logic here
		//	//
		//}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="g"></param>
		/// <param name="drawRectF"></param>
		/// <param name="drawColor"></param>
		/// <param name="orientation"></param>
		public static void DrawAquaPill(Graphics g, RectangleF drawRectF, Color drawColor, Orientation orientation)
			
		{
            LinearGradientBrush gradientBrush;
            Color color1 = ColorHelper.OpacityMix(Color.White, ColorHelper.SoftLightMix(drawColor, Color.Black, 100), 40),
                  color2 = ColorHelper.OpacityMix(Color.White, ColorHelper.SoftLightMix(drawColor, Color.FromArgb(64, 64, 64), 100), 20),
                  color3 = ColorHelper.SoftLightMix(drawColor, Color.FromArgb(128, 128, 128), 100),
                  color4 = ColorHelper.SoftLightMix(drawColor, Color.FromArgb(192, 192, 192), 100),
                  color5 = ColorHelper.OverlayMix(ColorHelper.SoftLightMix(drawColor, Color.White, 100), Color.White, 75);
            ColorBlend colorBlend = new ColorBlend();

            colorBlend.Colors = new Color[] { color1, color2, color3, color4, color5 };
            colorBlend.Positions = new float[] { 0, .25F, .5F, .75F, 1 };
            if(orientation == Orientation.Horizontal)
				gradientBrush = new LinearGradientBrush(new Point((int)drawRectF.Left, (int)drawRectF.Top-1), new Point((int)drawRectF.Left, (int)drawRectF.Top + (int)drawRectF.Height+1), color1, color5);
			else
				gradientBrush = new LinearGradientBrush(new Point((int)drawRectF.Left-1, (int)drawRectF.Top), new Point((int)drawRectF.Left + (int)drawRectF.Width+1, (int)drawRectF.Top), color1, color5);
			gradientBrush.InterpolationColors = colorBlend;
			fillPill(gradientBrush, gradientBrush, drawRectF, new RectangleF(), g);

			color2 = Color.White;
            colorBlend.Colors = new Color[] { color2, color3, color4, color5 };
            colorBlend.Positions = new float[] { 0, .5F, .75F, 1 };
            if(orientation == Orientation.Horizontal)
				gradientBrush = new LinearGradientBrush(new Point((int)drawRectF.Left + 1, (int)drawRectF.Top), new Point((int)drawRectF.Left + 1, (int)drawRectF.Top + (int)drawRectF.Height - 1), color2, color5);
			else
				gradientBrush = new LinearGradientBrush(new Point((int)drawRectF.Left, (int)drawRectF.Top + 1), new Point((int)drawRectF.Left + (int)drawRectF.Width - 1, (int)drawRectF.Top + 1), color2, color5);
			gradientBrush.InterpolationColors = colorBlend;
            fillPill(gradientBrush, gradientBrush, RectangleF.Inflate(drawRectF, -3, -3), new RectangleF(), g);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="drawRectF"></param>
        /// <param name="drawColor"></param>
        /// <param name="orientation"></param>
        public static void DrawAquaPillSingleLayer(Graphics g, RectangleF drawRectF, RectangleF trackerRectF, Color drawColor, Orientation orientation)
		{
            LinearGradientBrush gradientBrush, gradientBrush1;
            Color color1 = drawColor, color2 = ControlPaint.Light(color1), color3 = ControlPaint.Light(color2), color4 = ControlPaint.Light(color3);
            Color color11 = Color.Gray, color21 = ControlPaint.Light(color11), color31 = ControlPaint.Light(color21), color41 = ControlPaint.Light(color31);
            ColorBlend colorBlend = new ColorBlend(), colorBlend1 = new ColorBlend();

            colorBlend.Colors = new Color[] { color1, color2, color3, color4 };
            colorBlend1.Colors = new Color[] { color11, color21, color31, color41 };
            colorBlend.Positions = colorBlend1.Positions = new float[] { 0, .25F, .65F, 1 };

            if(orientation == Orientation.Horizontal)
            {
                gradientBrush = new LinearGradientBrush(new Point((int)drawRectF.Left, (int)drawRectF.Top), new Point((int)drawRectF.Left, (int)drawRectF.Top + (int)drawRectF.Height), color1, color4);
                gradientBrush1 = new LinearGradientBrush(new Point((int)drawRectF.Left, (int)drawRectF.Top), new Point((int)drawRectF.Left, (int)drawRectF.Top + (int)drawRectF.Height), color11, color41);
            }
            else
            {
                gradientBrush = new LinearGradientBrush(new Point((int)drawRectF.Left, (int)drawRectF.Top), new Point((int)drawRectF.Left + (int)drawRectF.Width, (int)drawRectF.Top), color1, color4);
                gradientBrush1 = new LinearGradientBrush(new Point((int)drawRectF.Left, (int)drawRectF.Top), new Point((int)drawRectF.Left + (int)drawRectF.Width, (int)drawRectF.Top), color11, color41);
            }
            gradientBrush.InterpolationColors = colorBlend;
            gradientBrush1.InterpolationColors = colorBlend1;

            fillPill(gradientBrush, gradientBrush1, drawRectF, trackerRectF, g);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="b"></param>
		/// <param name="rect"></param>
		/// <param name="g"></param>
		private static void fillPill(Brush b, Brush b1, RectangleF rect, RectangleF trackerRect, Graphics g)
		{
            g.SmoothingMode = SmoothingMode.HighQuality;
            if(rect.Width > rect.Height) 
			{
				g.FillEllipse(b, new RectangleF(rect.Left, rect.Top, rect.Height, rect.Height));
				g.FillEllipse(b, new RectangleF(rect.Left + rect.Width - rect.Height, rect.Top, rect.Height, rect.Height));				

				float w = rect.Width - rect.Height;
				float l = rect.Left + ((rect.Height)/ 2);
				g.FillRectangle(b, new RectangleF(l, rect.Top, w, rect.Height));
			} 
			else if (rect.Width < rect.Height) 
			{
                float t, h;

                g.FillEllipse(b1, new RectangleF(rect.Left, rect.Top, rect.Width, rect.Width));
				g.FillEllipse(b, new RectangleF(rect.Left, rect.Top + rect.Height - rect.Width, rect.Width, rect.Width));

                t = rect.Top + (rect.Width / 2);
                h = trackerRect.Top - rect.Width;
                g.FillRectangle(b1, new RectangleF(rect.Left, t, rect.Width, h));

                t = trackerRect.Bottom - (rect.Width / 2);
                h = rect.Height - trackerRect.Bottom + rect.Width;
                g.FillRectangle(b, new RectangleF(rect.Left, t, rect.Width, h));
            }
            else if (rect.Width == rect.Height) g.FillEllipse(b, rect);
            g.SmoothingMode = SmoothingMode.Default;
        }

    }
}
