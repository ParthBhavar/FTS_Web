using FTS.Data.CommonList;
using FTS.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.CommonList
{
    public class CommonListBI : ICommonListBI
    {
        private readonly ICommonListRepository _CommonRepository;

        public CommonListBI(ICommonListRepository CommonListRepository)
        {
            this._CommonRepository = CommonListRepository;
        }
        public List<MCommonList> ZoneList(int mode)
        {
            return _CommonRepository.ZoneList(mode);
        }

        public List<MCommonList> PositionList(int mode)
        {
            return _CommonRepository.PositionList(mode);
        }
        public List<MCommonList> RegionList(int mode)
        {
            return _CommonRepository.RegionList(mode);
        }

        public List<MCommonList> RoleList(int mode)
        {
            return _CommonRepository.RoleList(mode);
        }

        public List<MCommonList> BranchList(int mode , int DistrictID)
        {
            return _CommonRepository.BranchList(mode, DistrictID);
        }
        public List<MCommonList> DesignationList(int mode)
        {
            return _CommonRepository.DesignationList(mode);
        }

        public List<MCommonList> GanderList()
        {
            return _CommonRepository.GanderList();
        }

        public List<MCommonList> TalukaList(int mode, int DistrictID)
        {
            return _CommonRepository.TalukaList(mode, DistrictID);
        }

        public List<MCommonList> DistrictList(int mode)
        {
            return _CommonRepository.DistrictList(mode);
        }


        public List<MTradeUnion> TradunionList(int mode)
        {
            return _CommonRepository.TradunionList(mode);
        }

        public List<MCommonList> WorkTypeList(int mode)
        {
            return _CommonRepository.WorkTypeList(mode);
        }

        public List<MCommonList> DepartmentList(int mode)
        {
            return _CommonRepository.DepartmentList(mode);
        }
        public List<MEstablishment> EstablishmentList(int mode)
        {
            return _CommonRepository.EstablishmentList(mode);
        }

        public List<MArea> AreaList(int mode, int DistrictID)
        {
            return _CommonRepository.AreaList(mode, DistrictID);
        }
        public List<MCommonList> EmployeeList(int mode)
        {
            return _CommonRepository.EmployeeList(mode);
        }


        public List<MCommonList> HearingReasonList(int mode,int ISACL)
        {
            return _CommonRepository.HearingReasonList(mode,ISACL);
        }
        public List<MCommonList> ReferActionList(int mode)
        {
            return _CommonRepository.ReferActionList(mode);
        }
        public List<MCommonList> HOReferActionList(int mode)
        {
            return _CommonRepository.HOReferActionList(mode);
        }

        public List<MCommonList> MenuList(int mode)
        {
            return _CommonRepository.MenuList(mode);

        }

        public List<MCommonList> TypeOfBodyList(int mode)
        {
            return _CommonRepository.TypeOfBodyList(mode);
        }

        public List<MCommonList> CessPaymentTypeList(int mode)
        {
            return _CommonRepository.CessPaymentTypeList(mode);

        }

        public int LogErrorintbl(Exception ex, string ClassName, String FunctionName, int UserMode, int UserID, string ipaddress)
        {
           return  _CommonRepository.LogErrorintbl(ex, ClassName, FunctionName, UserMode, UserID, ipaddress);
        }

        public List<MCommonList> ProjectList(int mode)
        {
            return _CommonRepository.ProjectList(mode);

        }

        public List<MCommonList> TypeOfBusinessTradeList()
        {
            return _CommonRepository.TypeOfBusinessTradeList();

        }

        public List<MEmployeeData> EmployeeData(int mode, int EmployeeID)
        {
            return _CommonRepository.EmployeeData(mode, EmployeeID);

        }
        public int Officeloginlogout(int EmployeeID, string Mode,string ipaddress)
        {
            return _CommonRepository.Officeloginlogout(EmployeeID, Mode, ipaddress);
        }
        public int Applicantloginlogout(int ApplicantID, string Mode, string ipaddress)
        {
            return _CommonRepository.Applicantloginlogout(ApplicantID, Mode, ipaddress);
        }
        public int Emaillog(string Subject, String Body, int UserMode, int EmailID, string ipaddress)
        {
            return _CommonRepository.Emaillog(Subject, Body, UserMode, EmailID, ipaddress);
        }
    }
}