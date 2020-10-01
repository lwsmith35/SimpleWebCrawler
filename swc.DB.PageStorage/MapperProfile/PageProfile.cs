namespace swc.DB.PageStorage.MapperProfile
{
    public class PageProfile : AutoMapper.Profile
    {
        public PageProfile()
        {
            CreateMap<Repository.Page, Model.Page>();
        }
    }
}
