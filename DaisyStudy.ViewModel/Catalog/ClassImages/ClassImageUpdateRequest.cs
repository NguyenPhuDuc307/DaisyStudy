using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DaisyStudy.ViewModel.Catalog.ClassImages
{
    public class ClassImageUpdateRequest
    {
        public int Id { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}