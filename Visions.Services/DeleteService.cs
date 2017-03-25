﻿using Bytes2you.Validation;
using Visions.Data.Contracts;
using Visions.Services.Contracts;

namespace Visions.Services
{
    public class DeleteService<T> : IDeleteService<T>
        where T : class
    {
        private readonly IEfRepository<T> repository;

        public DeleteService(IEfRepository<T> repository)
        {
            Guard.WhenArgument(repository, "repository").IsNull().Throw();

            this.repository = repository;
        }

        public void Delete(T item)
        {
            Guard.WhenArgument(item, "item").IsNull().Throw();

            this.repository.Delete(item);
        }
    }
}