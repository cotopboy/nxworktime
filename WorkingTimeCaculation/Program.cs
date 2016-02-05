using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Utilities.DataTypes.ExtensionMethods;
using Utilities.IO.ExtensionMethods;
using Utilities.Reflection.ExtensionMethods;
using Utilities.Math.ExtensionMethods;
using System.IO;
using Utilities.IO;


namespace WorkingTimeCaculation
{
    class Program
    {
        static void Main(string[] args)
        {          
            

            new MainProcessor().StartProcess();

            Console.WriteLine("Press any key to end");

            Console.ReadKey();
        }
    }
}


/*
 
 * 
 * 
 */