using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace wcfp3
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService2" in both code and config file together.
    [ServiceContract]
    public interface IService2
    {

        [OperationContract]
        BookPurchaseResponse PurchaseBooks(BookPurchaseInfo info);
    }


    [MessageContract]
    public class BookPurchaseInfo
    {
        [MessageBodyMember]
        public double budget;
        [MessageBodyMember]
        public Dictionary<int, int> items;

    }

    [MessageContract]
    public class BookPurchaseResponse
    {
        [MessageHeader]
        public bool result;
        [MessageHeader]
        public string response;
    }
}
