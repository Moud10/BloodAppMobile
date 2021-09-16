using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BloodAPP.Helpers
{
    public class FilesHelper
    {
        public static byte[] ReadFully(Stream Input)
        {
            using (MemoryStream ms= new MemoryStream())
            {
                Input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
