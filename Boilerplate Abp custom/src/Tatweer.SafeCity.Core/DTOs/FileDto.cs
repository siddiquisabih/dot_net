using System;
using System.Collections.Generic;
using System.Text;

namespace Tatweer.SafeCity.DTOs
{
    public class FileDto
    {
        public string FileName { get; set; }
        public Guid FileToken { get; set; }

        public string ContentType { get; set; }

        public FileDto()
        {
            FileToken = Guid.NewGuid();
        }
    }
}
