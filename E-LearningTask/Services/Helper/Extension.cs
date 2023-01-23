using System.Net;
using System.Net.Mail;

namespace E_LearningTask.Services.Helper
{
    public class Extension : IExtension
    {

        private readonly IConfiguration _configuration;
        //private readonly smtp _smtp;
        public Extension(IConfiguration configuration /*,IOptions<smtp> options*/ )
        {
            _configuration = configuration;

            //  _smtp = options.Value;
        }

        public string UploudFile(string rootpath, string folderName, IFormFile file)
        {
            string filePath;
            //Guid
            string changvar = Guid.NewGuid().ToString();                 //DateTime.Now.Ticks
            string fileName = changvar + file.FileName;

            string folderPath = Path.Combine(rootpath + folderName);
            if (!Directory.Exists(folderPath))   //check
            {
                Directory.CreateDirectory(folderPath);    //create
            }

            filePath = folderName + fileName;   //DB
            string fullPath = Path.Combine(rootpath + filePath);

            var straem = new FileStream(fullPath, FileMode.Create);
            file.CopyTo(straem);
            straem.Flush();

            return filePath;
        }

        public bool SendEmail(string receiver, string subject, string body)
        {
            ///Iopion use and smtp class....
            string smtpadreess = _configuration.GetSection("smtp:smtpAdd").Value;
            int port = int.Parse(_configuration.GetSection("smtp:port").Value);
            string sender = _configuration["smtp:emailSender"];    //"sayme8726@gmail.com"; 
            string password = _configuration["smtp:password"];    //P@$4Asp.Net 

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(sender);
            mail.To.Add(receiver);
            mail.Subject = subject;
            mail.Body = body;

            try
            {
                SmtpClient smtp = new SmtpClient(smtpadreess, port);
                smtp.Credentials = new NetworkCredential(sender, password);
                smtp.EnableSsl = true;   //https
                smtp.UseDefaultCredentials = false;
                smtp.Send(mail);
                return true;
            }

            catch (Exception)
            {
                return false;
            }
        }


    }
}
