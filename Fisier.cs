using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
namespace Proiect;

public static class Fisier
{
    public static string UltimaSalvare = "";
    public static string CaleFisier = "istoric_Zone.json";
    
    public static void IncarcaUltimaStareDinFisier()
    {
        if (File.Exists(CaleFisier))
        {
            
            var toateLiniile = File.ReadAllLines(CaleFisier);
            if (toateLiniile.Length > 0)
            {
              
                UltimaSalvare = toateLiniile.Last();
            }
        }
    }
    public static void SalveazaStareaInFisier()
    {
        try
        {
            string caleFisier = "istoric_Zone.json";

            string ZonaProgram = JsonSerializer.Serialize(Program.Zone);
            if (UltimaSalvare != ZonaProgram)
                File.AppendAllText(caleFisier, ZonaProgram+"\n");
        }
        catch (Exception exeptie)
        {
            Console.WriteLine("Nu s a putut realiza adaugarea");
        }
    }
    
}