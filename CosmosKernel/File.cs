using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cosmos.Hardware;


namespace CosmosKernel1
{
   public class File
    {
        public String name { get; set; }
        public int size { get; set; } //IN BYTES
        public String ext { get; set; }
        public List<String> content { get; set; }
        private int month { get; set; }
        private int day { get; set; }
        private int year { get; set; }

       public File(String name, String ext)
       {
           
           this.name = name;
           this.ext = ext;
           month = RTC.Month;
           day = RTC.DayOfTheMonth;
           year = RTC.Year;
           size = 0;
           content = new List<string>();
          
           
           
       }
       public void addLine(String line)
       {
           size += line.ToCharArray().Length;
           content.Add(line);
       }

       public void printContent(int line)
       {
           string[] arr = content.ToArray<string>();
           for (int i = 0 ; i < arr.Length; i++){
               Console.WriteLine((i + 1) + ". " + arr[i]);
           }
               

       }

       public String GetDate()
       {
           return month + "/" + day + "/" + year;
       }


     

      
    }
}
