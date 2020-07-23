using System.Collections.Generic;

namespace Binit.Framework.Localization
{
    public static class LocalizationConstants
    {
        public static class BinitFramework
        {
            public static class ExceptionHandling
            {
                public const string HandleEmailEx = "Binit.Framework.ExceptionHandling.HandleEmailEx";
                public const string HandleSQLEx = "Binit.Framework.ExceptionHandling.HandleSQLEx";
                public const string HandleDbValidationEx = "Binit.Framework.ExceptionHandling.HandleDbValidationEx";
                public const string HandleIOEx = "Binit.Framework.ExceptionHandling.HandleIOEx";
                public const string HandleGenericEx = "Binit.Framework.ExceptionHandling.HandleGenericEx";
                public const string RealmAuthorizationFailedEx = "Binit.Framework.ExceptionHandling.RealmAuthorizationFailedEx";
                public const string ResourceNotFoundGenericEx = "Binit.Framework.ExceptionHandling.ResourceNotFoundGenericEx";
                public const string HandleUnauthorizedEx = "Binit.Framework.ExceptionHandling.HandleUnauthorizedEx";
            }

            public static class Helpers
            {
                public static class Email
                {
                    public static class Views
                    {
                        public static class Welcome
                        {
                            public const string Title = "Binit.Framework.Helpers.Email.Views.Welcome.Title";
                            public const string Intro = "Binit.Framework.Helpers.Email.Views.Welcome.Intro";
                            public const string CallToActionText = "Binit.Framework.Helpers.Email.Views.Welcome.CallToActionText";
                            public const string CallToActionButton = "Binit.Framework.Helpers.Email.Views.Welcome.CallToActionButton";
                            public const string Footer = "Binit.Framework.Helpers.Email.Views.Welcome.Footer";
                        }
                        public static class ForgotPassword
                        {
                            public const string Title = "Binit.Framework.Helpers.Email.Views.Welcome.Title";
                            public const string Intro = "Binit.Framework.Helpers.Email.Views.Welcome.Intro";
                            public const string CallToActionText = "Binit.Framework.Helpers.Email.Views.Welcome.CallToActionText";
                            public const string CallToActionButton = "Binit.Framework.Helpers.Email.Views.Welcome.CallToActionButton";
                            public const string Footer = "Binit.Framework.Helpers.Email.Views.Welcome.Footer";
                        }
                        public static class PasswordRecovery
                        {
                            public const string Title = "Binit.Framework.Helpers.Email.Views.Welcome.Title";
                            public const string CallToActionText = "Binit.Framework.Helpers.Email.Views.Welcome.CallToActionText";
                            public const string CallToActionButton = "Binit.Framework.Helpers.Email.Views.Welcome.CallToActionButton";
                            public const string Footer = "Binit.Framework.Helpers.Email.Views.Welcome.Footer";
                        }
                    }
                }

                public static class ExcelHelper
                {
                    public const string ImportWrongFileExtensionEx = "Binit.Framework.Helpers.ExcelHelper.ImportWrongFileExtensionEx";
                    public const string ImportFileNotProvidedEx = "Binit.Framework.Helpers.ExcelHelper.ImportFileNotProvidedEx";
                    public const string ImportInvalidDateFormat = "Binit.Framework.Helpers.ExcelHelper.ImportInvalidDateFormat";
                    public const string ImportCouldntCastValue = "Binit.Framework.Helpers.ExcelHelper.ImportCouldntCastValue";
                    public const string ExportHeadersDontMatchCells = "Binit.Framework.Helpers.ExcelHelper.ExportHeadersDontMatchCells";
                }
            }
        }

        public static class DAL
        {
            public static class Service
            {
                public const string GetNotFoundEx = "DAL.Service.GetNotFoundEx";
                public const string GetAsyncNotFoundEx = "DAL.Service.GetAsyncNotFoundEx";
                public const string AccessDeniedEx = "DAL.Service.AccessDeniedEx";
                public const string CanAccessUnexpectedEx = "DAL.Service.CanAccessUnexpectedEx";
            }

            public static class ServiceTenantDependents
            {
                public const string AccessDeniedEx = "DAL.ServiceTenantDependents.AccessDeniedEx";
            }
        }

        public static class DomainLogic
        {
            public static class BusinessLogic
            {
                public static class HolidayBusinessLogic
                {
                    public const string ExcelExportFilename = "Domain.Logic.BusinessLogic.HolidayBusinessLogic.ExcelExportFilename";
                }
                public static class StatisticsBusinessLogic
                {
                    public const string ExcelExportFilename = "Domain.Logic.BusinessLogic.StatisticsBusinessLogic.ExcelExportFilename";
                    public const string TotalGames = "Domain.Logic.BusinessLogic.StatisticsBusinessLogic.TotalGames";
                    public const string Finished = "Domain.Logic.BusinessLogic.StatisticsBusinessLogic.Finished";
                    public const string CorrectAnswers = "Domain.Logic.BusinessLogic.StatisticsBusinessLogic.CorrectAnswers";
                }
            }

            public static class Services
            {
                public static class ErrorLogService
                {
                    public const string LogErrorFailedEx = "Domain.Logic.Services.ErrorLogService.LogErrorFailedEx";
                }
                public static class AccountService
                {
                    public const string LoginUserNotFoundEx = "Domain.Logic.Services.AccountService.LoginUserNotFoundEx";
                    public const string RegisterFailedEx = "Domain.Logic.Services.AccountService.RegisterFailedEx";
                    public const string RegisterInvalidPasswordEx = "Domain.Logic.Services.AccountService.RegisterInvalidPasswordEx";
                    public const string ConfirmEmailUserNotFoundEx = "Domain.Logic.Services.AccountService.ConfirmEmailUserNotFoundEx";
                    public const string ConfirmEmailFailedEx = "Domain.Logic.Services.AccountService.ConfirmEmailFailedEx";
                    public const string CreatePasswordUserNotFoundEx = "Domain.Logic.Services.AccountService.CreatePasswordUserNotFoundEx";
                    public const string CreatePasswordConfirmEmailFailedEx = "Domain.Logic.Services.AccountService.CreatePasswordConfirmEmailFailedEx";
                    public const string CreatePasswordAddPasswordFailedEx = "Domain.Logic.Services.AccountService.CreatePasswordAddPasswordFailedEx";
                    public const string GeneratePasswordTokenUserNotFoundEx = "Domain.Logic.Services.AccountService.GeneratePasswordTokenUserNotFoundEx";
                    public const string ResetPasswordUserNotFoundEx = "Domain.Logic.Services.AccountService.ResetPasswordUserNotFoundEx";
                    public const string ResetPasswordFailedEx = "Domain.Logic.Services.AccountService.ResetPasswordFailedEx";
                    public const string ChangePasswordUserNotFoundEx = "Domain.Logic.Services.AccountService.ChangePasswordUserNotFoundEx";
                    public const string ChangePasswordFailedEx = "Domain.Logic.Services.AccountService.ChangePasswordFailedEx";
                    public const string SetPasswordUserNotFoundEx = "Domain.Logic.Services.AccountService.SetPasswordUserNotFoundEx";
                    public const string SetPasswordFailedEx = "Domain.Logic.Services.AccountService.SetPasswordFailedEx";
                    public const string SocialAuthenticationFailedEx = "Domain.Logic.Services.AccountService.SocialAuthenticationFailedEx";
                }

                public static class CategoryService
                {
                    public const string DeleteWithRelatedProductEx = "Domain.Logic.Services.CategoryService.DeleteWithRelatedProductEx";
                    public const string DeleteAsyncWithRelatedProductEx = "Domain.Logic.Services.CategoryService.DeleteAsyncWithRelatedProductEx";
                }
                public static class GeneroService
                {
                    public const string DeleteWithRelatedPeliculaEx = "Domain.Logic.Services.GeneroService.DeleteWithRelatedPeliculaEx";
                    public const string DeleteAsyncWithRelatedPeliculaEx = "Domain.Logic.Services.GeneroService.DeleteAsyncWithRelatedPeliculaEx";
                }

                public static class ProductService
                {
                    public const string GetFullNotFoundEx = "Domain.Logic.Services.ProductService.GetFullNotFoundEx";
                    public const string GetFullAsyncNotFoundEx = "Domain.Logic.Services.ProductService.GetFullAsyncNotFoundEx";
                }
                public static class QuestionService
                {
                    public const string GetFullNotFoundEx = "Domain.Logic.Services.QuestionService.GetFullNotFoundEx";
                    public const string GetFullAsyncNotFoundEx = "Domain.Logic.Services.QuestionService.GetFullAsyncNotFoundEx";
                }

                public static class ThemeService
                {
                    public const string GetFullNotFoundEx = "Domain.Logic.Services.ThemeService.GetFullNotFoundEx";
                    public const string DeleteAsyncWithRelatedThemeEx = "Domain.Logic.Services.ThemeService.DeleteAsyncWithRelatedThemeEx";
                    public const string GetFullAsyncNotFoundEx = "Domain.Logic.Services.ThemeService.GetFullAsyncNotFoundEx";
                }

                public static class PeliculaService
                {
                    public const string GetFullNotFoundEx = "Domain.Logic.Services.PeliculatService.GetFullNotFoundEx";
                    public const string GetFullAsyncNotFoundEx = "Domain.Logic.Services.PeliculatService.GetFullAsyncNotFoundEx";

                }

                public static class ActorService
                {
                    public const string GetFullNotFoundEx = "Domain.Logic.Services.ActorService.GetFullNotFoundEx";
                    public const string GetFullAsyncNotFoundEx = "Domain.Logic.Services.ActorService.GetFullAsyncNotFoundEx";
                    public const string DeleteWithRelatedPeliculaEx = "Domain.Logic.Services.ActorService.DeleteWithRelatedPeliculaEx";
                    public const string DeleteAsyncWithRelatedPeliculaEx = "Domain.Logic.Services.ActorService.DeleteAsyncWithRelatedPeliculaEx";
                }
                public static class UserService
                {
                    public const string CreateAsyncFailedEx = "Domain.Logic.Services.UserService.CreateAsyncFailed";
                    public const string CreateAsyncAddRolesFailedEx = "Domain.Logic.Services.UserService.CreateAsyncAddRolesFailed";
                    public const string CreateAsyncAddTenantClaimFailedEx = "Domain.Logic.Services.UserService.CreateAsyncAddTenantClaimFailedEx";
                    public const string UpdateAsyncFailedEx = "Domain.Logic.Services.UserService.UpdateAsyncFailed";
                    public const string UpdateAsyncRemoveRolesFailedEx = "Domain.Logic.Services.UserService.UpdateAsyncRemoveRolesFailed";
                    public const string UpdateAsyncAddRolesFailedEx = "Domain.Logic.Services.UserService.UpdateAsyncAddRolesFailed";
                    public const string UpdateAsyncReplaceTenantClaimFailedEx = "Domain.Logic.Services.UserService.UpdateAsyncReplaceTenantClaimFailedEx";
                    public const string AutoDeleteNotAllowed = "Domain.Logic.Services.UserService.AutoDeleteNotAllowed";
                }

                public static class FrontUserService
                {
                    public const string CreateAsyncFailedEx = "Domain.Logic.Services.FrontUserService.CreateAsyncFailed";
                    public const string CreateAsyncAddRolesFailedEx = "Domain.Logic.Services.FrontUserService.CreateAsyncAddRolesFailed";
                    public const string UpdateAsyncFailedEx = "Domain.Logic.Services.FrontUserService.UpdateAsyncFailed";
                    public const string UpdateAsyncRemoveRolesFailedEx = "Domain.Logic.Services.FrontUserService.UpdateAsyncRemoveRolesFailed";
                    public const string UpdateAsyncAddRolesFailedEx = "Domain.Logic.Services.FrontUserService.UpdateAsyncAddRolesFailed";
                }

                public static class BackOfficeUserService
                {
                    public const string CreateAsyncFailedEx = "Domain.Logic.Services.BackOfficeUserService.CreateAsyncFailed";
                    public const string CreateAsyncAddRolesFailedEx = "Domain.Logic.Services.BackOfficeUserService.CreateAsyncAddRolesFailed";
                    public const string UpdateAsyncFailedEx = "Domain.Logic.Services.BackOfficeUserService.UpdateAsyncFailed";
                    public const string UpdateAsyncRemoveRolesFailedEx = "Domain.Logic.Services.BackOfficeUserService.UpdateAsyncRemoveRolesFailed";
                    public const string UpdateAsyncAddRolesFailedEx = "Domain.Logic.Services.BackOfficeUserService.UpdateAsyncAddRolesFailed";
                }

                public static class HolidayTypeService
                {
                    public const string DeleteWithRelatedProductEx = "Domain.Logic.Services.HolidayTypeService.DeleteWithRelatedHolidayEx";
                    public const string DeleteAsyncWithRelatedProductEx = "Domain.Logic.Services.HolidayTypeService.DeleteAsyncWithRelatedHolidayEx";
                }

                public static class HTypeService
                {
                    public const string DeleteWithRelatedProductEx = "Domain.Logic.Services.HolidayTypeService.DeleteWithRelatedHolidayEx";
                    public const string DeleteAsyncWithRelatedProductEx = "Domain.Logic.Services.HolidayTypeService.DeleteAsyncWithRelatedHolidayEx";
                }

                public static class HolidayService
                {
                    public const string GetFullNotFoundEx = "Domain.Logic.Services.HolidayService.GetFullNotFoundEx";
                    public const string GetFullAsyncNotFoundEx = "Domain.Logic.Services.HolidayService.GetFullAsyncNotFoundEx";
                }

                public static class EventService
                {
                    public const string GetFullNotFoundEx = "Domain.Logic.Services.EventService.GetFullNotFoundEx";
                    public const string GetFullAsyncNotFoundEx = "Domain.Logic.Services.EventService.GetFullAsyncNotFoundEx";
                }
            }

        }

        public static class WebAPI
        {
            public static class AccountController
            {
                public const string LoginRequires2FA = "WebAPI.Controllers.AccountController.LoginRequires2FA";
                public const string LoginAccountLocked = "WebAPI.Controllers.AccountController.LoginAccountLocked";
                public const string LoginIncorrectCredentials = "WebAPI.Controllers.AccountController.LoginAccountLocked";
                public const string LoginError = "WebAPI.Controllers.AccountController.LoginError";
                public const string RegisterEmailSubject = "WebAPI.Controllers.AccountController.RegisterEmailSubject";
                public const string ForgotPasswordEmailSubject = "WebAPI.Controllers.AccountController.ForgotPasswordEmailSubject";
                public const string ChangePasswordNotFound = "WebAPI.Controllers.AccountController.ChangePasswordNotFound";
                public const string SetPasswordNotFound = "WebAPI.Controllers.AccountController.SetPasswordNotFound";
                public const string CreatePasswordNotFound = "WebAPI.Controllers.AccountController.CreatePasswordNotFound";
            }

            public static class UserController
            {
                public const string CreateWelcomeEmailSubject = "WebAPI.Controllers.UserController.CreateWelcomeEmailSubject";
                public const string PasswordRecoveryEmailSubject = "WebAPI.Controllers.UserController.PasswordRecoveryEmailSubject";
            }

            public static class DTOs
            {
                public static class AccountDTOs
                {
                    public static class ChangePasswordReq
                    {
                        public const string OldPasswordStringLength = "WebAPI.DTOs.AccountDTOs.ChangePasswordReq.OldPasswordStringLength";
                        public const string OldPasswordRequired = "WebAPI.DTOs.AccountDTOs.ChangePasswordReq.OldPasswordRequired";
                        public const string NewPasswordStringLength = "WebAPI.DTOs.AccountDTOs.ChangePasswordReq.NewPasswordStringLength";
                        public const string NewPasswordRequired = "WebAPI.DTOs.AccountDTOs.ChangePasswordReq.NewPasswordRequired";
                        public const string ConfirmPasswordCompare = "WebAPI.DTOs.AccountDTOs.ChangePasswordReq.ConfirmPasswordCompare";
                    }

                    public static class ConfirmEmailReq
                    {
                        public const string UserIdRequired = "WebAPI.DTOs.AccountDTOs.ConfirmEmailReq.UserIdRequired";
                        public const string CodeRequired = "WebAPI.DTOs.AccountDTOs.ConfirmEmailReq.CodeRequired";
                    }

                    public static class CreatePasswordReq
                    {
                        public const string UserIdRequired = "WebAPI.DTOs.AccountDTOs.CreatePasswordReq.UserIdRequired";
                        public const string CodeRequired = "WebAPI.DTOs.AccountDTOs.CreatePasswordReq.CodeRequired";
                        public const string NewPasswordRequired = "WebAPI.DTOs.AccountDTOs.CreatePasswordReq.NewPasswordRequired";
                        public const string NewPasswordStringLength = "WebAPI.DTOs.AccountDTOs.CreatePasswordReq.NewPasswordStringLength";
                        public const string ConfirmPasswordCompare = "WebAPI.DTOs.AccountDTOs.CreatePasswordReq.ConfirmPasswordCompare";
                    }

                    public static class ForgotPasswordReq
                    {
                        public const string EmailRequired = "WebAPI.DTOs.AccountDTOs.ForgotPasswordReq.EmailRequired";
                        public const string EmailInvalid = "WebAPI.DTOs.AccountDTOs.ForgotPasswordReq.EmailInvalid";
                        public const string ForgotPasswordEmailCallbackRequired = "WebAPI.DTOs.AccountDTOs.ForgotPasswordReq.ForgotPasswordEmailCallbackRequired";
                    }

                    public static class LoginReq
                    {
                        public const string EmailRequired = "WebAPI.DTOs.AccountDTOs.LoginReq.EmailRequired";
                        public const string EmailInvalid = "WebAPI.DTOs.AccountDTOs.LoginReq.EmailInvalid";
                        public const string PasswordRequired = "WebAPI.DTOs.AccountDTOs.LoginReq.PasswordRequired";
                    }

                    public static class RegisterReq
                    {
                        public const string EmailRequired = "WebAPI.DTOs.AccountDTOs.RegisterReq.EmailRequired";
                        public const string EmailInvalid = "WebAPI.DTOs.AccountDTOs.RegisterReq.EmailInvalid";
                        public const string PasswordRequired = "WebAPI.DTOs.AccountDTOs.RegisterReq.PasswordRequired";
                        public const string NameRequired = "WebAPI.DTOs.AccountDTOs.RegisterReq.NameRequired";
                        public const string NameStringLength = "WebAPI.DTOs.AccountDTOs.RegisterReq.NameStringLength";
                        public const string LastNameRequired = "WebAPI.DTOs.AccountDTOs.RegisterReq.LastNameRequired";
                        public const string LastNameStringLength = "WebAPI.DTOs.AccountDTOs.RegisterReq.LastNameStringLength";
                        public const string ConfirmEmailCallbackRequired = "WebAPI.DTOs.AccountDTOs.RegisterReq.ConfirmEmailCallbackRequired";
                    }

                    public static class ResetPasswordReq
                    {
                        public const string EmailRequired = "WebAPI.DTOs.AccountDTOs.ResetPasswordReq.EmailRequired";
                        public const string EmailInvalid = "WebAPI.DTOs.AccountDTOs.ResetPasswordReq.EmailInvalid";
                        public const string PasswordRequired = "WebAPI.DTOs.AccountDTOs.ResetPasswordReq.PasswordRequired";
                        public const string PasswordStringLength = "WebAPI.DTOs.AccountDTOs.ResetPasswordReq.PasswordStringLength";
                        public const string ConfirmPasswordCompare = "WebAPI.DTOs.AccountDTOs.ResetPasswordReq.ConfirmPasswordCompare";
                        public const string CodeRequired = "WebAPI.DTOs.AccountDTOs.ResetPasswordReq.CodeRequired";
                    }

                    public static class SetPasswordReq
                    {
                        public const string NewPasswordRequired = "WebAPI.DTOs.AccountDTOs.SetPasswordReq.NewPasswordRequired";
                        public const string NewPasswordStringLength = "WebAPI.DTOs.AccountDTOs.SetPasswordReq.NewPasswordStringLength";
                        public const string ConfirmPasswordCompare = "WebAPI.DTOs.AccountDTOs.SetPasswordReq.ConfirmPasswordCompare";
                    }
                }

                public static class ApplicationUserDTOs
                {
                    public static class ApplicationUserDTO
                    {
                        public const string EmailRequired = "WebAPI.DTOs.ApplicationUserDTOs.ApplicationUserDTO.EmailRequired";
                        public const string EmailInvalid = "WebAPI.DTOs.ApplicationUserDTOs.ApplicationUserDTO.EmailInvalid";
                        public const string NameRequired = "WebAPI.DTOs.ApplicationUserDTOs.ApplicationUserDTO.NameRequired";
                        public const string NameStringLength = "WebAPI.DTOs.ApplicationUserDTOs.ApplicationUserDTO.NameStringLength";
                        public const string LastNameRequired = "WebAPI.DTOs.ApplicationUserDTOs.ApplicationUserDTO.LastNameRequired";
                        public const string LastNameStringLength = "WebAPI.DTOs.ApplicationUserDTOs.ApplicationUserDTO.LastNameStringLength";
                        public const string RolesRequired = "WebAPI.DTOs.ApplicationUserDTOs.ApplicationUserDTO.RolesRequired";
                    }

                    public static class CreateUserReq
                    {
                        public const string EmailRequired = "WebAPI.DTOs.ApplicationUserDTOs.CreateUserReq.EmailRequired";
                        public const string EmailInvalid = "WebAPI.DTOs.ApplicationUserDTOs.CreateUserReq.EmailInvalid";
                        public const string NameRequired = "WebAPI.DTOs.ApplicationUserDTOs.CreateUserReq.NameRequired";
                        public const string NameStringLength = "WebAPI.DTOs.ApplicationUserDTOs.CreateUserReq.NameStringLength";
                        public const string LastNameRequired = "WebAPI.DTOs.ApplicationUserDTOs.CreateUserReq.LastNameRequired";
                        public const string LastNameStringLength = "WebAPI.DTOs.ApplicationUserDTOs.CreateUserReq.LastNameStringLength";
                        public const string ConfirmEmailCallbackRequired = "WebAPI.DTOs.ApplicationUserDTOs.CreateUserReq.ConfirmEmailCallbackRequired";
                        public const string RolesRequired = "WebAPI.DTOs.ApplicationUserDTOs.CreateUserReq.RolesRequired";
                    }

                    public static class PasswordRecoveryReq
                    {
                        public const string IdRequired = "WebAPI.DTOs.ApplicationUserDTOs.PasswordRecoveryReq.IdRequired";
                        public const string RecoveryEmailCallbackRequired = "WebAPI.DTOs.ApplicationUserDTOs.PasswordRecoveryReq.RecoveryEmailCallbackRequired";
                    }
                }

                public static class ProductDTOs
                {
                    public static class FeatureDTO
                    {
                        public const string NameRequired = "WebAPI.DTOs.ProductDTOs.FeatureDTO.NameRequired";
                        public const string DescriptionRequired = "WebAPI.DTOs.ProductDTOs.FeatureDTO.DescriptionRequired";
                        public const string DescriptionStringLenth = "WebAPI.DTOs.ProductDTOs.FeatureDTO.DescriptionStringLenth";
                    }

                    public static class ProductDTO
                    {
                        public const string NameRequired = "WebAPI.DTOs.ProductDTOs.ProductDTO.NameRequired";
                        public const string DescriptionRequired = "WebAPI.DTOs.ProductDTOs.ProductDTO.DescriptionRequired";
                        public const string DescriptionStringLenth = "WebAPI.DTOs.ProductDTOs.ProductDTO.DescriptionStringLenth";
                        public const string PriceRequired = "WebAPI.DTOs.ProductDTOs.ProductDTO.PriceRequired";
                    }

                    public static class ProductEditorDTO
                    {
                        public const string EditorIdRequired = "WebAPI.DTOs.ProductDTOs.ProductEditorDTO.EditorIdRequired";
                    }
                }

                public static class PeliculaDTOs
                {
                    

                    public static class PeliculaDTO
                    {
                        public const string NameRequired = "WebAPI.DTOs.PeliculaDTOs.PeliculaDTO.NameRequired";
                        public const string DescriptionRequired = "WebAPI.DTOs.PeliculaDTOs.PeliculaDTO.DescriptionRequired";
                        public const string DescriptionStringLenth = "WebAPI.DTOs.PeliculaDTOs.PeliculaDTO.DescriptionStringLenth";
                        public const string PriceRequired = "WebAPI.DTOs.PeliculaDTOs.PeliculaDTO.PriceRequired";
                    }

                    public static class PeliculaEditorDTO
                    {
                        public const string EditorIdRequired = "WebAPI.DTOs.PeliculaDTOs.PeliculaEditorDTO.EditorIdRequired";
                    }
                }

                public static class CategoryDTOs
                {
                    public static class CategoryDTO
                    {
                        public const string NameRequired = "WebAPI.DTOs.CategoryDTOs.CategoryDTO.NameRequired";
                        public const string DescriptionRequired = "WebAPI.DTOs.CategoryDTOs.CategoryDTO.DescriptionRequired";
                        public const string DescriptionStringLenth = "WebAPI.DTOs.CategoryDTOs.CategoryDTO.DescriptionStringLenth";
                    }
                }

                public static class GeneroDTOs
                {
                    public static class GeneroDTO
                    {
                        public const string NameRequired = "WebAPI.DTOs.GeneroDTOs.GeneroDTO.NameRequired";
                        public const string DescriptionRequired = "WebAPI.DTOs.GeneroDTOs.GeneroDTO.DescriptionRequired";
                        public const string DescriptionStringLenth = "WebAPI.DTOs.GeneroDTOs.GeneroDTO.DescriptionStringLenth";
                    }
                }

                public static class HolidayDTOs
                {
                    public static class HolidayDTO
                    {
                        public const string NameRequired = "WebAPI.DTOs.HolidayDTOs.HolidayDTO.NameRequired";
                        public const string DescriptionRequired = "WebAPI.DTOs.HolidayDTOs.HolidayDTO.DescriptionRequired";
                        public const string DescriptionStringLength = "WebAPI.DTOs.HolidayDTOs.HolidayDTO.DescriptionStringLength";
                        public const string MessageStringLength = "WebAPI.DTOs.HolidayDTOs.HolidayDTO.MessageStringLength";
                        public const string StartDateRequired = "WebAPI.DTOs.HolidayDTOs.HolidayDTO.StartDateRequired";
                        public const string ReasonRequired = "WebAPI.DTOs.HolidayDTOs.HolidayDTO.ReasonRequired";
                    }
                }

            }
        }

        public static class WebApp
        {
            public static class Areas
            {

                public static class Identity
                {

                    public static class Pages
                    {

                        public static class Account
                        {

                            public static class AccessDenied
                            {

                                public const string Title = "WebApp.Areas.Identity.Pages.Account.AccessDenied.Title";
                                public const string Description = "WebApp.Areas.Identity.Pages.Account.AccessDenied.Description";

                            }

                            public static class ConfirmEmail
                            {
                                public const string Title = "WebApp.Areas.Identity.Pages.Account.ConfirmEmail.Title";
                                public const string Subtitle = "WebApp.Areas.Identity.Pages.Account.ConfirmEmail.Subtitle";
                                public const string BackToLogin = "WebApp.Areas.Identity.Pages.Account.ConfirmEmail.BackToLogin";
                                public const string InvalidOperationException = "WebApp.Areas.Identity.Pages.Account.ConfirmEmail.InvalidOperationException";
                            }

                            public static class CreatePassword
                            {
                                public const string Title = "WebApp.Areas.Identity.Pages.Account.CreatePassword.Title";
                                public const string PasswordLabel = "WebApp.Areas.Identity.Pages.Account.CreatePassword.PasswordLabel";
                                public const string PasswordRequired = "WebApp.Areas.Identity.Pages.Account.CreatePassword.PasswordRequired";
                                public const string PasswordStringLength = "WebApp.Areas.Identity.Pages.Account.CreatePassword.PasswordStringLength";
                                public const string ConfirmPasswordLabel = "WebApp.Areas.Identity.Pages.Account.CreatePassword.ConfirmPasswordLabel";
                                public const string ConfirmPasswordCompare = "WebApp.Areas.Identity.Pages.Account.CreatePassword.ConfirmPasswordCompare";
                                public const string SubmitButton = "WebApp.Areas.Identity.Pages.Account.CreatePassword.SubmitButton";
                            }

                            public static class CreatePasswordConfirmation
                            {
                                public const string Title = "WebApp.Areas.Identity.Pages.Account.CreatePasswordConfirmation.Title";
                                public const string Subtitle = "WebApp.Areas.Identity.Pages.Account.CreatePasswordConfirmation.Subtitle";
                                public const string BackToLogin = "WebApp.Areas.Identity.Pages.Account.CreatePasswordConfirmation.BackToLogin";
                            }

                            public static class ExternalLogin
                            {
                                public const string Title = "WebApp.Areas.Identity.Pages.Account.ExternalLogin.Title";
                                public const string Subtitle = "WebApp.Areas.Identity.Pages.Account.ExternalLogin.Subtitle";
                                public const string Info = "WebApp.Areas.Identity.Pages.Account.ExternalLogin.Info";
                                public const string EmailLabel = "WebApp.Areas.Identity.Pages.Account.ExternalLogin.EmailLabel";
                                public const string EmailRequired = "WebApp.Areas.Identity.Pages.Account.ExternalLogin.EmailRequired";
                                public const string EmailInvalid = "WebApp.Areas.Identity.Pages.Account.ExternalLogin.EmailInvalid";
                                public const string ExternalProviderError = "WebApp.Areas.Identity.Pages.Account.ExternalLogin.ExternalProviderError";
                                public const string ExternalLoginErrorOnConfirmation = "WebApp.Areas.Identity.Pages.Account.ExternalLogin.ExternalLoginErrorOnConfirmation";
                                public const string ExternalLoginError = "WebApp.Areas.Identity.Pages.Account.ExternalLogin.ExternalLoginError";
                                public const string SubmitButton = "WebApp.Areas.Identity.Pages.Account.ExternalLogin.SubmitButton";
                                public const string NameRequired = "WebApp.Areas.Identity.Pages.Account.ExternalLogin.NameRequired";
                                public const string LastNameRequired = "WebApp.Areas.Identity.Pages.Account.ExternalLogin.LastNameRequired";
                                public const string NameLabel = "WebApp.Areas.Identity.Pages.Account.ExternalLogin.NameLabel";
                                public const string LastNameLabel = "WebApp.Areas.Identity.Pages.Account.ExternalLogin.LastNameLabel";
                                public const string AccountExistsMessage = "WebApp.Areas.Identity.Pages.Account.ExternalLogin.AccountExistsMessage";
                                public const string AccountExistsBtn = "WebApp.Areas.Identity.Pages.Account.ExternalLogin.AccountExistsBtn";
                            }

                            public static class ForgotPassword
                            {
                                public const string Title = "WebApp.Areas.Identity.Pages.Account.ForgotPassword.Title";
                                public const string Subtitle = "WebApp.Areas.Identity.Pages.Account.ForgotPassword.Subtitle";
                                public const string EmailLabel = "WebApp.Areas.Identity.Pages.Account.ForgotPassword.EmailLabel";
                                public const string EmailRequired = "WebApp.Areas.Identity.Pages.Account.ForgotPassword.EmailRequired";
                                public const string EmailInvalid = "WebApp.Areas.Identity.Pages.Account.ForgotPassword.EmailInvalid";
                                public const string SubmitButton = "WebApp.Areas.Identity.Pages.Account.ForgotPassword.SubmitButton";
                                public const string BackToLogin = "WebApp.Areas.Identity.Pages.Account.ForgotPassword.BackToLogin";
                                public const string SendEmailTitle = "WebApp.Areas.Identity.Pages.Account.ForgotPassword.SendEmailTitle";
                            }

                            public static class ForgotPasswordConfirmation
                            {
                                public const string Title = "WebApp.Areas.Identity.Pages.Account.ForgotPasswordConfirmation.Title";
                                public const string Subtitle = "WebApp.Areas.Identity.Pages.Account.ForgotPasswordConfirmation.Subtitle";
                                public const string BackToLogin = "WebApp.Areas.Identity.Pages.Account.ForgotPasswordConfirmation.BackToLogin";
                            }

                            public static class Lockout
                            {
                                public const string Title = "WebApp.Areas.Identity.Pages.Account.Lockout.Title";
                                public const string Subtitle = "WebApp.Areas.Identity.Pages.Account.Lockout.Subtitle";
                            }

                            public static class Login
                            {
                                public const string Title = "WebApp.Areas.Identity.Pages.Account.Login.Title";
                                public const string RememberMe = "WebApp.Areas.Identity.Pages.Account.Login.RememberMe";
                                public const string ForgotPassword = "WebApp.Areas.Identity.Pages.Account.Login.ForgotPassword";
                                public const string SubmitButton = "WebApp.Areas.Identity.Pages.Account.Login.SubmitButton";
                                public const string NoAccount = "WebApp.Areas.Identity.Pages.Account.Login.NoAccount";
                                public const string SignUp = "WebApp.Areas.Identity.Pages.Account.Login.SignUp";
                                public const string WithGoogle = "WebApp.Areas.Identity.Pages.Account.Login.WithGoogle";
                                public const string WithFacebook = "WebApp.Areas.Identity.Pages.Account.Login.WithFacebook";
                                public const string WithTwitter = "WebApp.Areas.Identity.Pages.Account.Login.WithTwitter";
                                public const string WithAny = "WebApp.Areas.Identity.Pages.Account.Login.WithAny";
                                public const string EmailLabel = "WebApp.Areas.Identity.Pages.Account.Login.EmailLabel";
                                public const string EmailRequired = "WebApp.Areas.Identity.Pages.Account.Login.EmailRequired";
                                public const string EmailInvalid = "WebApp.Areas.Identity.Pages.Account.Login.EmailInvalid";
                                public const string PasswordLabel = "WebApp.Areas.Identity.Pages.Account.Login.PasswordLabel";
                                public const string PasswordRequired = "WebApp.Areas.Identity.Pages.Account.Login.PasswordRequired";
                                public const string InvalidLoginAttempt = "WebApp.Areas.Identity.Pages.Account.Login.InvalidLoginAttempt";
                                public const string NotAllowedAttempt = "WebApp.Areas.Identity.Pages.Account.Login.NotAllowedAttempt";
                            }

                            public static class LoginWith2FA
                            {
                                public const string Title = "WebApp.Areas.Identity.Pages.Account.LoginWith2FA.Title";
                                public const string Subtitle = "WebApp.Areas.Identity.Pages.Account.LoginWith2FA.Subtitle";
                                public const string SubmitButton = "WebApp.Areas.Identity.Pages.Account.LoginWith2FA.SubmitButton";
                                public const string LoginRecoveryCodeText = "WebApp.Areas.Identity.Pages.Account.LoginWith2FA.LoginRecoveryCodeText";
                                public const string LoginRecoveryCodeButton = "WebApp.Areas.Identity.Pages.Account.LoginWith2FA.LoginRecoveryCodeButton";
                                public const string TwoFactorCodeStringLength = "WebApp.Areas.Identity.Pages.Account.LoginWith2FA.TwoFactorCodeStringLength";
                                public const string TwoFactorCodeLabel = "WebApp.Areas.Identity.Pages.Account.LoginWith2FA.TwoFactorCodeLabel";
                                public const string TwoFactorCodeRequired = "WebApp.Areas.Identity.Pages.Account.LoginWith2FA.TwoFactorCodeRequired";
                                public const string RememberMachineLabel = "WebApp.Areas.Identity.Pages.Account.LoginWith2FA.RememberMachineLabel";
                                public const string InvalidOperationException = "WebApp.Areas.Identity.Pages.Account.LoginWith2FA.InvalidOperationException";
                                public const string InvalidCode = "WebApp.Areas.Identity.Pages.Account.LoginWith2FA.InvalidCode";
                            }

                            public static class LoginWithRecoveryCode
                            {
                                public const string Title = "WebApp.Areas.Identity.Pages.Account.LoginWithRecoveryCode.Title";
                                public const string Subtitle = "WebApp.Areas.Identity.Pages.Account.LoginWithRecoveryCode.Subtitle";
                                public const string SubmitButton = "WebApp.Areas.Identity.Pages.Account.LoginWithRecoveryCode.SubmitButton";
                                public const string RecoveryCodeLabel = "WebApp.Areas.Identity.Pages.Account.LoginWithRecoveryCode.RecoveryCodeLabel";
                                public const string RecoveryCodeRequired = "WebApp.Areas.Identity.Pages.Account.LoginWithRecoveryCode.RecoveryCodeRequired";
                                public const string InvalidOperationException = "WebApp.Areas.Identity.Pages.Account.LoginWithRecoveryCode.InvalidOperationException";
                                public const string InvalidCode = "WebApp.Areas.Identity.Pages.Account.LoginWithRecoveryCode.InvalidCode";
                            }

                            public static class Logout
                            {
                                public const string Title = "WebApp.Areas.Identity.Pages.Account.Logout.Title";
                                public const string Subtitle = "WebApp.Areas.Identity.Pages.Account.Logout.Subtitle";
                            }

                            public static class Register
                            {
                                public const string Title = "WebApp.Areas.Identity.Pages.Account.Register.Title";
                                public const string Subtitle = "WebApp.Areas.Identity.Pages.Account.Register.Subtitle";
                                public const string SubmitButton = "WebApp.Areas.Identity.Pages.Account.Register.SubmitButton";
                                public const string ExistingAccount = "WebApp.Areas.Identity.Pages.Account.Register.ExistingAccount";
                                public const string LoginButton = "WebApp.Areas.Identity.Pages.Account.Register.LoginButton";
                                public const string EmailRequired = "WebApp.Areas.Identity.Pages.Account.Register.EmailRequired";
                                public const string NameRequired = "WebApp.Areas.Identity.Pages.Account.Register.NameRequired";
                                public const string LastNameRequired = "WebApp.Areas.Identity.Pages.Account.Register.LastNameRequired";
                                public const string PasswordRequired = "WebApp.Areas.Identity.Pages.Account.Register.PasswordRequired";
                                public const string EmailInvalid = "WebApp.Areas.Identity.Pages.Account.Register.EmailInvalid";
                                public const string EmailLabel = "WebApp.Areas.Identity.Pages.Account.Register.EmailLabel";
                                public const string NameLabel = "WebApp.Areas.Identity.Pages.Account.Register.NameLabel";
                                public const string LastNameLabel = "WebApp.Areas.Identity.Pages.Account.Register.LastNameLabel";
                                public const string PasswordLabel = "WebApp.Areas.Identity.Pages.Account.Register.PasswordLabel";
                                public const string ConfirmPasswordLabel = "WebApp.Areas.Identity.Pages.Account.Register.ConfirmPasswordLabel";
                                public const string NameStringLength = "WebApp.Areas.Identity.Pages.Account.Register.NameStringLength";
                                public const string LastNameStringLength = "WebApp.Areas.Identity.Pages.Account.Register.LastNameStringLength";
                                public const string PasswordStringLength = "WebApp.Areas.Identity.Pages.Account.Register.PasswordStringLength";
                                public const string ConfirmPasswordCompare = "WebApp.Areas.Identity.Pages.Account.Register.ConfirmPasswordCompare";
                                public const string ConfirmationEmailTitle = "WebApp.Areas.Identity.Pages.Account.Register.ConfirmationEmailTitle";
                                public const string PasswordRequirements = "WebApp.Areas.Identity.Pages.Account.Register.PasswordRequirements";
                            }

                            public static class ResetPassword
                            {
                                public const string Title = "WebApp.Areas.Identity.Pages.Account.ResetPassword.Title";
                                public const string Subtitle = "WebApp.Areas.Identity.Pages.Account.ResetPassword.Subtitle";
                                public const string SubmitButton = "WebApp.Areas.Identity.Pages.Account.ResetPassword.SubmitButton";
                                public const string EmailRequired = "WebApp.Areas.Identity.Pages.Account.ResetPassword.EmailRequired";
                                public const string PasswordRequired = "WebApp.Areas.Identity.Pages.Account.ResetPassword.PasswordRequired";
                                public const string EmailInvalid = "WebApp.Areas.Identity.Pages.Account.ResetPassword.EmailInvalid";
                                public const string PasswordStringLength = "WebApp.Areas.Identity.Pages.Account.ResetPassword.PasswordStringLength";
                                public const string EmailLabel = "WebApp.Areas.Identity.Pages.Account.ResetPassword.EmailLabel";
                                public const string PasswordLabel = "WebApp.Areas.Identity.Pages.Account.ResetPassword.PasswordLabel";
                                public const string ConfirmPasswordLabel = "WebApp.Areas.Identity.Pages.Account.ResetPassword.ConfirmPasswordLabel";
                                public const string CodeLabel = "WebApp.Areas.Identity.Pages.Account.ResetPassword.CodeLabel";
                                public const string ConfirmPasswordCompare = "WebApp.Areas.Identity.Pages.Account.ResetPassword.ConfirmPasswordCompare";
                            }

                            public static class ResetPasswordConfirmation
                            {
                                public const string Title = "WebApp.Areas.Identity.Pages.Account.ResetPasswordConfirmation.Title";
                                public const string SubtitleText = "WebApp.Areas.Identity.Pages.Account.ResetPasswordConfirmation.SubtitleText";
                                public const string SubtitleButton = "WebApp.Areas.Identity.Pages.Account.ResetPasswordConfirmation.SubtitleButton";
                            }

                            public static class Manage
                            {
                                public const string Title = "WebApp.Areas.Identity.Pages.Account.Manage.Title";
                                public const string Subtitle = "WebApp.Areas.Identity.Pages.Account.Manage.Subtitle";

                            }

                            public static class ManageNav
                            {
                                public const string Profile = "WebApp.Areas.Identity.Pages.Account.ManageNav.Profile";
                                public const string ChangePassword = "WebApp.Areas.Identity.Pages.Account.ManageNav.ChangePassword";
                                public const string ExternalLogin = "WebApp.Areas.Identity.Pages.Account.ManageNav.ExternalLogin";
                                public const string TwoFA = "WebApp.Areas.Identity.Pages.Account.ManageNav.TwoFA";

                            }

                            public static class Profile
                            {
                                public const string Title = "WebApp.Areas.Identity.Pages.Account.Profile.Title";
                                public const string SendVerificationEmail = "WebApp.Areas.Identity.Pages.Account.Profile.SendVerificationEmail";
                                public const string SubmitButton = "WebApp.Areas.Identity.Pages.Account.Profile.SubmitButton";
                                public const string UsernameLabel = "WebApp.Areas.Identity.Pages.Account.Profile.UsernameLabel";
                                public const string EmailLabel = "WebApp.Areas.Identity.Pages.Account.Profile.EmailLabel";
                                public const string NameLabel = "WebApp.Areas.Identity.Pages.Account.Profile.NameLabel";
                                public const string LastNameLabel = "WebApp.Areas.Identity.Pages.Account.Profile.LastNameLabel";
                                public const string PhoneNumberLabel = "WebApp.Areas.Identity.Pages.Account.Profile.PhoneNumberLabel";
                                public const string EmailRequired = "WebApp.Areas.Identity.Pages.Account.Profile.EmailRequired";
                                public const string NameRequired = "WebApp.Areas.Identity.Pages.Account.Profile.NameRequired";
                                public const string LastNameRequired = "WebApp.Areas.Identity.Pages.Account.Profile.LastNameRequired";
                                public const string NameStringLength = "WebApp.Areas.Identity.Pages.Account.Profile.NameStringLength";
                                public const string LastNameStringLength = "WebApp.Areas.Identity.Pages.Account.Profile.LastNameStringLength";
                                public const string CannotLoadUser = "WebApp.Areas.Identity.Pages.Account.Profile.CannotLoadUser";
                                public const string ErrorSettingEmail = "WebApp.Areas.Identity.Pages.Account.Profile.ErrorSettingEmail";
                                public const string ErrorSettingPhone = "WebApp.Areas.Identity.Pages.Account.Profile.ErrorSettingPhone";
                                public const string Updated = "WebApp.Areas.Identity.Pages.Account.Profile.Updated";
                                public const string VerificationEmailTitle = "WebApp.Areas.Identity.Pages.Account.Profile.VerificationEmailTitle";
                                public const string VerificationEmailBody = "WebApp.Areas.Identity.Pages.Account.Profile.VerificationEmailBody";
                                public const string EmailSent = "WebApp.Areas.Identity.Pages.Account.Profile.EmailSent";
                                public const string PhoneNumberInvalid = "WebApp.Areas.Identity.Pages.Account.Profile.PhoneNumberInvalid";
                                public const string ClickHere = "WebApp.Areas.Identity.Pages.Account.Profile.ClickHere";
                                public const string PhotoLabel = "WebApp.Areas.Identity.Pages.Account.Profile.PhotoLabel";
                            }

                            public static class ChangePassword
                            {
                                public const string Title = "WebApp.Areas.Identity.Pages.Account.ChangePassword.Title";
                                public const string SubmitButton = "WebApp.Areas.Identity.Pages.Account.ChangePassword.SubmitButton";
                                public const string OldPasswordRequired = "WebApp.Areas.Identity.Pages.Account.ChangePassword.OldPasswordRequired";
                                public const string NewPasswordRequired = "WebApp.Areas.Identity.Pages.Account.ChangePassword.NewPasswordRequired";
                                public const string OldPasswordLabel = "WebApp.Areas.Identity.Pages.Account.ChangePassword.OldPasswordLabel";
                                public const string NewPasswordLabel = "WebApp.Areas.Identity.Pages.Account.ChangePassword.NewPasswordLabel";
                                public const string ConfirmNewPasswordLabel = "WebApp.Areas.Identity.Pages.Account.ChangePassword.ConfirmNewPasswordLabel";
                                public const string NewPasswordStringLength = "WebApp.Areas.Identity.Pages.Account.ChangePassword.NewPasswordStringLength";
                                public const string ConfirmPasswordCompare = "WebApp.Areas.Identity.Pages.Account.ChangePassword.ConfirmPasswordCompare";
                                public const string CannotLoadUser = "WebApp.Areas.Identity.Pages.Account.ChangePassword.CannotLoadUser";
                                public const string PasswordChanged = "WebApp.Areas.Identity.Pages.Account.ChangePassword.PasswordChanged";
                            }

                            public static class ResetAuthenticator
                            {
                                public const string Title = "WebApp.Areas.Identity.Pages.Account.ChangePassword.ResetAuthenticator.Title";
                                public const string ParagraphOne = "WebApp.Areas.Identity.Pages.Account.ChangePassword.ResetAuthenticator.ParagraphOne";
                                public const string ParagraphTwo = "WebApp.Areas.Identity.Pages.Account.ChangePassword.ResetAuthenticator.ParagraphTwo";
                                public const string SubmitButton = "WebApp.Areas.Identity.Pages.Account.ChangePassword.ResetAuthenticator.SubmitButton";
                                public const string CannotLoadUser = "WebApp.Areas.Identity.Pages.Account.ChangePassword.ResetAuthenticator.CannotLoadUser";
                                public const string KeyResetSuccess = "WebApp.Areas.Identity.Pages.Account.ChangePassword.ResetAuthenticator.KeyResetSuccess";
                            }

                            public static class DeletePersonalData
                            {
                                public const string Title = "WebApp.Areas.Identity.Pages.Account.DeletePersonalData.Title";
                                public const string Subtitle = "WebApp.Areas.Identity.Pages.Account.DeletePersonalData.Subtitle";
                                public const string SubmitButton = "WebApp.Areas.Identity.Pages.Account.DeletePersonalData.SubmitButton";
                                public const string PasswordRequired = "WebApp.Areas.Identity.Pages.Account.DeletePersonalData.PasswordRequired";
                                public const string CannotLoadUser = "WebApp.Areas.Identity.Pages.Account.DeletePersonalData.CannotLoadUser";
                                public const string PasswordNotCorrect = "WebApp.Areas.Identity.Pages.Account.DeletePersonalData.PasswordNotCorrect";
                                public const string InvalidOperationException = "WebApp.Areas.Identity.Pages.Account.DeletePersonalData.InvalidOperationException";
                                public const string PasswordLabel = "WebApp.Areas.Identity.Pages.Account.DeletePersonalData.PasswordLabel";
                            }

                            public static class DisableTwoFA
                            {
                                public const string Title = "WebApp.Areas.Identity.Pages.Account.DisableTwoFA.Title";
                                public const string ParagraphOne = "WebApp.Areas.Identity.Pages.Account.DisableTwoFA.ParagraphOne";
                                public const string ParagraphTwo = "WebApp.Areas.Identity.Pages.Account.DisableTwoFA.ParagraphTwo";
                                public const string ParagraphTwoButton = "WebApp.Areas.Identity.Pages.Account.DisableTwoFA.ParagraphTwoButton";
                                public const string SubmitButton = "WebApp.Areas.Identity.Pages.Account.DisableTwoFA.SubmitButton";
                                public const string CannotLoadUser = "WebApp.Areas.Identity.Pages.Account.DisableTwoFA.CannotLoadUser";
                                public const string CannotDisable2FA = "WebApp.Areas.Identity.Pages.Account.DisableTwoFA.CannotDisable2FA";
                                public const string InvalidOperationException = "WebApp.Areas.Identity.Pages.Account.DisableTwoFA.InvalidOperationException";
                                public const string TwoFADisabled = "WebApp.Areas.Identity.Pages.Account.DisableTwoFA.TwoFADisabled";
                            }

                            public static class DownloadYourData
                            {
                                public const string Title = "WebApp.Areas.Identity.Pages.Account.DownloadYourData.Title";
                                public const string CannotLoadUser = "WebApp.Areas.Identity.Pages.Account.DownloadYourData.CannotLoadUser";
                            }

                            public static class EnableAuthenticator
                            {
                                public const string Title = "WebApp.Areas.Identity.Pages.Account.EnableAuthenticator.Title";
                                public const string Subtitle = "WebApp.Areas.Identity.Pages.Account.EnableAuthenticator.Subtitle";
                                public const string ParagraphOne = "WebApp.Areas.Identity.Pages.Account.EnableAuthenticator.ParagraphOne";
                                public const string And = "WebApp.Areas.Identity.Pages.Account.EnableAuthenticator.And";
                                public const string Or = "WebApp.Areas.Identity.Pages.Account.EnableAuthenticator.Or";
                                public const string ScanCodeFirst = "WebApp.Areas.Identity.Pages.Account.EnableAuthenticator.ScanCodeFirst";
                                public const string ScanCodeSecond = "WebApp.Areas.Identity.Pages.Account.EnableAuthenticator.ScanCodeSecond";
                                public const string Enable = "WebApp.Areas.Identity.Pages.Account.EnableAuthenticator.Enable";
                                public const string EnableButton = "WebApp.Areas.Identity.Pages.Account.EnableAuthenticator.EnableButton";
                                public const string EnterTheCode = "WebApp.Areas.Identity.Pages.Account.EnableAuthenticator.EnterTheCode";
                                public const string VerificationCode = "WebApp.Areas.Identity.Pages.Account.EnableAuthenticator.VerificationCode";
                                public const string SubmitButton = "WebApp.Areas.Identity.Pages.Account.EnableAuthenticator.SubmitButton";
                                public const string CodeRequired = "WebApp.Areas.Identity.Pages.Account.EnableAuthenticator.CodeRequired";
                                public const string CodeStringLength = "WebApp.Areas.Identity.Pages.Account.EnableAuthenticator.CodeStringLength";
                                public const string CodeLabel = "WebApp.Areas.Identity.Pages.Account.EnableAuthenticator.CodeLabel";
                                public const string CannotLoadUser = "WebApp.Areas.Identity.Pages.Account.EnableAuthenticator.CannotLoadUser";
                                public const string InvalidCode = "WebApp.Areas.Identity.Pages.Account.EnableAuthenticator.InvalidCode";
                                public const string AuthenticatorVerified = "WebApp.Areas.Identity.Pages.Account.EnableAuthenticator.AuthenticatorVerified";
                            }

                            public static class ExternalLogins
                            {
                                public const string Title = "WebApp.Areas.Identity.Pages.Account.ExternalLogins.Title";
                                public const string Subtitle = "WebApp.Areas.Identity.Pages.Account.ExternalLogins.Subtitle";
                                public const string SubmitButton = "WebApp.Areas.Identity.Pages.Account.ExternalLogins.SubmitButton";
                                public const string AddAnotherService = "WebApp.Areas.Identity.Pages.Account.ExternalLogins.AddAnotherService";
                                public const string CannotLoadUser = "WebApp.Areas.Identity.Pages.Account.ExternalLogins.CannotLoadUser";
                                public const string ErrorRemovingExternalService = "WebApp.Areas.Identity.Pages.Account.ExternalLogins.ErrorRemovingExternalService";
                                public const string ExternalLoginRemoved = "WebApp.Areas.Identity.Pages.Account.ExternalLogins.ExternalLoginRemoved";
                                public const string ErrorLoadingExternalService = "WebApp.Areas.Identity.Pages.Account.ExternalLogins.ErrorLoadingExternalService";
                                public const string ErrorAddingExternalService = "WebApp.Areas.Identity.Pages.Account.ExternalLogins.ErrorAddingExternalService";
                                public const string ExternalLoginAdded = "WebApp.Areas.Identity.Pages.Account.ExternalLogins.ExternalLoginAdded";
                                public const string WithGoogle = "WebApp.Areas.Identity.Pages.Account.ExternalLogins.WithGoogle";
                                public const string WithFacebook = "WebApp.Areas.Identity.Pages.Account.ExternalLogins.WithFacebook";
                                public const string WithTwitter = "WebApp.Areas.Identity.Pages.Account.ExternalLogins.WithTwitter";
                            }

                            public static class GenerateRecoveryCodes
                            {
                                public const string Title = "WebApp.Areas.Identity.Pages.Account.GenerateRecoveryCodes.Title";
                                public const string ParagraphOne = "WebApp.Areas.Identity.Pages.Account.GenerateRecoveryCodes.ParagraphOne";
                                public const string ParagraphTwo = "WebApp.Areas.Identity.Pages.Account.GenerateRecoveryCodes.ParagraphTwo";
                                public const string ParagraphThree = "WebApp.Areas.Identity.Pages.Account.GenerateRecoveryCodes.ParagraphThree";
                                public const string ParagraphThreeButton = "WebApp.Areas.Identity.Pages.Account.GenerateRecoveryCodes.ParagraphThreeButton";
                                public const string SubmitButton = "WebApp.Areas.Identity.Pages.Account.GenerateRecoveryCodes.SubmitButton";
                                public const string CannotLoadUser = "WebApp.Areas.Identity.Pages.Account.GenerateRecoveryCodes.CannotLoadUser";
                                public const string ErrorTwoFADisabled = "WebApp.Areas.Identity.Pages.Account.GenerateRecoveryCodes.ErrorTwoFADisabled";
                                public const string NewCodesGenerated = "WebApp.Areas.Identity.Pages.Account.GenerateRecoveryCodes.NewCodesGenerated";
                            }

                            public static class SetPassword
                            {
                                public const string Title = "WebApp.Areas.Identity.Pages.Account.SetPassword.Title";
                                public const string Subtitle = "WebApp.Areas.Identity.Pages.Account.SetPassword.Subtitle";
                                public const string ParagraphOne = "WebApp.Areas.Identity.Pages.Account.SetPassword.ParagraphOne";
                                public const string SubmitButton = "WebApp.Areas.Identity.Pages.Account.SetPassword.SubmitButton";
                                public const string NewPasswordRequired = "WebApp.Areas.Identity.Pages.Account.SetPassword.NewPasswordRequired";
                                public const string NewPasswordStringLength = "WebApp.Areas.Identity.Pages.Account.SetPassword.NewPasswordStringLength";
                                public const string NewPasswordLabel = "WebApp.Areas.Identity.Pages.Account.SetPassword.NewPasswordLabel";
                                public const string ConfirmPasswordLabel = "WebApp.Areas.Identity.Pages.Account.SetPassword.ConfirmPasswordLabel";
                                public const string ConfirmPasswordCompare = "WebApp.Areas.Identity.Pages.Account.SetPassword.ConfirmPasswordCompare";
                                public const string CannotLoadUser = "WebApp.Areas.Identity.Pages.Account.SetPassword.CannotLoadUser";
                                public const string PasswordSet = "WebApp.Areas.Identity.Pages.Account.SetPassword.PasswordSet";
                            }

                            public static class ShowRecoveryCodes
                            {
                                public const string Title = "WebApp.Areas.Identity.Pages.Account.ShowRecoveryCodes.Title";
                                public const string ParagraphOne = "WebApp.Areas.Identity.Pages.Account.ShowRecoveryCodes.ParagraphOne";
                                public const string ParagraphTwo = "WebApp.Areas.Identity.Pages.Account.ShowRecoveryCodes.ParagraphTwo";
                            }

                            public static class TwoFactorAuthentication
                            {
                                public const string Title = "WebApp.Areas.Identity.Pages.Account.TwoFactorAuthentication.Title";
                                public const string NoRecoveryCodesLeft = "WebApp.Areas.Identity.Pages.Account.TwoFactorAuthentication.NoRecoveryCodesLeft";
                                public const string NoRecoveryCodesLeftDescription = "WebApp.Areas.Identity.Pages.Account.TwoFactorAuthentication.NoRecoveryCodesLeftDescription";
                                public const string RecoveryCodesLeftButton = "WebApp.Areas.Identity.Pages.Account.TwoFactorAuthentication.RecoveryCodesLeftButton";
                                public const string OneRecoveryCodeLeft = "WebApp.Areas.Identity.Pages.Account.TwoFactorAuthentication.OneRecoveryCodeLeft";
                                public const string RecoveryCodesLeft = "WebApp.Areas.Identity.Pages.Account.TwoFactorAuthentication.RecoveryCodesLeft";
                                public const string RecoveryCodesLeftDescription = "WebApp.Areas.Identity.Pages.Account.TwoFactorAuthentication.RecoveryCodesLeftDescription";
                                public const string ForgetBrowser = "WebApp.Areas.Identity.Pages.Account.TwoFactorAuthentication.ForgetBrowser";
                                public const string DisableTwoFA = "WebApp.Areas.Identity.Pages.Account.TwoFactorAuthentication.DisableTwoFA";
                                public const string ResetRecoveryCodes = "WebApp.Areas.Identity.Pages.Account.TwoFactorAuthentication.ResetRecoveryCodes";
                                public const string SecondSubtitle = "WebApp.Areas.Identity.Pages.Account.TwoFactorAuthentication.SecondSubtitle";
                                public const string AddApp = "WebApp.Areas.Identity.Pages.Account.TwoFactorAuthentication.AddApp";
                                public const string SetupApp = "WebApp.Areas.Identity.Pages.Account.TwoFactorAuthentication.SetupApp";
                                public const string ResetApp = "WebApp.Areas.Identity.Pages.Account.TwoFactorAuthentication.ResetApp";
                                public const string CannotLoadUser = "WebApp.Areas.Identity.Pages.Account.TwoFactorAuthentication.CannotLoadUser";
                                public const string BrowserForgotten = "WebApp.Areas.Identity.Pages.Account.TwoFactorAuthentication.BrowserForgotten";
                            }

                            public static class RegisterConfirmation
                            {
                                public const string Title = "WebApp.Areas.Identity.Pages.Account.RegisterConfirmation.Title";
                                public const string Subtitle = "WebApp.Areas.Identity.Pages.Account.RegisterConfirmation.Subtitle";
                                public const string BackToLogin = "WebApp.Areas.Identity.Pages.Account.RegisterConfirmation.BackToLogin";
                            }
                        }
                    }
                    public static class Errors
                    {
                        public const string EmailTaken = "WebApp.Areas.Identity.Errors.EmailTaken";
                        public const string UsernameTaken = "WebApp.Areas.Identity.Errors.UsernameTaken";
                    }
                }
            }

            public static class Models
            {
                public static class EntityViewModel
                {
                    public const string IdLabel = "WebApp.Models.EntityViewModel.IdLabel";
                }

                public static class ApplicationUserViewModel
                {
                    public const string EmailRequired = "WebApp.Models.ApplicationUserViewModel.EmailRequired";
                    public const string NameRequired = "WebApp.Models.ApplicationUserViewModel.NameRequired";
                    public const string LastNameRequired = "WebApp.Models.ApplicationUserViewModel.LastNameRequired";
                    public const string PhoneNumberRequired = "WebApp.Models.ApplicationUserViewModel.PhoneNumberRequired";
                    public const string RolesRequired = "WebApp.Models.ApplicationUserViewModel.RolesRequired";
                    public const string EmailInvalid = "WebApp.Models.ApplicationUserViewModel.EmailInvalid";
                    public const string NameStringLength = "WebApp.Models.ApplicationUserViewModel.NameStringLength";
                    public const string LastNameStringLength = "WebApp.Models.ApplicationUserViewModel.LastNameStringLength";
                    public const string EmailLabel = "WebApp.Models.ApplicationUserViewModel.EmailLabel";
                    public const string NameLabel = "WebApp.Models.ApplicationUserViewModel.NameLabel";
                    public const string LastNameLabel = "WebApp.Models.ApplicationUserViewModel.LastNameLabel";
                    public const string PhoneNumberLabel = "WebApp.Models.ApplicationUserViewModel.PhoneNumberLabel";
                    public const string RolesLabel = "WebApp.Models.ApplicationUserViewModel.RolesLabel";
                    public const string TenantsLabel = "WebApp.Models.ApplicationUserViewModel.TenantsLabel";
                    public const string TenantRequired = "WebApp.Models.ApplicationUserViewModel.TenantRequired";
                }

                public static class BackOfficeUserViewModel
                {
                    public const string EmailRequired = "WebApp.Models.BackOfficeUserViewModel.EmailRequired";
                    public const string NameRequired = "WebApp.Models.BackOfficeUserViewModel.NameRequired";
                    public const string LastNameRequired = "WebApp.Models.BackOfficeUserViewModel.LastNameRequired";
                    public const string PhoneNumberRequired = "WebApp.Models.BackOfficeUserViewModel.PhoneNumberRequired";
                    public const string RolesRequired = "WebApp.Models.BackOfficeUserViewModel.RolesRequired";
                    public const string EmailInvalid = "WebApp.Models.BackOfficeUserViewModel.EmailInvalid";
                    public const string NameStringLength = "WebApp.Models.BackOfficeUserViewModel.NameStringLength";
                    public const string LastNameStringLength = "WebApp.Models.BackOfficeUserViewModel.LastNameStringLength";
                    public const string EmailLabel = "WebApp.Models.BackOfficeUserViewModel.EmailLabel";
                    public const string NameLabel = "WebApp.Models.BackOfficeUserViewModel.NameLabel";
                    public const string LastNameLabel = "WebApp.Models.BackOfficeUserViewModel.LastNameLabel";
                    public const string PhoneNumberLabel = "WebApp.Models.BackOfficeUserViewModel.PhoneNumberLabel";
                    public const string RolesLabel = "WebApp.Models.BackOfficeUserViewModel.RolesLabel";
                    public const string TenantsLabel = "WebApp.Models.BackOfficeUserViewModel.TenantsLabel";
                    public const string TenantRequired = "WebApp.Models.BackOfficeUserViewModel.TenantRequired";
                    public const string JobTitleLabel = "WebApp.Models.BackOfficeUserViewModel.JobTitleLabel";
                }
                public static class FrontUserViewModel
                {
                    public const string EmailRequired = "WebApp.Models.FrontUserViewModel.EmailRequired";
                    public const string NameRequired = "WebApp.Models.FrontUserViewModel.NameRequired";
                    public const string LastNameRequired = "WebApp.Models.FrontUserViewModel.LastNameRequired";
                    public const string PhoneNumberRequired = "WebApp.Models.FrontUserViewModel.PhoneNumberRequired";
                    public const string RolesRequired = "WebApp.Models.FrontUserViewModel.RolesRequired";
                    public const string EmailInvalid = "WebApp.Models.FrontUserViewModel.EmailInvalid";
                    public const string NameStringLength = "WebApp.Models.FrontUserViewModel.NameStringLength";
                    public const string LastNameStringLength = "WebApp.Models.FrontUserViewModel.LastNameStringLength";
                    public const string EmailLabel = "WebApp.Models.FrontUserViewModel.EmailLabel";
                    public const string NameLabel = "WebApp.Models.FrontUserViewModel.NameLabel";
                    public const string LastNameLabel = "WebApp.Models.FrontUserViewModel.LastNameLabel";
                    public const string PhoneNumberLabel = "WebApp.Models.FrontUserViewModel.PhoneNumberLabel";
                    public const string RolesLabel = "WebApp.Models.FrontUserViewModel.RolesLabel";
                    public const string TenantsLabel = "WebApp.Models.FrontUserViewModel.TenantsLabel";
                    public const string TenantRequired = "WebApp.Models.FrontUserViewModel.TenantRequired";
                    public const string BirthdateLabel = "WebApp.Models.FrontUserViewModel.BirthdateLabel";
                }

                public static class CategoryViewModel
                {
                    public const string NameRequired = "WebApp.Models.CategoryViewModel.NameRequired";
                    public const string DescriptionRequired = "WebApp.Models.CategoryViewModel.DescriptionRequired";
                    public const string NameLabel = "WebApp.Models.CategoryViewModel.NameLabel";
                    public const string DescriptionLabel = "WebApp.Models.CategoryViewModel.DescriptionLabel";
                    public const string DescriptionStringLength = "WebApp.Models.CategoryViewModel.DescriptionStringLength";
                }

                public static class QuestionViewModel
                {
                    public const string NameRequired = "WebApp.Models.QuestionViewModel.NameRequired";
                    public const string DescriptionRequired = "WebApp.Models.QuestionViewModel.DescriptionRequired";
                    public const string NameLabel = "WebApp.Models.QuestionViewModel.NameLabel";
                    public const string AnswersLabel = "WebApp.Models.QuestionViewModel.AnswersLabel";
                    public const string ThemeLabel = "WebApp.Models.QuestionViewModel.ThemeLabel";
                    public const string DescriptionStringLength = "WebApp.Models.QuestionViewModel.DescriptionStringLength";
                }

                public static class GameViewModel
                {
                    public const string NameRequired = "WebApp.Models.GameViewModel.NameRequired";
                    public const string DescriptionRequired = "WebApp.Models.GameViewModel.DescriptionRequired";
                    public const string NameLabel = "WebApp.Models.GameViewModel.NameLabel";
                    public const string AnswersLabel = "WebApp.Models.GameViewModel.AnswersLabel";
                    public const string ThemeLabel = "WebApp.Models.GameViewModel.ThemeLabel";
                    public const string DescriptionStringLength = "WebApp.Models.GameViewModel.DescriptionStringLength";
                }

                public static class GeneroViewModel
                {
                    public const string NameRequired = "WebApp.Models.GeneroViewModel.NameRequired";
                    public const string DescriptionRequired = "WebApp.Models.GeneroViewModel.DescriptionRequired";
                    public const string NameLabel = "WebApp.Models.GeneroViewModel.NameLabel";
                    public const string DescriptionLabel = "WebApp.Models.GeneroViewModel.DescriptionLabel";
                    public const string DescriptionStringLength = "WebApp.Models.GeneroViewModel.DescriptionStringLength";
                }

                public static class ActorViewModel
                {
                    public const string NameRequired = "WebApp.Models.ActorViewModel.NameRequired";
                    public const string NameLabel = "WebApp.Models.ActorViewModel.NameLabel";
                }
                public static class FeatureViewModel
                {
                    public const string NameRequired = "WebApp.Models.FeatureViewModel.NameRequired";
                    public const string DescriptionRequired = "WebApp.Models.FeatureViewModel.DescriptionRequired";
                    public const string NameLabel = "WebApp.Models.FeatureViewModel.NameLabel";
                    public const string DescriptionLabel = "WebApp.Models.FeatureViewModel.DescriptionLabel";
                    public const string DescriptionStringLength = "WebApp.Models.FeatureViewModel.DescriptionStringLength";
                }
                public static class CuriosidadViewModel
                {
                    public const string NameRequired = "WebApp.Models.CuriosidaViewModel.NameRequired";
                    public const string DescriptionRequired = "WebApp.Models.CuriosidaViewModel.DescriptionRequired";
                    public const string NameLabel = "WebApp.Models.CuriosidaViewModel.NameLabel";
                    public const string DescriptionLabel = "WebApp.Models.CuriosidaViewModel.DescriptionLabel";
                    public const string DescriptionStringLength = "WebApp.Models.CuriosidaViewModel.DescriptionStringLength";
                }

                public static class ProductViewModel
                {
                    public const string NameRequired = "WebApp.Models.ProductViewModel.NameRequired";
                    public const string DescriptionRequired = "WebApp.Models.ProductViewModel.DescriptionRequired";
                    public const string PriceRequired = "WebApp.Models.ProductViewModel.PriceRequired";
                    public const string NameLabel = "WebApp.Models.ProductViewModel.NameLabel";
                    public const string DescriptionLabel = "WebApp.Models.ProductViewModel.DescriptionLabel";
                    public const string PriceLabel = "WebApp.Models.ProductViewModel.PriceLabel";
                    public const string ReleaseDateLabel = "WebApp.Models.ProductViewModel.ReleaseDateLabel";
                    public const string CategoryLabel = "WebApp.Models.ProductViewModel.CategoryLabel";
                    public const string FeaturesLabel = "WebApp.Models.ProductViewModel.FeaturesLabel";
                    public const string EditorsIdsLabel = "WebApp.Models.ProductViewModel.EditorsIdsLabel";
                    public const string EditorsLabel = "WebApp.Models.ProductViewModel.EditorsLabel";
                    public const string DescriptionStringLength = "WebApp.Models.ProductViewModel.DescriptionStringLength";
                }

                public static class ThemeViewModel
                {
                    public const string NameRequired = "WebApp.Models.ThemeViewModel.NameRequired";
                    public const string ColorRequired = "WebApp.Models.ThemeViewModel.DescriptionRequired";
                    public const string NameLabel = "WebApp.Models.ThemeViewModel.NameLabel"; 
                    public const string ColorLabel = "WebApp.Models.ThemeViewModel.ColorLabel";
                    public const string QuestionQuantityLabel = "WebApp.Models.ThemeViewModel.QuestionQuantityLabel";
                    public const string ColorStringLength = "WebApp.Models.ThemeViewModel.ColorStringLength";
                    public const string NameStringLength = "WebApp.Models.ThemeViewModel.NameStringLength";
                }

                public static class PeliculaViewModel
                {
                    public const string NameRequired = "WebApp.Models.PeliculaViewModel.NameRequired";
                    public const string DescriptionRequired = "WebApp.Models.PeliculaViewModel.DescriptionRequired";
                    public const string NameLabel = "WebApp.Models.PeliculaViewModel.NameLabel";
                    public const string DescriptionLabel = "WebApp.Models.PeliculaViewModel.DescriptionLabel";
                    public const string GeneroLabel = "WebApp.Models.PeliculaViewModel.CategoryLabel";
                    public const string FeaturesLabel = "WebApp.Models.PeliculaViewModel.FeaturesLabel";
                    public const string ActoresIdsLabel = "WebApp.Models.PeliculaViewModel.ActoresIdsLabel";
                    public const string ActoresLabel = "WebApp.Models.PeliculaViewModel.ActoresLabel";
                    public const string CuriosidadLabel = "WebApp.Models.PeliculaViewModel.CuriosidadLabel";
                    public const string DescriptionStringLength = "WebApp.Models.PeliculaViewModel.DescriptionStringLength";
                }

                public static class HolidayTypeViewModel
                {
                    public const string NameRequired = "WebApp.Models.HolidayTypeViewModel.NameRequired";
                    public const string DescriptionRequired = "WebApp.Models.HolidayTypeViewModel.DescriptionRequired";
                    public const string NameLabel = "WebApp.Models.HolidayTypeViewModel.NameLabel";
                    public const string DescriptionLabel = "WebApp.Models.HolidayTypeViewModel.DescriptionLabel";
                    public const string DescriptionStringLength = "WebApp.Models.HolidayTypeViewModel.DescriptionStringLength";
                }

                public static class HolidayViewModel
                {
                    public const string NameRequired = "WebApp.Models.HolidayViewModel.NameRequired";
                    public const string DescriptionRequired = "WebApp.Models.HolidayViewModel.DescriptionRequired";
                    public const string NameLabel = "WebApp.Models.HolidayViewModel.NameLabel";
                    public const string DescriptionLabel = "WebApp.Models.HolidayViewModel.DescriptionLabel";
                    public const string DescriptionStringLength = "WebApp.Models.HolidayViewModel.DescriptionStringLength";
                    public const string MessageLabel = "WebApp.Models.HolidayViewModel.MessageLabel";
                    public const string MessageStringLength = "WebApp.Models.HolidayViewModel.MessageStringLength";
                    public const string StartDateRequired = "WebApp.Models.HolidayViewModel.StartDateRequired";
                    public const string StartDateLabel = "WebApp.Models.HolidayViewModel.StartDateLabel";
                    public const string EndDateLabel = "WebApp.Models.HolidayViewModel.EndDateLabel";
                    public const string ReasonLabel = "WebApp.Models.HolidayViewModel.ReasonLabel";
                    public const string UsersIdsLabel = "WebApp.Models.HolidayViewModel.UsersIdsLabel";
                    public const string UsersLabel = "WebApp.Models.HolidayViewModel.UsersLabel";
                    public const string ReasonRequired = "WebApp.Models.HolidayViewModel.ReasonRequired";
                }

                public static class TenantViewModel
                {
                    public const string CodeRequired = "WebApp.Models.TenantViewModel.CodeRequired";
                    public const string CodeLabel = "WebApp.Models.TenantViewModel.CodeLabel";
                    public const string CodeRegexError = "WebApp.Models.TenantViewModel.CodeRegexError";
                    public const string NameRequired = "WebApp.Models.TenantViewModel.NameRequired";
                    public const string NameLabel = "WebApp.Models.TenantViewModel.NameLabel";
                    public const string DescriptionRequired = "WebApp.Models.TenantViewModel.DescriptionRequired";
                    public const string DescriptionLabel = "WebApp.Models.TenantViewModel.DescriptionLabel";
                    public const string DescriptionStringLength = "WebApp.Models.TenantViewModel.DescriptionStringLength";
                    public const string PathLabel = "WebApp.Models.TenantViewModel.PathLabel";
                }

                public static class EventViewModel
                {
                    public const string NameRequired = "WebApp.Models.EventViewModel.NameRequired";
                    public const string NameLabel = "WebApp.Models.EventViewModel.NameLabel";
                    public const string DescriptionRequired = "WebApp.Models.EventViewModel.DescriptionRequired";
                    public const string DescriptionLabel = "WebApp.Models.EventViewModel.DescriptionLabel";
                    public const string DescriptionStringLength = "WebApp.Models.EventViewModel.DescriptionStringLength";
                    public const string DurationLabel = "WebApp.Models.EventViewModel.DurationLabel";
                    public const string DateRequired = "WebApp.Models.EventViewModel.DateRequired";
                    public const string DateLabel = "WebApp.Models.EventViewModel.DateLabel";
                    public const string TenantLabel = "WebApp.Models.EventViewModel.TenantLabel";
                    public const string TenantRequired = "WebApp.Models.EventViewModel.TenantRequired";
                    public const string FilesLabel = "WebApp.Models.EventViewModel.FilesLabel";
                    public const string LocationLabel = "WebApp.Models.EventViewModel.LocationLabel";
                }

                public static class AddressViewModel
                {
                    public const string CodeLabel = "WebApp.Models.AddressViewModel.CodeLabel";
                    public const string StreetLabel = "WebApp.Models.AddressViewModel.StreetLabel";
                    public const string StreetNumberLabel = "WebApp.Models.AddressViewModel.StreetNumberLabel";
                    public const string PostalCodeLabel = "WebApp.Models.AddressViewModel.PostalCodeLabel";
                    public const string LocalityLabel = "WebApp.Models.AddressViewModel.LocalityLabel";
                    public const string CityLabel = "WebApp.Models.AddressViewModel.CityLabel";
                    public const string CountryLabel = "WebApp.Models.AddressViewModel.CountryLabel";
                    public const string LatitudeLabel = "WebApp.Models.AddressViewModel.LatitudeLabel";
                    public const string LongitudeLabel = "WebApp.Models.AddressViewModel.LongitudeLabel";
                }

                public static class ProductRow
                {
                    public const string DeleteQuestionMessage = "WebApp.Models.ProductRow.DeleteQuestionMessage";
                    public const string DeleteConfirmationMessage = "WebApp.Models.ProductRow.DeleteConfirmationMessage";
                }
                public static class PeliculaRow
                {
                    public const string DeleteQuestionMessage = "WebApp.Models.PeliculaRow.DeleteQuestionMessage";
                    public const string DeleteConfirmationMessage = "WebApp.Models.PeliculaRow.DeleteConfirmationMessage";
                }
                public static class ActorRow
                {
                    public const string DeleteQuestionMessage = "WebApp.Models.PeliculaRow.DeleteQuestionMessage";
                    public const string DeleteConfirmationMessage = "WebApp.Models.PeliculaRow.DeleteConfirmationMessage";
                }
                public static class CategoryRow
                {
                    public const string DeleteQuestionMessage = "WebApp.Models.CategoryRow.DeleteQuestionMessage";
                    public const string DeleteConfirmationMessage = "WebApp.Models.CategoryRow.DeleteConfirmationMessage";
                }

                public static class GeneroRow
                {
                    public const string DeleteQuestionMessage = "WebApp.Models.GeneroRow.DeleteQuestionMessage";
                    public const string DeleteConfirmationMessage = "WebApp.Models.GeneroRow.DeleteConfirmationMessage";
                }

                public static class ApplicationUserRow
                {
                    public const string DeleteQuestionMessage = "WebApp.Models.ApplicationUserRow.DeleteQuestionMessage";
                    public const string DeleteConfirmationMessage = "WebApp.Models.ApplicationUserRow.DeleteConfirmationMessage";
                    public const string PasswordRecoveryConfirmationMessage = "WebApp.Models.ApplicationUserRow.PasswordRecoveryConfirmationMessage";
                    public const string PasswordRecoveryLabel = "WebApp.Models.ApplicationUserRow.PasswordRecoveryLabel";
                }
                public static class BackOfficeUserRow
                {
                    public const string DeleteQuestionMessage = "WebApp.Models.BackOfficeUserRow.DeleteQuestionMessage";
                    public const string DeleteConfirmationMessage = "WebApp.Models.BackOfficeUserRow.DeleteConfirmationMessage";
                    public const string PasswordRecoveryConfirmationMessage = "WebApp.Models.BackOfficeUserRow.PasswordRecoveryConfirmationMessage";
                    public const string PasswordRecoveryLabel = "WebApp.Models.BackOfficeUserRow.PasswordRecoveryLabel";
                }
                public static class FrontUserRow
                {
                    public const string DeleteQuestionMessage = "WebApp.Models.FrontUserRow.DeleteQuestionMessage";
                    public const string DeleteConfirmationMessage = "WebApp.Models.FrontUserRow.DeleteConfirmationMessage";
                    public const string PasswordRecoveryConfirmationMessage = "WebApp.Models.FrontUserRow.PasswordRecoveryConfirmationMessage";
                    public const string PasswordRecoveryLabel = "WebApp.Models.FrontUserRow.PasswordRecoveryLabel";
                }

                public static class HolidayRow
                {
                    public const string DeleteQuestionMessage = "WebApp.Models.HolidayRow.DeleteQuestionMessage";
                    public const string DeleteConfirmationMessage = "WebApp.Models.HolidayRow.DeleteConfirmationMessage";
                }

                public static class HolidayTypeRow
                {
                    public const string DeleteQuestionMessage = "WebApp.Models.HolidayTypeRow.DeleteQuestionMessage";
                    public const string DeleteConfirmationMessage = "WebApp.Models.HolidayTypeRow.DeleteConfirmationMessage";
                }

                public static class TenantRow
                {
                    public const string DeleteQuestionMessage = "WebApp.Models.TenantRow.DeleteQuestionMessage";
                    public const string DeleteConfirmationMessage = "WebApp.Models.TenantRow.DeleteConfirmationMessage";
                    public const string LabelBtnAddChild = "WebApp.Models.TenantRow.LabelBtnAddChild";
                }

                public static class EventRow
                {
                    public const string DeleteQuestionMessage = "WebApp.Models.EventRow.DeleteQuestionMessage";
                    public const string DeleteConfirmationMessage = "WebApp.Models.EventRow.DeleteConfirmationMessage";
                }
            }

            public static class Controllers
            {
                public static class CategoryController
                {
                    public const string IndexTitle = "WebApp.Controllers.CategoryController.IndexTitle";
                    public const string CreateTitle = "WebApp.Controllers.CategoryController.CreateTitle";
                    public const string EditTitle = "WebApp.Controllers.CategoryController.EditTitle";
                    public const string DetailsTitle = "WebApp.Controllers.CategoryController.DetailsTitle";
                    public const string DeleteSuccess = "WebApp.Controllers.CategoryController.DeleteSuccess";
                    public const string DeleteUnexpectedError = "WebApp.Controllers.CategoryController.DeleteUnexpectedError";
                }
                public static class StatisticsController
                {
                    public const string IndexTitle = "WebApp.Controllers.StatisticsController.IndexTitle";
                    public const string PieChart = "WebApp.Controllers.StatisticsController.PieChart";
                    public const string BarChart = "WebApp.Controllers.StatisticsController.BarChart";
                    public const string InvertChart = "WebApp.Controllers.StatisticsController.InvertChart";
                    public const string DeleteUnexpectedError = "WebApp.Controllers.StatisticsController.DeleteUnexpectedError";
                }
                public static class GameController
                {
                    public const string IndexTitle = "WebApp.Controllers.GameController.IndexTitle";
                    public const string PlayTitle = "WebApp.Controllers.GameController.PlayTitle";
                    public const string ResultTitle = "WebApp.Controllers.GameController.ResultTitle";
                    public const string EditTitle = "WebApp.Controllers.GameController.EditTitle";
                    public const string DetailsTitle = "WebApp.Controllers.GameController.DetailsTitle";
                    public const string DeleteSuccess = "WebApp.Controllers.GameController.DeleteSuccess";
                    public const string DeleteUnexpectedError = "WebApp.Controllers.GameController.DeleteUnexpectedError";
                }
                public static class QuestionController
                {
                    public const string IndexTitle = "WebApp.Controllers.QuestionController.IndexTitle";
                    public const string CreateTitle = "WebApp.Controllers.QuestionController.CreateTitle";
                    public const string FourAnswers = "WebApp.Controllers.QuestionController.FourAnswers";
                    public const string OneCorrect = "WebApp.Controllers.QuestionController.OneCorrect";
                    public const string EditTitle = "WebApp.Controllers.QuestionController.EditTitle";
                    public const string DetailsTitle = "WebApp.Controllers.QuestionController.DetailsTitle";
                    public const string DeleteSuccess = "WebApp.Controllers.QuestionController.DeleteSuccess";
                    public const string DeleteUnexpectedError = "WebApp.Controllers.QuestionController.DeleteUnexpectedError";
                }

                public static class GeneroController
                {
                    public const string IndexTitle = "WebApp.Controllers.GeneroController.IndexTitle";
                    public const string CreateTitle = "WebApp.Controllers.GeneroController.CreateTitle";
                    public const string EditTitle = "WebApp.Controllers.GeneroController.EditTitle";
                    public const string DeleteSuccess = "WebApp.Controllers.GeneroController.DeleteSuccess";
                    public const string DeleteUnexpectedError = "WebApp.Controllers.GeneroController.DeleteUnexpectedError";
                }

                public static class ActorController
                {
                    public const string IndexTitle = "WebApp.Controllers.ActorController.IndexTitle";
                    public const string CreateTitle = "WebApp.Controllers.ActorController.CreateTitle";
                    public const string EditTitle = "WebApp.Controllers.ActorController.EditTitle";
                    public const string DeleteSuccess = "WebApp.Controllers.ActorController.DeleteSuccess";
                    public const string DeleteUnexpectedError = "WebApp.Controllers.ActorController.DeleteUnexpectedError";
                    public const string DetailsTitle = "WebApp.Controllers.ActorController.DetailsTitle";
                }
                public static class ChartsController
                {
                    public const string IndexTitle = "WebApp.Controllers.ChartsController.IndexTitle";
                    public const string RadarFirstDatasetTitle = "WebApp.Controllers.ChartsController.RadarFirstDatasetTitle";
                    public const string RadarSecondDatasetTitle = "WebApp.Controllers.ChartsController.RadarSecondDatasetTitle";
                }

                public static class HomeController
                {
                    public const string IndexTitle = "WebApp.Controllers.HomeController.IndexTitle";
                    public const string PrivacyTitle = "WebApp.Controllers.HomeController.PrivacyTitle";
                    public const string ErrorActionTitle = "WebApp.Controllers.HomeController.ErrorActionTitle";
                    public const string ErrorActionDescription = "WebApp.Controllers.HomeController.ErrorActionDescription";
                }

                public static class ProductController
                {
                    public const string IndexTitle = "WebApp.Controllers.ProductController.IndexTitle";
                    public const string CreateTitle = "WebApp.Controllers.ProductController.CreateTitle";
                    public const string EditTitle = "WebApp.Controllers.ProductController.EditTitle";
                    public const string DetailsTitle = "WebApp.Controllers.ProductController.DetailsTitle";
                    public const string DeleteSuccess = "WebApp.Controllers.ProductController.DeleteSuccess";
                    public const string DeleteUnexpectedError = "WebApp.Controllers.ProductController.DeleteUnexpectedError";
                }

                public static class ThemeController
                {
                    public const string IndexTitle = "WebApp.Controllers.ThemeController.IndexTitle";
                    public const string CreateTitle = "WebApp.Controllers.ThemeController.CreateTitle";
                    public const string EditTitle = "WebApp.Controllers.ThemeController.EditTitle";
                    public const string DetailsTitle = "WebApp.Controllers.ThemeController.DetailsTitle";
                    public const string DeleteSuccess = "WebApp.Controllers.ThemeController.DeleteSuccess";
                    public const string DeleteUnexpectedError = "WebApp.Controllers.ThemeController.DeleteUnexpectedError";
                }

                public static class PeliculaController
                {
                    public const string IndexTitle = "WebApp.Controllers.PeliculaController.IndexTitle";
                    public const string CreateTitle = "WebApp.Controllers.PeliculaController.CreateTitle";
                    public const string EditTitle = "WebApp.Controllers.PeliculaController.EditTitle";
                    public const string DetailsTitle = "WebApp.Controllers.PeliculaController.DetailsTitle";
                    public const string DeleteSuccess = "WebApp.Controllers.PeliculaController.DeleteSuccess";
                    public const string DeleteUnexpectedError = "WebApp.Controllers.PeliculaController.DeleteUnexpectedError";
                }
                public static class UserController
                {
                    public const string IndexTitle = "WebApp.Controllers.UserController.IndexTitle";
                    public const string CreateTitle = "WebApp.Controllers.UserController.CreateTitle";
                    public const string EditTitle = "WebApp.Controllers.UserController.EditTitle";
                    public const string DetailsTitle = "WebApp.Controllers.UserController.DetailsTitle";
                    public const string DeleteSuccess = "WebApp.Controllers.UserController.DeleteSuccess";
                    public const string DeleteUnexpectedError = "WebApp.Controllers.UserController.DeleteUnexpectedError";
                    public const string PasswordRecoveryEmailSubject = "WebApp.Controllers.UserController.PasswordRecoveryEmailSubject";
                    public const string PasswordRecoverySuccess = "WebApp.Controllers.UserController.PasswordRecoverySuccess";
                    public const string PasswordRecoveryUnexpectedError = "WebApp.Controllers.UserController.PasswordRecoveryUnexpectedError";
                }
                public static class BackOfficeUserController
                {
                    public const string IndexTitle = "WebApp.Controllers.BackOfficeUserController.IndexTitle";
                    public const string CreateTitle = "WebApp.Controllers.BackOfficeUserController.CreateTitle";
                    public const string EditTitle = "WebApp.Controllers.BackOfficeUserController.EditTitle";
                    public const string DetailsTitle = "WebApp.Controllers.BackOfficeUserController.DetailsTitle";
                    public const string DeleteSuccess = "WebApp.Controllers.BackOfficeUserController.DeleteSuccess";
                    public const string DeleteUnexpectedError = "WebApp.Controllers.BackOfficeUserController.DeleteUnexpectedError";
                    public const string PasswordRecoveryEmailSubject = "WebApp.Controllers.BackOfficeUserController.PasswordRecoveryEmailSubject";
                    public const string PasswordRecoverySuccess = "WebApp.Controllers.BackOfficeUserController.PasswordRecoverySuccess";
                    public const string PasswordRecoveryUnexpectedError = "WebApp.Controllers.BackOfficeUserController.PasswordRecoveryUnexpectedError";
                }

                public static class FrontUserController
                {
                    public const string IndexTitle = "WebApp.Controllers.FrontUserController.IndexTitle";
                    public const string CreateTitle = "WebApp.Controllers.FrontUserController.CreateTitle";
                    public const string EditTitle = "WebApp.Controllers.FrontUserController.EditTitle";
                    public const string DetailsTitle = "WebApp.Controllers.FrontUserController.DetailsTitle";
                    public const string DeleteSuccess = "WebApp.Controllers.FrontUserController.DeleteSuccess";
                    public const string DeleteUnexpectedError = "WebApp.Controllers.FrontUserController.DeleteUnexpectedError";
                    public const string PasswordRecoveryEmailSubject = "WebApp.Controllers.FrontUserController.PasswordRecoveryEmailSubject";
                    public const string PasswordRecoverySuccess = "WebApp.Controllers.FrontUserController.PasswordRecoverySuccess";
                    public const string PasswordRecoveryUnexpectedError = "WebApp.Controllers.FrontUserController.PasswordRecoveryUnexpectedError";
                }

                public static class HolidayTypeController
                {
                    public const string IndexTitle = "WebApp.Controllers.HolidayTypeController.IndexTitle";
                    public const string CreateTitle = "WebApp.Controllers.HolidayTypeController.CreateTitle";
                    public const string EditTitle = "WebApp.Controllers.HolidayTypeController.EditTitle";
                    public const string DetailsTitle = "WebApp.Controllers.HolidayTypeController.DetailsTitle";
                    public const string DeleteSuccess = "WebApp.Controllers.HolidayTypeController.DeleteSuccess";
                    public const string DeleteUnexpectedError = "WebApp.Controllers.HolidayTypeController.DeleteUnexpectedError";
                }

                public static class HolidayController
                {
                    public const string IndexTitle = "WebApp.Controllers.HolidayController.IndexTitle";
                    public const string CreateTitle = "WebApp.Controllers.HolidayController.CreateTitle";
                    public const string EditTitle = "WebApp.Controllers.HolidayController.EditTitle";
                    public const string DetailsTitle = "WebApp.Controllers.HolidayController.DetailsTitle";
                    public const string DeleteSuccess = "WebApp.Controllers.HolidayController.DeleteSuccess";
                    public const string DeleteUnexpectedError = "WebApp.Controllers.HolidayController.DeleteUnexpectedError";
                    public const string NoUsersMessage = "WebApp.Controllers.HolidayController.NoUsersMessage";
                    public const string ImportUnexpectedError = "WebApp.Controllers.HolidayController.ImportUnexpectedError";

                }

                public static class TenantController
                {
                    public const string IndexTitle = "WebApp.Controllers.TenantController.IndexTitle";
                    public const string CreateTitle = "WebApp.Controllers.TenantController.CreateTitle";
                    public const string EditTitle = "WebApp.Controllers.TenantController.EditTitle";
                    public const string DetailsTitle = "WebApp.Controllers.TenantController.DetailsTitle";
                    public const string DeleteSuccess = "WebApp.Controllers.TenantController.DeleteSuccess";
                    public const string DeleteUnexpectedError = "WebApp.Controllers.TenantController.DeleteUnexpectedError";
                }

                public static class EventController
                {
                    public const string IndexTitle = "WebApp.Controllers.EventController.IndexTitle";
                    public const string CreateTitle = "WebApp.Controllers.EventController.CreateTitle";
                    public const string EditTitle = "WebApp.Controllers.EventController.EditTitle";
                    public const string DetailsTitle = "WebApp.Controllers.EventController.DetailsTitle";
                    public const string DeleteSuccess = "WebApp.Controllers.EventController.DeleteSuccess";
                    public const string DeleteUnexpectedError = "WebApp.Controllers.EventController.DeleteUnexpectedError";
                }
            }

            public static class Views
            {
                public static class Category
                {
                    public static class CreateOrEdit
                    {
                        public const string BtnCancel = "WebApp.Views.Category.CreateOrEdit.BtnCancel";
                        public const string BtnCreate = "WebApp.Views.Category.CreateOrEdit.BtnCreate";
                        public const string BtnUpdate = "WebApp.Views.Category.CreateOrEdit.BtnUpdate";
                    }
                    public static class Details
                    {
                        public const string BtnBack = "WebApp.Views.Category.Details.BtnBack";
                    }

                    public static class Index
                    {
                        public const string BtnNew = "WebApp.Views.Category.Index.BtnNew";
                        public const string TableColName = "WebApp.Views.Category.Index.TableColName";
                        public const string TableColDescription = "WebApp.Views.Category.Index.TableColDescription";
                        public const string TableColActions = "WebApp.Views.Category.Index.TableColActions";
                    }
                }
                public static class Genero
                {
                    public static class CreateOrEdit
                    {
                        public const string BtnCancel = "WebApp.Views.Genero.CreateOrEdit.BtnCancel";
                        public const string BtnCreate = "WebApp.Views.Genero.CreateOrEdit.BtnCreate";
                        public const string BtnUpdate = "WebApp.Views.Genero.CreateOrEdit.BtnUpdate";
                    }
                    public static class Details
                    {
                        public const string BtnBack = "WebApp.Views.Genero.Details.BtnBack";
                    }

                    public static class Index
                    {
                        public const string BtnNew = "WebApp.Views.Genero.Index.BtnNew";
                        public const string TableColName = "WebApp.Views.Genero.Index.TableColName";
                        public const string TableColActions = "WebApp.Views.Genero.Index.TableColActions";
                    }
                }
                public static class Actor
                {
                    public static class CreateOrEdit
                    {
                        public const string BtnCancel = "WebApp.Views.Actor.CreateOrEdit.BtnCancel";
                        public const string BtnCreate = "WebApp.Views.Actor.CreateOrEdit.BtnCreate";
                        public const string BtnUpdate = "WebApp.Views.Actor.CreateOrEdit.BtnUpdate";
                    }
                    public static class Details
                    {
                        public const string BtnBack = "WebApp.Views.Actor.Details.BtnBack";
                    }

                    public static class Index
                    {
                        public const string BtnNew = "WebApp.Views.Actor.Index.BtnNew";
                        public const string TableColName = "WebApp.Views.Actor.Index.TableColName";
                        public const string TableColActions = "WebApp.Views.Actor.Index.TableColActions";
                    }
                }

                public static class HolidayType
                {
                    public static class CreateOrEdit
                    {
                        public const string BtnCancel = "WebApp.Views.HolidayType.CreateOrEdit.BtnCancel";
                        public const string BtnCreate = "WebApp.Views.HolidayType.CreateOrEdit.BtnCreate";
                        public const string BtnUpdate = "WebApp.Views.HolidayType.CreateOrEdit.BtnUpdate";
                    }
                    public static class Details
                    {
                        public const string BtnBack = "WebApp.Views.HolidayType.Details.BtnBack";
                    }

                    public static class Index
                    {
                        public const string BtnNew = "WebApp.Views.HolidayType.Index.BtnNew";
                        public const string TableColName = "WebApp.Views.HolidayType.Index.TableColName";
                        public const string TableColDescription = "WebApp.Views.HolidayType.Index.TableColDescription";
                        public const string TableColActions = "WebApp.Views.HolidayType.Index.TableColActions";
                    }
                }

                public static class Holiday
                {
                    public static class CreateOrEdit
                    {
                        public const string BtnCancel = "WebApp.Views.Holiday.CreateOrEdit.BtnCancel";
                        public const string BtnCreate = "WebApp.Views.Holiday.CreateOrEdit.BtnCreate";
                        public const string BtnUpdate = "WebApp.Views.Holiday.CreateOrEdit.BtnUpdate";
                        public const string PlaceholderSelectReason = "WebApp.Views.Holiday.CreateOrEdit.PlaceholderSelectReason";
                    }
                    public static class Details
                    {
                        public const string BtnBack = "WebApp.Views.Holiday.Details.BtnBack";
                    }

                    public static class Index
                    {
                        public const string BtnNew = "WebApp.Views.Holiday.Index.BtnNew";
                        public const string BtnExportAll = "WebApp.Views.Holiday.Index.BtnExportAll";
                        public const string BtnExportFiltered = "WebApp.Views.Holiday.Index.BtnExportFiltered";
                        public const string TableColName = "WebApp.Views.Holiday.Index.TableColName";
                        public const string TableColDescription = "WebApp.Views.Holiday.Index.TableColDescription";
                        public const string TableColReason = "WebApp.Views.Holiday.Index.TableColReason";
                        public const string TableColUsers = "WebApp.Views.Holiday.Index.TableColUsers";
                        public const string TableColActions = "WebApp.Views.Holiday.Index.TableColActions";
                    }
                }

                public static class Home
                {
                    public static class Privacy
                    {
                        public const string Detail = "WebApp.Views.Home.Privacy.Detail";
                    }
                    public static class Index
                    {
                        public const string Title = "WebApp.Views.Home.Index.Title";
                        public const string Play = "WebApp.Views.Statistics.Index.Play";
                    }
                }

                public static class Product
                {
                    public static class CreateOrEdit
                    {
                        public const string BtnCancel = "WebApp.Views.Product.CreateOrEdit.BtnCancel";
                        public const string BtnCreate = "WebApp.Views.Product.CreateOrEdit.BtnCreate";
                        public const string BtnUpdate = "WebApp.Views.Product.CreateOrEdit.BtnUpdate";
                        public const string BtnAddFeature = "WebApp.Views.Product.CreateOrEdit.BtnAddFeature";
                        public const string BtnDeleteFeature = "WebApp.Views.Product.CreateOrEdit.BtnDeleteFeature";
                        public const string PlaceholderSelectCategory = "WebApp.Views.Product.CreateOrEdit.PlaceholderSelectCategory";
                        public const string TableFeatureName = "WebApp.Views.Product.CreateOrEdit.TableFeatureName";
                        public const string TableFeatureDescription = "WebApp.Views.Product.CreateOrEdit.TableFeatureDescription";
                        public const string TableFeatureActions = "WebApp.Views.Product.CreateOrEdit.TableFeatureActions";
                    }

                    public static class Details
                    {
                        public const string BtnBack = "WebApp.Views.Product.Details.BtnBack";
                    }

                    public static class Index
                    {
                        public const string BtnNew = "WebApp.Views.Product.Index.BtnNew";
                        public const string TableColName = "WebApp.Views.Product.Index.TableColName";
                        public const string TableColDescription = "WebApp.Views.Product.Index.TableColDescription";
                        public const string TableColPrice = "WebApp.Views.Product.Index.TableColPrice";
                        public const string TableColActions = "WebApp.Views.Product.Index.TableColActions";
                    }
                }

                public static class Theme
                {
                    public static class CreateOrEdit
                    {
                        public const string BtnCancel = "WebApp.Views.Theme.CreateOrEdit.BtnCancel";
                        public const string BtnCreate = "WebApp.Views.Theme.CreateOrEdit.BtnCreate";
                        public const string BtnUpdate = "WebApp.Views.Theme.CreateOrEdit.BtnUpdate";
                    }

                    public static class Details
                    {
                        public const string BtnBack = "WebApp.Views.Theme.Details.BtnBack";
                    }

                    public static class Index
                    {
                        public const string BtnNew = "WebApp.Views.Theme.Index.BtnNew";
                        public const string TableColName = "WebApp.Views.Theme.Index.TableColName";
                        public const string TableColColor = "WebApp.Views.Theme.Index.TableColColor";
                        public const string TableColQuestionQuantity = "WebApp.Views.Theme.Index.TableColQuestionQuantity";
                        public const string TableColActions = "WebApp.Views.Theme.Index.TableColActions";
                    }
                }
                public static class Statistics
                {
                    public static class Index
                    {

                        public const string Play = "WebApp.Views.Statistics.Index.Play";
                    }

                }

                public static class Game
                {


                    public static class Play
                    {
                        public const string PlayTitle = "WebApp.Controllers.GameController.PlayTitle";
                        public const string Next = "WebApp.Views.Game.Play.Next";
                    }

                    public static class Index
                    {
                        public const string IndexTitle = "WebApp.Views.Game.Index.IndexTitle";
                        public const string Play = "WebApp.Views.Game.Index.Play";
                        public const string Soon = "WebApp.Views.Game.Index.Soon";
                        public const string Statistics = "WebApp.Views.Game.Index.Statistics";
                        public const string TableColColor = "WebApp.Views.Game.Index.TableColColor";
                        public const string TableColQuestionQuantity = "WebApp.Views.Game.Index.TableColQuestionQuantity";
                        public const string TableColActions = "WebApp.Views.Theme.Index.TableColActions";
                    }

                    public static class Result
                    {
                        
                        public const string Done = "WebApp.Views.Game.Play.Done";
                    }
                }
                public static class Pelicula
                {
                    public static class CreateOrEdit
                    {
                        public const string BtnCancel = "WebApp.Views.Pelicula.CreateOrEdit.BtnCancel";
                        public const string BtnCreate = "WebApp.Views.Pelicula.CreateOrEdit.BtnCreate";
                        public const string BtnUpdate = "WebApp.Views.Pelicula.CreateOrEdit.BtnUpdate";
                        public const string BtnAddCuriosidad = "WebApp.Views.Pelicula.CreateOrEdit.BtnAddCuriosidad";
                        public const string BtnDeleteCuriosidad = "WebApp.Views.Pelicula.CreateOrEdit.BtnDeleteCuriosidad";
                        public const string PlaceholderSelectCategory = "WebApp.Views.Pelicula.CreateOrEdit.PlaceholderSelectCategory";
                        public const string TableCuriosidadName = "WebApp.Views.Pelicula.CreateOrEdit.TableCuriosidadName";
                        public const string TableCuriosidadDescription = "WebApp.Views.Pelicula.CreateOrEdit.TableCuriosidadDescription";
                        public const string TableCuriosidadActions = "WebApp.Views.Pelicula.CreateOrEdit.TableCuriosidadActions";
                    }

                    public static class Details
                    {
                        public const string BtnBack = "WebApp.Views.Pelicula.Details.BtnBack";
                    }

                    public static class Index
                    {
                        public const string BtnNew = "WebApp.Views.Pelicula.Index.BtnNew";
                        public const string TableColName = "WebApp.Views.Pelicula.Index.TableColName";
                        public const string TableColDescription = "WebApp.Views.Pelicula.Index.TableColDescription";
                        public const string TableColPrice = "WebApp.Views.Pelicula.Index.TableColPrice";
                        public const string TableColActions = "WebApp.Views.Pelicula.Index.TableColActions";
                    }
                }
                public static class Question
                {
                    public static class CreateOrEdit
                    {
                        public const string BtnCancel = "WebApp.Views.Question.CreateOrEdit.BtnCancel";
                        public const string BtnCreate = "WebApp.Views.Question.CreateOrEdit.BtnCreate";
                        public const string BtnUpdate = "WebApp.Views.Question.CreateOrEdit.BtnUpdate";
                        public const string BtnAddAnswer = "WebApp.Views.Question.CreateOrEdit.BtnAddAnswer";
                        public const string BtnDeleteAnswer = "WebApp.Views.Question.CreateOrEdit.BtnDeleteAnswer";
                        public const string PlaceholderSelectQuestion = "WebApp.Views.Question.CreateOrEdit.PlaceholderSelectQuestion";
                        public const string TableCuriosidadName = "WebApp.Views.Question.CreateOrEdit.TableCuriosidadName";
                        public const string TableAnswerName = "WebApp.Views.Question.CreateOrEdit.TableAnswerName";
                        public const string TableAnswerIsCorrect = "WebApp.Views.Question.CreateOrEdit.TableAnswerIsCorrect";
                        public const string TableAnswerActions = "WebApp.Views.Question.CreateOrEdit.TableAnswerActions";
                        public const string NameLabel = "WebApp.Models.QuestionViewModel.NameLabel";
                        public const string ThemeLabel = "WebApp.Models.QuestionViewModel.ThemeLabel";

                    }

                    public static class Details
                    {
                        public const string BtnBack = "WebApp.Views.Question.Details.BtnBack";
                        public const string TableAnswerName = "WebApp.Views.Question.Details.TableAnswerName";
                        public const string TableAnswerIsCorrect = "WebApp.Views.Question.Details.TableAnswerIsCorrect";
                        public const string TableAnswerActions = "WebApp.Views.Question.Details.TableAnswerActions";
                    }

                    public static class Index
                    {
                        public const string BtnNew = "WebApp.Views.Question.Index.BtnNew";
                        public const string TableColName = "WebApp.Views.Question.Index.TableColName";
                        public const string TableColTheme = "WebApp.Views.Question.Index.TableColTheme";
                        public const string TableColActions = "WebApp.Views.Question.Index.TableColActions";
                    }
                }

                public static class User
                {
                    public static class CreateOrEdit
                    {
                        public const string BtnCancel = "WebApp.Views.User.CreateOrEdit.BtnCancel";
                        public const string BtnCreate = "WebApp.Views.User.CreateOrEdit.BtnCreate";
                        public const string BtnUpdate = "WebApp.Views.User.CreateOrEdit.BtnUpdate";
                        public const string PlaceholderSelectTenant = "WebApp.Views.User.CreateOrEdit.PlaceholderSelectTenant";
                    }

                    public static class Details
                    {
                        public const string BtnBack = "WebApp.Views.User.Details.BtnBack";
                    }

                    public static class Index
                    {
                        public const string BtnNew = "WebApp.Views.User.Index.BtnNew";
                        public const string TableColName = "WebApp.Views.User.Index.TableColName";
                        public const string TableColLastName = "WebApp.Views.User.Index.TableColLastName";
                        public const string TableColEmail = "WebApp.Views.User.Index.TableColEmail";
                        public const string TableColPhone = "WebApp.Views.User.Index.TableColPhone";
                        public const string TableColActions = "WebApp.Views.User.Index.TableColActions";
                    }
                }

                public static class BackOfficeUser
                {
                    public static class CreateOrEdit
                    {
                        public const string BtnCancel = "WebApp.Views.BackOfficeUser.CreateOrEdit.BtnCancel";
                        public const string BtnCreate = "WebApp.Views.BackOfficeUser.CreateOrEdit.BtnCreate";
                        public const string BtnUpdate = "WebApp.Views.BackOfficeUser.CreateOrEdit.BtnUpdate";
                        public const string PlaceholderSelectTenant = "WebApp.Views.BackOfficeUser.CreateOrEdit.PlaceholderSelectTenant";
                    }

                    public static class Details
                    {
                        public const string BtnBack = "WebApp.Views.BackOfficeUser.Details.BtnBack";
                    }

                    public static class Index
                    {
                        public const string BtnNew = "WebApp.Views.BackOfficeUser.Index.BtnNew";
                        public const string TableColName = "WebApp.Views.BackOfficeUser.Index.TableColName";
                        public const string TableColLastName = "WebApp.Views.BackOfficeUser.Index.TableColLastName";
                        public const string TableColEmail = "WebApp.Views.BackOfficeUser.Index.TableColEmail";
                        public const string TableColPhone = "WebApp.Views.BackOfficeUser.Index.TableColPhone";
                        public const string TableColJobTitle = "WebApp.Views.BackOfficeUser.Index.TableColJobTitle";
                        public const string TableColActions = "WebApp.Views.BackOfficeUser.Index.TableColActions";
                    }
                }

                public static class FrontUser
                {
                    public static class CreateOrEdit
                    {
                        public const string BtnCancel = "WebApp.Views.FrontUser.CreateOrEdit.BtnCancel";
                        public const string BtnCreate = "WebApp.Views.FrontUser.CreateOrEdit.BtnCreate";
                        public const string BtnUpdate = "WebApp.Views.FrontUser.CreateOrEdit.BtnUpdate";
                        public const string PlaceholderSelectTenant = "WebApp.Views.FrontUser.CreateOrEdit.PlaceholderSelectTenant";
                    }

                    public static class Details
                    {
                        public const string BtnBack = "WebApp.Views.FrontUser.Details.BtnBack";
                    }

                    public static class Index
                    {
                        public const string BtnNew = "WebApp.Views.FrontUser.Index.BtnNew";
                        public const string TableColName = "WebApp.Views.FrontUser.Index.TableColName";
                        public const string TableColLastName = "WebApp.Views.FrontUser.Index.TableColLastName";
                        public const string TableColEmail = "WebApp.Views.FrontUser.Index.TableColEmail";
                        public const string TableColPhone = "WebApp.Views.FrontUser.Index.TableColPhone";
                        public const string TableColBirthdate = "WebApp.Views.FrontUser.Index.TableColBirthdate";
                        public const string TableColActions = "WebApp.Views.FrontUser.Index.TableColActions";
                    }
                }

                public static class Shared
                {
                    public static class Error
                    {
                        public const string BtnBack = "WebApp.Views.Shared.Error.BtnBack";
                        public const string ErrorTitle = "WebApp.Views.Shared.Error.ErrorTitle";
                    }

                    public static class _LoginPartial
                    {
                        public const string BtnAccount = "WebApp.Views.Shared._LoginPartial.BtnAccount";
                        public const string BtnLogout = "WebApp.Views.Shared._LoginPartial.BtnLogout";
                        public const string BtnRegister = "WebApp.Views.Shared._LoginPartial.BtnRegister";
                        public const string BtnLogin = "WebApp.Views.Shared._LoginPartial.BtnLogin";
                        public const string NotificationsTitle = "WebApp.Views.Shared._LoginPartial.NotificationsTitle";
                        public const string BtnAllNotifications = "WebApp.Views.Shared._LoginPartial.BtnAllNotifications";
                        public const string Privacy = "WebApp.Views.Shared._LoginPartial.Privacy";
                    }

                    public static class _Navbar
                    {
                        public const string LogoText = "WebApp.Views.Shared._Navbar.LogoText";
                    }

                    public static class _Sidebar
                    {
                        public const string BtnFrontUsers = "WebApp.Views.Shared._Sidebar.BtnFrontUsers";
                        public const string BtnBackOfficeUsers = "WebApp.Views.Shared._Sidebar.BtnBackOfficeUsers";
                        public const string BtnAccount = "WebApp.Views.Shared._Sidebar.BtnAccount";
                        public const string BtnProducts = "WebApp.Views.Shared._Sidebar.BtnProducts";
                        public const string BtnCategories = "WebApp.Views.Shared._Sidebar.BtnCategories";
                        public const string BtnCharts = "WebApp.Views.Shared._Sidebar.BtnCharts";
                        public const string BtnHolidayType = "WebApp.Views.Shared._Sidebar.BtnHolidayType";
                        public const string BtnHoliday = "WebApp.Views.Shared._Sidebar.BtnHoliday";
                        public const string BtnSettings = "WebApp.Views.Shared._Sidebar.BtnSettings";
                        public const string BtnTenants = "WebApp.Views.Shared._Sidebar.BtnTenants";
                        public const string BtnEvents = "WebApp.Views.Shared._Sidebar.BtnEvents";
                        public const string BtnPlay = "WebApp.Views.Statistics.Index.Play";
                        public const string BtnStatistics = "WebApp.Views.Game.Index.Statistics";
                    }

                    public static class _FileManager
                    {
                        public const string BtnDownload = "WebApp.Views.Shared._FileManager.BtnDownload";
                        public const string NoFilesToDisplay = "WebApp.Views.Shared._FileManager.NoFilesToDisplay";
                    }

                    public static class _ExcelImport
                    {
                        public const string BtnImport = "WebApp.Views.Shared._ExcelImport.BtnImport";
                        public const string Title = "WebApp.Views.Shared._ExcelImport.Title";
                        public const string BtnClose = "WebApp.Views.Shared._ExcelImport.BtnClose";
                    }
                }

                public static class Tenant
                {
                    public static class CreateOrEdit
                    {
                        public const string BtnCancel = "WebApp.Views.Tenant.CreateOrEdit.BtnCancel";
                        public const string BtnCreate = "WebApp.Views.Tenant.CreateOrEdit.BtnCreate";
                        public const string BtnUpdate = "WebApp.Views.Tenant.CreateOrEdit.BtnUpdate";
                    }
                    public static class Details
                    {
                        public const string BtnBack = "WebApp.Views.Tenant.Details.BtnBack";
                    }

                    public static class Index
                    {
                        public const string BtnNew = "WebApp.Views.Tenant.Index.BtnNew";
                        public const string TableColName = "WebApp.Views.Tenant.Index.TableColName";
                        public const string TableColCode = "WebApp.Views.Tenant.Index.TableColCode";
                        public const string TableColPath = "WebApp.Views.Tenant.Index.TableColPath";
                        public const string TableColActions = "WebApp.Views.Tenant.Index.TableColActions";
                    }
                }

                public static class Event
                {
                    public static class CreateOrEdit
                    {
                        public const string BtnCancel = "WebApp.Views.Event.CreateOrEdit.BtnCancel";
                        public const string BtnCreate = "WebApp.Views.Event.CreateOrEdit.BtnCreate";
                        public const string BtnUpdate = "WebApp.Views.Event.CreateOrEdit.BtnUpdate";
                    }
                    public static class Details
                    {
                        public const string BtnBack = "WebApp.Views.Event.Details.BtnBack";
                    }

                    public static class Index
                    {
                        public const string BtnNew = "WebApp.Views.Event.Index.BtnNew";
                        public const string TableColName = "WebApp.Views.Event.Index.TableColName";
                        public const string TableColDescription = "WebApp.Views.Event.Index.TableColDescription";
                        public const string TableColDate = "WebApp.Views.Event.Index.TableColDate";
                        public const string TableColActions = "WebApp.Views.Event.Index.TableColActions";
                    }
                }

                public static class Charts
                {
                    public static class Index
                    {
                        public const string LineChartTitle = "WebApp.Views.Charts.Index.LineChartTitle";
                        public const string BarChartTitle = "WebApp.Views.Charts.Index.BarChartTitle";
                        public const string PieChartTitle = "WebApp.Views.Charts.Index.PieChartTitle";
                        public const string DoughnutChartTitle = "WebApp.Views.Charts.Index.DoughnutChartTitle";
                        public const string PolarChartTitle = "WebApp.Views.Charts.Index.PolarChartTitle";
                        public const string RadarChartTitle = "WebApp.Views.Charts.Index.RadarChartTitle";
                    }
                }
            }

            public static class WebTools
            {
                public static class ExceptionHandlerMiddleware
                {
                    public const string ErrorTitle = "WebApp.WebTools.ExceptionHandlerMiddleware.ErrorTitle";
                    public const string ErrorTitle404 = "WebApp.WebTools.ExceptionHandlerMiddleware.ErrorTitle404";
                    public const string ErrorGenericDescription = "WebApp.WebTools.ExceptionHandlerMiddleware.ErrorGenericDescription";
                }

                public static class FileManagerOptions
                {
                    public const string DictDefaultMessage = "WebApp.WebTools.FileManagerOptions.DictDefaultMessage";
                    public const string DictCancelUpload = "WebApp.WebTools.FileManagerOptions.DictCancelUpload";
                    public const string DictUploadCanceled = "WebApp.WebTools.FileManagerOptions.DictUploadCanceled";
                    public const string DictCancelUploadConfirmation = "WebApp.WebTools.FileManagerOptions.DictCancelUploadConfirmation";
                    public const string DictRemoveFile = "WebApp.WebTools.FileManagerOptions.DictRemoveFile";
                    public const string DictMaxFilesExceeded = "WebApp.WebTools.FileManagerOptions.DictMaxFilesExceeded";
                    public const string DictInvalidFileType = "WebApp.WebTools.FileManagerOptions.DictInvalidFileType";
                    public const string DictFileTooBig = "WebApp.WebTools.FileManagerOptions.DictFileTooBig";
                }

                public static class SweetAlert
                {
                    public const string ConfirmLabel = "WebApp.WebTools.SweetAlert.ConfirmLabel";
                    public const string CancelLabel = "WebApp.WebTools.SweetAlert.CancelLabel";
                }

                public static class Datatable
                {
                    public const string ActionsLabel = "WebApp.WebTools.Datatable.ActionsLabel";
                }

                public static class IgniteAddress
                {
                    public const string ClearCurrentLocation = "WebApp.WebTools.IgniteAddress.ClearCurrentLocation";
                }

                public static class ExcelImportOptions
                {
                    public const string ActionUrlNotDefined = "WebApp.WebTools.ExcelImportOptions.ActionUrlNotDefined";
                    public const string DictDefaultMessage = "WebApp.WebTools.ExcelImportOptions.DictDefaultMessage";
                    public const string DictCancelUpload = "WebApp.WebTools.ExcelImportOptions.DictCancelUpload";
                    public const string DictUploadCanceled = "WebApp.WebTools.ExcelImportOptions.DictUploadCanceled";
                    public const string DictCancelUploadConfirmation = "WebApp.WebTools.ExcelImportOptions.DictCancelUploadConfirmation";
                    public const string DictRemoveFile = "WebApp.WebTools.ExcelImportOptions.DictRemoveFile";
                    public const string DictMaxFilesExceeded = "WebApp.WebTools.ExcelImportOptions.DictMaxFilesExceeded";
                    public const string DictInvalidFileType = "WebApp.WebTools.ExcelImportOptions.DictInvalidFileType";
                    public const string DictFileTooBig = "WebApp.WebTools.ExcelImportOptions.DictFileTooBig";
                    public const string DictImportSucceededTitle = "WebApp.WebTools.ExcelImportOptions.DictImportSucceededTitle";
                    public const string DictImportSucceededMessage = "WebApp.WebTools.ExcelImportOptions.DictImportSucceededMessage";
                    public const string DictImportFailedTitle = "WebApp.WebTools.ExcelImportOptions.DictImportFailedTitle";
                }
            }

            public static class Js
            {
                public readonly static List<string> Datatables = new List<string>()
                {
                    "WebApp.Js.Datatables.LanguageProcessing",
                    "WebApp.Js.Datatables.LanguageLengthMenu",
                    "WebApp.Js.Datatables.LanguageZeroRecords",
                    "WebApp.Js.Datatables.LanguageEmptyTable",
                    "WebApp.Js.Datatables.LanguageInfo",
                    "WebApp.Js.Datatables.LanguageInfoEmpty",
                    "WebApp.Js.Datatables.LanguageInfoFiltered",
                    "WebApp.Js.Datatables.LanguageSearch",
                    "WebApp.Js.Datatables.LanguageLoadingRecords",
                    "WebApp.Js.Datatables.LanguagePaginateFirst",
                    "WebApp.Js.Datatables.LanguagePaginateLast",
                    "WebApp.Js.Datatables.LanguagePaginateNext",
                    "WebApp.Js.Datatables.LanguagePaginatePrevious",
                    "WebApp.Js.Datatables.LanguageAriaSortAscending",
                    "WebApp.Js.Datatables.LanguageAriaSortDescending",
                    "WebApp.Js.Datatables.ButtonsPrint",
                    "WebApp.Js.Datatables.LengthMenuAll",
                    "WebApp.Js.Datatables.NoData"
                };
            }
        }
    }
}