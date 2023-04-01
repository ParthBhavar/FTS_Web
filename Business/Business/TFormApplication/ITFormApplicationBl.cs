using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.TFormApplication
{
    public interface ITFormApplicationBl
    {
        List<TFormAppealApplicationModel> TFormApplicationList(TFormAppealApplicationModel model);
        TFormAppealApplicationModel TFormForEmployeeRecord(int AppealID);
        TFormAppealApplicationModel TFormForEmployerRecord(int AppealID);
        TFormAppealApplicationModel SaveTFormForEmployee(TFormAppealApplicationModel Obj);
        List<TFormAppealApplicationModel> TFormApplicationDCLList(TFormAppealApplicationModel model);
        TFormAppealApplicationModel TFormDCLStatusUpdate(int AppealID, int ISNew);
        TFormAppealApplicationModel SaveTFormHearingDCLRecord(TFormAppealApplicationModel Obj);
        TFormAppealApplicationModel SaveTFormResolutionBackDCLRecord(TFormAppealApplicationModel Obj);
        TFormAppealApplicationModel SaveTFormConfirmDCLRecord(TFormAppealApplicationModel Obj);
        TFormAppealApplicationModel SaveTFormDismissDCLRecord(TFormAppealApplicationModel Obj);
        TFormAppealApplicationModel TFormDCLHistory(TFormAppealApplicationModel Obj);
        TFormAppealApplicationModel SaveTFormForEmployer(TFormAppealApplicationModel Obj);
        TFormAppealApplicationModel TFormACLRecord(int AppliactionID, int ISNew);
    }
}
