using AutoMapper;
using System.Linq;
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

            CreateMap<TaskStatus, TaskStatusDTO>();
            CreateMap<TaskStatusDTO, TaskStatus>();

            CreateMap<Task, TaskDTO>()
             .ForMember(m => m.ModeratorIDs, m => m.ResolveUsing(e =>
                    e.Moderators.Select(t => t.UserId).ToList()))
             .ForMember(m => m.ViewerIDs, m => m.ResolveUsing(e =>
                    e.Viewers.Select(t => t.UserId).ToList()));

            CreateMap<TaskDTO, Task>()
                .ForMember(m => m.Moderators, m => m.ResolveUsing(d => 
                d.ModeratorIDs.Select(id => new TaskModerator_User
                {
                    TaskId = d.Id,
                    UserId = id
                })))
                .ForMember(m => m.Viewers, m => m.ResolveUsing(d =>
                d.ViewerIDs.Select(id => new TaskViewer_User
                {
                    TaskId = d.Id,
                    UserId = id
                })));

            #endregion
        }
    }
}
