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
       public String name;
       public int size; //IN BYTES
       public String ext;
       public List<String> content;
       private int month;
       private int day;
       private int year;

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
