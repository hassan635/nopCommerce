﻿using FluentValidation.TestHelper;
using Nop.Tests;
using Nop.Web.Models.Customer;
using Nop.Web.Validators.Customer;
using NUnit.Framework;

namespace Nop.Web.MVC.Tests.Public.Validators.Customer
{
    [TestFixture]
    public class ChangePasswordValidatorTests : BaseNopTest
    {
        private ChangePasswordValidator _validator;
        
        [SetUp]
        public void Setup()
        {
            _validator = GetService<ChangePasswordValidator>();
        }
        
        [Test]
        public void ShouldHaveErrorWhenOldPasswordIsNullOrEmpty()
        {
            var model = new ChangePasswordModel
            {
                OldPassword = null
            };
            _validator.ShouldHaveValidationErrorFor(x => x.OldPassword, model);
            model.OldPassword = "";
            _validator.ShouldHaveValidationErrorFor(x => x.OldPassword, model);
        }

        [Test]
        public void ShouldNotHaveErrorWhenOldPasswordIsSpecified()
        {
            var model = new ChangePasswordModel
            {
                OldPassword = "old password"
            };
            _validator.ShouldNotHaveValidationErrorFor(x => x.OldPassword, model);
        }

        [Test]
        public void ShouldHaveErrorWhenNewPasswordIsNullOrEmpty()
        {
            var model = new ChangePasswordModel
            {
                NewPassword = null
            };
            //we know that new password should equal confirmation password
            model.ConfirmNewPassword = model.NewPassword;
            _validator.ShouldHaveValidationErrorFor(x => x.NewPassword, model);
            model.NewPassword = "";
            //we know that new password should equal confirmation password
            model.ConfirmNewPassword = model.NewPassword;
            _validator.ShouldHaveValidationErrorFor(x => x.NewPassword, model);
        }

        [Test]
        public void ShouldNotHaveErrorWhenNewPasswordIsSpecified()
        {
            var model = new ChangePasswordModel
            {
                NewPassword = "new password"
            };
            //we know that new password should equal confirmation password
            model.ConfirmNewPassword = model.NewPassword;
            _validator.ShouldNotHaveValidationErrorFor(x => x.NewPassword, model);
        }

        [Test]
        public void ShouldHaveErrorWhenConfirmNewPasswordIsNullOrEmpty()
        {
            var model = new ChangePasswordModel
            {
                ConfirmNewPassword = null
            };
            _validator.ShouldHaveValidationErrorFor(x => x.ConfirmNewPassword, model);
            model.ConfirmNewPassword = "";
            _validator.ShouldHaveValidationErrorFor(x => x.ConfirmNewPassword, model);
        }

        [Test]
        public void ShouldNotHaveErrorWhenConfirmNewPasswordIsSpecified()
        {
            var model = new ChangePasswordModel
            {
                ConfirmNewPassword = "some password"
            };
            //we know that new password should equal confirmation password
            model.NewPassword = model.ConfirmNewPassword;
            _validator.ShouldNotHaveValidationErrorFor(x => x.ConfirmNewPassword, model);
        }

        [Test]
        public void ShouldHaveErrorWhenNewPasswordDoesNotEqualConfirmationPassword()
        {
            var model = new ChangePasswordModel
            {
                NewPassword = "some password",
                ConfirmNewPassword = "another password"
            };
            _validator.ShouldHaveValidationErrorFor(x => x.ConfirmNewPassword, model);
        }

        [Test]
        public void ShouldNotHaveErrorWhenNewPasswordEqualsConfirmationPassword()
        {
            var model = new ChangePasswordModel
            {
                NewPassword = "some password",
                ConfirmNewPassword = "some password"
            };
            _validator.ShouldNotHaveValidationErrorFor(x => x.NewPassword, model);
        }
    }
}
