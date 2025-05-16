using System.Text;
using Renci.SshNet;
using SY.OnlineApp.Services.Interfaces;
public class SftpService : ISftpService
{
    private readonly string _host = "sftp.yourdomain.com";
    private readonly int _port = 22;
    private readonly string _username = "user";
    private readonly string _password = "pass";

    public string GetEmailTemplate(string remotePath)
    {
        using var client = new SftpClient(_host, _port, _username, _password);
        client.Connect();

        if (!client.Exists(remotePath))
            throw new FileNotFoundException("Template not found on SFTP.");

        using var ms = new MemoryStream();
        client.DownloadFile(remotePath, ms);
        client.Disconnect();

        return Encoding.UTF8.GetString(ms.ToArray());
    }
}
