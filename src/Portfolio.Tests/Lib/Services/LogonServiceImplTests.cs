﻿using System;
using System.Linq.Expressions;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Portfolio.Lib.Data;
using Portfolio.Models;
using Portfolio.ViewModels;

namespace Portfolio.Lib.Services
{
    [TestFixture]
    public class LogonServiceImplTests
    {
        private Credentials credentials;
        private LogonResult logonResult;
        private Mock<IPasswordUtility> mockPasswordUtility;
        private Mock<IRepository> mockRepository;
        private Mock<ITransactionAdapter> mockTransaction;
        private ILogonService service;

        [SetUp]
        public void Before_each_test()
        {
            mockTransaction = new Mock<ITransactionAdapter> { DefaultValue = DefaultValue.Mock };
            mockRepository = new Mock<IRepository> { DefaultValue = DefaultValue.Mock };
            mockRepository.Setup(x => x.BeginTransaction()).Returns(mockTransaction.Object);

            mockPasswordUtility = new Mock<IPasswordUtility>();

            service = new LogonServiceImpl(mockRepository.Object, mockPasswordUtility.Object);

            credentials = new Credentials("tester", "s3cr3t");
        }

        [Test]
        public void It_should_begin_a_transaction()
        {
            service.Logon(credentials);
            mockRepository.Verify(x => x.BeginTransaction(), Times.Once());
        }

        [Test]
        public void It_should_commit_a_transaction()
        {
            service.Logon(credentials);
            mockTransaction.Verify(x => x.Commit(), Times.Once());
        }

        [Test]
        public void It_should_compare_passwords()
        {
            service.Logon(credentials);
            mockPasswordUtility.Verify(x => x.CompareText(credentials.Password, It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void It_should_fail_when_a_user_is_not_found()
        {
            SetRepositoryHitSuccess(false);
            logonResult = service.Logon(credentials);
            logonResult.IsSuccessful.Should().BeFalse();
        }

        [Test]
        public void It_should_fail_when_the_password_does_not_match()
        {
            SetRepositoryHitSuccess(true);
            SetPasswordSuccess(false);            
            logonResult = service.Logon(credentials);
            logonResult.IsSuccessful.Should().BeFalse();
        }

        [Test]
        public void It_should_fetch_a_user_by_username()
        {
            service.Logon(credentials);
            mockRepository.Verify(x => x.FindOne(It.IsAny<Expression<Func<User, bool>>>()), Times.Once());
        }

        [Test]
        public void It_should_return_successful()
        {
            SetPasswordSuccess(true);
            logonResult = service.Logon(credentials);
            logonResult.IsSuccessful.Should().BeTrue();
        }

        [Test]
        public void It_should_update_LastLogonAt()
        {
            SetRepositoryHitSuccess(true);
            SetPasswordSuccess(true);
            logonResult = service.Logon(credentials);
            logonResult.User.LastLogonAt.Should().BeCloseTo(DateTime.UtcNow);            
        }

        [Test]
        public void It_should_update_UpdatedAt()
        {
            SetRepositoryHitSuccess(true);
            SetPasswordSuccess(true);
            logonResult = service.Logon(credentials);
            logonResult.User.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow);
        }

        private void SetPasswordSuccess(bool isSuccessful = true)
        {
            mockPasswordUtility.Setup(x => x.CompareText(It.IsAny<string>(), It.IsAny<string>())).Returns(isSuccessful);
        }

        private void SetRepositoryHitSuccess(bool isSuccessful = true)
        {
            User user = null;
            if (isSuccessful)
            {
                user = new User
                {
                    Username = "tester"
                };    
            }            
            mockRepository.Setup(x => x.FindOne<User>(u => u.Username == "tester")).Returns(user);
        }
    }
}
