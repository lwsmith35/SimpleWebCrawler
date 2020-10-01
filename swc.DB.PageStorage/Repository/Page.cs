using System;

namespace swc.DB.PageStorage.Repository
{
    public class Page
    {
        public Guid Id { get; set; }
        public string ResourceUrl { get; set; }
        public string Domain { get; set; }
        public string ResourceLocation { get; set; }
        public string RawContent { get; set; }

        //public IEnumerable<(string reference, string text)> Links { get; set; }

        //public IEnumerable<(ContentType type, string tag, string content)> StaticContent { get; set; }
    }
}
