using System;
namespace DaisyStudy.Data.Entities
{
    public class CommentImage
    {
        public int ImageID { set; get; }
        public int CommentID { set; get; }
        public Comment Comment { set; get; }
        public string ImagePath { set; get; }
        public long ImageFileSize { set; get; }
    }
}

