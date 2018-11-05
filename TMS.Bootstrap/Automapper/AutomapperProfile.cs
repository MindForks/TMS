using AutoMapper;
using TMS.Entities;
using TMS.EntitiesDTO;

namespace TMS.Bootstrap.Automapper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            #region MapSettings
            CreateMap<NotificationType, NotificationTypeDTO>();
            CreateMap<UserApp, UserAppDTO>();
            #endregion
        }
    }
}
