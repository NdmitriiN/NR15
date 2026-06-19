using System;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace PK15.UsersClasses
{
    public class SendingEmail
    {
        private InfoEmailSending InfoEmailSending { get; set; }

        public SendingEmail(InfoEmailSending infoEmailSending)
        {
            InfoEmailSending = infoEmailSending ?? 
                throw new ArgumentNullException(nameof(infoEmailSending));
        }

        public void Send()
        {
            try
            {
                SmtpClient mySmtpClient = new SmtpClient(InfoEmailSending.SmtpClientAdress);

                mySmtpClient.UseDefaultCredentials = false;
                mySmtpClient.EnableSsl = true;

                NetworkCredential basicAuthentivationInfo = new NetworkCredential(
                    InfoEmailSending.EmailAdressForm.EmailAdress,
                    InfoEmailSending.EmailPassword);

                mySmtpClient.Credentials = basicAuthentivationInfo;

                MailAddress from = new MailAddress(
                    InfoEmailSending.EmailAdressForm.EmailAdress,
                    InfoEmailSending.EmailAdressForm.Name);

                MailAddress to = new MailAddress(
                    InfoEmailSending.EmailAdressTo.EmailAdress,
                    InfoEmailSending.EmailAdressTo.Name);

                MailMessage myMeil = new MailMessage(from, to);

                MailAddress replyTo = new MailAddress(InfoEmailSending.EmailAdressForm.EmailAdress);
                myMeil.ReplyToList.Add(replyTo);

                Encoding encoding = Encoding.UTF8;

                myMeil.Subject = InfoEmailSending.Subject;
                myMeil.SubjectEncoding = encoding;

                myMeil.Body = InfoEmailSending.Body;
                myMeil.BodyEncoding = encoding;

                mySmtpClient.Send(myMeil);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
