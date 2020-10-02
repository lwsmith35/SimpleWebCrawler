namespace swc.DB.PageStorage.MapperProfile
{
    public class PageProfile : AutoMapper.Profile
    {
        public PageProfile()
        {
            // Respository to API model
            CreateMap<Repository.Page, Model.Page>();

            // API Model to Repository
            CreateMap<Model.NewPage, Repository.Page>();
        }
    }
}
