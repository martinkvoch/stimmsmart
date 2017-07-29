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
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace EConTech.Windows.MACUI
{
    /*======================================================================================
     * Pro úèely MDM byly:
     * - provedeny zmìny v SoftLightMath() a OverlayMath()
    ========================================================================================*/

    /// <summary>
    /// Summary description for ColorHelper.
    /// </summary>
    internal class ColorHelper
	{
		///// <summary>
		///// 
		///// </summary>
		///// <param name="red"></param>
		///// <param name="green"></param>
		///// <param name="blue"></param>
		///// <returns></returns>
		//public static Color CreateColorFromRGB(int red, int green, int blue)
		//{
		//	//Corect Red element
		//	int r = red;
		//	if (r > 255) 
		//	{
		//		r = 255;
		//	}
		//	if (r < 0) 
		//	{
		//		r = 0;
		//	}
		//	//Corect Green element
		//	int g = green;
		//	if (g > 255) 
		//	{
		//		g = 255;
		//	}
		//	if (g < 0) 
		//	{
		//		g = 0;
		//	}
		//	//Correct Blue Element
		//	int b = blue;
		//	if (b > 255) 
		//	{
		//		b = 255;
		//	}
		//	if (b < 0) 
		//	{
		//		b = 0;
		//	}
		//	return Color.FromArgb(r, g, b);
		//}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="blendColor"></param>
		/// <param name="baseColor"></param>
		/// <param name="opacity"></param>
		/// <returns></returns>
		public static Color OpacityMix(Color blendColor, Color baseColor, int opacity)
		{
			int r = (int)(((blendColor.R * ((double)opacity / 100D)) + (baseColor.R * (1D - ((double)opacity / 100D))))),
                g = (int)(((blendColor.G * ((double)opacity / 100D)) + (baseColor.G * (1D - ((double)opacity / 100D))))),
                b = (int)(((blendColor.B * ((double)opacity / 100D)) + (baseColor.B * (1D - ((double)opacity / 100D)))));

			//return CreateColorFromRGB(r3, g3, b3);
            return Color.FromArgb(r, g, b);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="baseColor"></param>
		/// <param name="blendColor"></param>
		/// <param name="opacity"></param>
		/// <returns></returns>
		public static Color SoftLightMix(Color baseColor, Color blendColor, int opacity)
		{
			int r = softLightMath(baseColor.R, blendColor.R), g = softLightMath(baseColor.G, blendColor.G), b = softLightMath(baseColor.B, blendColor.B);

			//return OpacityMix(CreateColorFromRGB(r, g, b), baseColor, opacity);
            return OpacityMix(Color.FromArgb(r, g, b), baseColor, opacity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseColor"></param>
        /// <param name="blendColor"></param>
        /// <param name="opacity"></param>
        /// <returns></returns>
        public static Color OverlayMix(Color baseColor, Color blendColor, int opacity)
		{
            int r = overlayMath(baseColor.R, blendColor.R), g = overlayMath(baseColor.G, blendColor.G), b = overlayMath(baseColor.B, blendColor.B);

			//return OpacityMix(CreateColorFromRGB(r, g, b), baseColor, opacity);
            return OpacityMix(Color.FromArgb(r, g, b), baseColor, opacity);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ibase"></param>
        /// <param name="blend"></param>
        /// <returns></returns>
        private static int softLightMath(byte ibase, byte blend)
		{
			double dbase = (double)ibase / byte.MaxValue;
			double dblend = (double)blend / byte.MaxValue;

			if (dblend < 0.5) return (int)(((2 * dbase * dblend) + (Math.Pow(dbase, 2)) * (1 - (2 * dblend))) * byte.MaxValue);
			else return (int)(((Math.Sqrt(dbase) * (2 * dblend - 1)) + ((2 * dbase) * (1 - dblend))) * byte.MaxValue);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ibase"></param>
		/// <param name="blend"></param>
		/// <returns></returns>
		private static int overlayMath(byte ibase, byte blend)
		{
			double dbase = (double)ibase / byte.MaxValue;
			double dblend = (double)blend / byte.MaxValue;

			if (dbase < .5) return (int)((2D * dbase * dblend) * byte.MaxValue);
			else return (int)((1D - (2D * (1D - dbase) * (1D - dblend))) * byte.MaxValue);
		}

	}
}