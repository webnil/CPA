using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace CheckIn
{
    [DataContract]
    public class CustomerDetails
    {
        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public DateTime DateOfBirth { get; set; }

        [DataMember]
        public string Gender { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }

        [DataMember]
        public string UserID { get; set; }

        [DataMember]
        public byte[] Image { get; set; }

        [DataMember]
        public string ActivationToken { get; set; }

        [DataMember]
        public string Active { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        


    }

    [DataContract]
    public class CPADetails
    {
        [DataMember]
        public string UserID { get; set; }

        [DataMember]
        public string CompanyName { get; set; }

        [DataMember]
        public byte[]  Image { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public DateTime DateOfBirth { get; set; }

        [DataMember]
        public string Gender { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }

        [DataMember]
        public string ZipCode { get; set; }

        [DataMember]
        public string Address1 { get; set; }

        [DataMember]
        public string Address2 { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string State { get; set; }

        [DataMember]
        public string SpecialityID { get; set; }

        [DataMember]
        public string Speciality { get; set; }

        [DataMember]
        public string ActivationToken { get; set; }

        [DataMember]
        public string Active { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }
}