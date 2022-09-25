using ApplicationCore.Interfaces;
using ApplicationCore.Repositories.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Repositories
{
    public class TagRepository : GenericRepository<Tag>, ITagRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TagRepository> _logger;

        public TagRepository(ApplicationDbContext context, ILogger<TagRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
    }
}
