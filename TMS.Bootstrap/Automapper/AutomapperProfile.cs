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
            CreateMap<NotificationTypeDTO, NotificationType>();

            CreateMap<Label, LabelDTO>();
            CreateMap<LabelDTO, Label>();

            CreateMap<UserApp, UserAppDTO>();
            CreateMap<UserAppDTO, UserApp>();

            #endregion
        }
    }
}
