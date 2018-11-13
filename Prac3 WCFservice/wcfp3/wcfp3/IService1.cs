using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace wcfp3
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        List<Book> ReadBooks(String file);

        [OperationContract]
        void WriteBooks(List<Book> books, string file);

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class Book
    {
        /* 
          string id;
          string Name;
          string Author;
          int Year;
          float Price;
          int Stock;

          public Book(string id,
          string Name,
          string Author,
          int Year,
          float Price,
          int Stock) {
              this.id = id; this.Name = Name;
              this.Author = Author;this.Year = Year;
              this.Price = Price; this.Stock = Stock;*/



        [DataMember]
        public string ID;
        [DataMember]
        public string name;
        [DataMember]
        public string author;
        [DataMember]
        public int year;
        [DataMember]
        public double price;
        [DataMember]
        public int stock;
    }

    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
