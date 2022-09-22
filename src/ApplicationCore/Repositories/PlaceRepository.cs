﻿using ApplicationCore.Interfaces;
using ApplicationCore.Repositories.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Models;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Repositories
{
    public class PlaceRepository : GenericRepository<Place>, IPlaceRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly IAppLogger<Exception> _appLogger;
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public PlaceRepository(ApplicationDbContext context, IUserRepository userRepository, IAppLogger<Exception> appLogger, IJwtService jwtService): base(context)
        {
            _context = context;
            _userRepository = userRepository;
            _jwtService = jwtService;
            _appLogger = appLogger;
            
        }

        public async Task<List<Place>> GetListAsync()
        {
            try
            {
                return await _context.Places.Where(x => x.IsActive == 1).Take(50).ToListAsync();
            }
            catch (Exception ex)
            {
                _appLogger.LogWarning(ex.Message);
                return null;
            }
        }

        public async Task<List<Place>> GetListBySearchStringAsync(string searchedText)
        {
            try
            {
                return await _context.Places.Where(x => x.Name == searchedText && x.IsActive == 1).ToListAsync();  
            }
            catch (Exception ex)
            {
                _appLogger.LogWarning(ex.Message);
                return null;
            }
        }

        public async Task<List<Place>> GetListByCategory(PlaceTag tag)
        {
            try
            {
                return await _context.Places.Where(x => x.PlaceTags.Contains(tag) && x.IsActive == 1).ToListAsync();
            }
            catch (Exception ex)
            {
                _appLogger.LogWarning(ex.Message);
                return null;
            }
        }

        public async Task<PlaceDetailsModel>
    }
}