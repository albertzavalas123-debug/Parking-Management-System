using Microsoft.Extensions.Logging;
using System.Text.Json;
namespace Proiect;

class Program
{
    public static FileLog FisierLog=new FileLog();
    public static List<Zona> Zone = new List<Zona>();
    private static int Logare_Admin()
    {
        while (true)
        {
            int opt;
            Console.WriteLine("1.Selecteaza zona");
            Console.WriteLine("2.Vizualizeaza istoric");
            Console.WriteLine("3.Editeaza Reguli");
            Console.WriteLine("4.Editare tip abonament");
            Console.WriteLine("5.Stergere Log");
            Console.WriteLine("6.Back");
            if (!int.TryParse(Console.ReadLine(), out opt))
            {
                FisierLog.Eroare("Utilizatorul a introdus optiune gresita");
                Console.WriteLine("Optiune invalida");
                continue;
            }
            if (opt < 1 || opt > 6)
            {
                FisierLog.Eroare("Utilizatorul a introdus optiune gresita");
                Console.WriteLine("Optiune invalida");
                continue;
            }

            switch (opt)
            {
                case 1:
                    int indice_zona = Selectare_zone();
                    Modificare_zone(indice_zona);
                    break;
                case 2:
                    Afisare_istoric();
                    FisierLog.Info("Vizualizare istoric Admin");
                    break;
                case 3:
                    Editare_reguli();
                    break;
                case 4:
                    Editeaza_abonament();
                    break;
                case 5:
                    FisierLog.DelLog();
                    break;
                case 6: return 1;
                default: return -1;
            }
        }
        return -1;
    }

    private static int Selectare_zone()
    {
        int opt;
        int nr = 1;
        foreach (var i in Zone)
        {
            Console.WriteLine(nr + "."+ i.Locatie);
            nr += 1;
        }

        if (!int.TryParse(Console.ReadLine(), out opt))
        {
            FisierLog.Eroare("Utilizatorul a introdus optiune gresita");
            Console.WriteLine("Optiune invalida");
            return -1;
        }

        if (opt <= 0 || opt > nr)
        {
            FisierLog.Eroare("Utilizatorul a introdus optiune gresita");
            Console.WriteLine("Optiune invalida");
            return -1;
        }

        return opt - 1;

    }

    private static int Modificare_zone(int indice)
    {
        int opt;
        while (true)
        {
            Console.WriteLine("1.Stergere zona");
            Console.WriteLine("2.Edit zona");
            Console.WriteLine("3.Selectare parcare");
            Console.WriteLine("4.Back");
            if (!int.TryParse(Console.ReadLine(), out opt))
            {
                FisierLog.Eroare("Utilizatorul a introdus optiune gresita");
                Console.WriteLine("Optiune invalida");
                continue;
            }

            if (opt <= 0 || opt > 4)
            {
                FisierLog.Eroare("Utilizatorul a introdus optiune gresita");
                Console.WriteLine("Optiune invalida");
                continue;
            }

            switch (opt)
            {
                case 1:
                    Zone.RemoveAt(indice);
                    FisierLog.Info("O zona a fost stearsa");
                    return 1;
                case 2:
                    Console.WriteLine("Introduce noul nume al zonei:");
                    Zone[indice].Locatie = Console.ReadLine();
                    FisierLog.Info("O zona a fost redenumita");
                    return 1;
                case 3:
                    int indicep = Selectare_parcare(indice);
                    Modificare_parcare(indicep, indice);
                    return 1;
                case 4:
                    return 1;
            }
        }
    }

    public static int Selectare_parcare(int indice)
    {
        int opt;
        int nr = 1;
        foreach (var i in Zone[indice].Parcari)
        {
            Console.WriteLine(nr + "."+ i.IdParcare);
            nr += 1;
        }

        if (!int.TryParse(Console.ReadLine(), out opt))
        {
            FisierLog.Eroare("Utilizatorul a introdus optiune gresita");
            Console.WriteLine("Optiune invalida");
            return -1;
        }

        if (opt <= 0 || opt > nr)
        {
            FisierLog.Eroare("Utilizatorul a introdus optiune gresita");
            Console.WriteLine("Optiune invalida");
            return -1;
        }

        return opt - 1;

    }

    public static int Modificare_parcare(int indicep, int indicez)
    {
        int opt;
        while (true)
        {
            Console.WriteLine("1.Stergere parcare");
            Console.WriteLine("2.Edit parcare");
            Console.WriteLine("3.Adaugare parcare");
            Console.WriteLine("4.Back");
            if (!int.TryParse(Console.ReadLine(), out opt))
            {
                FisierLog.Eroare("Utilizatorul a introdus optiune gresita");
                Console.WriteLine("Optiune invalida");
                continue;
            }

            if (opt <= 0 || opt > 4)
            {
                FisierLog.Eroare("Utilizatorul a introdus optiune gresita");
                Console.WriteLine("Optiune invalida");
                continue;
            }

            switch (opt)
            {
                case 1:
                    Zone[indicez].Parcari.RemoveAt(indicep);
                    FisierLog.Info("O parcare a fost stearsa");
                    return 1;

                case 2:
                    Edit_parcare(indicep, indicez);
                    return 1;

                case 3:
                    Console.WriteLine("Introduce nume parcare:");
                    string IdParcare = Console.ReadLine();
                    Console.WriteLine("Numar locuri:");
                    int NrLocuri = Convert.ToInt32(Console.ReadLine());
                    Parcare p = new Parcare(IdParcare, NrLocuri);
                    Zone[indicez].Parcari.Add(p);
                    FisierLog.Info("O parcare a fost adaugata");
                    return 1;
                case 4: return 1;
            }
        }
    }

    public static int Edit_parcare(int indicep, int indicez)
    {
        int opt;
        while (true)
        {
            Console.WriteLine("1.Modificare numar de locuri");
            Console.WriteLine("2.Modificare nume");
            Console.WriteLine("3.Stergere client");
            Console.WriteLine("4.Adaugare client");
            Console.WriteLine("5.Back");
            if (!int.TryParse(Console.ReadLine(), out opt))
            {
                FisierLog.Eroare("Utilizatorul a introdus optiune gresita");
                Console.WriteLine("Optiune invalida");
                continue;
            }

            if (opt <= 0 || opt > 5)
            {
                FisierLog.Eroare("Utilizatorul a introdus optiune gresita");
                Console.WriteLine("Optiune invalida");
                continue;
            }

            switch (opt)
            {
                case 1:
                    Console.WriteLine("Introduce numarul de parcarii:");
                    int NrLocuriNou = Convert.ToInt32(Console.ReadLine());
                    Zone[indicez].Parcari[indicep].ModifcareParcare(NrLocuriNou);
                    return 1;

                case 2:
                    Console.WriteLine("Introduce numele parcarii");
                    Zone[indicez].Parcari[indicep].ModifcareParcare(Console.ReadLine());
                    return 1;
                case 3:
                    Console.WriteLine("Introduce numarul masinii:");
                    Zone[indicez].Parcari[indicep].StergereClientMasina(Console.ReadLine());
                    return 1;
                case 4:
                    string nume, prenume;
                    int CNP;
                    string nrmatricol;
                    Console.WriteLine("Introduce numele clientului:");
                    nume = Console.ReadLine();
                    Console.WriteLine("Introduce prenumele clientului:");
                    prenume = Console.ReadLine();
                    Console.WriteLine("Introduce CNP-ul clientului:");
                    CNP = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Introduce numarul masinii clientului:");
                    nrmatricol = Console.ReadLine();
                    Console.WriteLine("Selecteaza abonamentul clientului:");
                    TipAbonament tab = Selectare_abonament();
                    Console.WriteLine("Perioada abonamentului clientului:");
                    DateTime datainceput = DateTime.Parse(Console.ReadLine() +" "+ DateTime.Now.TimeOfDay.ToString());
                    DateTime datafinal = DateTime.Parse(Console.ReadLine() +" "+ DateTime.Now.TimeOfDay.ToString());
                    Persoana p = new Persoana(nume, prenume, CNP);
                    Masina m = new Masina(nrmatricol);
                    Abonament a = new Abonament(tab, datainceput, datafinal);
                    Client c = new Client(p, a, m);
                    Zone[indicez].Parcari[indicep].AdaugareClient(c);
                    return 1;
                case 5: return 1;
            }
        }
    }

    public static TipAbonament Selectare_abonament()
    {
        if (Administrator.TipuriAbonamente == null || Administrator.TipuriAbonamente.Count == 0)
        {
            Console.WriteLine("Nu exista tipuri de abonamente definite.");
            return null; 
        }

        int nr = 1;
        Console.WriteLine("\nSelecteaza tipul de abonament:");
        foreach (var i in Administrator.TipuriAbonamente)
        {
            Console.WriteLine($"{nr}. {i.ToString()}");
            nr++;
        }

        Console.Write("Optiunea ta: ");
        int opt;
    
        if (!int.TryParse(Console.ReadLine(), out opt))
        {
            Console.WriteLine("Optiune invalida.");
            return null; 
        }
    
        if (opt <= 0 || opt > Administrator.TipuriAbonamente.Count)
        {
            Console.WriteLine("Optiune invalida.");
            return null; 
        }
        
        return Administrator.TipuriAbonamente[opt - 1];
    }

    private static void Afisare_istoric()
    {
        string caleFisier = Fisier.CaleFisier; 

        if (!File.Exists(caleFisier))
        {
            Console.WriteLine("Nu exista istoric salvat.");
            return;
        }

        Console.WriteLine("\n--- ISTORIC MODIFICARI ZONE ---");
        int numarStare = 1;
        
        foreach (string linie in File.ReadLines(caleFisier))
        {
            if (string.IsNullOrWhiteSpace(linie)) continue; 

            try
            {
                List<Zona> stareVeche = JsonSerializer.Deserialize<List<Zona>>(linie);

                if (stareVeche != null)
                {
                    Console.WriteLine($"\n--- [Momentul {numarStare}] ---");
                    Console.WriteLine($"Numar Zone active: {stareVeche.Count}");
                
                    foreach (var z in stareVeche)
                    {
                        Console.Write($"Zona: {z.Locatie} | ");
                        int totalLocuri = 0;
                        foreach(var p in z.Parcari) totalLocuri += p.NrLocuri;
                        Console.WriteLine($"Locuri totale: {totalLocuri}");
                    }
                    numarStare++;
                }
            }
            catch
            {
                Console.WriteLine($" Linia {numarStare} este corupta.");
            }
        }
    
        Console.WriteLine("\nApasa orice tasta pentru a reveni...");
        Console.ReadKey();
    }

    private static int indice_reg()
    {
        if (Administrator.Reguli == null || Administrator.Reguli.Count == 0)
        {
            Console.WriteLine("Nu exista reguli inregistrate.");
            return -1; 
        }
    
        int nr = 1;
        foreach (var i in Administrator.Reguli)
        {
            
            Console.WriteLine(nr + ". " + i);
            nr++;
        }

        Console.Write("Alege numarul regulii: "); 
        int opt;
        
        if (!int.TryParse(Console.ReadLine(), out opt))
        {
            Console.WriteLine("Optiune invalida.");
            return -1;
        }
        
        if (opt <= 0 || opt > Administrator.Reguli.Count)
        {
            Console.WriteLine("Optiune invalida (numar inexistent).");
            return -1;
        }
        
        return opt - 1;
    }

  private static void Editare_reguli()
{
    int opt;
    while (true)
    {
        Console.WriteLine("1.Modificare regula");
        Console.WriteLine("2.Stergere regula");
        Console.WriteLine("3.Adaugare regula");
        Console.WriteLine("4.Vizualizare reguli");
        Console.WriteLine("5.Back");
        Console.Write("Alege optiunea:");
        
        if (!int.TryParse(Console.ReadLine(), out opt))
        {
            FisierLog.Eroare("Utilizatorul a introdus optiune gresita");
            Console.WriteLine("Optiune invalida.");
            continue;
        }
        
        if (opt <= 0 || opt > 5)
        {
            FisierLog.Eroare("Utilizatorul a introdus optiune gresita");
            Console.WriteLine("Optiune invalida.");
            continue;
        }

        switch (opt)
        {
            case 1:
                int idMod = indice_reg(); 
                if (idMod != -1) 
                {
                    Console.WriteLine("Regula actuala: " + Administrator.Reguli[idMod]);
                    Console.WriteLine("Introduce regula modificata:");
                    Administrator.ModificareRegula(Administrator.Reguli[idMod], Console.ReadLine());
                    Console.WriteLine("Regula schimbata cu succes");
                    FisierLog.Info("O regula a fost schimbata");
                }
                break;

            case 2: 
                int idSterg = indice_reg();
                if (idSterg != -1)
                {
                    Administrator.StergeRegula(Administrator.Reguli[idSterg]);
                    Console.WriteLine("Regula stearsa!");
                    FisierLog.Info("O regula a fost stearsa");
                }
                break;

            case 3:
                Console.WriteLine("Adauga regula noua:");
                Administrator.CreareRegula(Console.ReadLine());
                Console.WriteLine("Regula adaugata!");
                FisierLog.Info("Regula adaugata");
                break;

            case 4:
                if (Administrator.Reguli == null || Administrator.Reguli.Count == 0)
                {
                    Console.WriteLine("Nu exista reguli in lista.");
                }
                else
                {
                    Console.WriteLine("\nLista de reguli:");
                    int nr = 1;
                    foreach (var regula in Administrator.Reguli)
                    {
                        Console.WriteLine($"{nr}.{regula}");
                        nr++;
                    }
                }
          
                Console.WriteLine("Apasa orice tasta pentru a continua...");
                Console.ReadKey(); 
                break;

            case 5:
                return; 
        }
    }
}

    private static int Editeaza_abonament()
    {
        int opt;
        while (true)
        {
            Console.WriteLine("1.Stergere abonament");
            Console.WriteLine("2.Edit abonament");
            Console.WriteLine("3.Adaugare abonament");
            Console.WriteLine("4.Vizualizare abonamente");
            Console.WriteLine("5.Back");
            if (!int.TryParse(Console.ReadLine(), out opt))
            {
                FisierLog.Eroare("Utilizatorul a introdus optiune gresita");
                Console.WriteLine("Optiune invalida");
                continue;
            }

            if (opt <= 0 || opt > 5)
            {
                FisierLog.Eroare("Utilizatorul a introdus optiune gresita");
                Console.WriteLine("Optiune invalida");
                continue;
            }

            switch (opt)
            {
                case 1: Administrator.StergereTipAbonament(Selectare_abonament());
                    return 1;
                case 2:
                    TipAbonament ab=Selectare_abonament();
                    if (ab == null)
                    {
                        break;
                    }
                    Console.WriteLine("Introduce numele abonamentului:");
                    string nume = Console.ReadLine();
                    Console.WriteLine("Introduce pretul abonamentului:");
                    int pret = Int32.Parse(Console.ReadLine());
                    ab.NumeAbonament = nume;
                    ab.Pret = pret;
                    return 1;
                case 3: Console.WriteLine("Introduce numele abonamentului:");
                    string nume2 = Console.ReadLine();
                    Console.WriteLine("Introduce pretul abonamentului:");
                    int pret2 = Int32.Parse(Console.ReadLine()); 
                    Administrator.CreareTipAbonament(nume2, pret2);
                    return 1;
                case 4: 
                    Administrator.VizualizareTipAbonamente();
                    return 1;
                case 5: return 1;
            }
        }
    }
    
     private static Client EditeazaAbonamentClient(Client c)
    {
        int opt;
        int indiceP=-1, indiceZ=-1;
        for (int i = 0; i < Zone.Count; i++)
        {
            int aux = Zone[i].CautareClient(c);
            if (aux != -1)
            {
                indiceP = aux;
                indiceZ = i;
            }
        }

        if (indiceP == -1)
        {
            FisierLog.Eroare("Client inexistent");
            Console.WriteLine("Nu exista clientul");
            return c;
        }
        while (true)
        {
            Console.WriteLine("1.Stergere abonament");
            Console.WriteLine("2.Schimba clasa abonament");
            Console.WriteLine("3.Schimba masina");
            Console.WriteLine("4.Reinoieste abonamentul");
            Console.WriteLine("5.Back");
            if (!int.TryParse(Console.ReadLine(), out opt))
            {
                FisierLog.Eroare("Utilizatorul a introdus optiune gresita");
                Console.WriteLine("Optiune invalida");
                continue;
            }

            if (opt <= 0 || opt > 5)
            {
                FisierLog.Eroare("Utilizatorul a introdus optiune gresita");
                Console.WriteLine("Optiune invalida");
                continue;
            }

            switch (opt)
            {
                case 1: 
                    Zone[indiceZ].Parcari[indiceP].LocuriOcupate.Remove(c);
                    c.AnuleazaAbonament();
                    return c;
                case 2:
                    Console.WriteLine("Selecteaza abonamentul nou:");
                    TipAbonament tab = Selectare_abonament();
                    c.SchimbaTipAbonament(tab);
                    return c;
                case 3: 
                    Console.WriteLine("Introduce numarul masinii noi:");
                    string nrmatricol = Console.ReadLine();
                    foreach (var zone in Zone)
                    {
                        foreach (Parcare parcare in zone.Parcari)
                        {
                            if (parcare.NrMatricolExist(nrmatricol))
                            {
                                Console.WriteLine("Numarul de inmatriculare deja exista!");
                                break;
                            }
                        }
                    }
                    c.SchimbaMasina(new Masina(nrmatricol));
                    return c;
                case 4: 
                    Console.WriteLine("Format:dd.MM.yyyy");
                    Console.WriteLine("Perioada abonamentului nou:");
                    DateTime datainceput = DateTime.Parse(Console.ReadLine() +" "+ DateTime.Now.TimeOfDay.ToString());
                    DateTime datafinal = DateTime.Parse(Console.ReadLine() +" "+ DateTime.Now.TimeOfDay.ToString());
                    c.ReinnoiesteAbonament(datainceput,datafinal);
                    return c;
                case 5: return c;
            }
        }
    }
    
    public static int LogareClient()
    {
        Client client = null;
        Console.WriteLine("Introduce numarul masinii:");
        string nrMatricol=Console.ReadLine();
        foreach (var zone in Zone)
        {
            foreach (var parcare in zone.Parcari)
            {
                foreach (var clientf in parcare.LocuriOcupate)
                {
                    if (clientf.MasinaClient.NrInmatriculare == nrMatricol)
                    {
                        client=clientf;
                    }
                }
            }
        }
        if (client == null)
        {
            Console.WriteLine("Nu exista clientul");
            FisierLog.Warn("Client inexistent");
            return -1;
        }
        while (true)
        {
            int opt;
            Console.WriteLine("1.Editare abonament");
            Console.WriteLine("2.Vizualizare abonament");
            Console.WriteLine("3.Vizualizare reguli");
            Console.WriteLine("4.Back");
            if (!int.TryParse(Console.ReadLine(), out opt))
            {
                FisierLog.Eroare("Utilizatorul a introdus optiune gresita");
                Console.WriteLine("Optiune invalida");
                continue;
            }
            if (opt < 1 || opt > 4)
            {
                FisierLog.Eroare("Utilizatorul a introdus optiune gresita");
                Console.WriteLine("Optiune invalida");
                continue;
            }

            switch (opt)
            {
                case 1:
                    client=EditeazaAbonamentClient(client);
                    break;
                case 2:
                    client.VizualizeazaAbonament();
                    break;
                case 3:
                    client.VizReguli(Administrator.Reguli);
                    break;
                case 4:
                    return 1;
                default: continue;
            }
        }
        return -1;
    }

    private static Client CreareClient()
    {
        string nume, prenume,nrMatricol;
        int cnp;
        Console.WriteLine("Introduce numele:");
        nume = Console.ReadLine();
        Console.WriteLine("Introduce prenume:");
        prenume = Console.ReadLine();
        Console.WriteLine("Introduce cnp");
        cnp = int.Parse(Console.ReadLine());
        Persoana p=new Persoana(nume,prenume,cnp);
        Console.WriteLine("Introduce nrMatricol");
        nrMatricol = Console.ReadLine();
        foreach (var zone in Zone)
        {
            foreach (var parcare in zone.Parcari)
            {
                if (parcare.NrMatricolExist(nrMatricol))
                {
                    Console.WriteLine("Numarul de inmatriculare deja exista!");
                    FisierLog.Eroare("Numar de inmatriculare introdus gresit");
                    return null;
                }
            }
        }
        Masina masina = new Masina(nrMatricol);
        TipAbonament Ab = Selectare_abonament();
        if (Ab == null)
        {
            return null;
        }
        Console.WriteLine("Format:dd.MM.yyyy");
        Console.WriteLine("Perioada abonamentului nou:");
        DateTime datainceput = DateTime.Parse(Console.ReadLine() +" "+ DateTime.Now.TimeOfDay.ToString());
        DateTime datafinal = DateTime.Parse(Console.ReadLine() +" "+ DateTime.Now.TimeOfDay.ToString());
        Abonament A=new Abonament(Ab,datainceput,datafinal);
        return new Client(p, A, masina);
    }
    static void Main(string[] args)
    { 
        int alege,cod=1;
        Administrator.IncarcareTipuri();
        Fisier.IncarcaUltimaStareDinFisier();
        Administrator.IncarcareReguliDinFisier();
        Zone = JsonSerializer.Deserialize<List<Zona>>(Fisier.UltimaSalvare);
        while (cod==1)
        {
            Console.WriteLine("1.Logare Admin");
            Console.WriteLine("2.Logare Client");
            Console.WriteLine("3.Creare Cont Client");
            Console.WriteLine("4.Exit");

            if (!int.TryParse(Console.ReadLine(), out alege))
            {
                FisierLog.Eroare("Utilizatorul a introdus optiune gresita");
                Console.WriteLine("Optiune invalida");
                continue;
            }
            if (alege < 1 || alege > 4)
            {
                FisierLog.Eroare("Utilizatorul a introdus optiune gresita");
                Console.WriteLine("Optiune invalida");
                continue;
            }
            switch (alege)
            {
                case 1:
                    FisierLog.Info("Logare Admin");
                    Logare_Admin();
                    break;
                case 2:
                    FisierLog.Info("Logare Client");
                    LogareClient();
                    break;
                case 3:
                    Client c=CreareClient();
                    int indiceZ = Selectare_zone();
                    int indiceP=Selectare_parcare(indiceZ);
                    Zone[indiceZ].Parcari[indiceP].LocuriOcupate.Add(c);
                    FisierLog.Info("Client nou adaugat");
                    break;
                case 4:
                    cod = 0;
                    break;
                default: break;
            }
        }
        Fisier.SalveazaStareaInFisier();
        Administrator.SalvareReguliInFisier();
        Administrator.SalvareTipuri();
    }
}