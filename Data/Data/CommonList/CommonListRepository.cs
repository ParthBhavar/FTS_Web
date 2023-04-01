using FTS.Data.Common;
using FTS.Model.Common;
using Dapper;
using Data.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using context = System.Web;
using System.Data;
using System.Threading.Tasks;

namespace FTS.Data.CommonList
{
    public class CommonListRepository : ICommonListRepository
    {
        #region Private Variables
        private readonly IRepository<MCommonList> _CommonRepository;
        #endregion

        #region Constructor
        public CommonListRepository(IRepository<MCommonList> CommonRepository)
        {
            _CommonRepository = CommonRepository;

        }
        #endregion


        public List<MCommonList> ZoneList(int mode)
        {
            if (mode == 0)
            {
                //Only New Record In USe This Method
                List<MCommonList> lstRoleMaster = new List<MCommonList>();
                DynamicParameters param = new DynamicParameters();
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetActiveZoneList, param);
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstRoleMaster = result1.Select(x => new MCommonList
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                    }).ToList();
                };
                return lstRoleMaster;
            }
            else
            {
                //Only Edit Record In USe This Method
                List<MCommonList> lstRoleMaster = new List<MCommonList>();
                DynamicParameters param = new DynamicParameters();
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetAllZoneList, param);
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstRoleMaster = result1.Select(x => new MCommonList
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                    }).ToList();
                };
                return lstRoleMaster;
            }
        }

        public List<MCommonList> PositionList(int mode)
        {
            if (mode == 0)
            {
                //Only New Record In USe This Method
                List<MCommonList> lstCommonList = new List<MCommonList>();
                DynamicParameters param = new DynamicParameters();
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetActivePositionList, param);
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MCommonList
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                    }).ToList();
                };
                return lstCommonList;
            }
            else
            {
                //Only Edit Record In USe This Method
                List<MCommonList> lstCommonList = new List<MCommonList>();
                DynamicParameters param = new DynamicParameters();
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetAllPositionList, param);
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MCommonList
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                    }).ToList();
                };
                return lstCommonList;
            }
        }

        public List<MCommonList> RegionList(int mode)
        {
            if (mode == 0)
            {
                //Only New Record In USe This Method
                List<MCommonList> lstCommonList = new List<MCommonList>();
                DynamicParameters param = new DynamicParameters();
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetActiveRegionList, param);
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MCommonList
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                    }).ToList();
                };
                return lstCommonList;
            }
            else
            {
                //Only Edit Record In USe This Method
                List<MCommonList> lstCommonList = new List<MCommonList>();
                DynamicParameters param = new DynamicParameters();
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetAllRegionList, param);
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MCommonList
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                    }).ToList();
                };
                return lstCommonList;
            }
        }

        public List<MCommonList> RoleList(int mode)
        {
            if (mode == 0)
            {
                //Only New Record In USe This Method
                List<MCommonList> lstCommonList = new List<MCommonList>();
                DynamicParameters param = new DynamicParameters();
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetActiveRoleList, param);
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MCommonList
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                    }).ToList();
                };
                return lstCommonList;
            }
            else
            {
                //Only Edit Record In USe This Method
                List<MCommonList> lstCommonList = new List<MCommonList>();
                DynamicParameters param = new DynamicParameters();
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetAllRoleList, param);
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MCommonList
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                    }).ToList();
                };
                return lstCommonList;
            }
        }

        public List<MCommonList> BranchList(int mode , int DistrictID)
        {
            if (mode == 0)
            {
                //Only New Record In USe This Method
                List<MCommonList> lstCommonList = new List<MCommonList>();
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_DistrictID", DistrictID);
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetActiveBranchList, param);
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MCommonList
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                    }).ToList();
                };
                return lstCommonList;
            }
            else
            {
                //Only Edit Record In USe This Method
                List<MCommonList> lstCommonList = new List<MCommonList>();
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_DistrictID", DistrictID);
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetAllBranchList, param);
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MCommonList
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                    }).ToList();
                };
                return lstCommonList;
            }
        }

        public List<MCommonList> DesignationList(int mode)
        {
            if (mode == 0)
            {
                //Only New Record In USe This Method
                List<MCommonList> lstCommonList = new List<MCommonList>();
                DynamicParameters param = new DynamicParameters();
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetActiveDesignationList, param);
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MCommonList
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                    }).ToList();
                };
                return lstCommonList;
            }
            else
            {
                //Only Edit Record In USe This Method
                List<MCommonList> lstCommonList = new List<MCommonList>();
                DynamicParameters param = new DynamicParameters();
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetAllDesignationList, param);
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MCommonList
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                    }).ToList();
                };
                return lstCommonList;
            }
        }

        public List<MCommonList> GanderList()
        {
            //Only Edit Record In USe This Method
            List<MCommonList> lstCommonList = new List<MCommonList>();
            DynamicParameters param = new DynamicParameters();
            var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetGanderList, param);
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstCommonList = result1.Select(x => new MCommonList
                {
                    DataValue = (int)x.DataValue,
                    DisplayValue = (string)x.DisplayValue,
                }).ToList();
            };
            return lstCommonList;

        }

        public List<MCommonList> TalukaList(int mode, int DistrictID)
        {
            if (mode == 0)
            {
                //Only New Record In USe This Method
                List<MCommonList> lstCommonList = new List<MCommonList>();
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_DistrictID", DistrictID);
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetActiveTalukaList, param);
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MCommonList
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                    }).ToList();
                };
                return lstCommonList;
            }
            else
            {
                //Only Edit Record In USe This Method
                List<MCommonList> lstCommonList = new List<MCommonList>();
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_DistrictID", DistrictID);
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetAllTalukaList, param);
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MCommonList
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                    }).ToList();
                };
                return lstCommonList;
            }
        }

        public List<MCommonList> DistrictList(int mode)
        {
            if (mode == 0)
            {
                //Only New Record In USe This Method
                List<MCommonList> lstCommonList = new List<MCommonList>();
                DynamicParameters param = new DynamicParameters();
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetActiveDistrictList, param);
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MCommonList
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                    }).ToList();
                };
                return lstCommonList;
            }
            else
            {
                //Only Edit Record In USe This Method
                List<MCommonList> lstCommonList = new List<MCommonList>();
                DynamicParameters param = new DynamicParameters();
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetAllDistrictList, param);
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MCommonList
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                    }).ToList();
                };
                return lstCommonList;
            }
        }

        public List<MTradeUnion> TradunionList(int mode)
        {
            if (mode == 0)
            {
                //Only New Record In USe This Method
                List<MTradeUnion> lstCommonList = new List<MTradeUnion>();
                DynamicParameters param = new DynamicParameters();
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetActiveTradunionList, param);
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MTradeUnion
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                        RegistrationNo = (string)x.RegistrationNo,
                        PAddress = (string)x.PAddress,
                        SAddress = (string)x.SAddress,
                        TalukaID = (int)x.TalukaID,
                        DistrictID = (int)x.DistrictID,
                        Pincode = (int)x.Pincode,
                    }).ToList();
                };

                return lstCommonList;
            }
            else
            {
                //Only Edit Record In USe This Method
                List<MTradeUnion> lstCommonList = new List<MTradeUnion>();
                DynamicParameters param = new DynamicParameters();
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetAllTradunionList, param);
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MTradeUnion
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                        RegistrationNo = (string)x.RegistrationNo,
                        PAddress = (string)x.PAddress,
                        SAddress = (string)x.SAddress,
                        TalukaID = (int)x.TalukaID,
                        DistrictID = (int)x.DistrictID,
                        Pincode = (int)x.Pincode,
                    }).ToList();
                };
                return lstCommonList;
            }
        }

        public List<MCommonList> DepartmentList(int mode)
        {
            if (mode == 0)
            {
                //Only New Record In USe This Method
                List<MCommonList> lstCommonList = new List<MCommonList>();
                DynamicParameters param = new DynamicParameters();
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetActiveDepartmentList, param);
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MCommonList
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                    }).ToList();
                };
                return lstCommonList;
            }
            else
            {
                //Only Edit Record In USe This Method
                List<MCommonList> lstCommonList = new List<MCommonList>();
                DynamicParameters param = new DynamicParameters();
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetAllDepartmentList, param);
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MCommonList
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                    }).ToList();
                };
                return lstCommonList;
            }
        }

        public List<MCommonList> WorkTypeList(int mode)
        {
            if (mode == 0)
            {
                //Only New Record In USe This Method
                List<MCommonList> lstCommonList = new List<MCommonList>();
                DynamicParameters param = new DynamicParameters();
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetActiveWorkTypeList, param);
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MCommonList
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                    }).ToList();
                };
                return lstCommonList;
            }
            else
            {
                //Only Edit Record In USe This Method
                List<MCommonList> lstCommonList = new List<MCommonList>();
                DynamicParameters param = new DynamicParameters();
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetAllDWorkTypeList, param);
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MCommonList
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                    }).ToList();
                };
                return lstCommonList;
            }
        }

        public List<MEstablishment> EstablishmentList(int mode)
        {
            if (mode == 0)
            {
                //Only New Record In USe This Method
                List<MEstablishment> lstCommonList = new List<MEstablishment>();
                DynamicParameters param = new DynamicParameters();
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetActiveEstablishmentList, param);
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MEstablishment
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                        EstablishmentCode = (string)x.EstablishmentCode,
                        PAddress = (string)x.PAddress,
                        SAddress = (string)x.SAddress,
                        TalukaID = (int)x.TalukaID,
                        DistrictID = (int)x.DistrictID,
                        Pincode = (int)x.Pincode,
                    }).ToList();
                };
                return lstCommonList;
            }
            else
            {
                //Only Edit Record In USe This Method
                List<MEstablishment> lstCommonList = new List<MEstablishment>();
                DynamicParameters param = new DynamicParameters();
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetAllEstablishmentList, param);
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MEstablishment
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                        EstablishmentCode = (string)x.EstablishmentCode,
                        PAddress = (string)x.PAddress,
                        SAddress = (string)x.SAddress,
                        TalukaID = (int)x.TalukaID,
                        DistrictID = (int)x.DistrictID,
                        Pincode = (int)x.Pincode,
                    }).ToList();
                };
                return lstCommonList;
            }
        }

        public List<MArea> AreaList(int mode, int DistrictID)
        {
            if (mode == 0)
            {
                //Only New Record In USe This Method
                List<MArea> lstCommonList = new List<MArea>();
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_DistrictID", DistrictID);
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetActiveAreamasterList, param);
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MArea
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                        ZoneID = (int)x.ZoneID,
                    }).ToList();
                };
                return lstCommonList;
            }
            else
            {
                //Only Edit Record In USe This Method
                List<MArea> lstCommonList = new List<MArea>();
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_DistrictID", DistrictID);
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetAllDAreamasterList, param);
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MArea
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                        ZoneID = (int)x.ZoneID,
                    }).ToList();
                };
                return lstCommonList;
            }
        }

        public List<MCommonList> EmployeeList(int mode)
        {
            if (mode == 0)
            {
                //Only New Record In USe This Method
                List<MCommonList> lstCommonList = new List<MCommonList>();
                DynamicParameters param = new DynamicParameters();
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetActiveEmployeeList, param);
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MCommonList
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                    }).ToList();
                };
                return lstCommonList;
            }
            else
            {
                //Only Edit Record In USe This Method
                List<MCommonList> lstCommonList = new List<MCommonList>();
                DynamicParameters param = new DynamicParameters();
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetAllEmployeeList, param);
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MCommonList
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue
                    }).ToList();
                };
                return lstCommonList;
            }

        }



        public List<MCommonList> MenuList(int mode)
        {

            if (mode == 0)
            {
                //Only New Record In USe This Method
                List<MCommonList> lstCommonList = new List<MCommonList>();
                DynamicParameters param = new DynamicParameters();
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetActiveMenuList, param);

                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MCommonList
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                        ParentId = (int)x.ParentId,

                    }).ToList();
                };
                return lstCommonList;
            }
            else
            {
                //Only Edit Record In USe This Method
                List<MCommonList> lstCommonList = new List<MCommonList>();
                DynamicParameters param = new DynamicParameters();
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetAllMenuList, param);

                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MCommonList
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                        ParentId = (int)x.ParentId,
                    }).ToList();
                };
                return lstCommonList;
            }

        }

        public List<MCommonList> HearingReasonList(int mode, int ISACL)
        {

            if (mode == 0)
            {
                //Only New Record In USe This Method
                List<MCommonList> lstCommonList = new List<MCommonList>();
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_IsACL", ISACL);
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetActiveHearingReasonList, param);

                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MCommonList
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                    }).ToList();
                };
                return lstCommonList;
            }
            else
            {
                //Only Edit Record In USe This Method
                List<MCommonList> lstCommonList = new List<MCommonList>();
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_IsACL", ISACL);
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetAllHearingReasonList, param);

                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MCommonList
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                    }).ToList();
                };
                return lstCommonList;
            }
        }

        public List<MCommonList> ReferActionList(int mode)
        {

            if (mode == 0)
            {
                //Only New Record In USe This Method
                List<MCommonList> lstCommonList = new List<MCommonList>();
                DynamicParameters param = new DynamicParameters();

                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetActionReferList, param);

                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MCommonList
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                    }).ToList();
                };
                return lstCommonList;
            }
            else
            {
                //Only Edit Record In USe This Method
                List<MCommonList> lstCommonList = new List<MCommonList>();
                DynamicParameters param = new DynamicParameters();

                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetActionReferList, param);

                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MCommonList
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                    }).ToList();
                };
                return lstCommonList;
            }
        }

        public List<MCommonList> HOReferActionList(int mode)
        {

            if (mode == 0)
            {
                //Only New Record In USe This Method
                List<MCommonList> lstCommonList = new List<MCommonList>();
                DynamicParameters param = new DynamicParameters();

                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetHOActionReferList, param);

                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MCommonList
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                    }).ToList();
                };
                return lstCommonList;
            }
            else
            {
                //Only Edit Record In USe This Method
                List<MCommonList> lstCommonList = new List<MCommonList>();
                DynamicParameters param = new DynamicParameters();

                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetHOActionReferList, param);

                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MCommonList
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                    }).ToList();
                };
                return lstCommonList;
            }
        }


        public List<MCommonList> TypeOfBodyList(int mode)
        {
            if (mode == 0)
            {
                //Only New Record In USe This Method
                List<MCommonList> lstCommonList = new List<MCommonList>();
                DynamicParameters param = new DynamicParameters();
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetActiveTypeOfBodyList, param);
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MCommonList
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                    }).ToList();
                };
                return lstCommonList;
            }
            else
            {
                //Only Edit Record In USe This Method
                List<MCommonList> lstCommonList = new List<MCommonList>();
                DynamicParameters param = new DynamicParameters();
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetAllTypeOfBodyList, param);
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MCommonList
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                    }).ToList();
                };
                return lstCommonList;
            }
        }


        public List<MCommonList> CessPaymentTypeList(int mode)
        {
            if (mode == 0)
            {
                //Only New Record In USe This Method
                List<MCommonList> lstCommonList = new List<MCommonList>();
                DynamicParameters param = new DynamicParameters();
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetActiveCessPaymentTypeList, param);
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MCommonList
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                    }).ToList();
                };
                return lstCommonList;
            }
            else
            {
                //Only Edit Record In USe This Method
                List<MCommonList> lstCommonList = new List<MCommonList>();
                DynamicParameters param = new DynamicParameters();
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetAllCessPaymentTypeList, param);
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MCommonList
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                    }).ToList();
                };
                return lstCommonList;
            }
        }


        public int LogErrorintbl(Exception ex, string ClassName, String FunctionName, int UserMode, int UserID,string ipaddress)
        {
            try
            {

                //Only New Record In USe This Method

                DynamicParameters param = new DynamicParameters();
                param.Add("@p_ErrorMassage",  ex.Message.ToString());
                param.Add("@p_ClassName", ClassName);
                param.Add("@p_FunctionName", FunctionName);
                param.Add("@p_UserMode", UserMode);
                param.Add("@p_UserID", UserID);
                param.Add("@p_IP_Address", ipaddress);
                var key =   _CommonRepository.ExecuteProcedure(SPConstants.InsertTryCatchErrorLogHistory, param);
                return key;
            }
            catch
            {
                throw;
            }
        }


        public int Emaillog(string Subject, String Body, int UserMode, int EmailID, string ipaddress)
        {
            try
            {

                //Only New Record In USe This Method

                DynamicParameters param = new DynamicParameters();
                param.Add("@p_Subject", Subject);
                param.Add("@p_Body", Body);
                param.Add("@p_UserMode", UserMode);
                param.Add("@p_EmailID", EmailID);
                param.Add("@p_IP_Address", ipaddress);
                var key = _CommonRepository.ExecuteProcedure(SPConstants.InsertEmailLogHistory, param);
                return key;
            }
            catch
            {
                throw;
            }
        }

        public int Officeloginlogout(int EmployeeID, string Mode , string ipaddress)
        {
            try
            {

                //Only New Record In USe This Method

                DynamicParameters param = new DynamicParameters();
                param.Add("@p_EmployeeID", EmployeeID);
                param.Add("@p_Mode", Mode);
                param.Add("@p_IP_Address", ipaddress);
                var key = _CommonRepository.ExecuteProcedure(SPConstants.Insertofficeloginlogout, param);
                return key;
            }
            catch
            {
                throw;
            }
        }


        public int Applicantloginlogout(int ApplicantID, string Mode , string ipaddress)
        {
            try
            {
                //Only New Record In USe This Method
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_ApplicantID", ApplicantID);
                param.Add("@p_Mode", Mode);
                param.Add("@p_IP_Address", ipaddress);
                var key = _CommonRepository.ExecuteProcedure(SPConstants.InsertApplicantloginlogout, param);
                return key;
            }
            catch
            {
                throw;
            }
        }



        public List<MCommonList> ProjectList(int mode)
        {
            if (mode == 0)
            {
                //Only New Record In USe This Method
                List<MCommonList> lstCommonList = new List<MCommonList>();
                DynamicParameters param = new DynamicParameters();
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetActiveProjectList, param);
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MCommonList
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                    }).ToList();
                };
                return lstCommonList;
            }
            else
            {
                //Only Edit Record In USe This Method
                List<MCommonList> lstCommonList = new List<MCommonList>();
                DynamicParameters param = new DynamicParameters();
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetAllProjectList, param);
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MCommonList
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                    }).ToList();
                };
                return lstCommonList;
            }
        }

        public List<MCommonList> TypeOfBusinessTradeList()
        {
                //Only Edit Record In USe This Method
                List<MCommonList> lstCommonList = new List<MCommonList>();
                DynamicParameters param = new DynamicParameters();
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetTypeOfBusinessTradeList, param);
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MCommonList
                    {
                        DataValue = (int)x.DataValue,
                        DisplayValue = (string)x.DisplayValue,
                    }).ToList();
                };
                return lstCommonList;
            
        }


        public List<MEmployeeData> EmployeeData(int mode,int EmployeeID)
        {
            if (mode == 0)
            {
                //Only New Record In USe This Method
                List<MEmployeeData> lstCommonList = new List<MEmployeeData>();
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_EmployeeID", EmployeeID);
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetActiveEmployeeData, param);
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MEmployeeData
                    {
                        Email = (string)x.Email,
                        MobileNo = (string)x.MobileNo,
                    }).ToList();
                };
                return lstCommonList;
            }
            else
            {
                //Only Edit Record In USe This Method
                List<MEmployeeData> lstCommonList = new List<MEmployeeData>();
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_EmployeeID", EmployeeID);
                var keyValuePairs = _CommonRepository.QueryMultipleByProcedure(SPConstants.GetAllEmployeeDataList, param);
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstCommonList = result1.Select(x => new MEmployeeData
                    {
                        Email = (string)x.Email,
                        MobileNo = (string)x.MobileNo,
                    }).ToList();
                };
                return lstCommonList;
            }

        }
    }
}