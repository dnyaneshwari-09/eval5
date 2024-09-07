using EventManagementAPI.DTOs;
using EventManagementAPI.Models;
using EventManagementAPI.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManagementAPI.Services
{
    public class RegistrationService
    {
        private readonly IRegistrationRepository _registrationRepository;
        private readonly IMapper _mapper;

        public RegistrationService(IRegistrationRepository registrationRepository, IMapper mapper)
        {
            _registrationRepository = registrationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RegistrationDTO>> GetAllRegistrationsAsync()
        {
            var registrations = await _registrationRepository.GetAllRegistrationsAsync();
            return _mapper.Map<IEnumerable<RegistrationDTO>>(registrations);
        }

        public async Task<RegistrationDTO> GetRegistrationByIdAsync(Guid registrationId)
        {
            var registration = await _registrationRepository.GetRegistrationByIdAsync(registrationId);
            return _mapper.Map<RegistrationDTO>(registration);
        }

        public async Task<RegistrationDTO> AddRegistrationAsync(RegistrationDTO registrationDto)
        {
            var registration = _mapper.Map<Registration>(registrationDto);
            var createdRegistration = await _registrationRepository.AddRegistrationAsync(registration);
            return _mapper.Map<RegistrationDTO>(createdRegistration);
        }

        public async Task<RegistrationDTO> UpdateRegistrationAsync(RegistrationDTO registrationDto)
        {
            var registration = _mapper.Map<Registration>(registrationDto);
            var updatedRegistration = await _registrationRepository.UpdateRegistrationAsync(registration);
            return _mapper.Map<RegistrationDTO>(updatedRegistration);
        }

        public async Task<bool> DeleteRegistrationAsync(Guid registrationId)
        {
            return await _registrationRepository.DeleteRegistrationAsync(registrationId);
        }
    }
}
