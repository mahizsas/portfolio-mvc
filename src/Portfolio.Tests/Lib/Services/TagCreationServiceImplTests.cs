﻿using FluentAssertions;
using Moq;
using NUnit.Framework;
using Portfolio.Lib.Data;
using Portfolio.Lib.Models;
using Portfolio.Lib.ViewModels;
using Portfolio.Models;
using Portfolio.ViewModels;

namespace Portfolio.Lib.Services
{
    [TestFixture]
    public class TagCreationServiceImplTests
    {
        private Tag tag;
        private TagInputModel tagInputModel;
        private Mock<IRepository> mockRepository;
        private ITagCreationService service;

        [SetUp]
        public void Before_each_test()
        {
            // Configure the input model
            tagInputModel = new TagInputModel { Description = "" };

            // Configure mock repository
            mockRepository = new Mock<IRepository> { DefaultValue = DefaultValue.Mock };

            // Configure service
            service = new TagCreationServiceImpl(mockRepository.Object);
        }

        [Test]
        public void It_adds_a_category_to_the_repository()
        {
            service.CreateTag(tagInputModel);
            mockRepository.Verify(x => x.Add(It.IsAny<Tag>()), Times.Once());
        }

        [Test]
        public void It_sets_the_description()
        {
            tagInputModel.Description = "This is a test";
            tag = service.CreateTag(tagInputModel);
            tag.Description.Should().Be("This is a test");
        }

        [Test]
        public void It_sets_the_new_category_as_active()
        {
            tag = service.CreateTag(tagInputModel);
            tag.IsActive.Should().BeTrue();
        }
    }
}
