﻿using ICS.Domain.Data.Adapters;
using ICS.Domain.Data.Repositories.Contracts;
using ICS.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ICS.WebApp.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountOldController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly DomainContext _domainContext;
        private readonly SystemContext _systemContext;

        public AccountOldController(
            IProfileRepository profileRepository,
            IEmployeeRepository employeeRepository,
            DomainContext domainContext,
            SystemContext systemContext,
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _profileRepository = profileRepository;
            _employeeRepository = employeeRepository;
            _domainContext = domainContext;
            _systemContext = systemContext;
        }

        [HttpGet]
        [Authorize]
        [Route("details")]
        public  async Task<AccountDetailsDto> GetDetailsAsync()
        {
            var identityClaims = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identityClaims.Claims;

            var userId = Guid.Parse(identityClaims.FindFirst("UserId").Value);

            var profileId = await _userRepository.GetProfileId(userId).ConfigureAwait(false);
            var employeeId = await _userRepository.GetEmployeeId(userId).ConfigureAwait(false);

            var model = new AccountDetailsDto()
            {
                ProfileId = $"{profileId}",
                EmployeeId = $"{employeeId}"
            };

            return model;
        }

        // POST api/account/register
        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newUser = _userRepository.Create(model.UserName, model.Password);
            //_systemContext.SaveChanges();

            _employeeRepository.Create(newUser.Id);
            var newProfile = await _profileRepository.CreateAsync(newUser).ConfigureAwait(false);
            newUser.SetProfile(newProfile);

            _systemContext.SaveChanges();
            _domainContext.SaveChanges();

            return Ok();
        }
    }
}
