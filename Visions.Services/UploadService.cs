﻿using Bytes2you.Validation;
using Visions.Data.Contracts;
using Visions.Services.Contracts;

namespace Visions.Services
{
    public class UploadService<T> : IUploadService<T>
        where T : class
    {
        private readonly IEfRepository<T> repository;

        public UploadService(IEfRepository<T> repository)
        {
            Guard.WhenArgument(repository, "repository").IsNull().Throw();

            this.repository = repository;
        }

        public void Upload(T item)
        {
            Guard.WhenArgument(item, "item").IsNull().Throw();

            this.repository.Add(item);
        }
    }
}