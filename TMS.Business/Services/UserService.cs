using System;
using System.Collections.Generic;
using System.Text;
using TMS.Entities;
using TMS.Interfaces;

namespace TMS.Business.Services
{
   public class UserService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryAsync<UserApp> _repository;

        public UserService(IMapper mapper, IRepositoryAsync<UserApp> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async System.Threading.Tasks.Task<IEnumerable<UserApp>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
