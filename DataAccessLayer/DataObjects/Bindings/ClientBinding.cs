using System;

namespace Stemstudios.DataAccessLayer.DataObjects.Bindings
{
    [BindableObject]
    public class ClientBinding
    {
        [BindableProperty]
        public String clientID { get; set; }
        [BindableProperty]
        public String FirstName { get; set; }
        [BindableProperty]
        public String LastName { get; set; }
        [BindableProperty]
        public String Company { get; set; }
        [BindableProperty]
        public String Address { get; set; }
        [BindableProperty]
        public String City { get; set; }
        [BindableProperty]
        public String Province { get; set; }
        [BindableProperty]
        public String PostalCode { get; set; }
        [BindableProperty]
        public String PhoneNumber { get; set; }
        [BindableProperty]
        public String EmailAddress { get; set; }
        [BindableProperty]
        public int ClientType { get; set; }
    }
}
