using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Language;


namespace CompileBlazorInBlazor
{
    public class EmptyRazorProjectFileSystem : RazorProjectFileSystem
    {
        public override IEnumerable<RazorProjectItem> EnumerateItems(string basePath)
        {
            return Array.Empty<RazorProjectItem>();
        }

        public override RazorProjectItem GetItem(string path)
        {
            return new MemoryRazorProjectItem(null, false, null, null);
        }

        public override RazorProjectItem GetItem(string path, string fileKind)
        {
            return new MemoryRazorProjectItem(null, false, null, null);
        }
    }


    public class MemoryRazorProjectItem : RazorProjectItem
    {
        private byte[] data;
        
//        public MemoryRazorProjectItem(byte[] data, bool exists = true)
//        {
//            this.data = data;
//            this.exists = exists;
//        }


        public MemoryRazorProjectItem(string code, bool exists, string basePath, string filePath)
        {
            if (code != null)
            {
                var preamble = Encoding.UTF8.GetPreamble();
                var contentBytes = Encoding.UTF8.GetBytes(code);

                this.data = new byte[preamble.Length + contentBytes.Length];
                preamble.CopyTo(data, 0);
                contentBytes.CopyTo(data, preamble.Length);
            }
            this.Exists = exists;
            BasePath = basePath;
            FilePath = filePath;
            PhysicalPath = filePath;
            
        }


        public override Stream Read()
        {
            return new MemoryStream(data);
        }

        public override string BasePath { get; }
        public override string FilePath { get; }
        public override string PhysicalPath { get; }
        public override bool Exists { get; }
    }
}