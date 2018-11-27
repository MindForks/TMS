using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMS.Entities;
using TMS.EntitiesDTO;
using TMS.Interfaces;

namespace TMS.Business.Services
{
    public class TaskService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Task> _repository;
        private readonly UserService _userService;

        public TaskService(IMapper mapper, IRepository<Task> repository, UserService userService)
        {
            _mapper = mapper;
            _repository = repository;
            _userService = userService;
        }
        public IEnumerable<TaskDetailsDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<Task>, IEnumerable<TaskDetailsDTO>>(
                _repository.GetAll());
        }

        public IEnumerable<TaskDetailsDTO> GetAll(string userId) // where i`m moderator and viewer
        {
            return _mapper.Map<IEnumerable<Task>, IEnumerable<TaskDetailsDTO>>(
                _repository.Filter(i => i.Moderators.Any(j => j.UserId == userId)
                        || i.Viewers.Any(j => j.UserId == userId)));
        }

        public IEnumerable<string> GetAllTaskModerator()
        {
            return _repository.GetAll().Select(j => j.Moderators.Select(k => k.User.Email).ToString());
        }

        public TaskDetailsDTO GetById(int ItemId)
        {
            var item = _repository.GetItem(ItemId);
            if (item == null)
                throw new Exception("Entity wasn`t found");

            return _mapper.Map<Task, TaskDetailsDTO>(item);
        }

        public void Create(TaskDetailsDTO item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var itemEntity = _mapper.Map<TaskDetailsDTO, Task>(item);
            _repository.Create(itemEntity);
            _repository.SaveChanges();
        }

        public void Update(TaskDTO item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var itemEntity = _mapper.Map<TaskDTO, Task>(item);
            _repository.Update(itemEntity);
            _repository.SaveChanges();
        }

        public void Delete(int itemId)
        {
            _repository.Delete(itemId);
            _repository.SaveChanges();
        }

        public void AddOrUpdateLabel(TaskDetailsDTO item)
        {
            var itementity = _mapper.Map<TaskDetailsDTO, Task>(item);
            itementity.Labels.Add(new Task_Label_User
            {
                UserId = "5e55431f-5e94-4091-8dfe-6e78dbf687df",
                LabelId = 2,
            });

            _repository.Update(itementity);
            _repository.SaveChanges();
        }

        //public TaskDetailsDTO GetWithLabelById(int itemId)
        // {
        //     var tmp = new TaskDetailsDTO() {
        //         LabelIDs = { 1 },
        //         UserId = "ea182e92-b376-4ef8-9bc6-6f8ebf2d4237",
        //         StatusId = 1,
        //         CreationTime = DateTimeOffset.Now,
        //         EndDate = DateTimeOffset.Now,
        //         Title = "title",
        //         Description = "desc",
        //         Weight=2
        //     };
        //     var itementity = _mapper.Map<TaskDetailsDTO, Task>(tmp);
        //     _repository.Create(itementity);
        //     _repository.SaveChanges();


        //     var item = _repository.GetItem(itemId);
        //     if (item == null)
        //         throw new Exception("Entity wasn`t found");

        //     return _mapper.Map<Task, TaskDetailsDTO>(item);
        // }
    }
}
