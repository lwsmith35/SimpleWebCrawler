using System;

namespace swc.Function.FindLinks.Model
{
    public class Page
    {
        public Guid Id { get; set; }
        public string ResourceUrl { get; set; }
        public string Domain { get; set; }
        public string ResourceLocation { get; set; }
        public string RawContent { get; set; }
    }
}
