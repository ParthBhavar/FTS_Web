using Microsoft.Extensions.DependencyInjection;
using Email;
using Microsoft.Extensions.Configuration;
using FileManager;
using FTS.Configuration;
using FTS.Business.Account;
using FTS.Data.Account;
using Data.Generic;
using FTS.Data.RoleMaster;
using FTS.Business.RoleMaster;

using FTS.Data.BranchMaster;
using FTS.Business.BranchMaster;

using FTS.Data.RegionMaster;
using FTS.Business.RegionMaster;
using FTS.Business.ZoneMaster;
using FTS.Data.ZoneMaster;

//using FTS.Business.ApplicantMaster;
//using FTS.Data.ApplicantMaster;
using FTS.Data.CommonList;
using FTS.Business.CommonList;
using FTS.Data.ApplicantLoginMaster;
using FTS.Data.DesignationMaster;
using FTS.Business.DesignationMaster;
using FTS.Data.EmployeeMaster;
using FTS.Business.EmployeeMaster;
using FTS.Data.PositionMaster;
using FTS.Business.PositionMaster;
using FTS.Data.ReinstatementAppliaction;
using FTS.Business.ReinstatementAppliaction;
using FTS.Business.DistrictMaster;
using FTS.Data.DistrictMaster;
using FTS.Data.TalukaMater;
using FTS.Business.TalukaMaster;

using FTS.Data.TradeUnionRegistrationMaster;
using FTS.Business.TradeUnionRegistrationMaster;
using FTS.Data.EmpPositionMaster;
using FTS.Business.EmpPositionMaster;
using FTS.Business.ApplicantLoginMaster;
using FTS.Data.ApplicantForgotPassword;
using FTS.Business.ApplicantForgotPassword;

using FTS.Data.EstablishmentMaster;
using FTS.Business.EstablishmentMaster;

using FTS.Data.UserRegistration;
using FTS.Business.UserRegistration;

using FTS.Data.ConciliationApplication;
using FTS.Business.ConciliationApplication;


using FTS.Business.EmployeeLoginMaster;
using FTS.Data.EmployeeLoginMaster;
using FTS.Data.Home;
using FTS.Business.Home;

using FTS.Business.ApplicantProfile;
using FTS.Data.ApplicantProfile;

using FTS.Data.NFormApplication;
using FTS.Business.INFormApplication;

using FTS.Business.Profile;
using FTS.Data.Profile;
using FTS.Business.MenuMaster;
using FTS.Data.MenuMaster;
using FTS.Data.TFormApplication;
using FTS.Business.TFormApplication;



using FTS.Data.CessProjectDetails;
using FTS.Business.CessProjectDetails;
using FTS.Business.TypeOfBody;
using FTS.Data.TypeOfBody;
using FTS.Data.RegistrationCLRA;
using FTS.Business.RegistrationCLRA;

using FTS.Data.LicenceApplication;
using FTS.Business.LicenceApplication;
using FTS.Data.ApplicationRegistration;
using FTS.Data.Registration;
using FTS.Business.ApplicationRegistration;
using FTS.Data.AreaMaster;
using FTS.Business.AreaMaster;
using FTS.Data.ReinstatementActionMaster;
using FTS.Business.NFormActionMaster;
using FTS.Data.NFormActionMaster;
using FTS.Data.ConcilliationActionMaster;
using FTS.Business.ConcilliationActionMaster;
using FTS.Business.TFormActionMaster;
using FTS.Data.TFormActionMaster;
using FTS.Business.ReinstatementActionMaster;
using FTS.Data.DashBoard;
using FTS.Business.DashBoard;
using FTS.Data.ApplealDayMaster;
using FTS.Business.AppealDayMaster;
using FTS.Data.ApplicationRegistrationAction;
using FTS.Business.ApplicationRegistrationActionMaster;
using FTS.Business.LicenceActionMaster;
using FTS.Data.LicenceActionMaster;
using FTS.Model.Entities;
using FluentValidation;
using FTS.Business.Inspection;
using FTS.Data.Inspection;
//using FTS.Business.Demo;
//using FTS.Data.Demo;

namespace FTS.Business.Extensions
{
    public static class IServiceCollectionExtension
    {
        /// <summary>Configures the dependency injection for business layer,data layer etc.</summary>
        /// <param name="services">The services.</param>
        /// <returns>Services</returns>
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {

            #region Email
            services.AddTransient<IEmailSender, EmailSender>();
            #endregion

            #region Configuration
            services.AddTransient<IAppConfiguration, AppConfiguration>();
            #endregion


            #region Data Layer
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IApplicantLoginMasterRepository, ApplicantLoginMasterRepository>();
            services.AddTransient<IRoleMasterRepository, RoleMasterRepository>();
            services.AddTransient<IBranchMasterRepository, BranchMasterRepository>();    
            services.AddTransient<IRegionMasterRepository, RegionMasterRepository>();                       
            services.AddTransient<IZoneMasterRepository, ZoneMasterRepository>();
           // services.AddTransient<IApplicantMasterRepository, ApplicantMasterRepository>();
            services.AddTransient<ICommonListRepository, CommonListRepository>();
            services.AddTransient<IDesignationMasterRepository, DesignationMasterRepository>();
            services.AddTransient<IEmployeeMasterRepository, EmployeeMasterRepository>();
            services.AddTransient<IPositionMasterRepository, PositionMasterRepository>();
            services.AddTransient<IReinstatementAppliactionRepository, ReinstatementAppliactionRepository>();
            services.AddTransient<IDistrictMasterRepository, DistrictMasterRepository>();
            services.AddTransient<ITalukaMasterRepository, TalukaMasterRepository>();

            services.AddTransient<ITradeUnionRegistrationMasterRepository, TradeUnionRegistrationMasterRepository>();
            services.AddTransient<IEmpPositionMasterRepository, EmpPositionMasterRepository>();


            services.AddTransient<IEstablishmentMasterRepository, EstablishmentMasterRepository>();
            services.AddTransient<IUserRegistrationRepository, UserRegistrationRepository>();
            services.AddTransient<IConciliationApplicationRepository, ConciliationApplicationRepository>();


            services.AddTransient<IApplicantForgotPasswordRepository, ApplicantForgotPasswordRepository>();

            services.AddTransient<IEmployeeLoginMasterRepository, EmployeeLoginMasterRepository>();
            services.AddTransient<IHomeRepository, HomeRepository>();

            services.AddTransient<IApplicantProfileRepository, ApplicantProfileRepository>();

            services.AddTransient<INFormApplicationRepository, NFormApplicationRepository>();

            services.AddTransient<IProfileRepository, ProfileRepository>();
            services.AddTransient<IMenuMasterRepository, MenuMasterRepository>();

            services.AddTransient<ITFormApplicationRepository, TFormApplicationRepository>();
            services.AddTransient<ILicenceApplicationRepository, LicenceApplicationRepository>();


            services.AddTransient<ICessProjectDetailsRepository, CessProjectDetailsRepository>();
            services.AddTransient<ITypeOfBodyRepository, TypeOfBodyRepository>();
            services.AddTransient<IRegistrationCLRARepository, RegistrationCLRARepository>();
            services.AddTransient<IApplicationRegistrationRepository, ApplicationRegistrationRepository>();
            services.AddTransient<IAreaMasterRepository, AreaMasterRepository>();
            services.AddTransient<IReinstatementActionMasterRepository, ReinstatementActionMasterRepository>();
            services.AddTransient<INFormActionMasterRepository, NFormActionMasterRepository>();
            services.AddTransient<IConcilliationActionMasterRepository, ConcilliationActionMasterRepository>();
            services.AddTransient<ITFormActionMasterRepository, TFormActionMasterRepository>();
            services.AddTransient<IDashBoardRepository, DashBoardRepository>();
            services.AddTransient<IAppealDayMasterRepository, AppealDayMasterRepository>();
            services.AddTransient<IApplicationRegistrationActionMasterRepository, ApplicationRegistrationActionMasterRepository>();
            services.AddTransient<ILicenceActionMasterRepository, LicenceActionMasterRepository>();
            services.AddTransient<IInspectionRepository, InspectionRepository>();
            //services.AddTransient<IDemoRepository, DemoRepository>();
            #endregion

            #region Business Layer           
            services.AddTransient<IAccountBl, AccountBl>();                               
            services.AddTransient<IRoleMasterBl, RoleMasterBl>();
            services.AddTransient<IBranchMasterBl, BranchMasterBl>();
            services.AddTransient<IRegionMasterBl, RegionMasterBl>();                               
            services.AddTransient<IZoneMasterBl, ZoneMasterBl>();
            services.AddTransient<IDistrictMasterBl, DistrictMasterBl>();
            services.AddTransient<ITalukaMasterBl, TalukaMasterBl>();
            //services.AddTransient<IApplicantMasterBl, ApplicantMasterBl>();
            services.AddTransient<ICommonListBI, CommonListBI>();
            services.AddTransient<IDesignationMasterBI, DesignationMasterBI>();
            services.AddTransient<IEmployeeMasterBI, EmployeeMasterBI>();
            services.AddTransient<IPositionMasterBI, PositionMasterBI>();
            services.AddTransient<IReinstatementAppliactionBI, ReinstatementAppliactionBI>();

            services.AddTransient<ITradeUnionRegistrationMasterBl, TradeUnionRegistrationMasterBl>();
            services.AddTransient<IEmpPositionMasterBl, EmpPositionMasterBl>();

            services.AddTransient<IUserRegistrationBl, UserRegistrationBl>();
            services.AddTransient<IEstablishmentMasterBI, EstablishmentMasterBI>();
            services.AddTransient<IConciliationApplicationBl, ConciliationApplicationBl>();

            services.AddTransient<IEmployeeLoginMasterBI, EmployeeLoginMasterBI>();
            services.AddTransient<IHomeBI, HomeBI>();
            services.AddTransient<IProfileBI, ProfileBI>();
            services.AddTransient<IMenuMasterBI, MenuMasterBI>();
            services.AddTransient<IDashBoardBI, DashBoardBI>();
         

            services.AddTransient<IApplicantLoginMasterBl, ApplicantLoginMasterBl>();
            services.AddTransient<IApplicantForgotPasswordBl, ApplicantForgotPasswordBl>();
            services.AddTransient<IApplicantProfileBl, ApplicantProfileBl>();

            services.AddTransient<INFormApplicationBI, NFormApplicationBI>();
            //services.AddTransient<ITFormApplicationBl, TFormApplicationBl>();
            services.AddTransient<ITFormApplicationBl, TFormApplicationBl>();
            services.AddTransient<ILicenceApplicationBl, LicenceApplicationBl>();

            services.AddTransient<IProjectDetailsBl, ProjectDetailsBl>();
            services.AddTransient<ITypeOfBodyBl, TypeOfBodyBl>();
            services.AddTransient<IRegistrationCLRABl, RegistrationCLRABl>();
            services.AddTransient<IApplicationRegistrationBI, ApplicationRegistrationBI>();
            services.AddTransient<IAreaMasterBl, AreaMasterBl>();
            services.AddTransient<IReinstatementActionMasterBl, ReinstatementActionMasterBl>();
            services.AddTransient<INFormActionMasterBl, NFormActionMasterBl>();
            services.AddTransient<IConcilliationActionMasterBl, ConcilliationActionMasterBl>();
            services.AddTransient<ITFormActionMasterBl, TFormActionMasterBl>();
            services.AddTransient<IDashBoardBI, DashBoardBI>();
            services.AddTransient<IAppealDayMasterBl, AppealDayMasterBl>();
            services.AddTransient<IApplicationregistratationActionMasterBl, ApplicationregistratationActionMasterBl>();
            services.AddTransient<ILicenceActionMasterBl, LicenceActionMasterBl>();
            services.AddTransient<IInspectionBI, InspectionBI>();
            //services.AddTransient<IDemoBI, DemoBI>();
            #endregion


            #region File Upload Service
            services.AddTransient<IFileUploadService, FileUploadService>();

            #endregion

            return services;
        }
    }
}
