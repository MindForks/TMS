using System;
using System.Collections.Generic;
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

        public TaskService(IMapper mapper, IRepository<Task> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public IEnumerable<TaskDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<Task>, IEnumerable<TaskDTO>>(
                _repository.GetAll());
        }

        public TaskDTO GetById(int ItemId)
        {
            var item = _repository.GetItem(ItemId);
            if (item == null)
                throw new Exception("Entity wasn`t found");

            return _mapper.Map<Task, TaskDTO>(item);
        }

        public void Create(TaskDTO item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var itemEntity = _mapper.Map<TaskDTO, Task>(item);
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
    }
}
