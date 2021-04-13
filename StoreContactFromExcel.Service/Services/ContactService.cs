using ExcelDataReader;
using StoreContactFromExcel.Data.DbContexts;
using StoreContactFromExcel.Data.Entities;
using System.Net.Mail;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace StoreContactFromExcel.Service.Services
{
    public interface IContactService
    {
        int AddContactFromExcel(string filePath);
    }
    public class ContactService : IContactService
    {
        private readonly InfoDbContext _dbContext;
        public ContactService(InfoDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // Thêm contact từ file excel, trả về số contact thêm thành công
        public int AddContactFromExcel(string filePath)
        {

            IEnumerable<Contact> contacts = ReadContactFromExcel(filePath);
            foreach (var item in contacts)
            {
                if (ValidateContact(item))
                {
                    _dbContext.Contacts.Add(item);
                }    
            }
            _dbContext.SaveChanges();
            return _dbContext.Contacts.Count();
        }

        bool ValidateContact(Contact contact)
        {
            if (IsEmail(contact.Email) && IsPhoneNumber(contact.Phone))
            {
                contact.Phone = "84" + contact.Phone.Substring(1);
            }
            else
                return false;
            if (CheckDuplicatePhone(contact.Phone))
                return false;
            return true;
        }

        bool IsEmail(string email)
        {
            try
            {
                new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsPhoneNumber(string phone)
        {
            if (Regex.Match(phone, @"^[0-9]{10}$").Success)
                return true;
            return false;
        }

        public bool CheckDuplicatePhone(string phone)
        {
            return _dbContext.Contacts.ToList().Any(x => x.Phone == phone);
        }

        public IEnumerable<Contact> ReadContactFromExcel(string filePath)
        {
            // Mở và đọc file excel
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

            DataSet result = excelReader.AsDataSet();

            excelReader.Close();

            // Map dữ liệu từ dataSet vào model
            List<Contact> contacts = new List<Contact>();
            for (int i = 1; i < result.Tables[0].Rows.Count; i++)
            {
                var contact = new Contact
                {
                    Name = result.Tables[0].Rows[i][0].ToString(),
                    Email = result.Tables[0].Rows[i][1].ToString(),
                    Phone = result.Tables[0].Rows[i][2].ToString(),
                    Address = result.Tables[0].Rows[i][3].ToString()
                };
                contacts.Add(contact);
            }    

            return contacts;
        }
    }
}
