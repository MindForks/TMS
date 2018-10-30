using System;
using System.Collections.Generic;
using TMS.EntitiesDTO;
using TMS.Interfaces;
using TMS.Entities;

namespace TMS.Business
{
    public class LabelService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Label> _repository;

        public LabelService(IMapper mapper, IRepository<Label> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public IEnumerable<LabelDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<Label>, IEnumerable<LabelDTO>>(
                _repository.GetAll());
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
