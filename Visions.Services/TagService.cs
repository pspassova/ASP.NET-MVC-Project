using System;
using Visions.Models.Models;
using Visions.Services.Contracts;

namespace Visions.Services
{
    public class TagService : ITagService
    {
        public Tag Create(string text)
        {
            return new Tag()
            {
                Text = text
            };
        }
    }
}
