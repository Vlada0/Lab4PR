using System;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace TcpSendFiles
{
    // Класс для передачи десериализированного контейнера при 
    // возникновении события получения сетевых данных.
    class ReceiveEventArgs : EventArgs
    {
        private SendInfo send;

        public ReceiveEventArgs(SendInfo sendinfo)
        {
            send = sendinfo;
        }

        public SendInfo sendInfo {get { return send; }}
    }

    static class Parametrs
    {
        public const int maxbuffer = 1048576;
        public const int length_header = 9; // установленный размер главного заголовка
    }

    // Класс способный выступать в роли сервера или клиента в TCP соединении.
    // Отправляет и получает по сети файлы и текстовые сообщения.
    class TcpModule
    {

        // Типы делегатов для обработки событий в паре с соответствующим объектом события.

        // Обработчики события принятия клиентов прослушивающим сокетом
        public delegate void AcceptEventHandler(object sender);
        public event AcceptEventHandler Accept; 
        
        // Обработчики события подключения клиента к серверу
        public delegate void ConnectedEventHandler(object sender, string result);
        public event ConnectedEventHandler Connected;

        // Обработчики события отключения конечных точек (клиентов или сервера)
        public delegate void DisconnectedEventHandler(object sender, string result);
        public event DisconnectedEventHandler Disconnected;

        // Обработчики события извлечения данных 
        public delegate void ReceiveEventHandler(object sender, ReceiveEventArgs e);
        public event ReceiveEventHandler Receive;


        // Родительская форма необходима для визуальной информации 
        // о внутреннем состоянии и событиях работы сетвого модуля.
        public Form1 Parent;

        // Прослушивающий сокет для работы модуля в режиме сервера TCP
        private TcpListener tcpListener;

        // Удобный контейнер для подключенного клиента.
        private TcpClientData tcpClient;

        // Возможные режимы работы TCP модуля
        public enum Mode { Medium, Server, Client};

        // Режим работы TCP модуля
        public Mode mode;



        // Запускает сервер, прослушивающий все IP адреса, и одновременно
        // метод асинхронного принятия клиентов.
        public void StartServer()
        {
            if (mode == Mode.Medium)
            {
                try
                {
                    tcpListener = new TcpListener(IPAddress.Any, 8001);
                    tcpListener.Start();
                    tcpListener.BeginAcceptTcpClient(AcceptCallback, tcpListener);
                    mode = Mode.Server;
                    MessageBox.Show("Успешный запуск сервера", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception e)
                {
                    tcpListener = null;
                    Parent.ShowReceiveMessage(e.Message);
                }
            }
            else
            {
                Parent.ShowReceiveMessage("Сервер уже запущен"); 
            }
        }

        //Остановка сервера
        public void StopServer()
        {
            if (mode == Mode.Server)
            {
                mode = Mode.Medium;
                tcpListener.Stop();
                tcpListener = null;


                DeleteClient(tcpClient);
            }
        }


        // Асинхронное подключение клиента к серверу
        public void ConnectClient(string ipserver)
        {
            if (mode == Mode.Medium)
            {
                tcpClient = new TcpClientData();
                tcpClient.tcpClient.BeginConnect(IPAddress.Parse(ipserver), 8001, new AsyncCallback(ConnectCallback), tcpClient);

                mode = Mode.Client;
            }
            else
            {
                if (mode == Mode.Server)
                {
                    MessageBox.Show("Это окно сервера", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }



        // Отключение клиента от сервера
        public void DisconnectClient()
        {
            if (mode == Mode.Client)
            {
                mode = Mode.Medium;
                DeleteClient(tcpClient);
            }
        }


        // Завершение работы подключенного клиента
        private void DeleteClient(TcpClientData mtc)
        {
            if (mtc != null && mtc.tcpClient.Connected == true)
            {
                mtc.tcpClient.GetStream().Close(); // закрываем поток отдельно у клиента
                mtc.tcpClient.Close(); // затем закрываем самого клиента
            }
        }
 

        // Метод упрощенного создания заголовка с информацией о размере данных отправляемых по сети.
        // length - длина данных подготовленных для отправки
        // возращает байтовый массив заголовка
        private byte[] GetHeader(int length)
        {
            string header = length.ToString();
            if (header.Length < 9)
            {
                string zeros = null;
                for (int i = 0; i < (9 - header.Length); i++)
                {
                    zeros += "0";
                }
                header = zeros + header;
            }

            byte[] byteheader = Encoding.Default.GetBytes(header);


            return byteheader;
        }



        public string SendFileName = null;
        public void SendData()
        {

            SendInfo si = new SendInfo();
            si.message = Parent.textBoxSend.Text;


            //  Если нет сообщения и отсылаемого файла продолжать процедуру отправки нет смысла.
            if (String.IsNullOrEmpty(si.message) == true && String.IsNullOrEmpty(SendFileName) == true) return;

            if (SendFileName != null)
            {
                FileInfo fi = new FileInfo(SendFileName);
                if (fi.Exists == true)
                {
                    si.filesize = (int)fi.Length;
                    si.filename = fi.Name;
                }
                fi = null;
            }

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, si);
            ms.Position = 0;
            byte[] infobuffer = new byte[ms.Length];
            int r = ms.Read(infobuffer, 0, infobuffer.Length);
            ms.Close();

            byte[] header = GetHeader(infobuffer.Length);
            byte[] total = new byte[header.Length + infobuffer.Length + si.filesize];
            string str = Encoding.ASCII.GetString(infobuffer);

            Buffer.BlockCopy(header, 0, total, 0, header.Length);
            Buffer.BlockCopy(infobuffer, 0, total, header.Length, infobuffer.Length);

            // Если путь файла указан, добавим его содержимое в отправляемый массив байтов
            if (si.filesize > 0)
            {
                FileStream fs = new FileStream(SendFileName, FileMode.Open, FileAccess.Read);
                fs.Read(total, header.Length + infobuffer.Length, si.filesize);
                fs.Close();
                fs = null;
            }

            // Отправим данные подключенным клиентам
            NetworkStream ns = tcpClient.tcpClient.GetStream();
            // Так как данный метод вызывается в отдельном потоке рациональней использовать синхронный метод отправки
            ns.Write(total, 0, total.Length);

            // Обнулим все ссылки на многобайтные объекты и очистим память
            header = null;
            infobuffer = null;
            total = null;
            SendFileName = null;
            Parent.labelFileName.Text = "";
            GC.Collect();
            GC.WaitForPendingFinalizers();

            // Подтверждение успешной отправки
            Parent.ShowReceiveMessage("Данные успешно отправлены!");
        }


        // Универсальный метод останавливающий работу сервера и закрывающий все сокеты
        // вызывается в событии закрытия родительской формы.
        public void CloseSocket()
        {
            StopServer();
            DisconnectClient();
        }



        // Метод завершения принятия клиентов
        public void AcceptCallback(IAsyncResult ar)
        {
            if (mode == Mode.Medium) return;

            TcpListener listener = (TcpListener)ar.AsyncState;
            try
            {
                tcpClient = new TcpClientData();
                tcpClient.tcpClient = listener.EndAcceptTcpClient(ar);
               

                // Запускаем асинхронный метод извлечения данных
                // для принятого TCP клиента
                NetworkStream ns = tcpClient.tcpClient.GetStream();
                tcpClient.buffer = new byte[Parametrs.length_header];
                ns.BeginRead(tcpClient.buffer, 0, tcpClient.buffer.Length, new AsyncCallback(ReadCallback), tcpClient);


                // Продолжаем ждать запросы на подключение
                listener.BeginAcceptTcpClient(AcceptCallback, listener);

                // Активация события успешного подключения клиента
                if (Accept != null)
                {
                    Accept.BeginInvoke(this, null, null);
                }
            }
            catch
            {
                // Обработка исключительных ошибок возникших при принятии клиента.
                MessageBox.Show("Ошибка при получении данных от TCP клиента", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        // Метод вызываемый при поключении клиента
        public void ConnectCallback(IAsyncResult ar)
        {
            string result = "Подключение успешно!";
            try
            {
                // Получаем подключенного клиента
                TcpClientData myTcpClient = (TcpClientData)ar.AsyncState;
                NetworkStream ns = myTcpClient.tcpClient.GetStream();
                myTcpClient.tcpClient.EndConnect(ar);

                // Запускаем асинхронный метод чтения данных для подключенного TCP клиента
                myTcpClient.buffer = new byte[Parametrs.length_header];
                ns.BeginRead(myTcpClient.buffer, 0, myTcpClient.buffer.Length, new AsyncCallback(ReadCallback), myTcpClient);

            }
            catch (Exception e)
            {
                // Обработка ошибок подключения
                DisconnectClient();
                result = "Подключение провалено!";
            }

            // Активация события успешного или неуспешного подключения к серверу,
            // здесь серверу можно отослать ознакомительные данные о себе (например, имя клиента)
            if (Connected != null)
                Connected.BeginInvoke(this, result, null, null);
        }


        // Метод асинхронно вызываемый при наличие данных в буферах приема.

        public void ReadCallback(IAsyncResult ar)
        {
            if (mode == Mode.Medium) return;

            TcpClientData myTcpClient = (TcpClientData)ar.AsyncState;

            try
            {
                NetworkStream ns = myTcpClient.tcpClient.GetStream();

                int r = ns.EndRead(ar);

                if (r > 0)
                {
                    // Из главного заголовка получим размер массива байтов информационного объекта
                    string header = Encoding.Default.GetString(myTcpClient.buffer);
                    int leninfo = int.Parse(header);

                    // Получим и десериализуем объект с подробной информацией о содержании получаемого сетевого пакета
                    MemoryStream ms = new MemoryStream(leninfo);
                    byte[] temp = new byte[leninfo];
                    r = ns.Read(temp, 0, temp.Length);
                    ms.Write(temp, 0, r);
                    BinaryFormatter bf = new BinaryFormatter();
                    ms.Position = 0;
                    SendInfo sc = (SendInfo)bf.Deserialize(ms);
                    ms.Close();

                    if (sc.filesize > 0)
                    {
                        // Создадим файл на основе полученной информации и массива байтов следующих за объектом информации
                        FileStream fs = new FileStream(sc.filename, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite, sc.filesize);
                        do
                        {
                            temp = new byte[Parametrs.maxbuffer];
                            r = ns.Read(temp, 0, temp.Length);

                            // Записываем столько байтов сколько прочтено методом Read()
                            fs.Write(temp, 0, r);
                            if (fs.Length == sc.filesize)
                            {
                                fs.Close();
                                fs = null;
                                break;
                            }
                        }
                        while (r > 0);

                        temp = null;
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                    }



                    if (Receive != null)
                        Receive(this, new ReceiveEventArgs(sc));

                    myTcpClient.buffer = new byte[Parametrs.length_header];
                    ns.BeginRead(myTcpClient.buffer, 0, myTcpClient.buffer.Length, new AsyncCallback(ReadCallback), myTcpClient);

                }
                else
                {
                    DeleteClient(myTcpClient);

                    // Событие клиент отключился
                    if (Disconnected != null)
                        Disconnected.BeginInvoke(this, "Клиент отключился!", null, null);
                }
            }
            catch (Exception e)
            {

                DeleteClient(myTcpClient);


                // Событие клиент отключился
                if (Disconnected != null)
                    Disconnected.BeginInvoke(this, "Клиент отключился аварийно!", null, null);


            }

        }

    }


    class TcpClientData
    {
        public TcpClient tcpClient = new TcpClient();

        // Буфер для чтения и записи данных сетевого потока
        public byte[] buffer = null;

        public TcpClientData()
        {
            tcpClient.ReceiveBufferSize = Parametrs.maxbuffer;
        }
    }


    // Класс для отправки текстового сообщения и 
    // информации о пересылаемых байтах следующих последними в потоке сетевых данных.
    [Serializable]
    class SendInfo
    {
        public string message;
        public string filename;
        public int filesize;
    }


}
