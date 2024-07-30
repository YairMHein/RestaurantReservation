using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantReservation.Service
{
    public static class Settings
    {
        public static int SuccessCode = 1;
        public static int FailCode = 2;
        public static int WarningCode = 4;
        public static int LogoutCode = 3;
        public static string AddUserSuccess = "User Added Successfully!";
        public static string ActivateSuccess = "Activate Successful!";
        public static string DeActivateSuccess = "DeActivate Successful!";
        public static string AlreadyExist = "Already Exists!";
        public static string SaveSuccess = "Save Successful!";
        public static string UpdateSuccess = "Update Successful!";
        public static string DeleteSuccess = "Delete Successful!";
        public static string Password_changed = "Your password has changed! Please Login again.";
        public static string Profile_Updated = "Your profile has updated.";
        public static string FillMaterial = "Please fill material Charges!";
        public static string SendSuccess = "Your data has sent successfully!";
        public static string ReSendSuccess = "Your data has re-sent successfully!";
        public static string StatusUpdated_Acknowledged = "Marked as Acknowledged!";
        public static string StatusUpdated_Reject = "This proposal has been rejected!";
        public static string StatusUpdated_Select = "This proposal has been selected!";
        public static string AlreadyPending_Draught = "A pending request is currently reviewing for this Route and township!";
        public static string AlreadyPending_NonDraught = "A pending request is currently reviewing for this township and plant!";
        public static string AlreadyPending_Warehouse = "A pending request is currently reviewing for this Plants!";

        public static string SessionTimeout = "Session Time Out!";
        public static string ActivateFail = "Activate Failed!";
        public static string AddUserFail = "Add user Failed!";
        public static string SaveFail = "Save Failed!";
        public static string UpdateFail = "Update Failed!";
        public static string DeleteFail = "Delete Failed!";
        public static string ErrorOccur = "Error Occurred!";
        public static string UserExist = "User Name already exists!";

        public static string Status_Pending = "Pending";
        public static string Status_Acknowledged = "Acknowledged";
        public static string Status_Rejected = "Rejected";
        public static string Status_Selected = "Selected";
        public static string NoRecord_toAcknowledge = "There is no new record(s) to acknowledge!";

        public static string SystemEmail = "mbltransportcharges.service@gmail.com";
        public static string SystemAppPassword = "utxtdnblqumplcyg";

        public static string EncryptKey = "MBLTCSYS2023";

        public static string Module_Draught = "Draught";
        public static string Module_NonDraught = "NonDraught";
        public static string Module_Warehouse = "Warehouse";
    }
}