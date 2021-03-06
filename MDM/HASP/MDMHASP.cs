﻿using System.Xml.Linq;
using Aladdin.HASP;
using System.IO;

namespace MDM.HASP
{
    internal static class MDMHASP
    {
        private const string scope = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?><haspscope/>",
            format = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" +
                "<haspformat root=\"hasp_info\">" +
                "    <hasp>" +
                "        <attribute name=\"id\" />" +
                "        <attribute name=\"type\" />" +
                "        <feature>" +
                "            <attribute name=\"id\" />" +
                "        </feature>" +
                "    </hasp>" +
                "</haspformat>",
            vendorCode =
                "AzIceaqfA1hX5wS+M8cGnYh5ceevUnOZIzJBbXFD6dgf3tBkb9cvUF/Tkd/iKu2fsg9wAysYKw7RMAsV" +
                "vIp4KcXle/v1RaXrLVnNBJ2H2DmrbUMOZbQUFXe698qmJsqNpLXRA367xpZ54i8kC5DTXwDhfxWTOZrB" +
                "rh5sRKHcoVLumztIQjgWh37AzmSd1bLOfUGI0xjAL9zJWO3fRaeB0NS2KlmoKaVT5Y04zZEc06waU2r6" +
                "AU2Dc4uipJqJmObqKM+tfNKAS0rZr5IudRiC7pUwnmtaHRe5fgSI8M7yvypvm+13Wm4Gwd4VnYiZvSxf" +
                "8ImN3ZOG9wEzfyMIlH2+rKPUVHI+igsqla0Wd9m7ZUR9vFotj1uYV0OzG7hX0+huN2E/IdgLDjbiapj1" +
                "e2fKHrMmGFaIvI6xzzJIQJF9GiRZ7+0jNFLKSyzX/K3JAyFrIPObfwM+y+zAgE1sWcZ1YnuBhICyRHBh" +
                "aJDKIZL8MywrEfB2yF+R3k9wFG1oN48gSLyfrfEKuB/qgNp+BeTruWUk0AwRE9XVMUuRbjpxa4YA67SK" +
                "unFEgFGgUfHBeHJTivvUl0u4Dki1UKAT973P+nXy2O0u239If/kRpNUVhMg8kpk7s8i6Arp7l/705/bL" +
                "Cx4kN5hHHSXIqkiG9tHdeNV8VYo5+72hgaCx3/uVoVLmtvxbOIvo120uTJbuLVTvT8KtsOlb3DxwUrwL" +
                "zaEMoAQAFk6Q9bNipHxfkRQER4kR7IYTMzSoW5mxh3H9O8Ge5BqVeYMEW36q9wnOYfxOLNw6yQMf8f9s" +
                "JN4KhZty02xm707S7VEfJJ1KNq7b5pP/3RjE0IKtB2gE6vAPRvRLzEohu0m7q1aUp8wAvSiqjZy7FLaT" +
                "tLEApXYvLvz6PEJdj4TegCZugj7c8bIOEqLXmloZ6EgVnjQ7/ttys7VFITB3mazzFiyQuKf4J6+b/a/Y";
        private static string info = null;

        private static string vCode()
        {
            const string hvc = "CWOZO.hvc";
            string res = string.Empty;

            if(File.Exists(hvc)) res = File.ReadAllText(hvc);
            return res;
        }

        public static bool HaspCheck()
        {
            bool res = false;

            try
            {
                HaspStatus status = Hasp.GetInfo(scope, format, vCode(), ref info);

                res = status == HaspStatus.StatusOk;
            }
            catch { }
            return res;
        }

        public static string HaspGet()
        {
            XDocument xml;
            string res = string.Empty;
            HaspStatus status = Hasp.GetInfo(scope, format, vCode(), ref info);

            if(status == HaspStatus.StatusOk)
            {
                xml = XDocument.Parse(info);
                if(xml != null) res = xml.Element("hasp_info").Element("hasp").Attribute("id").Value;
            }
            return res;
        }
    }
}
