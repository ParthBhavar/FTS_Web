using FTS.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.CommonList
{
    public interface ICommonListBI
    {
        List<MCommonList> ZoneList(int mode);
        List<MCommonList> PositionList(int mode);

        List<MCommonList> GanderList();
        List<MCommonList> RegionList(int mode);
        List<MCommonList> RoleList(int mode);
        List<MCommonList> BranchList(int mode , int DistrictID);
        List<MCommonList> DesignationList(int mode);
        List<MCommonList> TalukaList(int mode, int DistrictID);
        List<MCommonList> DistrictList(int mode);
        List<MTradeUnion> TradunionList(int mode);
        List<MCommonList> WorkTypeList(int mode);
        List<MCommonList> DepartmentList(int mode);
        List<MEstablishment> EstablishmentList(int mode);
        List<MArea> AreaList(int mode, int DistrictID);
        List<MCommonList> EmployeeList(int mode);
        List<MCommonList> MenuList(int mode);
        List<MCommonList> HearingReasonList(int mode,int ISACL);
        List<MCommonList> ReferActionList(int mode);
        List<MCommonList> HOReferActionList(int mode);
        List<MCommonList> TypeOfBodyList(int mode);
        List<MCommonList> CessPaymentTypeList(int mode);
        List<MCommonList> ProjectList(int mode);
        int LogErrorintbl(Exception ex, string ClassName, String FunctionName, int UserMode, int UserID, string ipaddress);
        List<MCommonList> TypeOfBusinessTradeList();
        List<MEmployeeData> EmployeeData(int mode, int EmployeeID);
        int Officeloginlogout(int EmployeeID, string Mode, string ipaddress);
        int Applicantloginlogout(int ApplicantID, string Mode, string ipaddress);
        int Emaillog(string Subject, String Body, int UserMode, int EmailID, string ipaddress);

    }
}
