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

            CreateMap<RegisterUserDTO, UserApp>()
                .ForMember(m => m.AccessFailedCount, m => m.Ignore())
                .ForMember(m => m.ConcurrencyStamp, m => m.Ignore())
                .ForMember(m => m.Email, m => m.Ignore())
                .ForMember(m => m.EmailConfirmed, m => m.Ignore())
                .ForMember(m => m.LockoutEnabled, m => m.Ignore())
                .ForMember(m => m.LockoutEnd, m => m.Ignore())
                .ForMember(m => m.NormalizedEmail, m => m.Ignore())
                .ForMember(m => m.NormalizedUserName, m => m.Ignore())
                .ForMember(m => m.PasswordHash, m => m.Ignore())
                .ForMember(m => m.PhoneNumber, m => m.Ignore())
                .ForMember(m => m.PhoneNumberConfirmed, m => m.Ignore())
                .ForMember(m => m.SecurityStamp, m => m.Ignore())
                .ForMember(m => m.TwoFactorEnabled, m => m.Ignore())
                .ForMember(m => m.TaskUsers, m => m.Ignore());
            #endregion
        }
    }
}
