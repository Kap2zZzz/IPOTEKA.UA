using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPOTEKA.UA.Code
{
    public struct ProcStawka
    {
        public decimal Stawka;
        public int Period;
    }

    public struct Schedule
    {
        public decimal Ostatok;
        public decimal Proc;
        public decimal Tilo;
        public decimal Platizh;
        public string Oplata;
        public decimal SumaKomisiyi;
        public DateTime OplataDateTime;
    }

    public class MyMath
    {
        public static decimal Round(decimal InPut, double digits)
        {
            Int64 InPutInt = (long)(InPut * (decimal)Math.Pow(10, digits));
            Int64 InPutIntPlus = (long)(InPut * (decimal)Math.Pow(10, digits + 1));
            if (InPutIntPlus % 10 == 5)
                return (InPutInt + 1) / (decimal)Math.Pow(10, digits);
            else
                return Math.Round(InPut, (int)digits);
        }
    }

    public class MaskNumber
    {
        public static string MaskDecimal(decimal InPut)
        {
            bool IsNegative = InPut < 0;
            if (IsNegative) InPut = InPut * (-1);
            string result = Convert.ToString(Math.Truncate(InPut));
            int CountZero = Convert.ToString(Math.Truncate(InPut)).Length - 1;
            int CountSpaces = (int)(CountZero / 3);
            int index = 0;
            decimal DrobChastyna = InPut % 1;
            for (int i = 0; i < CountSpaces; i++)
            {
                index = (CountZero % 3) + 1 + i * 4;
                result = result.Insert(index, " ");
            }
            if (DrobChastyna == 0)
                result += ",00";
            else if (DrobChastyna.ToString().Length == 3)
                result += DrobChastyna.ToString().Substring(1).Replace('.', ',') + "0";
            else
                result += DrobChastyna.ToString().Substring(1).Replace('.', ',');
            if (IsNegative)
                return "-" + result;
            else
                return result;
        }

        public static string MaskInt(Int64 InPut)
        {
            bool IsNegative = InPut < 0;
            if (IsNegative) InPut = InPut * (-1);
            string result = Convert.ToString(InPut);
            int CountZero = Convert.ToString(InPut).Length - 1;
            int CountSpaces = (int)(CountZero / 3);
            int index = 0;
            for (int i = 0; i < CountSpaces; i++)
            {
                index = (CountZero % 3) + 1 + i * 4;
                result = result.Insert(index, " ");
            }
            if (IsNegative)
                return "-" + result;
            else
                return result;
        }
    }

    public class SchedulesClass
    {
        bool IfIncreaseStrokKredytu(DateTime DataRozrahunku, DateTime DataPershogoPlatezhu)
        {
            return (DataRozrahunku - DataPershogoPlatezhu.AddMonths(-1)).TotalDays > 2;
        }

        bool IfBodyIsZero(DateTime DataPo4atkuProc, DateTime DataPershogoPlatezhu)
        {
            return (DataPershogoPlatezhu - DataPo4atkuProc).TotalDays <= 16;
        }

        decimal SumaProcWPeriodi(decimal OstatokKredytu, decimal Stawka, DateTime DataStart, DateTime DataEnd)
        {
            int k = (int)(DataEnd - DataStart).TotalDays;
            return (k * OstatokKredytu * Stawka) / 36000;
        }

        public static void ReturnAnnuitySchedule(decimal SumaKredytu, int StrokKredytu, List<ProcStawka> ListStawok, decimal Komisiya, DateTime DataRozrahunku, DateTime DataPo4atkuProc,
            DateTime DataPershogoPlatezhu, DateTime DataOplatyKredytu, out List<Schedule> ResultList)
        {
            ResultList = new List<Schedule>();
            SchedulesClass Schedules = new SchedulesClass();
            int i = 0;

            decimal StawkaMax = 0; decimal SumaAnuiteta = 0;
            decimal SumaKomisiyi = MyMath.Round((SumaKredytu * Komisiya / 100), 2);
            foreach (ProcStawka s in ListStawok)
            {
                StawkaMax = Math.Max(StawkaMax, s.Stawka);
            }
            decimal MinSumaAnuiteta = 0;

            if (Schedules.IfIncreaseStrokKredytu(DataRozrahunku, DataPershogoPlatezhu))
                StrokKredytu = StrokKredytu + 1;

            MinSumaAnuiteta = (SumaKredytu / StrokKredytu) + SumaKomisiyi;

            decimal MaxSumaProcZaMisyac = SumaKredytu * 31 * StawkaMax / 36000;
            decimal MaxSumaAnuiteta = SumaAnuiteta;
            if (SumaAnuiteta == 0)
                MaxSumaAnuiteta = MinSumaAnuiteta + MaxSumaProcZaMisyac;

            bool KinecObrahunku = false; bool ChyTiloZero = false;

            while (true)
            {
                ResultList.Clear();
                ChyTiloZero = Schedules.IfBodyIsZero(DataPo4atkuProc, DataPershogoPlatezhu);

                DateTime DataStart = DataPo4atkuProc;
                DateTime DataEnd = DataPershogoPlatezhu;

                decimal OstatokKredytu = SumaKredytu;
                SumaAnuiteta = (MinSumaAnuiteta + MaxSumaAnuiteta) / 2;
                i = 0; int s = 0;
                DateTime DataZminy = DateTime.MinValue;
                while (true)
                {
                    Schedule Schedule = new Schedule();
                    Schedule.SumaKomisiyi = SumaKomisiyi;
                    DataZminy = DataRozrahunku.AddMonths(ListStawok[s].Period);
                    bool ChyZminaStawky = DataStart <= DataZminy && DataEnd > DataZminy && ListStawok.Count > s + 1;

                    if (ChyZminaStawky)
                    {
                        Schedule.Proc = Schedules.SumaProcWPeriodi(OstatokKredytu, ListStawok[s].Stawka, DataStart, DataZminy)
                            + Schedules.SumaProcWPeriodi(OstatokKredytu, ListStawok[s + 1].Stawka, DataZminy, DataEnd);
                        s++;
                    }
                    else
                        Schedule.Proc = Schedules.SumaProcWPeriodi(OstatokKredytu, ListStawok[s].Stawka, DataStart, DataEnd);

                    if (KinecObrahunku)
                        Schedule.Proc = MyMath.Round(Schedule.Proc, 2);

                    if (SumaAnuiteta - SumaKomisiyi >= OstatokKredytu - (SumaAnuiteta - SumaKomisiyi) / 2 && (i + 1) == StrokKredytu)
                        Schedule.Tilo = OstatokKredytu;
                    else if (ChyTiloZero)
                        Schedule.Tilo = 0;
                    else
                        Schedule.Tilo = Math.Min(OstatokKredytu, Math.Max(SumaAnuiteta - Schedule.Proc - SumaKomisiyi, 0));

                    Schedule.Ostatok = OstatokKredytu - Schedule.Tilo;
                    Schedule.OplataDateTime = DataEnd;
                    Schedule.Oplata = DataEnd.ToString("dd.MM.yyyy");
                    DataStart = DataEnd;
                    if (DataPershogoPlatezhu.AddMonths(i + 1) > DataOplatyKredytu)
                        DataEnd = DataOplatyKredytu;
                    else
                        DataEnd = DataPershogoPlatezhu.AddMonths(i + 1);
                    OstatokKredytu = OstatokKredytu - Schedule.Tilo;
                    if (OstatokKredytu == 0)
                        Schedule.Platizh = Schedule.Proc + Schedule.Tilo + SumaKomisiyi;
                    else if (ChyTiloZero)
                    {
                        Schedule.Platizh = MyMath.Round(Schedule.Proc, 2);
                        ChyTiloZero = false;
                    }
                    else
                        Schedule.Platizh = SumaAnuiteta;

                    ResultList.Add(Schedule);

                    i++;
                    if (OstatokKredytu <= 0 || i > StrokKredytu)
                        break;
                }

                if (KinecObrahunku)
                {
                    break;
                }
                else if ((MaxSumaAnuiteta - MinSumaAnuiteta) <= 0.001m)
                {
                    MinSumaAnuiteta = MyMath.Round(SumaAnuiteta, 0);
                    MaxSumaAnuiteta = MyMath.Round(SumaAnuiteta, 0);
                    KinecObrahunku = true;
                }
                else if (i > StrokKredytu)
                {
                    MinSumaAnuiteta = SumaAnuiteta;
                }
                else if (i < StrokKredytu)
                {
                    MaxSumaAnuiteta = SumaAnuiteta;
                }
                else if (ResultList[i - 1].Platizh < SumaAnuiteta)
                {
                    MaxSumaAnuiteta = SumaAnuiteta;
                }
                else
                {
                    MinSumaAnuiteta = SumaAnuiteta;
                }
            }
        }

        public static void ReturnClassicSchedule(decimal SumaKredytu, int StrokKredytu, List<ProcStawka> ListStawok, DateTime DataRozrahunku, DateTime DataPo4atkuProc,
            DateTime DataPershogoPlatezhu, DateTime DataOplatyKredytu, out List<Schedule> ResultList)
        {
            ResultList = new List<Schedule>();
            SchedulesClass Schedules = new SchedulesClass();
            int i = 0; decimal Tilo = 0;

            bool ChyTiloZero = false;
            if (Schedules.IfBodyIsZero(DataPo4atkuProc, DataPershogoPlatezhu))
            {
                ChyTiloZero = true;
                Tilo = (SumaKredytu / StrokKredytu);
            }
            else if (Schedules.IfIncreaseStrokKredytu(DataRozrahunku, DataPershogoPlatezhu))
            {
                Tilo = (SumaKredytu / (StrokKredytu + 1));
            }
            else
            {
                Tilo = (SumaKredytu / StrokKredytu);
            }
            Tilo = MyMath.Round(Tilo, 2);
            if (Schedules.IfIncreaseStrokKredytu(DataRozrahunku, DataPershogoPlatezhu))
                StrokKredytu = StrokKredytu + 1;

            ResultList.Clear();

            DateTime DataStart = DataPo4atkuProc;
            DateTime DataEnd = DataPershogoPlatezhu;
            DateTime DataEndTilo = DateTime.MinValue;

            decimal OstatokKredytu = SumaKredytu;
            decimal TiloFull = 0; decimal ProcentFull = 0; decimal WnesokFull = 0;
            i = 0; int s = 0;
            DateTime DataZminy = DateTime.MinValue;
            while (true)
            {
                if (DataEnd.Day == 1)
                    DataEndTilo = DataEnd.AddDays(-1);
                else
                    DataEndTilo = DataEnd;
                Schedule Schedule = new Schedule();
                DataZminy = DataRozrahunku.AddMonths(ListStawok[s].Period);
                bool ChyZminaStawky = DataStart < DataZminy && DataEnd > DataZminy && ListStawok.Count > s + 1;

                if (DataEndTilo != DataEnd)
                {
                    if (ChyZminaStawky && DataEndTilo > DataZminy)
                    {
                        Schedule.Proc = Schedules.SumaProcWPeriodi(OstatokKredytu, ListStawok[s].Stawka, DataStart, DataZminy)
                            + Schedules.SumaProcWPeriodi(OstatokKredytu, ListStawok[s + 1].Stawka, DataZminy, DataEndTilo)
                            + Schedules.SumaProcWPeriodi(Math.Max(OstatokKredytu - Tilo, 0), ListStawok[s + 1].Stawka, DataEndTilo, DataEnd);
                        s++;
                    }
                    else if (ChyZminaStawky)
                    {
                        Schedule.Proc = Schedules.SumaProcWPeriodi(OstatokKredytu, ListStawok[s].Stawka, DataStart, DataZminy)
                            + Schedules.SumaProcWPeriodi(Math.Max(OstatokKredytu - Tilo, 0), ListStawok[s + 1].Stawka, DataZminy, DataEnd);
                        s++;
                    }
                    else if (ChyTiloZero)
                        Schedule.Proc = Schedules.SumaProcWPeriodi(OstatokKredytu, ListStawok[s].Stawka, DataStart, DataEnd);
                    else
                        Schedule.Proc = Schedules.SumaProcWPeriodi(OstatokKredytu, ListStawok[s].Stawka, DataStart, DataEndTilo)
                            + Schedules.SumaProcWPeriodi(Math.Max(OstatokKredytu - Tilo, 0), ListStawok[s].Stawka, DataEndTilo, DataEnd);
                }
                else
                {
                    if (ChyZminaStawky)
                    {
                        Schedule.Proc = Schedules.SumaProcWPeriodi(OstatokKredytu, ListStawok[s].Stawka, DataStart, DataZminy)
                            + Schedules.SumaProcWPeriodi(OstatokKredytu, ListStawok[s + 1].Stawka, DataZminy, DataEnd);
                        s++;
                    }
                    else
                        Schedule.Proc = Schedules.SumaProcWPeriodi(OstatokKredytu, ListStawok[s].Stawka, DataStart, DataEnd);
                }



                Schedule.Proc = MyMath.Round(Schedule.Proc, 2);

                if (ChyTiloZero)
                {
                    Schedule.Tilo = 0;
                    ChyTiloZero = false;
                }
                else if (Tilo < OstatokKredytu - Tilo / 2)
                    Schedule.Tilo = Tilo;
                else
                    Schedule.Tilo = OstatokKredytu;

                Schedule.Ostatok = OstatokKredytu - Schedule.Tilo;
                Schedule.OplataDateTime = DataEnd;
                Schedule.Oplata = DataEnd.ToString("dd.MM.yyyy");
                DataStart = DataEnd;
                if (DataPershogoPlatezhu.AddMonths(i + 1) > DataOplatyKredytu)
                    DataEnd = DataOplatyKredytu;
                else
                    DataEnd = DataPershogoPlatezhu.AddMonths(i + 1);

                OstatokKredytu = OstatokKredytu - Schedule.Tilo;
                Schedule.Platizh = Schedule.Proc + Schedule.Tilo;

                TiloFull = TiloFull + Schedule.Tilo;
                ProcentFull = ProcentFull + Schedule.Proc;
                WnesokFull = WnesokFull + Schedule.Platizh;

                ResultList.Add(Schedule);

                i++;
                if (OstatokKredytu <= 0 || i > StrokKredytu)
                    break;
            }
        }
    }

    public class Calculation
    {
        static string CalculateEffectiveRate(decimal SumaKredytu, decimal Wytraty, DateTime DataRozrahunku, List<Schedule> ResultList)
        {
            SumaKredytu = -1 * SumaKredytu + Wytraty;
            decimal EffectiveRate = 0.15m;
            decimal Step = EffectiveRate;
            List<Schedule> AmountFlow = new List<Schedule>();
            Schedule s = new Schedule();
            s.OplataDateTime = DataRozrahunku;
            s.Platizh = SumaKredytu;
            AmountFlow.Add(s);
            AmountFlow.AddRange(ResultList);
            while (true)
            {
                decimal Drib = 0;
                for (int i = 0; i < AmountFlow.Count; i++)
                {
                    double dd = (AmountFlow[i].OplataDateTime - DataRozrahunku).TotalDays;
                    double Platizh = (double)AmountFlow[i].Platizh;
                    double er = (double)EffectiveRate;
                    Drib += (decimal)(Platizh / Math.Pow(1 + er, dd / 365));
                }
                //Drib = MyMath.Round(Drib, 2);
                if (MyMath.Round(Drib, 1) == 0)
                    break;
                else if (Drib > 0)
                    EffectiveRate += Step;
                else
                {
                    Step = Step / 2;
                    EffectiveRate -= Step;
                }
            }
            return MaskNumber.MaskDecimal(Math.Round(EffectiveRate * 100, 2));
        }
        public static void GetResults(decimal SumaKredytu, int StrokKredytu, decimal Stawka, bool IsSchemaClassic,
            out string Ammount, out string RealRate, out string EffectiveRate)
        {
            List<ProcStawka> ListStawok = new List<ProcStawka>();
            List<Schedule> ResultList;
            ProcStawka ps = new ProcStawka();
            ps.Stawka = Stawka; ps.Period = 1000;
            ListStawok.Add(ps);
            DateTime DataRozrahunku = DateTime.Now; DataRozrahunku = DataRozrahunku.Subtract(DataRozrahunku.TimeOfDay);
            DateTime DataPo4atkuProc = DataRozrahunku;
            DateTime DataPershogoPlatezhu = DataRozrahunku.AddDays((DataRozrahunku.AddMonths(1) - DataRozrahunku).TotalDays - DataRozrahunku.Day + 1);
            DateTime DataOplatyKredytu = DataRozrahunku.AddMonths(StrokKredytu).AddDays(-1);
            if (IsSchemaClassic)
                SchedulesClass.ReturnClassicSchedule(SumaKredytu, StrokKredytu, ListStawok, DataRozrahunku, DataPo4atkuProc, DataPershogoPlatezhu, DataOplatyKredytu,
                    out ResultList);
            else
                SchedulesClass.ReturnAnnuitySchedule(SumaKredytu, StrokKredytu, ListStawok, 0, DataRozrahunku, DataPo4atkuProc, DataPershogoPlatezhu, DataOplatyKredytu,
                    out ResultList);

            Ammount = MaskNumber.MaskDecimal(ResultList[1].Platizh);
            EffectiveRate = CalculateEffectiveRate(SumaKredytu, 0, DataRozrahunku, ResultList);
            decimal Procenty = 0;
            foreach (Schedule s in ResultList)
                Procenty += s.Proc;
            RealRate = MaskNumber.MaskDecimal(Math.Round(Procenty * 100 * 12 / (SumaKredytu * StrokKredytu), 2));
        }
    }
}