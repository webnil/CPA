using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckIn
{
    internal class Constants
    {
        internal const string CPA_IMAGE_QUERY = "select CPAImage from CPA where CPAID = {0}";
        internal const string Customer_IMAGE_QUERY = "select UserImage from Customer where CustomerID = {0}";
        internal const string SPECIALITY_QUERY = "SELECT ID, Speciality FROM LuSpeciality";
        internal const string STATE_QUERY = "SELECT * FROM LuState";
        internal const string CITY_QUERY = "SELECT * FROM LuCity where State_Code='{0}'";
        internal const string CPA_DETAILS_QUERY = "SELECT * FROM CPA where CPAID = {0}";
        internal const string CUSTOMER_ID_QUERY = "SELECT FirstName, LastName, Phone FROM Customer where CustomerID = {0}";
        internal const string CUSTOMER_PURPOSE_QUERY = "SELECT PurposeOfVisit ,CustomerName,ContactNumber form CPAAppointment where CPAID={0}";
        internal const string SPECIALITY_BYID_QUERY = "SELECT Speciality FROM LuSpeciality where ID={0}";
        internal const string TEMP_CUSTOMER_QUERY = "SELECT * from TempUsers where ActivationToken='{0}'";
        internal const string TEMP_CUSTOMER_DELETE_QUERY = "Delete  from TempUsers where ActivationToken='{0}'";
        internal const string TEMP_CPA_DETAILS_QUERY = "SELECT * FROM TempCPA where ActivationToken='{0}'";
        internal const string TEMP_CPA_DELETE_QUERY = "Delete  from TempCPA where ActivationToken='{0}'";
        internal const string IS_VALID_EMAIL = "SELECT 1 FROM Users WHERE Email = '{0}'";
    }

    internal class StoredProcedure
    {
        internal const string GetAllCPA = "[GetAllCPA]";
        internal const string GetAvailableAppointments = "[GetAvailableAppointments]";
        internal const string SignUpNewCustomer = "[SignUpNewCustomer]";
        internal const string SignUpNewTempCustomer = "[SignUpNewTempCustomer]";
        internal const string SignUpNewTempCPA = "[SignUpNewTempCPA]";
        internal const string SignUpNewCPA = "[SignUpNewCPA]";
        internal const string BookAppointment = "[BookAppointment]";
        internal const string GetUserProfile ="[GetUserProfile]"; 
        internal const string UpdateCustomer="[UpdateCustomer]";
        internal const string GetCPAProfile = "[GetCPAProfile]";
        internal const string UpdateCPA = "[UpdateCPA]";
        internal const string CancelAppointment = "[CancelAppointment]";
        internal const string GetCPALocations = "[GetCPALocations]";


    }
}