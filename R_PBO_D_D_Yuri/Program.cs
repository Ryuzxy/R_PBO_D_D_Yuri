using System;
using System.Collections.Generic;

namespace Bank
{
    class Nasabah
    {
        public string NomorRekening { get; set; }
        public string Nama { get; set; }
        public decimal Saldo { get; private set; }

        public Nasabah(string nomorRekening, string nama, decimal saldoAwal)
        {
            NomorRekening = nomorRekening;
            Nama = nama;
            Saldo = saldoAwal;
        }

        public void Setor_Tunai(decimal jumlah)
        {
            if (jumlah <= 0)
            {
                Console.WriteLine("Minim Setor Lebih BesarDari 0 Boss.");
                return;
            }

            Saldo += jumlah;
            Console.WriteLine($"Setoran sebesar {jumlah:C} berhasil. Saldo sekarang: {Saldo:C}");
        }

        public void Tarik_Tunai(decimal jumlah)
        {
            if (jumlah <= 0)
            {
                Console.WriteLine("Minim Tarik Lebih Besar Dari 0 Boss.");
                return;
            }

            if (Saldo >= jumlah)
            {
                Saldo -= jumlah;
                Console.WriteLine($"Penarikan sebesar {jumlah:C} berhasil. Sisa saldo: {Saldo:C}");
            }
            else
            {
                Console.WriteLine("Saldo tidak mencukupi untuk penarikan.");
            }
        }

        public bool Transfer(Nasabah penerima, decimal jumlah)
        {
            if (jumlah <= 0)
            {
                Console.WriteLine("Minim Transfer Lebih Besar Dari 0 Boss.");
                return false;
            }

            if (Saldo >= jumlah)
            {
                Saldo -= jumlah;
                penerima.Saldo += jumlah;
                Console.WriteLine($"Transfer {jumlah:C} ke {penerima.Nama} ({penerima.NomorRekening}) berhasil.");
                return true;
            }
            else
            {
                Console.WriteLine("Saldo tidak mencukupi untuk transfer.");
                return false;
            }
        }

        public void SHOW()
        {
            Console.WriteLine("\n=== Info Rekening Nasabah ===");
            Console.WriteLine($"Nomor Rekening : {NomorRekening}");
            Console.WriteLine($"Nama Pemilik   : {Nama}");
            Console.WriteLine($"Saldo Rekening : {Saldo:C}");
        }
    }

    class Program
    {
        static Dictionary<string, Nasabah> daftarNasabah = new Dictionary<string, Nasabah>();

        static void Main(string[] args)
        {
            daftarNasabah["1"] = new Nasabah("1", "Andi", 5000000);
            daftarNasabah["2"] = new Nasabah("2", "Budi", 2500000);
            daftarNasabah["3"] = new Nasabah("3", "Citra", 8000000);

            Console.WriteLine("=== Selamat Datang di Bank Pelita ===");

            while (true)
            {
                Console.Write("\nMasukkan Nomor Rekening Anda: ");
                string nomorRek = Console.ReadLine();

                if (daftarNasabah.ContainsKey(nomorRek))
                {
                    Nasabah nasabah = daftarNasabah[nomorRek];
                    MenuNasabah(nasabah);
                }
                else
                {
                    Console.WriteLine("Nomor rekening tidak ditemukan.");
                }
            }
        }

        static void Menu_Nasabah(Nasabah nasabah)
        {
            while (true)
            {
                Console.WriteLine("\nPilih Menu:");
                Console.WriteLine("1. Tampilkan Info Rekening");
                Console.WriteLine("2. Setor Tunai");
                Console.WriteLine("3. Tarik Tunai");
                Console.WriteLine("4. Transfer ke Rekening Lain");
                Console.WriteLine("5. Keluar");

                Console.Write("Pilihan Anda: ");
                string pilihan = Console.ReadLine();

                switch (pilihan)
                {
                    case "1":
                        nasabah.SHOW();
                        break;
                    case "2":
                        Console.Write("Masukkan Berapa duit: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal setor))
                        {
                            nasabah.Setor_Tunai(setor);
                        }
                        else
                        {
                            Console.WriteLine("Input tidak valid.");
                        }
                        break;
                    case "3":
                        Console.Write("Lu mau ambil berapa wok: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal tarik))
                        {
                            nasabah.Tarik_Tunai(tarik);
                        }
                        else
                        {
                            Console.WriteLine("Ngelebokno opo le.");
                        }
                        break;
                    case "4":
                        Console.Write("Info Norek: ");
                        string rekTujuan = Console.ReadLine();
                        if (daftarNasabah.ContainsKey(rekTujuan) && rekTujuan != nasabah.NomorRekening)
                        {
                            Console.Write("Pe Depo piro: ");
                            if (decimal.TryParse(Console.ReadLine(), out decimal transfer))
                            {
                                nasabah.Transfer(daftarNasabah[rekTujuan], transfer);
                            }
                            else
                                Console.WriteLine("Kliru Le.");
                        }
                }
                        else
                {
                    Console.WriteLine("Gagal Depo LE.");
                }
                break;
                    case "5":
                    Console.WriteLine("THX.");
                    return;
                default:
                    Console.WriteLine("Pilihan tidak tersedia.");
                    break;
                }
            }
        }
    }
}
