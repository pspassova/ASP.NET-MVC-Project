using Bytes2you.Validation;
using System.Collections.Generic;
using Visions.Data.Contracts;
using Visions.Models.Models;
using Visions.Services.Contracts;

namespace Visions.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IEfRepository<Photo> repository;

        public PhotoService(IEfRepository<Photo> repository)
        {
            Guard.WhenArgument(repository, "repository").IsNull().Throw();

            this.repository = repository;
        }

        public IEnumerable<Photo> GetAll()
        {
            return this.repository.GetAll();
        }
    }
}
