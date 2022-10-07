using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace UDPProtokolFileTransfer
{
    //Uygulamamız hem client hem server görevi yapıyor.
    //Whatsapp konuşması gibi yada kendinizde dahil olduğu bir mail grubuna mail attığınızda mailin hem giden hem gelen kutunuza düştüğü gibi 
    class Program
    {
        static UdpClient udp;//bağlantı tipi Udp olduğu için upd client kullandık. Tcp de TCPClient var
        static IPEndPoint udp_ep;
        static string FileName = "ardiuno.txt";
        static void Main(string[] args)
        {
            string ip = GetLocalIPAddress();//ip adresi alınıyor
            if (!ip.Equals("Not Found!")) // aktif ibr ip var ise
            {
                Console.WriteLine("Dosya Transferi için bir tuşa basınız");
                Console.ReadKey();
                byte[] bSend = File.ReadAllBytes(FileName); //dosyayı byte dizisine çeviriyor

                udp_ep = new IPEndPoint(IPAddress.Any, 2280);//baplantı noktası oluşturuyor

                udp = new UdpClient(udp_ep);

                udp.Send(bSend, bSend.Length, ip, 2280);//data aktarılıyor

                udp.BeginReceive(new AsyncCallback(UDP_IncomingData), udp_ep);//asenksron olarakta bir data gelmesi bekleniyor burada uygulamadan bağımsız bir işlem başlatıyoruz

                Console.ReadKey();
            }

        }
        //Gelen byte dizisini alıyor ve aynı isim ile D dizinine kayıt ediyor
        static void UDP_IncomingData(IAsyncResult ar)
        {

            byte[] bResp = udp.EndReceive(ar, ref udp_ep); //gelen byte alınıyor

            File.WriteAllBytes(@"D:\" + FileName, bResp);//dosyaya çeviriliyor

            Console.WriteLine("Dosya Transfer Edildi.");
            udp.Close();//bağlantı kapatılıyor
        }
        //Aktif olan ip adresini alıyor
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "Not Found!";
        }
    }
}
