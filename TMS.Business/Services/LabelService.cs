using System;
using System.Collections.Generic;
using TMS.EntitiesDTO;
using TMS.Interfaces;
using TMS.Entities;
using System.Linq;

namespace TMS.Business.Services
{
    public class LabelService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Label> _repository;
        private readonly UserService _userService;

        public LabelService(IMapper mapper, IRepository<Label> repository, UserService userService)
        {
            _mapper = mapper;
            _repository = repository;
            _userService = userService;
        }

        public IEnumerable<LabelDTO> GetAll(string UserId)
        {
            return _mapper.Map<IEnumerable<Label>, IEnumerable<LabelDTO>>(
                _repository.GetAll().Where(i =>i.UserId==UserId));
        }

        public LabelDTO GetById(int labelId)
        {
            var label = _repository.GetItem(labelId);
            if (label == null)
                throw new Exception("Entity not found");

            return _mapper.Map<Label, LabelDTO>(label);
        }

        public void Create(LabelDTO label)
        {
            if(label == null)
                throw new ArgumentNullException(nameof(label));
            
            var labelEntity = _mapper.Map<LabelDTO, Label>(label);
            _repository.Create(labelEntity);
            _repository.SaveChanges();
        }

        public void Update(LabelDTO label)
        {
            if (label == null)
                throw new ArgumentNullException(nameof(label));

            var labelEntity = _mapper.Map<LabelDTO, Label>(label);
            _repository.Update(labelEntity);
            _repository.SaveChanges();
        }

        public void Delete(int labelId)
        {
            _repository.Delete(labelId);
            _repository.SaveChanges();
        }

    }
}
