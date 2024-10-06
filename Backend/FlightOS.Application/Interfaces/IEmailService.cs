namespace FlightOS.Application.Interfaces
{
    /// <summary>
    /// Interface for sending emails.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends an email asynchronously.
        /// </summary>
        /// <param name="recipientEmail"></param>
        /// <param name="subject"></param>
        /// <param name="messageBody"></param>
        /// <returns></returns>
        Task SendEmailAsync(string recipientEmail, string subject, string messageBody);
    }
}
