﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace CS3280InvoiceSystem.Search
{
    /// <summary>
    /// Class for retrieiving all relevent search data from the database.
    /// </summary>
    class clsSearchSQL
    {
        /// <summary>
        /// Used to access database.
        /// </summary>
        clsDataAccess db;

        public clsSearchSQL()
        {
            db = new clsDataAccess();
        }

        /// <summary>
        /// Queries database for all invoices that match the given InvoiceNumber, InvoiceDate, and TotalCost.
        /// Set number to -1 if no number selected.
        /// Set date to null if no date selected.
        /// Set total to -1 if no total selected.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="date"></param>
        /// <param name="total"></param>
        /// <returns>Data table for DataGrid</returns>
        public DataTable searchInvoices(int number, string date, int total)
        {
            try
            {
                DataSet ds;

                int iRet = 0;

                string sql = "SELECT * FROM Invoices WHERE ";
                if(number != -1)
                {
                    sql += ("InvoiceNum = " + number);
                }
                if(date != null)
                {
                    if(number != -1)
                    {
                        sql += " AND ";
                    }
                    sql += ("InvoiceDate = #" + date + "#");
                    Console.WriteLine(sql);
                }
                if(total != -1)
                {
                    if(number != -1 || date != null)
                    {
                        sql += " AND ";
                    }
                    sql += ("TotalCost = " + total);
                }

                ds = db.ExecuteSQLStatement(sql, ref iRet);

                return ds.Tables[0];
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Queries database for Invoice Numbers sorted numerically.
        /// </summary>
        /// <returns>List of int</returns>
        public List<int> getInvoiceNumbers()
        {
            try
            {
                DataSet ds;

                int iRet = 0;

                List<int> invoiceNumbers = new List<int>();

                ds = db.ExecuteSQLStatement(
                    "SELECT InvoiceNum FROM Invoices", ref iRet
                );

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    int n;
                    try
                    {
                        n = Int32.Parse(dr[0].ToString());
                        invoiceNumbers.Add(n);
                    }catch(Exception ex)
                    {
                        throw ex;
                    }
                }

                invoiceNumbers.Sort();
                return invoiceNumbers;

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Queries database for all Invoice Dates sorted by date.
        /// </summary>
        /// <returns>List of DateTime</returns>
        public List<string> getInvoiceDates()
        {
            try
            {
                DataSet ds;
                int iRet = 0;

                List<string> invoiceDates = new List<string>();

                ds = db.ExecuteSQLStatement(
                    "SELECT InvoiceDate FROM Invoices", ref iRet
                );

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string d;
                    DateTime dt;
                    try
                    {
                        dt = (DateTime) dr[0];
                        d = dt.ToString("M/dd/yyy");
                        invoiceDates.Add(d);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                invoiceDates.Sort();

                return invoiceDates;

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Queries database for all Invoice Total Charges and sorts by ammount.
        /// </summary>
        /// <returns>List of int</returns>
        public List<int> getInvoiceTotalCharges()
        {
            try
            {
                DataSet ds;
                int iRet = 0;

                List<int> invoiceTotals = new List<int>();

                ds = db.ExecuteSQLStatement(
                    "SELECT TotalCost FROM Invoices", ref iRet
                );

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    int t;
                    try
                    {
                        t = Int32.Parse(dr[0].ToString());
                        invoiceTotals.Add(t);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                invoiceTotals.Sort();

                return invoiceTotals;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
