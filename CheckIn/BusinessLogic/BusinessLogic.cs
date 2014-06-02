using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace CheckIn
{
    public class BusinessLogic
    {
        internal static DataSet GetAllCPA(string city, string zipCode)
        {
            SqlParameter pCity = new SqlParameter();
            pCity.ParameterName = "city";
            pCity.Value = city;


            SqlParameter paramZipCode = new SqlParameter();
            paramZipCode.ParameterName = "zipCode";
            paramZipCode.Value = zipCode;

            DataSet result = DBHelper.ExecuteStoredProcedure(StoredProcedure.GetAllCPA, pCity, paramZipCode);
            //if (result == null || result.Tables[0].Rows.Count == 0)
            //    return null;
            return result;
        }
        internal static DataSet GetTempUserDetails(string activationToken)
        {
            return DBHelper.GetSelectDataSet(string.Format(Constants.TEMP_CUSTOMER_QUERY, activationToken));
        }
        internal static DataSet GetTempCPADetails(string activationToken)
        {
            return DBHelper.GetSelectDataSet(string.Format(Constants.TEMP_CPA_DETAILS_QUERY, activationToken));
        }
        internal static int DeleteTempUserDetails(string activationToken)
        {
            return DBHelper.ExecuteNonQuery(string.Format(Constants.TEMP_CUSTOMER_DELETE_QUERY, activationToken));
        }
        internal static int DeleteTempCPADetails(string activationToken)
        {
            return DBHelper.ExecuteNonQuery(string.Format(Constants.TEMP_CPA_DELETE_QUERY, activationToken));
        }
        internal static DataSet GetuserProfileDetail(int UserID)
        {
            SqlParameter paramUserID = new SqlParameter();
            paramUserID.ParameterName = "UserID";
            paramUserID.Value = UserID;

            DataSet result = DBHelper.ExecuteStoredProcedure(StoredProcedure.GetUserProfile, paramUserID);
            if (result == null || result.Tables[0].Rows.Count == 0)
                return null;
            return result;
        }
        internal static DataSet GetCPAProfileDetail(int UserID)
        {
            SqlParameter paramUserID = new SqlParameter();
            paramUserID.ParameterName = "UserID";
            paramUserID.Value = UserID;

            DataSet result = DBHelper.ExecuteStoredProcedure(StoredProcedure.GetCPAProfile, paramUserID);
            if (result == null || result.Tables[0].Rows.Count == 0)
                return null;
            return result;
        }

        internal static DataSet GetAllAppointments(DateTime date, int CPAID)
        {
            SqlParameter paramScheduledDate = new SqlParameter();
            paramScheduledDate.ParameterName = "scheduledDate";
            paramScheduledDate.Value = date;

            SqlParameter paramCPAID = new SqlParameter();
            paramCPAID.ParameterName = "CPAID";
            paramCPAID.Value = CPAID;

            DataSet result = DBHelper.ExecuteStoredProcedure(StoredProcedure.GetAvailableAppointments, paramScheduledDate, paramCPAID);
            if (result == null || result.Tables[0].Rows.Count == 0)
                return null;
            return result;
        }

        public static byte[] GetCPAImage(int CPAID)
        {
            string strQuery = string.Format(Constants.CPA_IMAGE_QUERY, CPAID);

            var result = DBHelper.GetScalarValue(strQuery);
            if (result == DBNull.Value)
                return null;

            byte[] imageResult = (byte[])result;

            return imageResult;
        }
        public static byte[] GetUserImage(int CustomerID)
        {
            string strQuery = string.Format(Constants.Customer_IMAGE_QUERY, CustomerID);

            var result = DBHelper.GetScalarValue(strQuery);
            if (result == DBNull.Value)
                return null;

            byte[] imageResult = (byte[])result;

            return imageResult;
        }

        internal static DataSet GetAllSpecializationList()
        {
            return DBHelper.GetSelectDataSet(Constants.SPECIALITY_QUERY);
        }

        internal static DataSet GetAllStateList()
        {
            return DBHelper.GetSelectDataSet(Constants.STATE_QUERY);
        }

        internal static DataSet GetAllCityList(string State_Code)
        {
            
            return DBHelper.GetSelectDataSet(string.Format(Constants.CITY_QUERY, State_Code.ToString()));
        }
        internal static DataSet GetAllScheduledAppointmentForCustomer(int customerID)
        {
            String format = string.Format("select  distinct c.CPAID, c.CompanyName, c.Address1,c.Address2, c.City, c.State, c.ZipCode from CPA c  join  CPAAppointment ca  on c.CPAID=ca.CPAID where ca.CustomerID={0} and ca.StartTime>=GETUTCDATE()", customerID);
            return DBHelper.GetSelectDataSet(format);
        }
        internal static DataSet GetAllPastAppointmentForCustomer(int customerID)
        {
            String format = string.Format("select  distinct c.CPAID, c.CompanyName, c.Address1,c.Address2, c.City, c.State, c.ZipCode from CPA c  join  CPAAppointment ca  on c.CPAID=ca.CPAID where ca.CustomerID={0} and ca.StartTime<GETUTCDATE()", customerID);
            return DBHelper.GetSelectDataSet(format);
        }
        internal static List<DataSet> GetAllAppointmentsForWeek(int currentCPA, DateTime firstDateOfWeek)
        {
            List<DataSet> allAppointments = new List<DataSet>();
            DataSet tempAppointment = null;
            for (int index = 0; index < 7; index++)
            {
                if (firstDateOfWeek.AddDays(index) >= System.DateTime.Today)
                {
                    tempAppointment = GetAllAppointments(firstDateOfWeek.AddDays(index), currentCPA);
                }
                allAppointments.Add(tempAppointment);
            }
            return allAppointments;
        }

        internal static DataSet GetCPADetails(int cpaID)
        {
            return DBHelper.GetSelectDataSet(string.Format(Constants.CPA_DETAILS_QUERY, cpaID.ToString()));
        }

        internal static bool CreateNewCustomer(CustomerDetails newCustomer)
        {
            bool result = true;
            try
            {
                SqlParameter email = new SqlParameter() { ParameterName = "Email", Value = newCustomer.Email };
                SqlParameter firstName = new SqlParameter() { ParameterName = "FirstName", Value = newCustomer.FirstName };
                SqlParameter lastName = new SqlParameter() { ParameterName = "LastName", Value = newCustomer.LastName };
                SqlParameter dateOfBirth = new SqlParameter() { ParameterName = "DateOfBirth", Value = newCustomer.DateOfBirth };
                SqlParameter gender = new SqlParameter() { ParameterName = "Gender", Value = newCustomer.Gender };
                SqlParameter phoneNumber = new SqlParameter() { ParameterName = "PhoneNumber", Value = newCustomer.PhoneNumber };
                SqlParameter password = new SqlParameter() { ParameterName = "Password", Value = newCustomer.Password };
                SqlParameter image = new SqlParameter() { ParameterName = "UserImage", Value = newCustomer.Image };

                DBHelper.ExecuteStoredProcedure(StoredProcedure.SignUpNewCustomer, email, firstName, lastName, dateOfBirth, gender, phoneNumber,password,image);
            }
            catch (Exception ex)
            {
                result = false;
                throw ex;
            }
            return result;
        }
        internal static bool CreateNewTempCustomer(CustomerDetails newCustomer)
        {
            bool result = true;
            try
            {
                SqlParameter email = new SqlParameter() { ParameterName = "Email", Value = newCustomer.Email };
                SqlParameter firstName = new SqlParameter() { ParameterName = "FirstName", Value = newCustomer.FirstName };
                SqlParameter lastName = new SqlParameter() { ParameterName = "LastName", Value = newCustomer.LastName };
                SqlParameter dateOfBirth = new SqlParameter() { ParameterName = "DateOfBirth", Value = newCustomer.DateOfBirth };
                SqlParameter gender = new SqlParameter() { ParameterName = "Gender", Value = newCustomer.Gender };
                SqlParameter phoneNumber = new SqlParameter() { ParameterName = "PhoneNumber", Value = newCustomer.PhoneNumber };
                SqlParameter password = new SqlParameter() { ParameterName = "Password", Value = newCustomer.Password };
                SqlParameter image = null;
                if(newCustomer.Image == null)
                {
                    image = new SqlParameter();
                    image.ParameterName = "UserImage";
                    image.SqlDbType = SqlDbType.Image;
                    image.Value = new byte[0];
                }
                else
                {
                    image = new SqlParameter() { ParameterName = "UserImage", Value = newCustomer.Image };
                }
                SqlParameter activationToken = new SqlParameter() { ParameterName = "ActivationToken", Value = newCustomer.ActivationToken };
                SqlParameter createdDate = new SqlParameter() { ParameterName = "CreatedDate", Value = newCustomer.CreatedDate };

                DBHelper.ExecuteStoredProcedure(StoredProcedure.SignUpNewTempCustomer, email, firstName, lastName, dateOfBirth, gender, phoneNumber, password, image,activationToken,createdDate);
            }
            catch (Exception ex)
            {
                result = false;
                throw ex;
            }
            return result;
        }
        
        internal static bool UpdateCustomerDetails(CustomerDetails customer)
        {
            bool result = true;
            try
            {
                SqlParameter email = new SqlParameter() { ParameterName = "Email", Value = customer.Email };
                SqlParameter firstName = new SqlParameter() { ParameterName = "FirstName", Value = customer.FirstName };
                SqlParameter lastName = new SqlParameter() { ParameterName = "LastName", Value = customer.LastName };
                SqlParameter dateOfBirth = new SqlParameter() { ParameterName = "DateOfBirth", Value = customer.DateOfBirth };
                SqlParameter gender = new SqlParameter() { ParameterName = "Gender", Value = customer.Gender };
                SqlParameter phoneNumber = new SqlParameter() { ParameterName = "PhoneNumber", Value = customer.PhoneNumber };
                SqlParameter userID= new SqlParameter() { ParameterName="UserID",Value=customer.UserID };
                SqlParameter userImage = new SqlParameter() { ParameterName = "UserImage", Value = customer.Image };


                DBHelper.ExecuteStoredProcedure(StoredProcedure.UpdateCustomer, email, firstName, lastName, dateOfBirth, gender, phoneNumber,userID,userImage);
            }
            catch (Exception ex)
            {
                result = false;
                throw ex;
            }
            return result;
        }
        internal static bool UpdateCPADetails(CPADetails cpa)
        {
            bool result = true;
            try
            {
                SqlParameter CompanyName = new SqlParameter() { ParameterName = "CompanyName", Value = cpa.CompanyName };
                SqlParameter Address1 = new SqlParameter() { ParameterName = "Address1", Value = cpa.Address1 };
                SqlParameter Address2 = new SqlParameter() { ParameterName = "Address2", Value = cpa.Address2 };
                SqlParameter City = new SqlParameter() { ParameterName = "City", Value = cpa.City };
                SqlParameter State = new SqlParameter() { ParameterName = "State", Value = cpa.State };
                SqlParameter ZipCode = new SqlParameter() { ParameterName = "ZipCode", Value = cpa.ZipCode };
                SqlParameter PhoneNumber = new SqlParameter() { ParameterName = "PhoneNumber", Value = cpa.PhoneNumber };
                SqlParameter FirstName = new SqlParameter() { ParameterName = "FirstName", Value = cpa.FirstName };
                SqlParameter LastName = new SqlParameter() { ParameterName = "LastName", Value = cpa.LastName };
                SqlParameter Gender = new SqlParameter() { ParameterName = "Gender", Value = cpa.Gender };
                SqlParameter SpecialityID = new SqlParameter() { ParameterName = "SpecialityID", Value = cpa.SpecialityID };
                SqlParameter Email = new SqlParameter() { ParameterName = "Email", Value = cpa.Email };
                //SqlParameter Password = new SqlParameter() { ParameterName = "Password", Value = cpa.Password };
                SqlParameter CPAImage = new SqlParameter() { ParameterName = "CPAImage", Value = cpa.Image };
                SqlParameter UserID = new SqlParameter() { ParameterName = "UserID", Value = cpa.UserID };
                SqlParameter DateOFBirth = new SqlParameter() { ParameterName = "DateOfBirth", Value = cpa.DateOfBirth };
                SqlParameter Speciality = new SqlParameter() { ParameterName = "Speciality", Value = cpa.Speciality };

                DBHelper.ExecuteStoredProcedure(StoredProcedure.UpdateCPA, Address1, Address2, City, State, ZipCode, PhoneNumber, FirstName, LastName, Gender, SpecialityID, Email, CompanyName,DateOFBirth, CPAImage,UserID,Speciality);
            }
            catch (Exception ex)
            {
                result = false;
                throw ex;
            }
            return result;
        }

        internal static bool CreateNewCPA(CPADetails newCPA)
        {
            bool result =true;
            try
            {
                SqlParameter CompanyName = new SqlParameter() { ParameterName = "CompanyName", Value = newCPA.CompanyName };
                SqlParameter Address1 = new SqlParameter() { ParameterName = "Address1", Value = newCPA.Address1 };
                SqlParameter Address2 = new SqlParameter() { ParameterName = "Address2", Value = newCPA.Address2 };
                SqlParameter City = new SqlParameter() { ParameterName = "City", Value = newCPA.City };
                SqlParameter State = new SqlParameter() { ParameterName = "State", Value = newCPA.State };
                SqlParameter ZipCode = new SqlParameter() { ParameterName = "ZipCode", Value = newCPA.ZipCode };
                SqlParameter Phone = new SqlParameter() { ParameterName = "Phone", Value = newCPA.PhoneNumber };
                SqlParameter FirstName = new SqlParameter() { ParameterName = "FirstName", Value = newCPA.FirstName };
                SqlParameter LastName = new SqlParameter() { ParameterName = "LastName", Value = newCPA.LastName };
                SqlParameter DateOfBirth = new SqlParameter() { ParameterName = "DateOfBirth", Value = newCPA.DateOfBirth };
                SqlParameter Gender = new SqlParameter() { ParameterName = "Gender", Value = newCPA.Gender };
                //SqlParameter SpecialityID = new SqlParameter() { ParameterName = "SpecialityID", Value = newCPA.SpecialityID };
                SqlParameter Email = new SqlParameter() { ParameterName = "Email", Value = newCPA.Email };
                SqlParameter Password = new SqlParameter() {ParameterName = "Password", Value=newCPA.Password };
                SqlParameter CPAImage = new SqlParameter() { ParameterName = "CPAImage", Value = newCPA.Image };
                SqlParameter Speciality = new SqlParameter() { ParameterName = "Speciality", Value = newCPA.Speciality };
                SqlParameter Latitude = new SqlParameter() { ParameterName = "Latitude", Value = newCPA.Latitude };
                SqlParameter Longitude = new SqlParameter() { ParameterName = "Longitude", Value = newCPA.Longitude };
               
                DBHelper.ExecuteStoredProcedure(StoredProcedure.SignUpNewCPA, Address1 , Address2 , City , State , ZipCode , Phone , FirstName , LastName ,DateOfBirth, Gender ,Email,Password,CompanyName,CPAImage,Speciality,Latitude,Longitude);
            }
            catch (Exception ex)
            {
                result = false;
                throw ex;
            }
            return result;
        }
            internal static bool CreateNewTempCPA(CPADetails newCPA)
        {
            bool result =true;
            try
            {
                SqlParameter CompanyName = new SqlParameter() { ParameterName = "CompanyName", Value = newCPA.CompanyName };
                SqlParameter Address1 = new SqlParameter() { ParameterName = "Address1", Value = newCPA.Address1 };
                SqlParameter Address2 = new SqlParameter() { ParameterName = "Address2", Value = newCPA.Address2 };
                SqlParameter City = new SqlParameter() { ParameterName = "City", Value = newCPA.City };
                SqlParameter State = new SqlParameter() { ParameterName = "State", Value = newCPA.State };
                SqlParameter ZipCode = new SqlParameter() { ParameterName = "ZipCode", Value = newCPA.ZipCode };
                SqlParameter Phone = new SqlParameter() { ParameterName = "Phone", Value = newCPA.PhoneNumber };
                SqlParameter FirstName = new SqlParameter() { ParameterName = "FirstName", Value = newCPA.FirstName };
                SqlParameter LastName = new SqlParameter() { ParameterName = "LastName", Value = newCPA.LastName };
                SqlParameter DateOfBirth = new SqlParameter() { ParameterName = "DateOfBirth", Value = newCPA.DateOfBirth };
                SqlParameter Gender = new SqlParameter() { ParameterName = "Gender", Value = newCPA.Gender };
                //SqlParameter SpecialityID = new SqlParameter() { ParameterName = "SpecialityID", Value = newCPA.SpecialityID };
                SqlParameter Email = new SqlParameter() { ParameterName = "Email", Value = newCPA.Email };
                SqlParameter Password = new SqlParameter() {ParameterName = "Password", Value=newCPA.Password };
                SqlParameter CPAImage = new SqlParameter() { ParameterName = "CPAImage", Value = newCPA.Image };
                SqlParameter Speciality = new SqlParameter() { ParameterName = "Speciality", Value = newCPA.Speciality };
                SqlParameter ActivationToken = new SqlParameter() { ParameterName = "ActivationToken", Value = newCPA.ActivationToken };
                SqlParameter CreatedDate = new SqlParameter() { ParameterName = "CreatedDate", Value = newCPA.CreatedDate };
                SqlParameter Latitude = new SqlParameter() { ParameterName = "Latitude", Value = newCPA.Latitude };
                SqlParameter Longitude = new SqlParameter() { ParameterName = "Longitude", Value = newCPA.Longitude };

                DBHelper.ExecuteStoredProcedure(StoredProcedure.SignUpNewTempCPA, Address1, Address2, City, State, ZipCode, Phone, FirstName, LastName, DateOfBirth, Gender, Email, Password, CompanyName, CPAImage, Speciality, ActivationToken, CreatedDate,Latitude, Longitude);
            }
            catch (Exception ex)
            {
                result = false;
                throw ex;
            }
            return result;
            }
        
         internal static bool IsLoginSuccessful(string email, string password)
        {
            bool isValidLogin = false;
            string loginQuery = string.Format("select COUNT(1) from Users where Email = '{0}' and Password='{1}'",email,password);
            isValidLogin =  int.Parse(DBHelper.GetScalarValue(loginQuery).ToString()) > 0;
            return isValidLogin;
        }

        internal static  int GetRoleID(string email, string password)
        {   
            //bool isValidLogin = false;
            string loginQuery = string.Format("select [RoleID] from Users where Email = '{0}' and Password='{1}'",email,password);
            int role = int.Parse(DBHelper.GetScalarValue(loginQuery).ToString());
            return role;
        }

        internal static int GetUserID(string email, string password)
        {
            string userIDQuery = string.Format("select UserID from Users where Email = '{0}' and Password='{1}'", email, password);
            return int.Parse(DBHelper.GetScalarValue(userIDQuery).ToString());

        }

        internal static  string GetLoggedInUserName(string email, string password )

        { string UserQuery= string.Format("select C.FirstName from Customer C inner join Users U on U.UserID=C.CustomerID where  U.Email ='{0}' AND U.Password ='{1}'",email,password);
         return (string)DBHelper.GetScalarValue(UserQuery);
        }

        internal static string GetLoggedInEmailID(int UserID )
        {
            string UserQuery = string.Format("select Email from  Users  where UserID={0}", UserID);
            return (string)DBHelper.GetScalarValue(UserQuery);
        }
        internal static string GetLoggedInName(int UserID)
        {
            string UserQuery = string.Format("select FirstName from  Customer  where CustomerID={0}", UserID);
            return (string)DBHelper.GetScalarValue(UserQuery);
        }
        internal static string GetLoggedInCPAName(string email, string password)
        {
            string UserQuery = string.Format("select C.FirstName from CPA C inner join Users U on U.UserID=C.CPAID where  U.Email ='{0}' AND U.Password ='{1}'", email, password);
            return DBHelper.GetScalarValue(UserQuery).ToString();
        }
        internal static int GetNewUserID()
        {
            int userID = 0;
            userID = int.Parse(DBHelper.GetScalarValue("select MAX(userid) from Users").ToString());

            return userID;
        }
        internal static int GetNewSpecialityID()
        {
            int SpecialityID = 0;
            SpecialityID = int.Parse(DBHelper.GetScalarValue("select MAX(ID)+1 from LuSpeciality").ToString());

            return SpecialityID;
        }

        internal static DataSet GetCustomerDetails(string customerID)
        {
            return DBHelper.GetSelectDataSet(string.Format(Constants.CUSTOMER_ID_QUERY, customerID.ToString()));
        }

        internal static string GetSpeciatityByID(string SpecialityID)
         {
             return DBHelper.GetScalarValue(string.Format(Constants.SPECIALITY_BYID_QUERY, SpecialityID)).ToString();
        }
        internal static DataSet GetSCheduleCPACustomerDetails(string CPAID ,string customerID )
        {
            string format = string.Format("SELECT PurposeOfVisit ,CustomerName,ContactNumber from CPAAppointment where (CPAID={0} and CustomerID={1} and StartTime>=GETUTCDATE()) ", CPAID, customerID);
            return DBHelper.GetSelectDataSet(format);
        }
        internal static DataSet GetSCheduleCPACustomerDetails1(string CPAID, string customerID)
        {
            string format = string.Format("SELECT PurposeOfVisit ,CustomerName,ContactNumber,datename(weekday,StartTime)+', '+dateName(month,StartTime)+' '+DATEname(day,StartTime)+', '+dateName(year,StarTtime)+'-'+CONVERT(varchar(15),CAST(StartTime AS TIME),100) as 'Time' ,CPAID,ID from CPAAppointment where (CPAID={0} and CustomerID={1} and StartTime>=GETUTCDATE()) ", CPAID, customerID);
            return DBHelper.GetSelectDataSet(format);
        }
        internal static DataSet GetCPACustomerSchedule1(string CPAID, string customerID)
        {
            string format = string.Format("SELECT datename(weekday,StartTime)+', '+dateName(month,StartTime)+' '+DATEname(day,StartTime)+', '+dateName(year,StarTtime)+'-'+CONVERT(varchar(15),CAST(StartTime AS TIME),100) as 'Time' ,CPAID,ID from CPAAppointment where CPAID={0} and CustomerID={1} and StartTime>=GETUTCDATE() ", CPAID, customerID);
            return DBHelper.GetSelectDataSet(format);
        }
        internal static DataSet GetSCheduleCPACustomerDetails2( string customerID)
        {
            string format = string.Format("SELECT  c.CPAID, c.CompanyName, c.Address1,c.Address2, c.City, c.State, c.ZipCode  , ca.PurposeOfVisit ,ca.CustomerName,ca.ContactNumber,datename(weekday,ca.StartTime)+', '+dateName(month,ca.StartTime)+' '+DATEname(day,ca.StartTime)+', '+dateName(year,ca.StarTtime)+'-'+CONVERT(varchar(15),CAST(ca.StartTime AS TIME),100) as 'Time' ,ca.ID from CPA c  join  CPAAppointment ca  on c.CPAID=ca.CPAID where ca.CustomerID={0} and ca.StartTime>=GETUTCDATE() order by StartTime ", customerID);
            return DBHelper.GetSelectDataSet(format);
        }
        internal static int GetSCheduleMaxAppointment(string customerID)
        {
            string format = string.Format("SELECT  count(*)  from [dbo].[CPAAppointment]   where CustomerID={0} and StartTime>=GETDATE() ", customerID);
           
            return int.Parse(DBHelper.GetScalarValue(format).ToString());
        }
        internal static int GetFirmSettingValue(string Key)
        {
            string format = string.Format("Select Value from FirmSetting where [Key]='{0}'", Key);

            return int.Parse(DBHelper.GetScalarValue(format).ToString());
        }
        internal static bool IsBookAppointmentAvailable(string ID)
        {
            string format = string.Format("SELECT  IsOpen  from [dbo].[CPAAppointment]   where ID={0} ", ID);

            return (bool)DBHelper.GetScalarValue(format);
        }
        internal static DataSet GetCPACustomerSchedule2(string CPAID, string customerID)
        {
            string format = string.Format("SELECT datename(weekday,StartTime)+', '+dateName(month,StartTime)+' '+DATEname(day,StartTime)+', '+dateName(year,StarTtime)+'-'+CONVERT(varchar(15),CAST(StartTime AS TIME),100) as 'Time' ,CPAID,ID from CPAAppointment where CPAID={0} and CustomerID={1} and StartTime>=GETUTCDATE() ", CPAID, customerID);
            return DBHelper.GetSelectDataSet(format);
        }
        internal static DataSet GetPastSCheduleCPACustomerDetails2(string customerID)
        {
            string format = string.Format("SELECT  c.CPAID, c.CompanyName, c.Address1,c.Address2, c.City, c.State, c.ZipCode  , ca.PurposeOfVisit ,ca.CustomerName,ca.ContactNumber,datename(weekday,ca.StartTime)+', '+dateName(month,ca.StartTime)+' '+DATEname(day,ca.StartTime)+', '+dateName(year,ca.StarTtime)+'-'+CONVERT(varchar(15),CAST(ca.StartTime AS TIME),100) as 'Time' ,ca.ID from CPA c  join  CPAAppointment ca  on c.CPAID=ca.CPAID where ca.CustomerID={0} and ca.StartTime<GETUTCDATE() order by StartTime desc ", customerID);
            return DBHelper.GetSelectDataSet(format);
        }
        internal static DataSet GetPastCPACustomerDetails(string CPAID, string customerID)
        {
            string format = string.Format("SELECT PurposeOfVisit ,CustomerName,ContactNumber from CPAAppointment where CPAID={0} and CustomerID={1} and StartTime<GETUTCDATE() ", CPAID, customerID);
            return DBHelper.GetSelectDataSet(format);
        }

        internal static DataSet GetCPACustomerSchedule(string CPAID, string customerID)
        {
            string format = string.Format("SELECT datename(weekday,StartTime)+', '+dateName(month,StartTime)+' '+DATEname(day,StartTime)+', '+dateName(year,StarTtime)+'-'+CONVERT(varchar(15),CAST(StartTime AS TIME),100) as 'Time' ,CPAID,ID from CPAAppointment where CPAID={0} and CustomerID={1} and StartTime>=GETUTCDATE() ", CPAID, customerID);
            return DBHelper.GetSelectDataSet(format);
        }

        internal static DataSet GetPastCPACustomerSchedule(string CPAID, string customerID)
        {
            string format = string.Format("SELECT datename(weekday,StartTime)+', '+dateName(month,StartTime)+' '+DATEname(day,StartTime)+', '+dateName(year,StarTtime)+'-'+CONVERT(varchar(15),CAST(StartTime AS TIME),100) as 'Time' ,CPAID,ID from CPAAppointment where CPAID={0} and CustomerID={1} and StartTime<GETUTCDATE() ", CPAID, customerID);
            return DBHelper.GetSelectDataSet(format);
        }
        internal static DataSet GetEmailDetail(string EmailSpecificationId)
        {
            string format = string.Format("Select * from EmailSpecification where EmailSpecificationId={0}",EmailSpecificationId);
            return DBHelper.GetSelectDataSet(format);
        }
        internal static DataSet GetPassword(string EmailSpecificationId, string email)
        {
            string format = string.Format("Select e.*, u.Password from EmailSpecification e, Users u where EmailSpecificationId={0} AND Email ='{1}'", EmailSpecificationId, email);
            return DBHelper.GetSelectDataSet(format);
        }
        internal static int ReScheduleAppointment(string ID)
        { 
            string format=string.Format(" UPDATE [CPAAppointment] SET [CustomerID] = null,[CustomerName] = '',[PurposeOfVisit] ='',[ContactNumber] = '',[Note] = '',[IsOpen] = 'true' WHERE ID= {0}",ID);
            int result = DBHelper.ExecuteNonQuery(format);
            return result;
        }

        //internal static int CancelAppointment(string ID) 
        //{
        //    string format=string.Format("Delete from CPAAppointment where ID={0}",ID);
        //    int result = DBHelper.ExecuteNonQuery(format);
        //    return result;
        //}

        internal static DataSet CancelAppointment(int ID) 
        {
            SqlParameter paramUserID = new SqlParameter();
            paramUserID.ParameterName = "ID";
            paramUserID.Value = ID;

            DataSet result = DBHelper.ExecuteStoredProcedure(StoredProcedure.CancelAppointment, paramUserID);
            if (result == null || result.Tables[0].Rows.Count == 0)
                return null;
            return result;
        }
        internal static bool BookNewAppointment(string CPAID, string userID, string purposeOfVisit, string appointmentID)
        {
            bool result = true;
            try
            {
                SqlParameter paramAppointmentID = new SqlParameter() { ParameterName = "AppointmentID", Value = appointmentID };
                SqlParameter paramUserID = new SqlParameter() { ParameterName = "CustomerID", Value = userID};
                SqlParameter paramPurpose = new SqlParameter() { ParameterName = "PurposeOfVisit", Value = purposeOfVisit };
                SqlParameter paramCPAID = new SqlParameter() { ParameterName = "CPAID", Value = CPAID };
                SqlParameter paramNote = new SqlParameter() { ParameterName = "Note", Value = string.Empty };

                DBHelper.ExecuteStoredProcedure(StoredProcedure.BookAppointment, paramUserID, paramPurpose, paramCPAID, paramAppointmentID, paramNote);
            }
            catch (Exception ex)
            {
                result = false;
                throw ex;
            }
            return result;
        }

        internal static bool IsValidEmail(string email)
        {
            string strQuery = string.Format(Constants.IS_VALID_EMAIL, email);

            var result = DBHelper.GetScalarValue(strQuery);
            if (result == DBNull.Value || result==null)
                return false;
            return result.ToString().Equals("1");
        }

        internal static DataTable GetCPALocations()
        {
            DataSet result = DBHelper.ExecuteStoredProcedure(StoredProcedure.GetCPALocations);
            //if (result == null || result.Tables[0].Rows.Count == 0)
            //    return null;
            return result.Tables[0];
        }
    }
}