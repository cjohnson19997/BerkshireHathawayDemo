using System;
using System.Collections.Generic;
using System.IO;

namespace BerkshireHathawayIO
{
    public class BerkshireFileReader
    {
        private string location;

        public BerkshireFileReader(string fileLocation)
        {
            location = fileLocation;
        }
        
        public List<string> Read()
        {
            List<string> lines = new List<string>();
            string line;
            try
            {
                if (!String.IsNullOrEmpty(location))
                {
                    StreamReader reader = new StreamReader(location);
                    line = reader.ReadLine();
                    while (line != null)
                    {                        
                        lines.Add(line);
                        line = reader.ReadLine();
                    }
                    reader.Close();                                      
                }                
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            return lines;

        }        
    }
}
