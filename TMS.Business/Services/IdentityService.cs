using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMS.Entities;
using TMS.EntitiesDTO;
using TMS.Interfaces;

namespace TMS.Business.Services
{
   public class IdentityService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<UserApp> _userManager;

        public IdentityService(UserManager<UserApp> userManager, IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async System.Threading.Tasks.Task Register(RegisterUserDTO userDTO)
        {
            var user = _mapper.Map<RegisterUserDTO, UserApp>(userDTO);

            var result = await _userManager.CreateAsync(user, userDTO.Password);
            if (!result.Succeeded)
                throw new AggregateException(result.Errors.Select(error => new Exception(error.Description)));
        }
    }
}
