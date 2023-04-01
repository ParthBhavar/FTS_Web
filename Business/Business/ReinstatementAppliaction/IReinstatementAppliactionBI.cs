using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.ReinstatementAppliaction
{
    public interface IReinstatementAppliactionBI
    {
          
        List<ReinstatementAppliactionModel> ReinstatementList(ReinstatementAppliactionModel model);
        ReinstatementAppliactionModel AppliactionRecord(int AppliactionID);
        ReinstatementAppliactionModel SaveAppliactionRecord(ReinstatementAppliactionModel Obj);
        ReinstatementAppliactionModel ReinstatementTradunionRecord(int AppliactionID, int ISNew);
        ReinstatementAppliactionModel SaveReinstatementTradunionRecord(ReinstatementAppliactionModel Obj);
        //ReinstatementAppliactionModel ReiniEstablishmentRecord(int AppliactionID, int ISNew);
        List<ReinstatementAppliactionModel> ReiniEstablishmentRecord(int AppliactionID, int ISNew);
        ReinstatementAppliactionModel SaveReiniEstablishmentRecord(ReinstatementAppliactionModel Obj);
        ReinstatementAppliactionModel ReinAppliactionDocumentURLRecord(int AppliactionID);
        ReinstatementAppliactionModel SaveDocumnetandapplication(ReinstatementAppliactionModel Obj);
        ReinstatementAppliactionModel ReinstatementAppliactionUpdateSubmit(ReinstatementAppliactionModel Obj);
        List<ReinstatementAppliactionModel> ReinstatementACOLList(ReinstatementAppliactionModel model);
        ReinstatementAppliactionModel ReinstatementACOLRecord(int AppliactionID,int ISNew);
        ReinstatementAppliactionModel SaveReinstatementHearingACOLRecord(ReinstatementAppliactionModel Obj);
        ReinstatementAppliactionModel SaveReinstatementACOLSettlementrecord(ReinstatementAppliactionModel Obj);
        ReinstatementAppliactionModel SaveReinstatementSendtolabourACOLRecord(ReinstatementAppliactionModel Obj);
        ReinstatementAppliactionModel ReinstatementHistory(ReinstatementAppliactionModel Obj);


    }
}
