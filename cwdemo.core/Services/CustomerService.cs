﻿using AutoMapper;
using cwdemo.core.Services.Interfaces;
using cwdemo.data.Interfaces;

namespace cwdemo.core.Services
{
    public class CustomerService : BaseService, ICustomerService
    {
        public CustomerService(IRepositories repositories, IMapper mapper) : base(repositories, mapper)
        { }
    }
}
