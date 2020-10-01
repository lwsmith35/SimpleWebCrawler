using System.ComponentModel.DataAnnotations;

namespace swc.DB.PageStorage.Model
{
    public class NewPage
    {
        [Required]
        public string ResourceUrl { get; set; }
        [Required]
        public string RawContent { get; set; }
    }
}
