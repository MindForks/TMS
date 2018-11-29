using System;
using System.Collections.Generic;
using System.Text;
using TMS.Entities;
using TMS.EntitiesDTO;
using TMS.Interfaces;

namespace TMS.Business.Services
{
    public class TaskStatusService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<TaskStatus> _repository;

        public TaskStatusService(IMapper mapper, IRepository<TaskStatus> repository)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public IEnumerable<TaskStatusDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<TaskStatus>, IEnumerable<TaskStatusDTO>>(_repository.GetAll());
        }
    }
}
