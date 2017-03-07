﻿using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Visions.Models.Models;

namespace Visions.Tests.Visions.Models.ArticleTests
{
    [TestFixture]
    public class UserId_Should
    {
        [Test]
        public void Have_RequiredAttribute()
        {
            // Arrange
            Article article = new Article();
            string property = "UserId";

            // Act
            bool hasAttribute = article.GetType()
                .GetProperty(property)
                .GetCustomAttributes(false)
                .Where(p => p.GetType() == typeof(RequiredAttribute))
                .Any();

            // Assert
            Assert.IsTrue(hasAttribute);
        }
    }
}
