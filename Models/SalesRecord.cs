using System;
using testSalesMVC.Models.Enums;

namespace testSalesMVC.Models {

    public class SalesRecord {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double amount { get; set; }
        public SalesStatus status { get; set; }
        public Seller Seller { get; set; }

        public SalesRecord() {
        }

        public SalesRecord(int id, DateTime date, double amount, SalesStatus status, Seller seller) {
            Id = id;
            Date = date;
            this.amount = amount;
            this.status = status;
            Seller = seller;
        }
    }
}
