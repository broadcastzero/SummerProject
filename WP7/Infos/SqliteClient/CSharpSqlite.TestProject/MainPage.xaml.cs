using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using SQLiteClient;
using Community.CsharpSqlite;
using System.Collections;



namespace CSharpSqlite.TestProject
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private string fileName = "Test.DB";
        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            lbOutput.Text = "";
            if (db != null)
            {
                try
                {
                    SQLiteCommand cmd = db.CreateCommand("Create table test (id int primary key,name text,bytes numeric(50),modified datetime,condition boolean,stream blob)");
                    int i = cmd.ExecuteNonQuery();
                    lbOutput.Text += "\nCommand completed successfully";
                }
                catch (SQLiteException ex)
                {
                    lbOutput.Text += "\nError: " + ex.Message;
                }
            }
        }

        int callback(object pArg, System.Int64 nArg, object azArgs, object azCols)
        {
            int i;
            string[] azArg = (string[])azArgs;
            string[] azCol = (string[])azCols;
            String sb = "";
            for (i = 0; i < nArg; i++)
                sb += azCol[i] + " = " + azArg[i] + "\n";
            lbOutput.Text += ("\n" + sb.ToString());
            return 0;
        }

        private void btnDrop_Click(object sender, RoutedEventArgs e)
        {
            lbOutput.Text = "";
            if (db != null)
            {
                try
                {
                    SQLiteCommand cmd = db.CreateCommand("Drop table test");
                    int i = cmd.ExecuteNonQuery();
                    lbOutput.Text += "\nCommand completed successfully";
                }
                catch (SQLiteException ex)
                {
                    lbOutput.Text += "\nError: " + ex.Message;
                }
            }
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            lbOutput.Text = "";
            if (db != null)
            {
                try
                {
                    DateTime start = DateTime.Now;
                    SQLiteCommand cmd = db.CreateCommand("");
                    int rec;
                    Random rnd = new Random();
                    for (int i = 0; i < sliderRows.Value; i++)
                    {
                        byte[] stream = new byte[i + 1];
                        rnd.NextBytes(stream);
                        Test tst = new Test(i,"Entry "+i,i+10,DateTime.Now,(i%2==0)?true:false,stream);
                        //cmd.CommandText = " Insert into test (id,name) values (" + i.ToString() + ",\"Tst\")";
                        cmd.CommandText = " Insert into test (id,name,bytes,modified,condition,stream) values (@id,@name,@bytes,@modified,@condition,@stream)";
                        rec = cmd.ExecuteNonQuery(tst);
                        //i++;
                        //tst = new Test(i, "Entry " + i, i + 10, DateTime.Now, (i % 2 == 0) ? true : false);
                        //cmd.CommandText = " Insert into test values (?,?,?,?,?)";
                        //rec = cmd.ExecuteNonQuery(tst);
                    }
                    lbOutput.Text += "\nInserted " + sliderRows.Value + " rows\r\nGenerated in " + (DateTime.Now - start).TotalSeconds;
                }
                catch (SQLiteException ex)
                {
                    lbOutput.Text += "\nError: " + ex.Message;
                }
            }
        }
        SQLiteConnection db = null;
        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            //if (OpenDB())            
            lbOutput.Text = "";
            if (db == null)
            {
                db = new SQLiteConnection(fileName);
                db.Open();
                btnCreate.IsEnabled = true;
                btnDrop.IsEnabled = true;
                btnInsert.IsEnabled = true;
                btnClose.IsEnabled = true;
                btnOpen.IsEnabled = false;
                btnClear.IsEnabled = true;
                btnSelect.IsEnabled = true;
                btnGen2.IsEnabled = true;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            lbOutput.Text = "";
            if (db != null)
            {
                db.Dispose();
                db = null;
                //CloseDB();
                btnCreate.IsEnabled = false;
                btnDrop.IsEnabled = false;
                btnInsert.IsEnabled = false;
                btnOpen.IsEnabled = true;
                btnClose.IsEnabled = false;
                btnClear.IsEnabled = false;
                btnSelect.IsEnabled = false;
                btnGen2.IsEnabled = false;
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            lbOutput.Text = "";
            if (db != null)
            {
                try
                {
                    SQLiteCommand cmd = db.CreateCommand("Delete from test");
                    int rec = cmd.ExecuteNonQuery();
                    lbOutput.Text += "\nDeleted " + rec + " rows ";
                }
                catch (SQLiteException ex)
                {
                    lbOutput.Text += "\nError: " + ex.Message;
                }
            }
        }

        private void sliderRows_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var v = e.NewValue;
            if (v % 50 > 0)
                ((Slider)sender).Value = ((int)(v / 50)) * 50;
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            lbOutput.Text = "";
            if (db != null)
            {
                DateTime start = DateTime.Now;
                try
                {
                    //List<Test> lst=new List<Test>();
                    SQLiteCommand cmd = db.CreateCommand("SELECT * FROM test");
                    var lst = cmd.ExecuteQuery<Test>();
                    lbOutput.Text += "Selected " + lst.ToList().Count + " items\r\nTime " + (DateTime.Now - start).TotalSeconds;
                }
                catch (SQLiteException ex)
                {
                    lbOutput.Text += "Error: " + ex.Message;
                }
            }
        }

        private void btnGen2_Click(object sender, RoutedEventArgs e)
        {
            lbOutput.Text = "";
            if (db != null)
            {
                try
                {
                    DateTime start = DateTime.Now;
                    db.BeginTransaction();
                    SQLiteCommand cmd = db.CreateCommand("");
                    int rec;
                    for (int i = 0; i < sliderRows.Value; i++)
                    {
                        cmd.CommandText = " Insert into test (id,name) values (" + i.ToString() + ",\"Tst\")";
                        rec = cmd.ExecuteNonQuery();
                    }
                    db.CommitTransaction();
                    lbOutput.Text += "\nInserted " + sliderRows.Value + " rows\r\nGenerated in " + (DateTime.Now - start).TotalSeconds;
                }
                catch (SQLiteException ex)
                {
                    if (db.TransactionOpened)
                        db.RollbackTransaction();
                    lbOutput.Text += "\nError: " + ex.Message;
                }
            }
        }
    }

    public class Test
    {
        public Test()
        {
        }
        public Test(int Id, string Name, decimal Bytes, DateTime Modified, bool Condition,byte[] Stream)
        {
            id = Id;
            name = Name;
            bytes = Bytes;
            modified = Modified;
            condition = Condition;
            stream = Stream;
        }
        int _id;
        decimal _bytes;
        DateTime _modified;
        bool _condition;
        byte[] _stream;

        public byte[] stream
        {
            get { return _stream; }
            set { _stream = value; }
        }

        public decimal bytes
        {
            get { return _bytes; }
            set { _bytes = value; }
        }
        public DateTime modified
        {
            get { return _modified; }
            set { _modified = value; }
        }
        public bool condition
        {
            get { return _condition; }
            set { _condition = value; }
        }
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        string _name;
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

    }
}
