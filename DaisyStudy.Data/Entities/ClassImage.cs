using System;
namespace DaisyStudy.Data.Entities
{
    public class ClassImage
    {
        public int ImageID { set; get; }
        public int ClassID { set; get; }
        public Class? Class { set; get; }
        public string? ImagePath { set; get; }
        public long ImageFileSize { set; get; }
    }
}

