using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techtronica.Data.Services
{
    public static class ToRickRoll
    {
        public static void OpenWeb()
        {
            string url = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";

            try
            {
                System.Diagnostics.Process.Start(url);
            }
            catch
            {
                System.Diagnostics.Process.Start("C://Program Files/Google/Chrome/Application/Chrome.EXE", url);
            }
        }
    }
}
