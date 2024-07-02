// See https://aka.ms/new-console-template for more information

using System.ComponentModel.Design;

public class Program
{
    static List<(int norek, double saldo, int pin)> nasabahList = new List<(int, double, int)>
        {
            (123456,50000.0,121212),
            (154786,100000.0,154236),
            (852467,150000.0,151515),
            (472583,50000.0,161616),
            (516141,50000.0,181818)
        };

  
    static List<(int norek,DateTime tanggal, double debit, double kredit, string status, string keterangan)> historyList = new List<(int,DateTime, double, double, string, string)>();

    public static void Main(string[] args)
    {
        
        Console.WriteLine("Selamat Datang Di ATM Sederhana");
        Question();
    }

    public static void Question()
    {
        bool isValid = true;
        while (isValid)
        {
            Console.WriteLine("Masukan Nomor Kartu:");
            String norekInput = Console.ReadLine();
            try
            {
                int norek = Int32.Parse(norekInput);
                bool found = false;

                foreach (var nasabah in nasabahList)
                {
                    if (nasabah.norek == norek)
                    {
                        found = true;
                        QuestionPin(norek);
                    }
                }
                if (!found)
                {
                    Console.WriteLine("No Kartu Tidak Tersedia");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Input tidak valid. Silahkan masukkan noomor yang benar.");
            }


        }

    }

    public static void QuestionPin(int norek)
    {
        bool isValid = true;
        while (isValid)
        {
            Console.WriteLine("Masukan Pin Anda:");
            String pinInput = Console.ReadLine();
            int pin = Int32.Parse(pinInput);

            foreach(var nasabah in nasabahList)
            {
                if (nasabah.norek == norek)
                {
                    if(nasabah.pin == pin)
                    {
                        ShowMenu(nasabah.norek);
                    }
                    else
                    {
                        Console.WriteLine("PIN salah");
                    }
                    
                }

            }
        }

    }

    public static void ShowMenu(int norek)
    {
        Console.WriteLine("");
        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        Console.WriteLine("");
        Console.WriteLine("MENU:");
        Console.WriteLine("1.Informasi Saldo \n" +
            "2.Tarik Uang \n" +
            "3.Setor Uang \n" +
            "4.Transfer \n" +
            "5.Ganti Pin \n" +
            "6.History \n" +
            "7.Menu Utama \n" +
            "8.Exit \n" +
            "Pilih menu (1-8)");
        String pilihInput = Console.ReadLine();

        int pilih = Int32.Parse(pilihInput);

        try
        {
            if(pilih > 0 && pilih <= 8)
            {
                switch (pilih)
                {
                    case 1:
                        InfoSaldo(norek);
                        break;
                    case 2:
                        TarikUang(norek);
                        break;
                    case 3:
                        SetorUang(norek);
                        break;
                    case 4:
                        Transfer(norek);
                        break;
                    case 5:
                        GantiPin(norek);
                        break;
                    case 6:
                        History(norek);
                        break;
                    case 7:
                        Question();
                        break;
                    case 8:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Pilihan Tidak tersedia, Silahkan pilih lagi:");
                        ShowMenu(norek);
                        break;

                }
            }
            else {
                Console.WriteLine("Input harus antara 1-8, silahkan coba lagi!");
            }


        }
        catch (Exception ex) { 
            Console.WriteLine("Input harus antara 1-8, silahkan coba lagi!");
            ShowMenu(norek);
        }

    }

    public static void InfoSaldo(int norek)
    {
        foreach (var nasabah in nasabahList)
        {
            if (norek == nasabah.norek)
            {
                Console.WriteLine($"Saldo Anda : Rp. {nasabah.saldo}");
                ShowMenu(norek);
            }
        }
    }

    public static void TarikUang(int norek)
    {
        Console.WriteLine("Masukkan jumlah uang yang akan ditarik:");
        string jumlahInput = Console.ReadLine();
        double jumlah = double.Parse(jumlahInput);

        for (int i = 0; i < nasabahList.Count; i++)
        {
            if (norek == nasabahList[i].norek)
            {
                if (jumlah <= nasabahList[i].saldo)
                {
                    nasabahList[i] = (nasabahList[i].norek, nasabahList[i].saldo - jumlah, nasabahList[i].pin);
                    historyList.Add((nasabahList[i].norek, DateTime.Now, 0, jumlah, "Sukses", "Tarik Uang"));
                    Console.WriteLine("Sukses Tarik");
                }
                else
                {
                    historyList.Add((nasabahList[i].norek, DateTime.Now, 0, jumlah, "Gagal", "Tarik Uang"));
                    Console.WriteLine("Maaf Saldo Anda Tidak Cukup");
                }
                ShowMenu(norek);
                
            }
        }
    }
    public static void SetorUang(int norek)
    {
        Console.WriteLine("Masukkan jumlah uang yang akan disetor:");
        string jumlahInput = Console.ReadLine();
        double jumlah = double.Parse(jumlahInput);

        for (int i = 0; i < nasabahList.Count; i++)
        {
            if (norek == nasabahList[i].norek)
            {
                if (jumlah <= nasabahList[i].saldo)
                {
                    nasabahList[i] = (nasabahList[i].norek, nasabahList[i].saldo + jumlah, nasabahList[i].pin);
                    historyList.Add((nasabahList[i].norek, DateTime.Now, jumlah, 0, "Sukses", "Setor Uang"));
                    Console.WriteLine("Sukses ");
                }
                else
                {
                    historyList.Add((nasabahList[i].norek, DateTime.Now, jumlah, 0, "Gagal", "Setor Uang"));
                    Console.WriteLine("Maaf Gagal Setor Uang");
                }
                ShowMenu(norek);
                
            }
        }

    }

    public static void Transfer(int norek)
    {
        bool isValid = true;

        while (isValid)
        {
            Console.WriteLine("Masuk No Kartu Penerima:");
            String norekPenerimaInput = Console.ReadLine();
            int norekPenerima = Int32.Parse(norekPenerimaInput);

            bool found = false;

            if(norek != norekPenerima)
            {

                foreach (var nasabah in nasabahList)
                {

                    if (nasabah.norek == norekPenerima)
                    {
                        SaveTransfer(norek, norekPenerima);
                        isValid = false;
                        found = true;
                    }

                }

                if (!found)
                {
                    Console.WriteLine("Nomor Kartu Penerima Tidak Tersedia");

                }
            }
            else
            {
                Console.WriteLine("Tidak bisa transfer ke nomor sendiri");
            }
            

        }

    }

    public static void SaveTransfer(int norekPengirim, int norekPenerima)
    {
        int jumlah = QuestionJumlahTf(norekPengirim);

        //loop untuk pengirim
        for (int i = 0; i < nasabahList.Count; i++)
        {
            if (norekPengirim == nasabahList[i].norek)
            {
                
                nasabahList[i] = (nasabahList[i].norek, nasabahList[i].saldo - jumlah, nasabahList[i].pin);
                historyList.Add((nasabahList[i].norek, DateTime.Now, 0, jumlah, "Sukses", $"Transfer ke {norekPenerima}"));
                Console.WriteLine($"Berhasil Transfer ke {norekPenerima} sebesar Rp. {jumlah}");
            }

            if (norekPenerima == nasabahList[i].norek)
            {
                nasabahList[i] = (nasabahList[i].norek, nasabahList[i].saldo + jumlah, nasabahList[i].pin);
                historyList.Add((nasabahList[i].norek, DateTime.Now, jumlah, 0, "Sukses", $"Transfer dari {norekPengirim}"));
            }
        }
        if (jumlah == 0)
        {
            historyList.Add((norekPengirim, DateTime.Now, 0, jumlah, "Gagal", $"Transfer ke {norekPenerima}"));
            historyList.Add((norekPenerima, DateTime.Now, jumlah, 0, "Gagal", $"Transfer dari {norekPengirim}"));
            foreach(var nasabah in nasabahList)
            {
                if (nasabah.norek == norekPengirim) {
                    Console.WriteLine($"Maaf saldo anda tidak mencukupi. Saldo anda Rp. {nasabah.saldo}");

                }

            }
        }


        ShowMenu(norekPengirim);
    }

    public static int QuestionJumlahTf(int norek)
    {
        Console.WriteLine("Masukkan jumlah uang yang akan ditransfer:");
        string jumlahInput = Console.ReadLine();    
        int jumlah = Int32.Parse(jumlahInput);  


        foreach(var nasabah in nasabahList)
        {
            if( nasabah.saldo > jumlah)
            {
                return jumlah;
            }
            else
            {
                return jumlah = 0;
                ShowMenu(norek);
            }
        }
        return jumlah;
    }


    public static void GantiPin(int norek)
    {
      
        bool isValid = true;

        while (isValid)
        {
            Console.WriteLine("Masukan pin baru:");
            String pinInput = Console.ReadLine();
            int pin = Int32.Parse(pinInput);


            for (int i = 0; i < nasabahList.Count; i++)
            {
                if (norek == nasabahList[i].norek)
                { 
                    if(nasabahList[i].pin != pin)
                    {
                        nasabahList[i] = (nasabahList[i].norek, nasabahList[i].saldo, pin);
                        Console.WriteLine("PIN berhasil diubah.");
                        isValid = false;
                        ShowMenu(norek);
                    }
                    else
                    {
                        Console.WriteLine("PIN baru tidak boleh sama dengan PIN lama");

                    }
                }

            }

        }

    }

    public static void History(int norek)
    {
        foreach(var history in historyList)
        {
            foreach(var nasabah in nasabahList)
            {
                if (norek == history.norek)
                {
                    if(nasabah.norek == norek)
                    {
                        Console.WriteLine($"Tanggal: {history.tanggal} :: Debit:Rp. {history.debit} :: Kredit:Rp. {history.kredit} :: Saldo:Rp. {nasabah.saldo} :: Status: {history.status} :: Keterangan: {history.keterangan}");
                    }
                }
            }
            
        }

        ShowMenu(norek);
    }

    
    
    

    

    
}
