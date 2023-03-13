using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haromszogek
{
	class Program
	{
		class DHaromszog
		{
			double aOldal, bOldal, cOldal;
			int sorSzama = 1;
			public DHaromszog(string sor, int sorSzama)
			{

				string[] sorE = sor.Split(' ');
				AOldal = double.Parse(sorE[0]);
				BOldal = double.Parse(sorE[1]);
				COldal = double.Parse(sorE[2]);
				this.sorSzama = sorSzama;
				if (AOldal <= 0)
					throw new Exception(sorSzama + " sor: A(z) a oldal nem lehet nulla vagy negatív!");
				if (BOldal <= 0)
					throw new Exception(sorSzama + " sor: A(z) b oldal nem lehet nulla vagy negatív!");
				if (COldal <= 0)
					throw new Exception(sorSzama + " sor: A(z) c oldal nem lehet nulla vagy negatív!");
				if (!EllNovekvoSorrend)
					throw new Exception(sorSzama + " sor: A(z) adatok nincsenek növekvő sorrendben!");
				if (!EllMegszerkesztheto)
					throw new Exception(sorSzama + " sor: A háromszöget nem lehet megszerkeszteni!");
				if (!EllDerekszogu)
					throw new Exception(sorSzama + " sor: A háromszög nem derékszögű!");
			}

			public double a
			{
				get
				{
					if (aOldal > 0)
					{
						return aOldal;
					}
					else
						throw new Exception("A(z) a oldal nem lehet nulla vagy negatív!");
				}
				set { aOldal = value; }
			}

			public double b
			{
				get
				{
					if (bOldal > 0)
					{
						return bOldal;
					}
					else
						throw new Exception("A(z) b oldal nem lehet nulla vagy negatív!");

				}
				set { bOldal = value; }
			}

			public double c
			{
				get
				{
					if (cOldal > 0)
					{
						return cOldal;
					}
					else
						throw new Exception("A(z) c oldal nem lehet nulla vagy negatív!");
				}
				set { cOldal = value; }
			}


			private bool EllDerekszogu
			{
				get
				{
					if (Math.Pow(aOldal, 2) + Math.Pow(bOldal, 2) == Math.Pow(cOldal, 2))
					{
						return true;
					}
						return false;
				}
			}
			private bool EllMegszerkesztheto
			{
				get
				{
					if (aOldal + bOldal > cOldal)
					{
						return true;
					}
					return false;

				}
			}
			private bool EllNovekvoSorrend
			{
				get
				{
					if (aOldal <= bOldal && aOldal <= cOldal && bOldal <= cOldal)
					{
						return true;
					}
					return false;
				}
			}
			public int SorSzama { get => sorSzama; set => sorSzama = value; }
			public double AOldal { get => aOldal; set => aOldal = value; }
			public double BOldal { get => bOldal; set => bOldal = value; }
			public double COldal { get => cOldal; set => cOldal = value; }
			public double Kerulet { get => aOldal + bOldal + cOldal; }
			public double Terulet { get => aOldal * bOldal / 2; }
		}

		static List<DHaromszog> lista = new List<DHaromszog>();
		static void FajlBeolvasas()
		{
			StreamReader sr = new StreamReader("haromszogek.txt");
			int index = 1;
			Console.WriteLine("Hibák a kiválasztott állományban:");
			while (!sr.EndOfStream)
			{
				string besor = sr.ReadLine();
				try
				{
					DHaromszog d = new DHaromszog(besor, index);
					lista.Add(d);
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
				index++;
			}
			sr.Close();
		}

		static void Main(string[] args)
		{
			FajlBeolvasas();
			Console.WriteLine();
			Console.WriteLine("Derékszögű háromszögek:");
			foreach (var x in lista)
			{
				Console.WriteLine(x.SorSzama + " sor: a=" + x.a + " b=" + x.b + " c=" + x.c + " Kerület: " + x.Kerulet + " Terület:" + x.Terulet);
			}
			Console.ReadKey();
		}
	}
}