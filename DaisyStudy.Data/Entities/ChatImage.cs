using System;

namespace DaisyStudy.Data.Entities
{
    public class ChatImage
    {
        public int ImageID { set; get; }
        public int ChatID { set; get; }
        public Chat Chat { set; get; }
        public string ImagePath { set; get; }
        public long ImageFileSize { set; get; }
    }
}

